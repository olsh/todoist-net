using System.Net;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Todoist.Net.OAuth;

namespace Todoist.Net.Tests;

[Trait(Constants.TraitName, Constants.UnitTraitValue)]
public class TodoistOAuthTests
{
    private static readonly TodoistOAuthOptions Options = new()
    {
        ClientId = "client-id",
        ClientSecret = "client-secret"
    };

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task SendRequest_WithTokensWhichAreNotExpiring_AuthorizesWithoutRefreshing(bool knownExpiration)
    {
        var server = new FakeOAuthServer("access-0");
        var recorder = new TokensRecorder();
        var tokens = new TodoistTokens(
            "access-0",
            "refresh-0",
            knownExpiration ? DateTimeOffset.UtcNow.AddHours(1) : null);
        using var client = TodoistClient.CreateOAuthClient(Options, tokens, recorder.StoreAsync, server);


        // Step 1: Send a request.
        await ((IAdvancedTodoistClient)client).GetAsync(
            "projects",
            cancellationToken: TestContext.Current.CancellationToken);


        // Step 2: Assert the request carried the access token and the tokens were left alone.
        var request = Assert.Single(server.Requests);
        Assert.Equal("Bearer", request.Authorization?.Scheme);
        Assert.Equal("access-0", request.Authorization?.Parameter);
        Assert.Empty(recorder.Tokens);
    }

    [Fact]
    public async Task SendRequest_WithExpiringTokens_RefreshesThemBeforeSending()
    {
        var server = new FakeOAuthServer("access-0");
        var recorder = new TokensRecorder();
        var tokens = new TodoistTokens("access-0", "refresh-0", DateTimeOffset.UtcNow.AddSeconds(30));
        using var client = TodoistClient.CreateOAuthClient(Options, tokens, recorder.StoreAsync, server);


        // Step 1: Send a request with tokens which expire in less than a minute.
        await ((IAdvancedTodoistClient)client).GetAsync(
            "projects",
            cancellationToken: TestContext.Current.CancellationToken);


        // Step 2: Assert the tokens were refreshed first, with the refresh token and the application credentials.
        Assert.Equal(2, server.Requests.Count);
        var tokenRequest = server.Requests[0];
        Assert.Equal(FakeOAuthServer.TokenEndpoint, tokenRequest.Uri);
        Assert.Equal(HttpMethod.Post, tokenRequest.Method);
        Assert.Null(tokenRequest.Authorization);
        Assert.Equal("refresh_token", tokenRequest.Form["grant_type"]);
        Assert.Equal("refresh-0", tokenRequest.Form["refresh_token"]);
        Assert.Equal("client-id", tokenRequest.Form["client_id"]);
        Assert.Equal("client-secret", tokenRequest.Form["client_secret"]);


        // Step 3: Assert the request carried the refreshed access token, and the refreshed tokens were reported once.
        Assert.Equal("access-1", server.Requests[1].Authorization?.Parameter);

        var refreshedTokens = Assert.Single(recorder.Tokens);
        Assert.Equal("access-1", refreshedTokens.AccessToken);
        Assert.Equal("refresh-1", refreshedTokens.RefreshToken);
        Assert.NotNull(refreshedTokens.ExpiresAt);
        Assert.InRange(
            refreshedTokens.ExpiresAt.Value,
            DateTimeOffset.UtcNow.AddSeconds(3500),
            DateTimeOffset.UtcNow.AddSeconds(3600));
        Assert.Same(refreshedTokens, client.OAuthHandler?.Tokens);
    }

    [Fact]
    public async Task RefreshTokens_OfPublicClient_OmitsTheClientSecret()
    {
        var server = new FakeOAuthServer("access-0");
        var recorder = new TokensRecorder();
        var options = new TodoistOAuthOptions { ClientId = "client-id" };
        using var client = TodoistClient.CreateOAuthClient(
            options,
            new TodoistTokens("access-0", "refresh-0"),
            recorder.StoreAsync,
            server);


        // Step 1: Refresh the tokens of an application without a client secret.
        var refreshedTokens = await client.RefreshTokensAsync(TestContext.Current.CancellationToken);


        // Step 2: Assert the token request carried the client ID alone, and the refreshed tokens were reported.
        var tokenRequest = Assert.Single(server.TokenRequests);
        Assert.Equal("client-id", tokenRequest.Form["client_id"]);
        Assert.Null(tokenRequest.Form["client_secret"]);
        Assert.Equal("access-1", refreshedTokens.AccessToken);
        Assert.Same(refreshedTokens, Assert.Single(recorder.Tokens));
    }

