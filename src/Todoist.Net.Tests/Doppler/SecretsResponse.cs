namespace Todoist.Net.Tests.Doppler;

public sealed record SecretsResponse(
    string ClientId,
    string ClientSecret,
    TokensResponse PrimaryAccount,
    TokensResponse? SecondaryAccount,
    TokensResponse? TertiaryAccount
);
