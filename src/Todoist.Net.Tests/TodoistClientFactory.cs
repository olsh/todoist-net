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
        if (token is null)
        {
            return null;
        }

        var todoistOAuthHandler = new TodoistOAuthHandler(new TodoistTokens(token))
        {
            InnerHandler = new HttpClientHandler()
        }; 
        return new TodoistClient(new RateLimitAwareRestClient(todoistOAuthHandler, outputHelper));
    }

    public static ITodoistClient? CreateTertiary(ITestOutputHelper? outputHelper = null)
    {
        var token = SettingsProvider.GetTertiaryToken();
        if (token is null)
        {
            return null;
        }
        
        var todoistOAuthHandler = new TodoistOAuthHandler(new TodoistTokens(token))
        {
            InnerHandler = new HttpClientHandler()
        }; 
        return new TodoistClient(new RateLimitAwareRestClient(todoistOAuthHandler, outputHelper));
    }
}