    [Fact]
    public async Task SendRequest_WhenAccessTokenIsRejected_RefreshesTokensAndSendsTheRequestAgain()
    {
        var server = new FakeOAuthServer("revoked-elsewhere");
        var recorder = new TokensRecorder();
        using var client = TodoistClient.CreateOAuthClient(
            Options,
            new TodoistTokens("access-0", "refresh-0"),
            recorder.StoreAsync,
            server);


        // Step 1: Send a request with a body while the API rejects the access token.
        await ((IAdvancedTodoistClient)client).PostJsonAsync(
            "tasks",
            new { content = "Buy milk" },
            TestContext.Current.CancellationToken);


        // Step 2: Assert the request was sent again with the refreshed access token and the same body.
        Assert.Equal(2, server.ApiRequests.Count);
        Assert.Equal("access-0", server.ApiRequests[0].Authorization?.Parameter);
        Assert.Equal("access-1", server.ApiRequests[1].Authorization?.Parameter);
        Assert.Equal(server.ApiRequests[0].Body, server.ApiRequests[1].Body);
        Assert.Contains("Buy milk", server.ApiRequests[1].Body);

        Assert.Single(server.TokenRequests);
        Assert.Equal(
            "access-1",
            Assert.Single(recorder.Tokens)
                .AccessToken);
    }

    [Fact]
    public async Task SendRequest_WhenAccessTokenIsRejectedWithoutRefreshToken_ThrowsWithoutRefreshing()
    {
        var server = new FakeOAuthServer("revoked-elsewhere");
        using var client = TodoistClient.CreateOAuthClient(Options, new TodoistTokens("access-0"), null, server);


        // Step 1: Send a request while the API rejects the access token, which cannot be refreshed.
        var exception = await Assert.ThrowsAsync<TodoistException>(() =>
            ((IAdvancedTodoistClient)client).GetAsync(
                "projects",
                cancellationToken: TestContext.Current.CancellationToken));


        // Step 2: Assert the rejection surfaced and no refresh was attempted.
        Assert.Equal(401, exception.HttpCode);
        Assert.Single(server.ApiRequests);
        Assert.Empty(server.TokenRequests);
    }

    [Fact]
    public async Task SendRequest_WhenRefreshTokenIsRejected_ThrowsTheTokenEndpointError()
    {
        var server = new FakeOAuthServer("access-0")
        {
            TokenEndpointHandler = _ => Task.FromResult(
                FakeOAuthServer.CreateJsonResponse(HttpStatusCode.BadRequest, """{ "error": "invalid_grant" }"""))
        };
        var recorder = new TokensRecorder();
        var tokens = new TodoistTokens("access-0", "refresh-0", DateTimeOffset.UtcNow.AddSeconds(-1));
        using var client = TodoistClient.CreateOAuthClient(Options, tokens, recorder.StoreAsync, server);


        // Step 1: Send a request with expired tokens whose refresh token Todoist rejects.
        var exception = await Assert.ThrowsAsync<TodoistException>(() =>
            ((IAdvancedTodoistClient)client).GetAsync(
                "projects",
                cancellationToken: TestContext.Current.CancellationToken));


        // Step 2: Assert the token endpoint error surfaced, and nothing was sent or reported.
        Assert.Equal("invalid_grant", exception.Message);
        Assert.Equal(400, exception.HttpCode);
        Assert.Empty(server.ApiRequests);
        Assert.Empty(recorder.Tokens);
    }

