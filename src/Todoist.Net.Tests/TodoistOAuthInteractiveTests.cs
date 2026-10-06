using System.Text.Json;

using Todoist.Net.OAuth;
using Todoist.Net.Tests.Settings;

namespace Todoist.Net.Tests;

/// <summary>
/// Walks through the OAuth flow against the real Todoist API. A person has to authorize the application in the browser,
/// so the test is explicit, and it keeps nothing: every run starts from a fresh authorization and ends it afterwards,
/// even when a step fails. Ending it waits out Todoist's refresh token grace window, so the run takes over a minute.
/// </summary>
/// <remarks>
/// Needs a Todoist application with refresh tokens enabled (the default for new applications) whose redirect URIs include
/// the one from <see cref="SettingsProvider.GetOAuthRedirectUri" />, and its credentials in the <c>todoist_oauth_client_id</c>
/// and <c>todoist_oauth_client_secret</c> environment variables. Run it with
/// <c>dotnet test --filter "trait=oauth-interactive" -- xUnit.Explicit=only xUnit.ShowLiveOutput=true</c>.
/// </remarks>
[Trait(Constants.TraitName, Constants.OAuthInteractiveTraitValue)]
public class TodoistOAuthInteractiveTests
{
    private const string TokenEndpoint = "https://api.todoist.com/oauth/access_token";

    // Todoist tolerates a consumed refresh token for 60 seconds before it treats it as replayed.
    private static readonly TimeSpan ReplayDelay = TimeSpan.FromSeconds(65);

    private static readonly HttpClient TokenHttpClient = new();

    [Fact(Explicit = true, Timeout = 600_000)]
    public async Task OAuthFlow_AgainstTodoist_RefreshesRotatesAndRevokesTokens()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var clientId = SettingsProvider.GetOAuthClientId();
        var clientSecret = SettingsProvider.GetOAuthClientSecret();
        Assert.SkipWhen(
            clientId is null || clientSecret is null,
            "Set the todoist_oauth_client_id and todoist_oauth_client_secret environment variables to run the OAuth flow.");

        var options = new TodoistOAuthOptions { ClientId = clientId, ClientSecret = clientSecret };
        var redirectUri = SettingsProvider.GetOAuthRedirectUri();


        // Step 1: Authorize the application in the browser, and exchange the authorization code for tokens.
        var code = await OAuthLoopbackListener.AuthorizeAsync(clientId, redirectUri, "data:read", cancellationToken);
        var issuedTokens = await ExchangeCodeAsync(options, code, redirectUri, cancellationToken);

        // The newest tokens which still grant access, whose authorization is ended whatever happens in between.
        var latestTokens = issuedTokens;

        Func<TodoistTokens, Task> Store(TokensRecorder recorder)
        {
            return tokens =>
            {
                latestTokens = tokens;
                return recorder.StoreAsync(tokens);
            };
        }

