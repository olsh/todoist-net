using Todoist.Net.OAuth;
using Todoist.Net.Tests.Settings;

namespace Todoist.Net.Tests;

public static class TodoistClientFactory
{
    public static ITodoistClient CreatePrimary(ITestOutputHelper? outputHelper = null)
    {
        var token = SettingsProvider.GetPrimaryToken();
        var todoistOAuthHandler = new TodoistOAuthHandler(new TodoistTokens(token))
        {
            InnerHandler = new HttpClientHandler()
        }; 
        return new TodoistClient(new RateLimitAwareRestClient(todoistOAuthHandler, outputHelper));
    }

    public static ITodoistClient? CreateSecondary(ITestOutputHelper? outputHelper = null)
    {
        var token = SettingsProvider.GetSecondaryToken();
        var todoistOAuthHandler = new TodoistOAuthHandler(new TodoistTokens(token))
        {
            InnerHandler = new HttpClientHandler()
        }; 
        return token is null
            ? null
            : new TodoistClient(new RateLimitAwareRestClient(todoistOAuthHandler, outputHelper));
    }

    public static ITodoistClient? CreateTertiary(ITestOutputHelper? outputHelper = null)
    {
        var token = SettingsProvider.GetTertiaryToken();
        var todoistOAuthHandler = new TodoistOAuthHandler(new TodoistTokens(token))
        {
            InnerHandler = new HttpClientHandler()
        }; 
        return token is null
            ? null
            : new TodoistClient(new RateLimitAwareRestClient(todoistOAuthHandler, outputHelper));
    }
}