    [Fact]
    public async Task SendRequest_WhenTokenEndpointOmitsRefreshToken_KeepsTheAccessWithoutReportingTokens()
    {
        var server = new FakeOAuthServer("access-0");
        server.TokenEndpointHandler = _ => Task.FromResult(server.IssueTokens(includeRefreshToken: false));
        var recorder = new TokensRecorder();
        var tokens = new TodoistTokens("access-0", "refresh-0", DateTimeOffset.UtcNow.AddSeconds(-1));
        using var client = TodoistClient.CreateOAuthClient(Options, tokens, recorder.StoreAsync, server);


        // Step 1: Send a request, which refreshes the tokens with a refresh token another client already used,
        // so Todoist answers within its grace window with the access token alone.
        await ((IAdvancedTodoistClient)client).GetAsync(
            "projects",
            cancellationToken: TestContext.Current.CancellationToken);


        // Step 2: Assert the new access token was used but not reported, since reporting it would overwrite
        // the stored refresh token with none.
        Assert.Equal(
            "access-1",
            Assert.Single(server.ApiRequests)
                .Authorization?.Parameter);
        Assert.Empty(recorder.Tokens);
        Assert.Null(client.OAuthHandler?.Tokens.RefreshToken);


        // Step 3: Assert a rejected access token is no longer refreshed.
        server.ValidAccessToken = "revoked-elsewhere";
        await Assert.ThrowsAsync<TodoistException>(() =>
            ((IAdvancedTodoistClient)client).GetAsync(
                "projects",
                cancellationToken: TestContext.Current.CancellationToken));
        Assert.Single(server.TokenRequests);
    }

    [Fact]
    public async Task SendRequests_WhenAccessTokenIsRejectedConcurrently_RefreshesTokensOnce()
    {
        const int requestCount = 5;

        var releaseTokenResponse = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var server = new FakeOAuthServer("revoked-elsewhere");
        server.TokenEndpointHandler = async _ =>
        {
            await releaseTokenResponse.Task;
            return server.IssueTokens();
        };
        var recorder = new TokensRecorder();
        using var client = TodoistClient.CreateOAuthClient(
            Options,
            new TodoistTokens("access-0", "refresh-0"),
            recorder.StoreAsync,
            server);


        // Step 1: Send concurrent requests which all get rejected, and hold the refresh until all of them wait for it.
        var requests = Enumerable.Range(0, requestCount)
            .Select(_ => ((IAdvancedTodoistClient)client).GetAsync(
                "projects",
                cancellationToken: TestContext.Current.CancellationToken))
            .ToList();

        await WaitUntilAsync(() => server.ApiRequests.Count == requestCount && server.TokenRequests.Count == 1);
        releaseTokenResponse.SetResult();
        await Task.WhenAll(requests);


        // Step 2: Assert a single refresh served every request.
        Assert.Single(server.TokenRequests);
        Assert.Single(recorder.Tokens);
        Assert.Equal(
            requestCount,
            server.ApiRequests.Count(request => request.Authorization?.Parameter == "access-1"));
    }

    [Fact]
    public async Task SendRequest_WhenRejectedTokensWereRefreshedMeanwhile_SendsTheRequestAgainWithoutRefreshing()
    {
        var releaseApiResponse = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var server = new FakeOAuthServer("access-0")
        {
            BeforeApiResponse = request => request.Authorization?.Parameter == "access-0"
                ? releaseApiResponse.Task
                : Task.CompletedTask
        };
        var recorder = new TokensRecorder();
        using var client = TodoistClient.CreateOAuthClient(
            Options,
            new TodoistTokens("access-0", "refresh-0"),
            recorder.StoreAsync,
            server);


        // Step 1: Send a request, and refresh the tokens before the API rejects its now stale access token.
        var request = ((IAdvancedTodoistClient)client).GetAsync(
            "projects",
            cancellationToken: TestContext.Current.CancellationToken);
        await WaitUntilAsync(() => server.ApiRequests.Count == 1);

        await client.RefreshTokensAsync(TestContext.Current.CancellationToken);
        releaseApiResponse.SetResult();
        await request;


        // Step 2: Assert the request was sent again with the tokens refreshed meanwhile, without another refresh.
        Assert.Single(server.TokenRequests);
        Assert.Single(recorder.Tokens);
        Assert.Equal(
            ["access-0", "access-1"],
            server.ApiRequests.Select(apiRequest => apiRequest.Authorization?.Parameter));
    }

    [Fact]
    public async Task SendRequest_WhenCanceledWhileWaitingForRefresh_LeavesTheRefreshToOthers()
    {
        var releaseTokenResponse = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var server = new FakeOAuthServer("access-0");
        server.TokenEndpointHandler = async _ =>
        {
            await releaseTokenResponse.Task;
            return server.IssueTokens();
        };
        var recorder = new TokensRecorder();
        var tokens = new TodoistTokens("access-0", "refresh-0", DateTimeOffset.UtcNow.AddSeconds(-1));
        using var client = TodoistClient.CreateOAuthClient(Options, tokens, recorder.StoreAsync, server);
        using var cancellationSource =
            CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);


