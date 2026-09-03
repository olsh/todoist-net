namespace Todoist.Net.Tests;

public static class TokenEnvVariables
{
    public static readonly bool IsTokenEnabled = Environment.GetEnvironmentVariables().Contains("TODOIST_TOKEN");

    public static readonly EnvVariable Primary = "TODOIST_TOKEN";
    public static readonly EnvVariable Secondary = new("TODOIST_TOKEN_SECONDARY", string.Empty);
    public static readonly EnvVariable Tertiary = new("TODOIST_TOKEN_TERTIARY", string.Empty);
}
