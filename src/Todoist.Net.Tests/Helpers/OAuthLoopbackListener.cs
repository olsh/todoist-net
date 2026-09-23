using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Todoist.Net.Tests.Helpers;

/// <summary>
/// Runs the browser part of the OAuth authorization code flow: it opens the Todoist consent page and catches
/// the redirect back to a local address, so the authorization code never has to leave the machine.
/// </summary>
internal static class OAuthLoopbackListener
{
    private const string AuthorizeEndpoint = "https://app.todoist.com/oauth/authorize";

    private static readonly TimeSpan AuthorizationTimeout = TimeSpan.FromMinutes(3);

    /// <summary>
    /// Asks the user to authorize the application in a browser and waits for the authorization code.
    /// </summary>
    /// <param name="clientId">The client ID of the application.</param>
    /// <param name="redirectUri">A redirect URI of the application which points to the local machine.</param>
    /// <param name="scope">The comma separated permissions to request.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to stop waiting.</param>
    /// <returns>The authorization code.</returns>
    public static async Task<string> AuthorizeAsync(
        string clientId,
        Uri redirectUri,
        string scope,
        CancellationToken cancellationToken)
    {
        var state = Convert.ToHexString(RandomNumberGenerator.GetBytes(16));
        var authorizeUrl = $"{AuthorizeEndpoint}?client_id={Uri.EscapeDataString(clientId)}"
                           + $"&scope={Uri.EscapeDataString(scope)}"
                           + $"&state={state}"
                           + $"&redirect_uri={Uri.EscapeDataString(redirectUri.ToString())}"
                           + "&response_type=code";

        using var listener = new HttpListener();
        listener.Prefixes.Add(redirectUri.GetLeftPart(UriPartial.Authority) + "/");
        listener.Start();

        TestContext.Current.TestOutputHelper?.WriteLine($"Authorize the application in the browser: {authorizeUrl}");
        OpenBrowser(authorizeUrl);

        using var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutSource.CancelAfter(AuthorizationTimeout);

        // Waiting for a request cannot be canceled, but stopping the listener ends the wait.
        await using var registration = timeoutSource.Token.Register(listener.Stop);

        while (true)
        {
            HttpListenerContext context;
            try
            {
                context = await listener.GetContextAsync();
            }
            catch (Exception exception) when (exception is HttpListenerException or ObjectDisposedException
                                              && timeoutSource.IsCancellationRequested)
            {
                throw new OperationCanceledException(
                    "The application was not authorized in time.",
                    exception,
                    timeoutSource.Token);
            }

            if (context.Request.Url?.AbsolutePath != redirectUri.AbsolutePath)
            {
                // Browsers ask for more than the redirect, e.g. for a favicon.
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.Close();
                continue;
            }

            var error = context.Request.QueryString["error"];
            var code = context.Request.QueryString["code"];
            var returnedState = context.Request.QueryString["state"];

            await RespondAsync(
                context,
                error is null
                    ? "The application is authorized, you can close this tab."
                    : $"The authorization failed: {error}");

            if (error is not null)
            {
                throw new InvalidOperationException($"The authorization failed: {error}");
            }

            Assert.Equal(state, returnedState);
            Assert.NotNull(code);

            return code;
        }
    }

    private static void OpenBrowser(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch (Win32Exception)
        {
            // Without a browser to open, the URL in the test output has to be opened by hand.
        }
    }

    private static async Task RespondAsync(HttpListenerContext context, string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        context.Response.ContentType = "text/plain; charset=utf-8";
        context.Response.ContentLength64 = body.Length;
        await context.Response.OutputStream.WriteAsync(body);
        context.Response.Close();
    }
}
