using Todoist.Net.Tests.Authentication;

namespace Todoist.Net.Tests;

public sealed class TodoistClientFactory
{
    private readonly AuthContextProvider? _authContextProvider;
    
    public TodoistClientFactory()
    {
        if (DopplerEnvVariables.IsDopplerEnabled)
        {
            _authContextProvider = new AuthContextProvider();
        }
    }

    public async Task<ITodoistClient> CreatePrimaryAsync(ITestOutputHelper? outputHelper = null, CancellationToken cancellationToken = default)
    {
        if (_authContextProvider is not null) 
        {
            var authContext = await _authContextProvider.GetPrimaryAuthContextAsync(cancellationToken);
            return new TodoistClient(new RateLimitAwareRestClient(authContext, outputHelper));
        }
        var token = NonExpiringTokensProvider.GetPrimaryToken();
        return new TodoistClient(new RateLimitAwareRestClient(token, outputHelper));
    }

    public async Task<ITodoistClient?> CreateSecondaryAsync(ITestOutputHelper? outputHelper = null, CancellationToken cancellationToken = default)
    {
        if (_authContextProvider is not null) 
        {
            var authContext = await _authContextProvider.GetSecondaryAuthContextAsync(cancellationToken);
            return authContext is null
                ? null
                : new TodoistClient(new RateLimitAwareRestClient(authContext, outputHelper));
        }
        var token = NonExpiringTokensProvider.GetSecondaryToken();
        return token is null
            ? null
            : new TodoistClient(new RateLimitAwareRestClient(token, outputHelper));
    }

    public async Task<ITodoistClient?> CreateTertiaryAsync(ITestOutputHelper? outputHelper = null, CancellationToken cancellationToken = default)
    {
        if (_authContextProvider is not null) 
        {
            var authContext = await _authContextProvider.GetTertiaryAuthContextAsync(cancellationToken);
            return authContext is null
                ? null
                : new TodoistClient(new RateLimitAwareRestClient(authContext, outputHelper));
        }
        var token = NonExpiringTokensProvider.GetTertiaryToken();
        return token is null
            ? null
            : new TodoistClient(new RateLimitAwareRestClient(token, outputHelper));
    }
}
