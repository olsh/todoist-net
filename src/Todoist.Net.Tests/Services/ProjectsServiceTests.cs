namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
public class ProjectsServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public ProjectsServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    public async Task CreateChildProject_MoveToRoot_Reorder_Delete_Succeeds_Get_ThrowsNotFound()
    {
        var parentProject = await _apiFixture.GetPlaygroundProjectAsync();

        var childProject = TestData.Projects.AddProject(parentId: parentProject.Id);
        var expectedChildProject = TestData.Projects.ExpectedAddProject(parentId: parentProject.Id);

        // Step 1: Create child project.
        var response = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Projects.AddAsync(childProject, _cancellationToken),
            [ResourceType.Projects],
            cancellationToken: _cancellationToken);
        // Track the created entity for cleanup if assertions fail before deletion step, otherwise stop tracking after deletion step.
        await using var tracker = _apiFixture.TrackForCleanup(childProject, c => c.Projects.DeleteAsync);

        Assert.All(response.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualChildProject = Assert.Single(response.Projects, p => p.Id == childProject.Id);
        Assert.Equivalent(expectedChildProject, actualChildProject);


        // Step 2: Move child project to root.
        response = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Projects.MoveAsync(new(childProject.Id, null), _cancellationToken),
            [ResourceType.Projects],
            response.SyncToken,
            _cancellationToken);

        Assert.All(response.SyncStatus.Values, cr => cr.AssertSuccess());
        actualChildProject = Assert.Single(response.Projects, p => p.Id == childProject.Id);
        Assert.Null(actualChildProject.ParentId);


        // Step 3: Reorder child and parent projects to make child come first.
        var newOrderMap = new Dictionary<ComplexId, int> { { childProject.Id, 20 } };
        response = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Projects.ReorderAsync(new(newOrderMap), _cancellationToken),
            [ResourceType.Projects],
            response.SyncToken,
            _cancellationToken);

        Assert.All(response.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(response.Projects, p => p.Id == childProject.Id && p.ChildOrder == 20);


        // Step 4: Delete child project.
        response = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Projects.DeleteAsync(childProject.Id, _cancellationToken),
            [ResourceType.Projects],
            response.SyncToken,
            _cancellationToken);

        Assert.All(response.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(response.Projects, p => p.Id == childProject.Id && p.IsDeleted == true);

        tracker.StopTracking();


        // Step 5: Attempt to get the deleted project.
        var exception = await Assert.ThrowsAsync<TodoistException>(() =>
            _apiFixture.Client.Projects.GetAsync(childProject.Id.PersistentId, _cancellationToken));

        Assert.Equal(404, exception.HttpCode);
    }

    [Fact]
    public async Task CreateProject_UpdateAndSetViewOptions_Archive_GetAndGetArchived_Unarchive_Succeeds()
    {
        // Step 0: Create project for arrangement of the test.
        var project = new AddProject($"ProjectUnderTest_{Guid.NewGuid():N}");
        await _apiFixture.Client.Projects.AddAsync(project, _cancellationToken);
        await using var projectTracker = _apiFixture.TrackForCleanup(project, c => c.Projects.DeleteAsync);

        var updateProject = TestData.Projects.UpdateProject(project.Id);
        var expectedProject = TestData.Projects.ExpectedUpdateProject(project.Id);

        var viewOptions = TestData.Projects.ViewOptionsDefaults(project.Id);
        var expectedViewOptions = TestData.Projects.ExpectedViewOptionsDefaults(project.Id);


        // Step 1: Update project and set DefaultViewOptions.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Projects.UpdateAsync(updateProject, _cancellationToken);
                await t.Projects.SetViewOptionsDefaultsAsync(viewOptions, _cancellationToken);
            },
            [ResourceType.Projects, ResourceType.ProjectViewOptionsDefaults],
            cancellationToken: _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualProject = Assert.Single(syncResponse.Projects, p => p.Id == project.Id);
        Assert.Equivalent(expectedProject, actualProject);

        var actualViewOptions = Assert.Single(syncResponse.ProjectViewOptionsDefaults, v => v.ProjectId == project.Id);
        Assert.Equivalent(expectedViewOptions, actualViewOptions);


        // Step 2: Archive project.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Projects.ArchiveAsync(project.Id, _cancellationToken),
            [ResourceType.Projects],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Projects, p => p.Id == project.Id && p.IsArchived == true);


        // Step 3: Get active and archived projects, and validate project is in archived collection.
        var projectsResponse = await _apiFixture.Client.Projects.GetAsync(cancellationToken: _cancellationToken);
        Assert.DoesNotContain(projectsResponse.Results, p => p.Id == project.Id);

        projectsResponse = await _apiFixture.Client.Projects.GetArchivedAsync(cancellationToken: _cancellationToken);
        Assert.Contains(projectsResponse.Results, p => p.Id == project.Id && p.IsArchived == true);


        // Step 4: Unarchive project.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Projects.UnarchiveAsync(project.Id, _cancellationToken),
            [ResourceType.Projects],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Projects, p => p.Id == project.Id && p.IsArchived == false);


        // Step 5: Get active and archived projects, and validate project is in active collection.
        projectsResponse = await _apiFixture.Client.Projects.GetAsync(cancellationToken: _cancellationToken);
        Assert.Contains(projectsResponse.Results, p => p.Id == project.Id && p.IsArchived == false);

        projectsResponse = await _apiFixture.Client.Projects.GetArchivedAsync(cancellationToken: _cancellationToken);
        Assert.DoesNotContain(projectsResponse.Results, p => p.Id == project.Id);
    }

    [Fact]
    public async Task CreateProject_Search_MoveToWorkspace_MoveToPersonal_Succeeds()
    {
        var workspace = await _apiFixture.GetPlaygroundWorkspaceAsync();
        var tempFolder = new WorkspaceFolder("Temp Folder");

        var newProject = TestData.Projects.AddProject("NewProject#1");
        var expectedNewProject = TestData.Projects.ExpectedAddProject("NewProject#1");


        // Step 1: Create new project.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Projects.AddAsync(newProject, _cancellationToken),
            [ResourceType.Projects],
            cancellationToken: _cancellationToken);
        // Track the created entity for cleanup even if assertions fail.
        await using var tracker = _apiFixture.TrackForCleanup(newProject, c => c.Projects.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualNewProject = Assert.Single(syncResponse.Projects, p => p.Id == newProject.Id);
        Assert.Equivalent(expectedNewProject, actualNewProject);


        // Step 2: Search for the project by part of it's name.
        var searchResponse = await _apiFixture.Client.Projects.SearchAsync(new("New*1"), cancellationToken: _cancellationToken);

        Assert.Contains(searchResponse.Results, p => p.Id == newProject.Id);


        // Step 3: Create folder and move project to the workspace.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                var folderId = await t.Workspaces.AddFolderAsync(workspace.Id, tempFolder, _cancellationToken);
                await t.Projects.MoveToWorkspaceAsync(new(newProject.Id, workspace.Id, folderId), _cancellationToken);
            },
            [ResourceType.Projects],
            cancellationToken: _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Projects, p => p.Id == newProject.Id && p.WorkspaceId == workspace.Id && p.FolderId == tempFolder.Id);


        // Step 4: Move project out of workspace.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Projects.MoveToPersonalAsync(newProject.Id, _cancellationToken),
            [ResourceType.Projects],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Projects, p => p.Id == newProject.Id && p.WorkspaceId == null && p.FolderId == null);
    }
}
