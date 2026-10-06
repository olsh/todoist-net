namespace Todoist.Net.Tests.Settings;

public static class SettingsProvider
{
    /// <summary>
    /// Gets the primary token for authenticating with the Todoist API.
    /// This method will throw an exception if the token is not set in the environment variables.
    /// </summary>
    /// <remarks>
    /// In order to run "Premium" integration tests, the `todoist_token` environment variable must be set to a valid token of a Premium account.
    /// </remarks>
    /// <returns>The primary token for authenticating with the Todoist API.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the primary token is not set in the environment variables.</exception>
    public static string GetPrimaryToken()
    {
        return Environment.GetEnvironmentVariable("todoist_token")
               ?? Environment.GetEnvironmentVariable("todoist:token")
               ?? throw new InvalidOperationException("Required `todoist_token` environment variable is not set.");
    }

    /// <summary>
    /// Gets a secondary token for authenticating with the Todoist API, if available.
    /// This method will return null if the token is not set in the environment variables.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In order to run "Collaboration" integration tests,
    /// the `todoist_token_secondary` environment variable must be set to a valid token of a different account than the one used in `todoist_token`.
    /// </para>
    /// <para>
    /// In addition to "Collaboration" tests, this token is meant to be used in "Free-tier" tests to take some of the load off the primary token,
    /// which might be a Premium account used for "Premium" tests, to avoid hitting rate limits.
    /// </para>
    /// </remarks>
    /// <returns>A string containing the secondary token if the environment variable is set; otherwise, <c>null</c>.</returns>
    public static string? GetSecondaryToken()
    {
        return Environment.GetEnvironmentVariable("todoist_token_secondary")
               ?? Environment.GetEnvironmentVariable("todoist:token:secondary");
    }

    /// <summary>
    /// Gets a tertiary token for authenticating with the Todoist API, if available.
    /// This method will return null if the token is not set in the environment variables.
    /// </summary>
    /// <remarks>
    /// When available, this token helps to further distribute the load of integration tests across multiple accounts to avoid hitting rate limits on any single account.
    /// </remarks>
    /// <returns>A string containing the tertiary token if the environment variable is set; otherwise, <c>null</c>.</returns>
    public static string? GetTertiaryToken()
    {
        return Environment.GetEnvironmentVariable("todoist_token_tertiary")
               ?? Environment.GetEnvironmentVariable("todoist:token:tertiary");
    }

    /// <summary>
    /// Gets the client ID of the Todoist OAuth application used by the interactive OAuth tests, if available.
    /// </summary>
    /// <returns>A string containing the client ID if the environment variable is set; otherwise, <c>null</c>.</returns>
    public static string? GetOAuthClientId()
    {
        return Environment.GetEnvironmentVariable("todoist_oauth_client_id")
               ?? Environment.GetEnvironmentVariable("todoist:oauth:client_id");
    }

    /// <summary>
    /// Gets the client secret of the Todoist OAuth application used by the interactive OAuth tests, if available.
    /// </summary>
    /// <returns>A string containing the client secret if the environment variable is set; otherwise, <c>null</c>.</returns>
    public static string? GetOAuthClientSecret()
    {
        return Environment.GetEnvironmentVariable("todoist_oauth_client_secret")
               ?? Environment.GetEnvironmentVariable("todoist:oauth:client_secret");
    }

    /// <summary>
    /// Gets the redirect URI of the Todoist OAuth application used by the interactive OAuth tests.
    /// </summary>
    /// <remarks>
    /// It must match a redirect URI configured for the application exactly, and point to a free port on the local machine.
    /// </remarks>
    /// <returns>The redirect URI if the environment variable is set; otherwise, <c>http://localhost:8765/callback</c>.</returns>
    public static Uri GetOAuthRedirectUri()
    {
        return new Uri(
            Environment.GetEnvironmentVariable("todoist_oauth_redirect_uri")
            ?? Environment.GetEnvironmentVariable("todoist:oauth:redirect_uri")
            ?? "http://localhost:8765/callback");
    }
}
