namespace Todoist.Net.Tests;

public static class DopplerEnvVariables
{
    public static readonly bool IsDopplerEnabled = Environment.GetEnvironmentVariables().Contains("DOPPLER_TOKEN");

    public static readonly EnvVariable Token = "DOPPLER_TOKEN";
    public static readonly EnvVariable Project = new("DOPPLER_PROJECT", "todoist-net");
    public static readonly EnvVariable Config = new("DOPPLER_CONFIG", "dev");

    public static readonly EnvVariable ClientIdKey = new("CLIENT_ID_KEY", "CLIENT_ID");
    public static readonly EnvVariable ClientSecretKey = new("CLIENT_SECRET_KEY", "CLIENT_SECRET");
    public static readonly EnvVariable PrimaryAccessTokenKey = new("PRIMARY_ACCESS_TOKEN_KEY", "PRIMARY_ACCESS_TOKEN");
    public static readonly EnvVariable PrimaryRefreshTokenKey = new("PRIMARY_REFRESH_TOKEN_KEY", "PRIMARY_REFRESH_TOKEN");
    public static readonly EnvVariable SecondaryAccessTokenKey = new("SECONDARY_ACCESS_TOKEN_KEY", "SECONDARY_ACCESS_TOKEN");
    public static readonly EnvVariable SecondaryRefreshTokenKey = new("SECONDARY_REFRESH_TOKEN_KEY", "SECONDARY_REFRESH_TOKEN");
    public static readonly EnvVariable TertiaryAccessTokenKey = new("TERTIARY_ACCESS_TOKEN_KEY", "TERTIARY_ACCESS_TOKEN");
    public static readonly EnvVariable TertiaryRefreshTokenKey = new("TERTIARY_REFRESH_TOKEN_KEY", "TERTIARY_REFRESH_TOKEN");
}
