using Flurl.Http;

namespace Todoist.Net.Tests.Doppler;

public sealed class DopplerClient
{
    private const string DopplerApiSecretsUrl = "https://api.doppler.com/v3/configs/config/secrets";
    private readonly Func<IFlurlRequest> _requestFactory;

    public DopplerClient()
    {
        _requestFactory = () => DopplerApiSecretsUrl
            .WithOAuthBearerToken(DopplerEnvVariables.Token);
    }

    public async Task<SecretsResponse> GetSecretsAsync(CancellationToken cancellationToken = default)
    {
        var response = await _requestFactory()
            .SetQueryParams(new
            {
                project = DopplerEnvVariables.Project.Value,
                config = DopplerEnvVariables.Config.Value
            })
            .GetJsonAsync<RawSecretsResponse>(cancellationToken: cancellationToken);

        (string primaryAccessToken, string primaryRefreshToken) = (
            response.Secrets[DopplerEnvVariables.PrimaryAccessTokenKey].Computed,
            response.Secrets[DopplerEnvVariables.PrimaryRefreshTokenKey].Computed
        );
        (string? secondaryAccessToken, string? secondaryRefreshToken) = (
            response.Secrets.GetValueOrDefault(DopplerEnvVariables.SecondaryAccessTokenKey)?.Computed,
            response.Secrets.GetValueOrDefault(DopplerEnvVariables.SecondaryRefreshTokenKey)?.Computed
        );
        (string? tertiaryAccessToken, string? tertiaryRefreshToken) = (
            response.Secrets.GetValueOrDefault(DopplerEnvVariables.TertiaryAccessTokenKey)?.Computed,
            response.Secrets.GetValueOrDefault(DopplerEnvVariables.TertiaryRefreshTokenKey)?.Computed
        );

        return new SecretsResponse(
            ClientId: response.Secrets[DopplerEnvVariables.ClientIdKey].Computed,
            ClientSecret: response.Secrets[DopplerEnvVariables.ClientSecretKey].Computed,
            PrimaryAccount: new TokensResponse(primaryAccessToken, primaryRefreshToken),
            SecondaryAccount: string.IsNullOrEmpty(secondaryAccessToken) || string.IsNullOrEmpty(secondaryRefreshToken)
                ? null
                : new TokensResponse(secondaryAccessToken, secondaryRefreshToken),
            TertiaryAccount: string.IsNullOrEmpty(tertiaryAccessToken) || string.IsNullOrEmpty(tertiaryRefreshToken)
                ? null
                : new TokensResponse(tertiaryAccessToken, tertiaryRefreshToken)
        );
    }

    public async Task<TokensResponse> UpdateSecretsAsync(string accessToken, string refreshToken, AccountType accountType, CancellationToken cancellationToken = default)
    {
        (string accessTokenKey, string refreshTokenKey) = accountType switch
        {
            AccountType.Primary => (DopplerEnvVariables.PrimaryAccessTokenKey, DopplerEnvVariables.PrimaryRefreshTokenKey),
            AccountType.Secondary => (DopplerEnvVariables.SecondaryAccessTokenKey, DopplerEnvVariables.SecondaryRefreshTokenKey),
            AccountType.Tertiary => (DopplerEnvVariables.TertiaryAccessTokenKey, DopplerEnvVariables.TertiaryRefreshTokenKey),
            _ => throw new ArgumentOutOfRangeException(nameof(accountType), accountType, null)
        };
        
        var response = await _requestFactory()
            .PostJsonAsync(new
            {
                project = DopplerEnvVariables.Project.Value,
                config = DopplerEnvVariables.Config.Value,
                secrets = new Dictionary<string, string>
                {
                    [accessTokenKey] = accessToken,
                    [refreshTokenKey] = refreshToken
                }
            }, cancellationToken: cancellationToken)
            .ReceiveJson<RawSecretsResponse>();
        
        return new TokensResponse(
            AccessToken: response.Secrets[accessTokenKey].Computed,
            RefreshToken: response.Secrets[refreshTokenKey].Computed
        );
    }


    private sealed class RawSecretsResponse
    {
        public Dictionary<string, RawSecretResponse> Secrets { get; set; } = []; 
    }

    private sealed class RawSecretResponse
    {
        public required string Raw { get; set; }
        public required string Computed { get; set; }
        public string? Note { get; set; }
    }
}
