namespace Todoist.Net.Tests;

[Collection(TodoistApiTestCollection.Name)]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
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
    public async Task SyncAllResources_Success()
    {
        var resources = await _apiFixture.Client.SyncResourcesAsync(cancellationToken: _cancellationToken);

        Assert.NotNull(resources);
        Assert.NotNull(resources.UserInfo?.Id);
        Assert.NotNull(resources.SyncToken);
        Assert.True(resources.FullSync);
    }

    [Fact]
    public async Task SyncAllResourcesWithSyncToken_Success()
    {
        var resources = await _apiFixture.Client.SyncResourcesAsync(cancellationToken: _cancellationToken);
        resources = await _apiFixture.Client.SyncResourcesAsync(syncToken: resources.SyncToken, cancellationToken: _cancellationToken);

        Assert.NotNull(resources);
        Assert.NotNull(resources.SyncToken);
        Assert.False(resources.FullSync);
    }
}
