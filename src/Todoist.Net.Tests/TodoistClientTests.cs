namespace Todoist.Net.Tests;

[Collection(TodoistApiTestCollection.Name)]
public class TodoistClientTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public TodoistClientTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public async Task SyncAllResources_Success()
    {
        var resources = await _apiFixture.Client.SyncResourcesAsync(cancellationToken: _cancellationToken);

        Assert.NotNull(resources);
        Assert.NotNull(resources.UserInfo?.Id);
        Assert.NotNull(resources.SyncToken);
        Assert.True(resources.FullSync);
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public async Task SyncAllResourcesWithSyncToken_Success()
    {
        var resources = await _apiFixture.Client.SyncResourcesAsync(cancellationToken: _cancellationToken);
        resources = await _apiFixture.Client.SyncResourcesAsync(syncToken: resources.SyncToken, cancellationToken: _cancellationToken);

        Assert.NotNull(resources);
        Assert.NotNull(resources.SyncToken);
        Assert.False(resources.FullSync);
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationRefreshableTraitValue)]
    public async Task RefreshTokens_Success()
    {
        var response = await _apiFixture.Client.RefreshTokensAsync(cancellationToken: _cancellationToken);

        Assert.NotNull(response);
        Assert.NotEmpty(response.AccessToken);
        Assert.NotEmpty(response.RefreshToken);
    }
}