        // Step 1: Send a request which starts refreshing the expired tokens, then another one which waits for the refresh.
        var refreshingRequest = ((IAdvancedTodoistClient)client).GetAsync(
            "projects",
            cancellationToken: TestContext.Current.CancellationToken);
        await WaitUntilAsync(() => server.TokenRequests.Count == 1);

        var waitingRequest = ((IAdvancedTodoistClient)client).GetAsync(
            "projects",
            cancellationToken: cancellationSource.Token);


        // Step 2: Cancel the waiting request, then let the refresh finish.
        await cancellationSource.CancelAsync();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => waitingRequest);

        releaseTokenResponse.SetResult();
        await refreshingRequest;


        // Step 3: Assert the refresh completed for the request which kept waiting.
        Assert.Single(server.TokenRequests);
        Assert.Single(recorder.Tokens);
        Assert.Equal(
            "access-1",
            Assert.Single(server.ApiRequests)
                .Authorization?.Parameter);
    }

    [Fact]
    public async Task UploadFile_WhenAccessTokenIsRejected_SendsTheWholeFileAgain()
    {
        var server = new FakeOAuthServer("revoked-elsewhere");
        var recorder = new TokensRecorder();
        using var client = TodoistClient.CreateOAuthClient(
            Options,
            new TodoistTokens("access-0", "refresh-0"),
            recorder.StoreAsync,
            server);

        var file = new UploadFile(TestData.Files.GreenPng10x10, "green.png");


        // Step 1: Upload a file while the API rejects the access token.
        await ((IAdvancedTodoistClient)client).PostFilesAsync(
            "uploads",
            [file],
            cancellationToken: TestContext.Current.CancellationToken);


        // Step 2: Assert the whole file was sent again with the refreshed access token.
        Assert.Equal(2, server.ApiRequests.Count);
        Assert.Equal("access-1", server.ApiRequests[1].Authorization?.Parameter);
        Assert.Equal(server.ApiRequests[0].Body, server.ApiRequests[1].Body);
    }

    [Fact]
    public async Task UploadFile_FromStreamWhichCannotSeek_IsNotSentAgain()
    {
        var server = new FakeOAuthServer("revoked-elsewhere");
        var recorder = new TokensRecorder();
        using var client = TodoistClient.CreateOAuthClient(
            Options,
            new TodoistTokens("access-0", "refresh-0"),
            recorder.StoreAsync,
            server);

        var file = new UploadFile(new NonSeekableStream(TestData.Files.GreenPng10x10), "green.png");


        // Step 1: Upload a file which can be read only once while the API rejects the access token.
        var exception = await Assert.ThrowsAsync<TodoistException>(() =>
            ((IAdvancedTodoistClient)client).PostFilesAsync(
                "uploads",
                [file],
                cancellationToken: TestContext.Current.CancellationToken));


        // Step 2: Assert the rejection surfaced instead of a failure to read the file again.
        Assert.Equal(401, exception.HttpCode);
        Assert.Single(server.ApiRequests);
        Assert.Empty(server.TokenRequests);
    }

    [Fact]
    public async Task RefreshTokens_WhenStoringThemFails_ThrowsButKeepsUsingTheRefreshedTokens()
    {
        var server = new FakeOAuthServer("access-0");
        using var client = TodoistClient.CreateOAuthClient(
            Options,
            new TodoistTokens("access-0", "refresh-0"),
            _ => throw new InvalidOperationException("The storage is down."),
            server);


        // Step 1: Refresh the tokens while storing them fails.
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            client.RefreshTokensAsync(TestContext.Current.CancellationToken));


        // Step 2: Assert the client kept the refreshed tokens, since the previous ones no longer work.
        Assert.Equal("access-1", client.OAuthHandler?.Tokens.AccessToken);

        await ((IAdvancedTodoistClient)client).GetAsync(
            "projects",
            cancellationToken: TestContext.Current.CancellationToken);
        Assert.Equal(
            "access-1",
            Assert.Single(server.ApiRequests)
                .Authorization?.Parameter);
        Assert.Single(server.TokenRequests);
    }

    [Fact]
    public async Task RevokeTokens_WithClientSecret_RevokesTheAccessTokenAndStopsRefreshing()
    {
        var server = new FakeOAuthServer("access-0");
        var recorder = new TokensRecorder();
        using var client = TodoistClient.CreateOAuthClient(
            Options,
            new TodoistTokens("access-0", "refresh-0"),
            recorder.StoreAsync,
            server);


        // Step 1: Revoke the tokens.
        await client.RevokeTokensAsync(TestContext.Current.CancellationToken);


        // Step 2: Assert the access token was revoked with the application credentials. Todoist rejects
        // the revocation of refresh tokens, so the refresh token is not sent.
        var revokeRequest = Assert.Single(server.Requests);
        Assert.Equal(FakeOAuthServer.RevokeEndpoint, revokeRequest.Uri);
        Assert.Equal(HttpMethod.Post, revokeRequest.Method);
        Assert.Equal("Basic", revokeRequest.Authorization?.Scheme);
        Assert.Equal(
            Convert.ToBase64String(Encoding.UTF8.GetBytes("client-id:client-secret")),
            revokeRequest.Authorization?.Parameter);
        Assert.Equal("access-0", revokeRequest.Form["token"]);
        Assert.Equal("access_token", revokeRequest.Form["token_type_hint"]);


        // Step 3: Assert a rejected access token is no longer refreshed back to life.
        server.ValidAccessToken = "revoked";
        await Assert.ThrowsAsync<TodoistException>(() =>
            ((IAdvancedTodoistClient)client).GetAsync(
                "projects",
                cancellationToken: TestContext.Current.CancellationToken));
        Assert.Empty(server.TokenRequests);
        Assert.Empty(recorder.Tokens);
    }

    [Fact]
    public async Task RefreshTokens_WhenTokenResponseBodyStalls_TimesOutAndReleasesTheRefresh()
    {
        var stallBody = true;
        var server = new FakeOAuthServer("access-0");
        server.TokenEndpointHandler = _ => Task.FromResult(
            stallBody
                ? new HttpResponseMessage(HttpStatusCode.OK) { Content = new StreamContent(new StallingStream()) }
                : server.IssueTokens());
        var recorder = new TokensRecorder();
        using var client = TodoistClient.CreateOAuthClient(
            Options,
            new TodoistTokens("access-0", "refresh-0"),
            recorder.StoreAsync,
            server);
        client.OAuthHandler!.TokenRequestTimeout = TimeSpan.FromMilliseconds(200);


        // Step 1: Refresh the tokens while the token endpoint sends the headers but never the body.
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            client.RefreshTokensAsync(TestContext.Current.CancellationToken));


        // Step 2: Assert the stalled refresh let go, so the next one could run.
        stallBody = false;
        var refreshedTokens = await client.RefreshTokensAsync(TestContext.Current.CancellationToken);

        Assert.Equal("access-1", refreshedTokens.AccessToken);
        Assert.Equal(2, server.TokenRequests.Count);
    }

    [Fact]
    public async Task RevokeTokens_WithoutClientSecret_Throws()
    {
        var server = new FakeOAuthServer("access-0");
        var options = new TodoistOAuthOptions { ClientId = "client-id" };
        using var client = TodoistClient.CreateOAuthClient(
            options,
            new TodoistTokens("access-0", "refresh-0"),
            _ => Task.CompletedTask,
            server);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            client.RevokeTokensAsync(TestContext.Current.CancellationToken));
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task RefreshAndRevokeTokens_OfClientWithoutOAuth_Throw()
    {
        using var client = new TodoistClient(new StubTodoistRestClient());

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            client.RefreshTokensAsync(TestContext.Current.CancellationToken));
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            client.RevokeTokensAsync(TestContext.Current.CancellationToken));
    }

    [Fact]
    public void Constructor_WithRefreshTokenButNoCallback_Throws()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new TodoistClient(Options, new TodoistTokens("access-0", "refresh-0"), null));

        Assert.Equal("onTokensRefreshed", exception.ParamName);
    }

    [Fact]
    public async Task CreateOAuthClient_FromServiceProvider_AppliesTheConfiguredHttpClientOptions()
    {
        var server = new FakeOAuthServer("access-0");
        var services = new ServiceCollection();
        services.AddTodoistClient(options => options.ClientId = "client-id");
        services.ConfigureHttpClientDefaults(builder => builder
            .ConfigurePrimaryHttpMessageHandler(() => server)
            .ConfigureHttpClient(httpClient => httpClient.DefaultRequestHeaders.Add("X-Configured", "yes")));
        await using var serviceProvider = services.BuildServiceProvider();

        using var oauthClient = serviceProvider.GetRequiredService<ITodoistOAuthClientFactory>()
            .CreateClient(new TodoistTokens("access-0"), null);
        using var tokenClient = serviceProvider.GetRequiredService<ITodoistClientFactory>()
            .CreateClient("access-0");


        // Step 1: Send a request with a client of each kind.
        await ((IAdvancedTodoistClient)oauthClient).GetAsync(
            "projects",
            cancellationToken: TestContext.Current.CancellationToken);
        await ((IAdvancedTodoistClient)tokenClient).GetAsync(
            "projects",
            cancellationToken: TestContext.Current.CancellationToken);


        // Step 2: Assert both carried what the application configured for its HTTP clients.
        Assert.Equal(2, server.ApiRequests.Count);
        Assert.All(
            server.ApiRequests,
            request => Assert.Equal("yes", request.Headers.GetValueOrDefault("X-Configured")));
    }

    [Fact]
    public async Task CreateOAuthClients_FromServiceProvider_KeepTheTokensOfEachUserApart()
    {
        var server = new FakeOAuthServer("access-a");
        var services = new ServiceCollection();
        services.AddTodoistClient(options =>
        {
            options.ClientId = "client-id";
            options.ClientSecret = "client-secret";
        });
        services.ConfigureHttpClientDefaults(builder => builder.ConfigurePrimaryHttpMessageHandler(() => server));
        await using var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<ITodoistOAuthClientFactory>();
        var recorderA = new TokensRecorder();
        var recorderB = new TokensRecorder();
        using var clientA = factory.CreateClient(new TodoistTokens("access-a", "refresh-a"), recorderA.StoreAsync);
        using var clientB = factory.CreateClient(new TodoistTokens("access-b", "refresh-b"), recorderB.StoreAsync);


        // Step 1: Send a request as each user, while the API rejects the access token of the second one.
        await ((IAdvancedTodoistClient)clientA).GetAsync(
            "projects",
            cancellationToken: TestContext.Current.CancellationToken);
        await ((IAdvancedTodoistClient)clientB).GetAsync(
            "projects",
            cancellationToken: TestContext.Current.CancellationToken);


        // Step 2: Assert only the second user's tokens were refreshed, through the pipeline of the service provider.
        Assert.Equal(
            "refresh-b",
            Assert.Single(server.TokenRequests)
                .Form["refresh_token"]);
        Assert.Equal(
            ["access-a", "access-b", "access-1"],
            server.ApiRequests.Select(request => request.Authorization?.Parameter));

        Assert.Empty(recorderA.Tokens);
        Assert.Equal(
            "access-1",
            Assert.Single(recorderB.Tokens)
                .AccessToken);
        Assert.Equal("access-a", clientA.OAuthHandler?.Tokens.AccessToken);
    }

    [Fact]
    public async Task CreateOAuthClient_WithoutConfiguredClientId_Throws()
    {
        var services = new ServiceCollection();
        services.AddTodoistClient();
        await using var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<ITodoistOAuthClientFactory>();

        Assert.Throws<InvalidOperationException>(() =>
            factory.CreateClient(new TodoistTokens("access-0", "refresh-0"), _ => Task.CompletedTask));
        Assert.Same(factory, serviceProvider.GetRequiredService<ITodoistClientFactory>());
    }

    private static async Task WaitUntilAsync(Func<bool> condition)
    {
        using var timeoutSource =
            CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
        timeoutSource.CancelAfter(TimeSpan.FromSeconds(10));

        while (!condition())
        {
            await Task.Delay(10, timeoutSource.Token);
        }
    }

    private sealed class NonSeekableStream(byte[] content) : MemoryStream(content)
    {
        public override bool CanSeek => false;
    }

    /// <summary>
    /// A response body which never delivers a byte, like a connection that stalls after the headers.
    /// </summary>
    private sealed class StallingStream : MemoryStream
    {
        public override async ValueTask<int> ReadAsync(
            Memory<byte> buffer,
            CancellationToken cancellationToken = default)
        {
            await Task.Delay(Timeout.Infinite, cancellationToken);
            return 0;
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return ReadAsync(buffer.AsMemory(offset, count), cancellationToken)
                .AsTask();
        }
    }
}
