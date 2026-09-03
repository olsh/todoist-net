namespace Todoist.Net.Tests.Authentication;

public static class NonExpiringTokensProvider
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
        return TokenEnvVariables.Primary;
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
        return string.IsNullOrWhiteSpace(TokenEnvVariables.Secondary.Value) 
            ? null 
            : TokenEnvVariables.Secondary.Value;
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
        return string.IsNullOrWhiteSpace(TokenEnvVariables.Tertiary.Value) 
            ? null 
            : TokenEnvVariables.Tertiary.Value;
    }
}
