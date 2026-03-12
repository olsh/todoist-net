namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
public class SectionsServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public SectionsServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public async Task CreateSections_UpdateAndReorder_GetById_GetAndSearch_Delete_Succeeds()
    {
        var project = await _apiFixture.GetPlaygroundProjectAsync();

        var newSection = TestData.Sections.AddSection(project.Id, $"NewSection_{Guid.NewGuid():N}", 10);
        var expectedNewSection = TestData.Sections.ExpectedAddSection(project.Id, newSection.Name, newSection.SectionOrder);
        var siblingSection = TestData.Sections.AddSection(project.Id, $"SiblingSection_{Guid.NewGuid():N}", 20);
        var expectedSiblingSection = TestData.Sections.ExpectedAddSection(project.Id, siblingSection.Name, siblingSection.SectionOrder);


        // Step 1: Create sections.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Sections.AddAsync(newSection, _cancellationToken);
                await t.Sections.AddAsync(siblingSection, _cancellationToken);
            },
            [ResourceType.Sections],
            cancellationToken: _cancellationToken);
        await using var newSectionTracker = _apiFixture.TrackForCleanup(newSection, c => c.Sections.DeleteAsync);
        await using var siblingSectionTracker = _apiFixture.TrackForCleanup(siblingSection, c => c.Sections.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualNewSection = Assert.Single(syncResponse.Sections, s => s.Id == newSection.Id);
        Assert.Equivalent(expectedNewSection, actualNewSection);

        var actualSiblingSection = Assert.Single(syncResponse.Sections, s => s.Id == siblingSection.Id);
        Assert.Equivalent(expectedSiblingSection, actualSiblingSection);


        // Step 2: Update one section and reorder both sections.
        var updateSection = TestData.Sections.UpdateSection(newSection.Id, $"UpdatedSection_{Guid.NewGuid():N}", isCollapsed: true);
        var expectedUpdatedSection = TestData.Sections.ExpectedUpdateSection(newSection.Id, updateSection.Name, updateSection.IsCollapsed);

        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Sections.UpdateAsync(updateSection, _cancellationToken);
                await t.Sections.ReorderAsync(new(new Dictionary<ComplexId, int>
                {
                    { newSection.Id, 30 },
                    { siblingSection.Id, 10 }
                }), _cancellationToken);
            },
            [ResourceType.Sections],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        actualNewSection = Assert.Single(syncResponse.Sections, s => s.Id == newSection.Id);
        Assert.Equivalent(expectedUpdatedSection, actualNewSection);
        Assert.Equal(30, actualNewSection.SectionOrder);

        actualSiblingSection = Assert.Single(syncResponse.Sections, s => s.Id == siblingSection.Id);
        Assert.Equal(10, actualSiblingSection.SectionOrder);


        // Step 3: Get section by id.
        var actualSection = await _apiFixture.Client.Sections.GetAsync(newSection.Id.PersistentId, _cancellationToken);

        Assert.Equivalent(expectedUpdatedSection, actualSection);
        Assert.Equal(project.Id, actualSection.ProjectId);
        Assert.Equal(30, actualSection.SectionOrder);


        // Step 4: Get project sections and search for the updated section.
        var sectionsResponse = await _apiFixture.Client.Sections.GetAsync(
            new SectionsPaginationQuery(project.Id.PersistentId),
            _cancellationToken);

        Assert.Contains(sectionsResponse.Results, s => s.Id == newSection.Id && s.Name == updateSection.Name);
        Assert.Contains(sectionsResponse.Results, s => s.Id == siblingSection.Id);

        var searchResponse = await _apiFixture.Client.Sections.SearchAsync(
            new SectionsSearchQuery("UpdatedSection*", project.Id.PersistentId),
            _cancellationToken);

        Assert.Contains(searchResponse.Results, s => s.Id == newSection.Id && s.Name == updateSection.Name);
        Assert.DoesNotContain(searchResponse.Results, s => s.Id == siblingSection.Id);


        // Step 5: Delete sections.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Sections.DeleteAsync(newSection.Id, _cancellationToken);
                await t.Sections.DeleteAsync(siblingSection.Id, _cancellationToken);
            },
            [ResourceType.Sections],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Sections, s => s.Id == newSection.Id && s.IsDeleted == true);
        Assert.Contains(syncResponse.Sections, s => s.Id == siblingSection.Id && s.IsDeleted == true);

        newSectionTracker.StopTracking();
        siblingSectionTracker.StopTracking();
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public async Task CreateSection_MoveToInbox_Archive_Unarchive_Succeeds()
    {
        var project = await _apiFixture.GetPlaygroundProjectAsync();
        var mainUserInfo = await _apiFixture.GetMainUserInfoAsync();
        var inboxProjectId = new ComplexId(mainUserInfo.InboxProjectId);

        var newSection = TestData.Sections.AddSection(project.Id, $"InboxMoveSection_{Guid.NewGuid():N}", 10);
        var expectedNewSection = TestData.Sections.ExpectedAddSection(project.Id, newSection.Name, newSection.SectionOrder);


        // Step 1: Create section.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Sections.AddAsync(newSection, _cancellationToken),
            [ResourceType.Sections],
            cancellationToken: _cancellationToken);
        await using var sectionTracker = _apiFixture.TrackForCleanup(newSection, c => c.Sections.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualSection = Assert.Single(syncResponse.Sections, s => s.Id == newSection.Id);
        Assert.Equivalent(expectedNewSection, actualSection);


        // Step 2: Move section to Inbox.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Sections.MoveAsync(new(newSection.Id, inboxProjectId), _cancellationToken),
            [ResourceType.Sections],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        actualSection = Assert.Single(syncResponse.Sections, s => s.Id == newSection.Id);
        Assert.Equal(inboxProjectId, actualSection.ProjectId);


        // Step 3: Archive section.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Sections.ArchiveAsync(newSection.Id, _cancellationToken),
            [ResourceType.Sections],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        actualSection = Assert.Single(syncResponse.Sections, s => s.Id == newSection.Id);
        Assert.True(actualSection.IsArchived);
        Assert.NotNull(actualSection.ArchivedAt);


        // Step 4: Unarchive section.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Sections.UnarchiveAsync(newSection.Id, _cancellationToken),
            [ResourceType.Sections],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        actualSection = Assert.Single(syncResponse.Sections, s => s.Id == newSection.Id);
        Assert.False(actualSection.IsArchived);
    }
}