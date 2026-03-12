namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
public class LabelsServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public LabelsServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public async Task CreateLabel_UpdateAndReorder_GetById_GetSearch_Delete_Succeeds()
    {
        var newLabel = TestData.Labels.AddLabel($"NewLabel_{Guid.NewGuid():N}");
        var expectedNewLabel = TestData.Labels.ExpectedAddLabel(newLabel.Name);


        // Step 1: Create label.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Labels.AddAsync(newLabel, _cancellationToken),
            [ResourceType.Labels],
            cancellationToken: _cancellationToken);
        // Track the created entity for cleanup if assertions fail before deletion step, otherwise stop tracking after deletion step.
        await using var tracker = _apiFixture.TrackForCleanup(newLabel, c =>
        {
            return (id, ct) => c.Labels.DeleteAsync(id, cancellationToken: ct);
        });

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualNewLabel = Assert.Single(syncResponse.Labels, l => l.Id == newLabel.Id);
        Assert.Equivalent(expectedNewLabel, actualNewLabel);


        // Step 2: Update label and reorder it.
        var updateLabel = TestData.Labels.UpdateLabel(newLabel.Id, $"UpdatedLabel_{Guid.NewGuid():N}");
        var expectedUpdatedLabel = TestData.Labels.ExpectedUpdatedLabel(newLabel.Id, updateLabel.Name);

        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Labels.UpdateAsync(updateLabel, _cancellationToken);
                await t.Labels.UpdateOrderAsync(new(new Dictionary<ComplexId, int>
                {
                    { newLabel.Id, updateLabel.ItemOrder!.Value }
                }), _cancellationToken);
            },
            [ResourceType.Labels],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualUpdatedLabel = Assert.Single(syncResponse.Labels, l => l.Id == newLabel.Id);
        Assert.Equivalent(expectedUpdatedLabel, actualUpdatedLabel);


        // Step 3: Get label by id, get all labels, and search labels.
        var actualLabel = await _apiFixture.Client.Labels.GetAsync(newLabel.Id.PersistentId, _cancellationToken);
        Assert.Equivalent(expectedUpdatedLabel, actualLabel);

        var labelsResponse = await _apiFixture.Client.Labels.GetAsync(cancellationToken: _cancellationToken);
        Assert.Contains(labelsResponse.Results, l => l.Id == newLabel.Id && l.Name == updateLabel.Name);

        var searchResponse = await _apiFixture.Client.Labels.SearchAsync(new($"UpdatedLabel*"), _cancellationToken);
        Assert.Contains(searchResponse.Results, l => l.Id == newLabel.Id && l.Name == updateLabel.Name);


        // Step 4: Delete label.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Labels.DeleteAsync(newLabel.Id, cancellationToken: _cancellationToken),
            [ResourceType.Labels],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Labels, l => l.Id == newLabel.Id && l.IsDeleted == true);

        tracker.StopTracking();
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public async Task CreateLabeledTask_DeleteLabelKeepAsShared_GetShared_RenameShared_DeleteShared_Succeeds()
    {
        var newLabel = TestData.Labels.AddLabel($"SharedLabel_{Guid.NewGuid():N}");
        var renamedLabelName = $"RenamedSharedLabel_{Guid.NewGuid():N}";
        var newTask = new AddTask($"Label flow task {Guid.NewGuid():N}")
        {
            Labels = [newLabel.Name]
        };


        // Step 1: Create label and task.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Labels.AddAsync(newLabel, _cancellationToken);
                await t.Tasks.AddAsync(newTask, _cancellationToken);
            },
            [ResourceType.Labels, ResourceType.Tasks],
            cancellationToken: _cancellationToken);
        await using var labelTracker = _apiFixture.TrackForCleanup(newLabel, c =>
        {
            return (id, ct) => c.Labels.DeleteAsync(id, cancellationToken: ct);
        });
        await using var taskTracker = _apiFixture.TrackForCleanup(newTask, c => c.Tasks.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualNewLabel = Assert.Single(syncResponse.Labels, l => l.Id == newLabel.Id);
        Assert.Equal(newLabel.Name, actualNewLabel.Name);

        var actualNewTask = Assert.Single(syncResponse.Tasks, t => t.Id == newTask.Id);
        Assert.Contains(newLabel.Name, actualNewTask.Labels);


        // Step 2: Delete the personal label and keep the task label as shared.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Labels.DeleteAsync(newLabel.Id, keepAsShared: true, _cancellationToken),
            [ResourceType.Labels],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Labels, l => l.Id == newLabel.Id && l.IsDeleted == true);

        labelTracker.StopTracking();


        // Step 3: Get shared labels.
        var sharedLabelsResponse = await _apiFixture.Client.Labels.GetSharedAsync(
            new SharedLabelsPaginationQuery(omitPersonal: true),
            _cancellationToken);

        Assert.Contains(newLabel.Name, sharedLabelsResponse.Results);


        // Step 4: Rename the shared label and verify the task label updates too.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Labels.RenameSharedAsync(newLabel.Name, renamedLabelName, _cancellationToken),
            [ResourceType.Tasks],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualRenamedTask = Assert.Single(syncResponse.Tasks, t => t.Id == newTask.Id);
        Assert.Contains(renamedLabelName, actualRenamedTask.Labels);
        Assert.DoesNotContain(newLabel.Name, actualRenamedTask.Labels);


        // Step 5: Delete the shared label occurrences and verify they are removed from the task.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Labels.DeleteSharedAsync(renamedLabelName, _cancellationToken),
            [ResourceType.Tasks],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualTaskWithoutSharedLabel = Assert.Single(syncResponse.Tasks, t => t.Id == newTask.Id);
        Assert.DoesNotContain(renamedLabelName, actualTaskWithoutSharedLabel.Labels);
    }
}