        try
        {
            Assert.False(
                string.IsNullOrEmpty(issuedTokens.RefreshToken),
                "Todoist issued no refresh token, so the application has refresh tokens disabled.");


            // Step 2: Call the API with the issued tokens.
            var recorderA = new TokensRecorder();
            using var clientA = new TodoistClient(options, issuedTokens, Store(recorderA));
            await clientA.User.GetInfoAsync(cancellationToken);


            // Step 3: Refresh the tokens, and assert Todoist rotated both of them.
            var rotatedTokens = await clientA.RefreshTokensAsync(cancellationToken);

            Assert.NotEqual(issuedTokens.AccessToken, rotatedTokens.AccessToken);
            Assert.NotEqual(issuedTokens.RefreshToken, rotatedTokens.RefreshToken);
            Assert.Same(rotatedTokens, Assert.Single(recorderA.Tokens));
            await clientA.User.GetInfoAsync(cancellationToken);


            // Step 4: Refresh again with the refresh token used a moment ago, like a second client of the same user would.
            // Todoist answers within its grace window with the rotated access token alone, which must not be reported.
            var recorderB = new TokensRecorder();
            using var clientB = new TodoistClient(options, Expired(issuedTokens), Store(recorderB));
            await clientB.User.GetInfoAsync(cancellationToken);

            Assert.Equal(rotatedTokens.AccessToken, clientB.OAuthHandler?.Tokens.AccessToken);
            Assert.Null(clientB.OAuthHandler?.Tokens.RefreshToken);
            Assert.Empty(recorderB.Tokens);


            // Step 5: Call the API with expired tokens, which are refreshed before the request.
            var recorderC = new TokensRecorder();
            using var clientC = new TodoistClient(options, Expired(rotatedTokens), Store(recorderC));
            await clientC.User.GetInfoAsync(cancellationToken);

            var proactivelyRefreshedTokens = Assert.Single(recorderC.Tokens);
            Assert.NotEqual(rotatedTokens.RefreshToken, proactivelyRefreshedTokens.RefreshToken);


            // Step 6: Call the API with an access token Todoist rejects, which is refreshed and the request sent again.
            var recorderD = new TokensRecorder();
            using var clientD = new TodoistClient(
                options,
                new TodoistTokens("rejected-access-token", proactivelyRefreshedTokens.RefreshToken),
                Store(recorderD));
            await clientD.User.GetInfoAsync(cancellationToken);

            var reactivelyRefreshedTokens = Assert.Single(recorderD.Tokens);


            // Step 7: Revoke the tokens, and assert the access token no longer works.
            await clientD.RevokeTokensAsync(cancellationToken);

            using var revokedAccessClient = new TodoistClient(
                options,
                new TodoistTokens(reactivelyRefreshedTokens.AccessToken),
                null);
            var accessException = await Assert.ThrowsAsync<TodoistException>(() =>
                revokedAccessClient.User.GetInfoAsync(cancellationToken));
            Assert.Equal(401, accessException.HttpCode);


            // Step 8: Assert the refresh token outlived the revocation, as documented for RevokeTokensAsync,
            // since Todoist can revoke access tokens only.
            using var survivingClient = new TodoistClient(
                options,
                reactivelyRefreshedTokens,
                Store(new TokensRecorder()));
            await survivingClient.RefreshTokensAsync(cancellationToken);
        }
        finally
        {
            await EndAuthorizationAsync(options, latestTokens);
        }
    }

    private static TodoistTokens Expired(TodoistTokens tokens)
    {
        return new TodoistTokens(tokens.AccessToken, tokens.RefreshToken, DateTimeOffset.UtcNow.AddMinutes(-1));
    }

    /// <summary>
    /// Ends the authorization the test got. Todoist cannot revoke refresh tokens, but it revokes every token
    /// of the user for the application once a consumed refresh token is presented after its 60-second grace window.
    /// </summary>
    private static async Task EndAuthorizationAsync(TodoistOAuthOptions options, TodoistTokens tokens)
    {
        const string manualCleanup = "remove the application in the Todoist settings under Integrations.";

        // The test's own cancellation token may be what ended the test, so the cleanup gets a time limit of its own.
        using var timeoutSource = new CancellationTokenSource(TimeSpan.FromMinutes(3));
        var cancellationToken = timeoutSource.Token;

        try
        {
            if (string.IsNullOrEmpty(tokens.RefreshToken))
            {
                Log($"Cleanup: no refresh token is left to end the authorization with, {manualCleanup}");
                return;
            }

            var consumedRefreshToken = tokens.RefreshToken;
            var newestTokens = await TryRefreshAsync(options, consumedRefreshToken, cancellationToken);
            if (newestTokens is null)
            {
                Log("Cleanup: the authorization had already ended.");
                return;
            }

            Log("Cleanup: waiting out the grace window to replay the consumed refresh token.");
            await Task.Delay(ReplayDelay, cancellationToken);
            await TryRefreshAsync(options, consumedRefreshToken, cancellationToken);

            var survivingTokens = string.IsNullOrEmpty(newestTokens.RefreshToken)
                ? null
                : await TryRefreshAsync(options, newestTokens.RefreshToken, cancellationToken);
            Log(
                survivingTokens is null
                    ? "Cleanup: the replayed refresh token ended the authorization."
                    : $"Cleanup: the authorization survived the replay, {manualCleanup}");
        }
        catch (Exception exception)
        {
            Log($"Cleanup: ending the authorization failed ({exception.Message}), {manualCleanup}");
        }
    }

    private static async Task<TodoistTokens> ExchangeCodeAsync(
        TodoistOAuthOptions options,
        string code,
        Uri redirectUri,
        CancellationToken cancellationToken)
    {
        var tokens = await PostToTokenEndpointAsync(
            options,
            new Dictionary<string, string>
            {
                { "code", code },
                { "redirect_uri", redirectUri.ToString() }
            },
            cancellationToken);

        Assert.NotNull(tokens);
        return tokens;
    }

    private static Task<TodoistTokens?> TryRefreshAsync(
        TodoistOAuthOptions options,
        string refreshToken,
        CancellationToken cancellationToken)
    {
        return PostToTokenEndpointAsync(
            options,
            new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            },
            cancellationToken);
    }

    private static async Task<TodoistTokens?> PostToTokenEndpointAsync(
        TodoistOAuthOptions options,
        Dictionary<string, string> formParams,
        CancellationToken cancellationToken)
    {
        formParams["client_id"] = options.ClientId;
        formParams["client_secret"] = options.ClientSecret;

        using var content = new FormUrlEncodedContent(formParams);
        using var response = await TokenHttpClient.PostAsync(TokenEndpoint, content, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            Log($"The token endpoint answered {(int)response.StatusCode}: {body}");
            return null;
        }

        using var json = JsonDocument.Parse(body);
        var root = json.RootElement;
        var expiresIn = root.TryGetProperty("expires_in", out var expiresInElement) ? expiresInElement.GetInt32() : 0;

        return new TodoistTokens(
            root.GetProperty("access_token")
                .GetString()!,
            root.TryGetProperty("refresh_token", out var refreshTokenElement) ? refreshTokenElement.GetString() : null,
            expiresIn > 0 ? DateTimeOffset.UtcNow.AddSeconds(expiresIn) : null);
    }

    private static void Log(string message)
    {
        TestContext.Current.TestOutputHelper?.WriteLine(message);
    }
}
