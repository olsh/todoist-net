using Todoist.Net.Tests.Settings;

namespace Todoist.Net.Tests;

public static class TodoistClientFactory
{
    public static ITodoistClient CreatePrimary(ITestOutputHelper? outputHelper = null)
    {
        var token = SettingsProvider.GetPrimaryToken();
        return new TodoistClient(new RateLimitAwareRestClient(token, outputHelper));
    }

    public static ITodoistClient? CreateSecondary(ITestOutputHelper? outputHelper = null)
    {
        var token = SettingsProvider.GetSecondaryToken();

        return token is null
            ? null
            : new TodoistClient(new RateLimitAwareRestClient(token, outputHelper));
    }

    public static ITodoistClient? CreateTertiary(ITestOutputHelper? outputHelper = null)
    {
        var token = SettingsProvider.GetTertiaryToken();

        return token is null
            ? null
            : new TodoistClient(new RateLimitAwareRestClient(token, outputHelper));
    }
}
