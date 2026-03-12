namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
public class FiltersServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public FiltersServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
    public async Task CreateFilters_UpdateOneAndReorder_Delete_Succeeds()
    {
        var newFilter = TestData.Filters.AddFilter($"NewFilter_{Guid.NewGuid():N}", "today & !p4");
        var expectedNewFilter = TestData.Filters.ExpectedAddFilter(newFilter.Name, newFilter.Query);

        var siblingFilter = TestData.Filters.AddFilter($"SiblingFilter_{Guid.NewGuid():N}", "overdue & !recurring", itemOrder: 20);
        var expectedSiblingFilter = TestData.Filters.ExpectedAddFilter(siblingFilter.Name, siblingFilter.Query, siblingFilter.ItemOrder);


        // Step 1: Create filters.
        var syncResponse = await _apiFixture.PremiumClient.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Filters.AddAsync(newFilter, _cancellationToken);
                await t.Filters.AddAsync(siblingFilter, _cancellationToken);
            },
            [ResourceType.Filters],
            cancellationToken: _cancellationToken);
        await using var newFilterTracker = _apiFixture.TrackForCleanup(newFilter, c => c.Filters.DeleteAsync, isPremium: true);
        await using var siblingFilterTracker = _apiFixture.TrackForCleanup(siblingFilter, c => c.Filters.DeleteAsync, isPremium: true);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualNewFilter = Assert.Single(syncResponse.Filters, f => f.Id == newFilter.Id);
        Assert.Equivalent(expectedNewFilter, actualNewFilter);

        var actualSiblingFilter = Assert.Single(syncResponse.Filters, f => f.Id == siblingFilter.Id);
        Assert.Equivalent(expectedSiblingFilter, actualSiblingFilter);


        // Step 2: Update one filter and reorder both filters.
        var updateFilter = TestData.Filters.UpdateFilter(newFilter.Id, $"UpdatedFilter_{Guid.NewGuid():N}", "(today | overdue) & !p4");
        var expectedUpdatedFilter = TestData.Filters.ExpectedUpdateFilter(newFilter.Id, updateFilter.Name, updateFilter.Query, itemOrder: 30);

        syncResponse = await _apiFixture.PremiumClient.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Filters.UpdateAsync(updateFilter, _cancellationToken);
                await t.Filters.UpdateOrderAsync(new(new Dictionary<ComplexId, int>
                {
                    { newFilter.Id, 30 },
                    { siblingFilter.Id, 10 }
                }), _cancellationToken);
            },
            [ResourceType.Filters],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        actualNewFilter = Assert.Single(syncResponse.Filters, f => f.Id == newFilter.Id);
        Assert.Equivalent(expectedUpdatedFilter, actualNewFilter);
        Assert.Equal(30, actualNewFilter.ItemOrder);

        actualSiblingFilter = Assert.Single(syncResponse.Filters, f => f.Id == siblingFilter.Id);
        Assert.Equal(10, actualSiblingFilter.ItemOrder);


        // Step 3: Delete filters.
        syncResponse = await _apiFixture.PremiumClient.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Filters.DeleteAsync(newFilter.Id, _cancellationToken);
                await t.Filters.DeleteAsync(siblingFilter.Id, _cancellationToken);
            },
            [ResourceType.Filters],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Filters, f => f.Id == newFilter.Id && f.IsDeleted == true);
        Assert.Contains(syncResponse.Filters, f => f.Id == siblingFilter.Id && f.IsDeleted == true);

        newFilterTracker.StopTracking();
        siblingFilterTracker.StopTracking();
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public async Task CreateWorkspaceFilters_UpdateOneAndReorder_Delete_Succeeds()
    {
        var workspace = await _apiFixture.GetPlaygroundWorkspaceAsync();

        var newWorkspaceFilter = TestData.WorkspaceFilters.AddWorkspaceFilter(
            workspace.Id,
            $"NewWorkspaceFilter_{Guid.NewGuid():N}",
            "priority 1 & assigned to: team");
        var expectedNewWorkspaceFilter = TestData.WorkspaceFilters.ExpectedAddWorkspaceFilter(
            workspace.Id,
            newWorkspaceFilter.Name,
            newWorkspaceFilter.Query,
            newWorkspaceFilter.ItemOrder);

        var siblingWorkspaceFilter = TestData.WorkspaceFilters.AddWorkspaceFilter(
            workspace.Id,
            $"SiblingWorkspaceFilter_{Guid.NewGuid():N}",
            "overdue & assigned to: team",
            itemOrder: 20);
        var expectedSiblingWorkspaceFilter = TestData.WorkspaceFilters.ExpectedAddWorkspaceFilter(
            workspace.Id,
            siblingWorkspaceFilter.Name,
            siblingWorkspaceFilter.Query,
            siblingWorkspaceFilter.ItemOrder);


        // Step 1: Create workspace filters.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.WorkspaceFilters.AddAsync(newWorkspaceFilter, _cancellationToken);
                await t.WorkspaceFilters.AddAsync(siblingWorkspaceFilter, _cancellationToken);
            },
            [ResourceType.WorkspaceFilters],
            cancellationToken: _cancellationToken);
        await using var newWorkspaceFilterTracker = _apiFixture.TrackForCleanup(
            (c, ct) => c.ExecuteTransactionAsync(t => t.WorkspaceFilters.DeleteAsync(newWorkspaceFilter.Id, ct), ct),
            $"Workspace filter with ID {newWorkspaceFilter.Id}");
        await using var siblingWorkspaceFilterTracker = _apiFixture.TrackForCleanup(
            (c, ct) => c.ExecuteTransactionAsync(t => t.WorkspaceFilters.DeleteAsync(siblingWorkspaceFilter.Id, ct), ct),
            $"Workspace filter with ID {siblingWorkspaceFilter.Id}");

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualNewWorkspaceFilter = Assert.Single(syncResponse.WorkspaceFilters, f => f.Id == newWorkspaceFilter.Id);
        Assert.Equivalent(expectedNewWorkspaceFilter, actualNewWorkspaceFilter);

        var actualSiblingWorkspaceFilter = Assert.Single(syncResponse.WorkspaceFilters, f => f.Id == siblingWorkspaceFilter.Id);
        Assert.Equivalent(expectedSiblingWorkspaceFilter, actualSiblingWorkspaceFilter);


        // Step 2: Update one workspace filter and reorder both workspace filters.
        var updateWorkspaceFilter = TestData.WorkspaceFilters.UpdateWorkspaceFilter(
            newWorkspaceFilter.Id,
            $"UpdatedWorkspaceFilter_{Guid.NewGuid():N}",
            "(priority 1 | overdue) & assigned to: team",
            itemOrder: 30);
        var expectedUpdatedWorkspaceFilter = TestData.WorkspaceFilters.ExpectedUpdateWorkspaceFilter(
            newWorkspaceFilter.Id,
            updateWorkspaceFilter.Name,
            updateWorkspaceFilter.Query,
            itemOrder: 30);

        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.WorkspaceFilters.UpdateAsync(updateWorkspaceFilter, _cancellationToken);
                await t.WorkspaceFilters.UpdateOrdersAsync(new(new Dictionary<ComplexId, int>
                {
                    { newWorkspaceFilter.Id, 30 },
                    { siblingWorkspaceFilter.Id, 10 }
                }), _cancellationToken);
            },
            [ResourceType.WorkspaceFilters],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        actualNewWorkspaceFilter = Assert.Single(syncResponse.WorkspaceFilters, f => f.Id == newWorkspaceFilter.Id);
        Assert.Equivalent(expectedUpdatedWorkspaceFilter, actualNewWorkspaceFilter);

        actualSiblingWorkspaceFilter = Assert.Single(syncResponse.WorkspaceFilters, f => f.Id == siblingWorkspaceFilter.Id);
        Assert.Equal(10, actualSiblingWorkspaceFilter.ItemOrder);


        // Step 3: Delete workspace filters.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.WorkspaceFilters.DeleteAsync(newWorkspaceFilter.Id, _cancellationToken);
                await t.WorkspaceFilters.DeleteAsync(siblingWorkspaceFilter.Id, _cancellationToken);
            },
            [ResourceType.WorkspaceFilters],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.WorkspaceFilters, f => f.Id == newWorkspaceFilter.Id && f.IsDeleted == true);
        Assert.Contains(syncResponse.WorkspaceFilters, f => f.Id == siblingWorkspaceFilter.Id && f.IsDeleted == true);

        newWorkspaceFilterTracker.StopTracking();
        siblingWorkspaceFilterTracker.StopTracking();
    }
}