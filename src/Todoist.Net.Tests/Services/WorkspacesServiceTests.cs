namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
public class WorkspacesServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public WorkspacesServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    public async Task CreateWorkspace_Update_GetPlanDetails_Delete_Succeeds()
    {
        // Make sure we are not reaching max workspace limit.
        await _apiFixture.DeletePlaygroundWorkspaceAsync();


        // Step 1: Create workspace.
        var newWorkspace = TestData.Workspaces.AddWorkspace();
        var expectedNewWorkspace = TestData.Workspaces.ExpectedAddWorkspace();

        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Workspaces.AddAsync(newWorkspace, _cancellationToken),
            [ResourceType.Workspaces],
            cancellationToken: _cancellationToken);
        // Track the created entity for cleanup if assertions fail before deletion step, otherwise stop tracking after deletion step.
        await using var tracker = _apiFixture.TrackForCleanup(newWorkspace, c => c.Workspaces.DeleteAsync, isPremium: true);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualWorkspace = Assert.Single(syncResponse.Workspaces, w => w.Id == newWorkspace.Id);
        Assert.Equivalent(expectedNewWorkspace, actualWorkspace);


        // Step 2: Update workspace and update sidebar preference.
        var updateWorkspace = TestData.Workspaces.UpdateWorkspace(newWorkspace.Id);
        var expectedUpdatedWorkspace = TestData.Workspaces.ExpectedUpdateWorkspace(newWorkspace.Id);

        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Workspaces.UpdateAsync(updateWorkspace, _cancellationToken);
                await t.Workspaces.UpdateProjectSortPreferenceAsync(new(newWorkspace.Id, WorkspaceSortPreference.ZToA), _cancellationToken);
            },
            [ResourceType.Workspaces],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        actualWorkspace = Assert.Single(syncResponse.Workspaces, w => w.Id == newWorkspace.Id);

        Assert.Equivalent(expectedUpdatedWorkspace, actualWorkspace);
        Assert.Equal(WorkspaceSortPreference.ZToA, actualWorkspace.ProjectSortPreference);


        // Step 3: Get workspace plan details.
        long workspaceParsedId = long.Parse(newWorkspace.Id.PersistentId);

        var planDetails = await _apiFixture.Client.Workspaces.GetPlanDetailsAsync(workspaceParsedId, _cancellationToken);

        Assert.NotNull(planDetails);
        Assert.Equal(workspaceParsedId, planDetails.WorkspaceId);
        Assert.Equal(1, planDetails.CurrentMemberCount);


        // Step 4: Delete workspace.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Workspaces.DeleteAsync(newWorkspace.Id, _cancellationToken),
            [ResourceType.Workspaces],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Workspaces, w => w.Id == newWorkspace.Id && w.IsDeleted == true);

        tracker.StopTracking();
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public async Task AddFolder_UpdateFolder_DeleteFolder_Succeeds()
    {
        // Step 1: Create workspace folder
        var workspace = await _apiFixture.GetPlaygroundWorkspaceAsync();
        var folder = new WorkspaceFolder("Test Folder", 3);

        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Workspaces.AddFolderAsync(workspace.Id, folder, _cancellationToken),
            [ResourceType.WorkspaceFolders],
            cancellationToken: _cancellationToken);
        // Track the created entity for cleanup if assertions fail before deletion step, otherwise stop tracking after deletion step.
        await using var tracker = _apiFixture.TrackForCleanup(folder, c =>
        {
            return (id, ct) => c.Workspaces.DeleteFolderAsync(workspace.Id, id, ct);
        });

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualFolder = Assert.Single(syncResponse.WorkspaceFolders, f => f.Id == folder.Id);
        Assert.Equal(folder.WorkspaceId, actualFolder.WorkspaceId);
        Assert.Equal(folder.Name, actualFolder.Name);
        Assert.Equal(folder.DefaultOrder, actualFolder.DefaultOrder);


        // Step 2: Update folder.
        folder.Name = "Updated Test Folder";

        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Workspaces.UpdateFolderAsync(folder.Id, workspace.Id, folder, _cancellationToken),
            [ResourceType.WorkspaceFolders],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        actualFolder = Assert.Single(syncResponse.WorkspaceFolders, f => f.Id == folder.Id);
        Assert.Equal(folder.WorkspaceId, actualFolder.WorkspaceId);
        Assert.Equal(folder.Name, actualFolder.Name);


        // Step 4: Delete folder.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Workspaces.DeleteFolderAsync(folder.Id, workspace.Id, _cancellationToken),
            [ResourceType.WorkspaceFolders],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.WorkspaceFolders, f => f.Id == folder.Id && f.IsDeleted == true);

        tracker.StopTracking();
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public async Task UpdateLogo_DeleteLogo_Succeeds()
    {
        var workspace = await _apiFixture.GetPlaygroundWorkspaceAsync();

        // Step 1: Update workspace logo.
        var logoFile = new UploadFile(TestData.Files.GreenPng10x10, "logo.png");
        long workspaceParsedId = long.Parse(workspace.Id.PersistentId);

        await _apiFixture.Client.Workspaces.UpdateLogoAsync(workspaceParsedId, logoFile, _cancellationToken);
        var workspaceSyncResponse = await _apiFixture.Client.Workspaces.SyncAsync(cancellationToken: _cancellationToken);

        Assert.NotNull(workspaceSyncResponse);
        var actualWorkspace = Assert.Single(workspaceSyncResponse.Data, w => w.Id == workspace.Id);
        Assert.NotNull(actualWorkspace.LogoBig);


        // Step 2: Delete workspace logo.
        await _apiFixture.Client.Workspaces.DeleteLogoAsync(workspaceParsedId, _cancellationToken);
        workspaceSyncResponse = await _apiFixture.Client.Workspaces.SyncAsync(workspaceSyncResponse.SyncToken, _cancellationToken);

        Assert.NotNull(workspaceSyncResponse);
        actualWorkspace = Assert.Single(workspaceSyncResponse.Data, w => w.Id == workspace.Id);
        Assert.Null(actualWorkspace.LogoBig);
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationCollaborationTraitValue)]
    public async Task JoinWorkspace_ChangeRole_GetUsers_DeleteUser_Succeeds()
    {
        var workspace = await _apiFixture.GetPlaygroundWorkspaceAsync();
        var collaboratorInfo = await _apiFixture.GetCollaboratorUserInfoAsync();

        // Step 1: Join workspace from collaborator account.
        var joinResult = await _apiFixture.CollaborationClient.Workspaces.JoinByCodeAsync(workspace.InviteCode, _cancellationToken);

        Assert.NotNull(joinResult);
        Assert.Equal(workspace.Id, joinResult.WorkspaceId);


        // Step 2: Change user role and get users.
        var parsedWorkspaceId = long.Parse(workspace.Id.PersistentId);

        await _apiFixture.Client.Workspaces.ChangeUserRoleAsync(new(workspace.Id, collaboratorInfo.Email, WorkspaceRole.Admin), _cancellationToken);

        var usersResponse = await _apiFixture.Client.Workspaces.GetUsersAsync(new(parsedWorkspaceId), _cancellationToken);

        Assert.NotNull(usersResponse);
        Assert.Contains(usersResponse.WorkspaceUsers, u => u.UserEmail == collaboratorInfo.Email && u.Role == WorkspaceRole.Admin);


        // Step 3: Delete user from workspace and get users.
        await _apiFixture.Client.Workspaces.DeleteUserAsync(new(workspace.Id, collaboratorInfo.Email), _cancellationToken);

        usersResponse = await _apiFixture.Client.Workspaces.GetUsersAsync(new(parsedWorkspaceId), _cancellationToken);

        Assert.NotNull(usersResponse);
        Assert.DoesNotContain(usersResponse.WorkspaceUsers, u => u.UserEmail == collaboratorInfo.Email);
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationCollaborationTraitValue)]
    public async Task InviteCollaborator_GetInvitations_Succeeds()
    {
        var workspace = await _apiFixture.GetPlaygroundWorkspaceAsync();
        
        const string invitationEmail = "example@example.com";
        var parsedWorkspaceId = long.Parse(workspace.Id.PersistentId);

        // Step 1: Invite collaborator to workspace.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Workspaces.InviteUsersAsync(new(workspace.Id, [invitationEmail], WorkspaceRole.Admin), _cancellationToken),
            [ResourceType.Workspaces],
            cancellationToken: _cancellationToken);
        await using var invitationTracker = _apiFixture.TrackForCleanup(
            (client, ct) => client.Workspaces.DeleteInvitationAsync(parsedWorkspaceId, invitationEmail, ct),
            $"workspace invitation for '{invitationEmail}'");

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        workspace = Assert.Single(syncResponse.Workspaces, w => w.Id == workspace.Id);
        Assert.Contains(workspace.PendingInvitations, e => e == invitationEmail);


        // Step 2: Get invitation details.
        var invitations = await _apiFixture.Client.Workspaces.GetInvitationDetailsAsync(parsedWorkspaceId, _cancellationToken);

        Assert.NotNull(invitations);
        Assert.Contains(invitations, i => i.UserEmail == invitationEmail && i.WorkspaceId == workspace.Id && i.Role == WorkspaceRole.Admin);
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public async Task CreateProjectInWorkspaceAndArchive_GetActiveProjects_GetArchivedProjects_Succeeds()
    {
        var workspace = await _apiFixture.GetPlaygroundWorkspaceAsync();
        var parsedWorkspaceId = long.Parse(workspace.Id.PersistentId);

        var project = new AddProject($"WorkspaceMoveProject_{Guid.NewGuid():N}")
        {
            WorkspaceId = workspace.Id
        };

        // Step 1: Move project to workspace and archive.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAsync(
            async t =>
            {
                var projectId = await t.Projects.AddAsync(project, _cancellationToken);
                await t.Projects.ArchiveAsync(projectId, _cancellationToken);
            },
            _cancellationToken);
        await using var projectTracker = _apiFixture.TrackForCleanup(project, c => c.Projects.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());


        // Step 2: Get active projects in workspace.
        var projectsResponse = await _apiFixture.Client.Workspaces.GetActiveProjectsAsync(parsedWorkspaceId, cancellationToken: _cancellationToken);

        Assert.NotNull(projectsResponse);
        Assert.DoesNotContain(projectsResponse.WorkspaceProjects, p => p.Id == project.Id);


        // Step 3: Get archived projects in workspace.
        projectsResponse = await _apiFixture.Client.Workspaces.GetArchivedProjectsAsync(parsedWorkspaceId, cancellationToken: _cancellationToken);

        Assert.NotNull(projectsResponse);
        Assert.Contains(projectsResponse.WorkspaceProjects, p => p.Id == project.Id);
    }
}
