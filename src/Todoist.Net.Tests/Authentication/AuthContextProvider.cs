using Todoist.Net.Tests.Doppler;

namespace Todoist.Net.Tests.Authentication;

public sealed class AuthContextProvider
{
    private readonly DopplerClient _dopplerClient;
    private readonly SemaphoreSlim _secretsApiGate;
    private SecretsResponse? _secrets;

    public AuthContextProvider()
    {
        _dopplerClient = new DopplerClient();
        _secretsApiGate = new SemaphoreSlim(1, 1);
    }


    /// <summary>
    /// Gets the primary account authentication context for authenticating with the Todoist API.
    /// This method will throw an exception if the tokens are not found in the provided Doppler configuration.
    /// </summary>
    /// <remarks>
    /// In order to run "Premium" integration tests, the <see cref="DopplerEnvVariables.PrimaryAccessTokenKey"/> and <see cref="DopplerEnvVariables.PrimaryRefreshTokenKey"/> 
    /// secrets must be set to valid/expired tokens of a Premium account.
    /// </remarks>
    /// <returns>The primary account authentication context for authenticating with the Todoist API.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the primary account tokens are not found in the provided Doppler configuration.</exception>
    public async Task<TodoistAuthenticationContext> GetPrimaryAuthContextAsync(CancellationToken cancellationToken = default)
    {
        var secrets = await GetSecretsAsync(cancellationToken);
        
        return new TodoistAuthenticationContext(
            credentials: new ClientCredentials(secrets.ClientId, secrets.ClientSecret),
            tokens: new TodoistTokens(secrets.PrimaryAccount.AccessToken, secrets.PrimaryAccount.RefreshToken),
            onRefresh: (res, state, ct) => UpdateSecretsAsync(res.AccessToken, res.RefreshToken, AccountType.Primary, ct)
        );
    }

    /// <summary>
    /// Gets the secondary account authentication context for authenticating with the Todoist API, if available.
    /// This method will return null if the tokens are not found in the provided Doppler configuration.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In order to run "Collaboration" integration tests, the <see cref="DopplerEnvVariables.SecondaryAccessTokenKey"/> and <see cref="DopplerEnvVariables.SecondaryRefreshTokenKey"/> 
    /// secrets must be set to valid/expired tokens of a different account than the one used in the primary account.
    /// </para>
    /// <para>
    /// In addition to "Collaboration" tests, this token is meant to be used in "Free-tier" tests to take some of the load off the primary token,
    /// which might be a Premium account used for "Premium" tests, to avoid hitting rate limits.
    /// </para>
    /// </remarks>
    /// <returns>The secondary account authentication context for authenticating with the Todoist API if available; otherwise, <c>null</c>.</returns>
    public async Task<TodoistAuthenticationContext?> GetSecondaryAuthContextAsync(CancellationToken cancellationToken = default)
    {
        var secrets = await GetSecretsAsync(cancellationToken);
        if (secrets.SecondaryAccount is null)
        {
            return null;
        }
        return new TodoistAuthenticationContext(
            credentials: new ClientCredentials(secrets.ClientId, secrets.ClientSecret),
            tokens: new TodoistTokens(secrets.SecondaryAccount.AccessToken, secrets.SecondaryAccount.RefreshToken),
            onRefresh: (res, state, ct) => UpdateSecretsAsync(res.AccessToken, res.RefreshToken, AccountType.Secondary, ct)
        );
    }

    /// <summary>
    /// Gets the tertiary account authentication context for authenticating with the Todoist API, if available.
    /// This method will return null if the tokens are not found in the provided Doppler configuration.
    /// </summary>
    /// <remarks>
    /// When available, this token helps to further distribute the load of integration tests across multiple accounts to avoid hitting rate limits on any single account.
    /// </remarks>
    /// <returns>The tertiary account authentication context for authenticating with the Todoist API if available; otherwise, <c>null</c>.</returns>
    public async Task<TodoistAuthenticationContext?> GetTertiaryAuthContextAsync(CancellationToken cancellationToken = default)
    {
        var secrets = await GetSecretsAsync(cancellationToken);
        if (secrets.TertiaryAccount is null)
        {
            return null;
        }
        return new TodoistAuthenticationContext(
            credentials: new ClientCredentials(secrets.ClientId, secrets.ClientSecret),
            tokens: new TodoistTokens(secrets.TertiaryAccount.AccessToken, secrets.TertiaryAccount.RefreshToken),
            onRefresh: (res, state, ct) => UpdateSecretsAsync(res.AccessToken, res.RefreshToken, AccountType.Tertiary, ct)
        );
    }


    private async Task<SecretsResponse> GetSecretsAsync(CancellationToken cancellationToken = default)
    {
        if (_secrets is not null)
        {
            return _secrets;
        }

        await _secretsApiGate.WaitAsync(cancellationToken);
        try
        {
            return _secrets ??= await _dopplerClient.GetSecretsAsync(cancellationToken);
        }
        finally
        {
            _secretsApiGate.Release();
        }
    }

    private async Task<SecretsResponse> UpdateSecretsAsync(string accessToken, string refreshToken, AccountType accountType, CancellationToken cancellationToken = default)
    {
        await _secretsApiGate.WaitAsync(cancellationToken);
        try
        {
            var updatedTokens = await _dopplerClient.UpdateSecretsAsync(accessToken, refreshToken, accountType, cancellationToken);

            return _secrets = _secrets is null
                ? await _dopplerClient.GetSecretsAsync(cancellationToken)
                : accountType switch
                {
                    AccountType.Primary => _secrets with { PrimaryAccount = updatedTokens },
                    AccountType.Secondary => _secrets with { SecondaryAccount = updatedTokens },
                    AccountType.Tertiary => _secrets with { TertiaryAccount = updatedTokens },
                    _ => _secrets
                };
        }
        finally
        {
            _secretsApiGate.Release();
        }
    }
}
