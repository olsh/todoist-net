namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
public class ViewOptionsServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public ViewOptionsServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    public async Task SetProjectViewOptions_Delete_Succeeds()
    {
        var project = await _apiFixture.GetPlaygroundProjectAsync();

        var newViewOptions = TestData.ViewOptions.ProjectViewOptions(project.Id);
        var expectedViewOptions = TestData.ViewOptions.ExpectedProjectViewOptions(project.Id);


        // Step 1: Set project view options.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.ViewOptions.SetAsync(newViewOptions, _cancellationToken),
            [ResourceType.ViewOptions],
            cancellationToken: _cancellationToken);
        await using var viewOptionsTracker = _apiFixture.TrackForCleanup(
            (c, ct) => c.ViewOptions.DeleteAsync(newViewOptions, ct),
            $"View options for project with ID {project.Id}");

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualViewOptions = Assert.Single(
            syncResponse.ViewOptions,
            v => v.ViewType == ViewOptionsType.Project && v.ObjectId == project.Id);
        Assert.Equivalent(expectedViewOptions, actualViewOptions);


        // Step 2: Delete project view options.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.ViewOptions.DeleteAsync(newViewOptions, _cancellationToken),
            [ResourceType.ViewOptions],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(
            syncResponse.ViewOptions,
            v => v.ViewType == ViewOptionsType.Project && v.ObjectId == project.Id && v.IsDeleted == true);

        viewOptionsTracker.StopTracking();
    }
}