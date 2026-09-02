namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
[Trait(Constants.TraitName, Constants.IntegrationCollaborationTraitValue)]
public class SharingServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public SharingServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    public async Task ShareProject_DeleteInvitation_Succeeds()
    {
        var collaboratorInfo = await _apiFixture.GetCollaboratorUserInfoAsync();

        var newProject = new AddProject($"SharedProject_{Guid.NewGuid():N}");


        // Step 1: Create project.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Projects.AddAsync(newProject, _cancellationToken),
            [ResourceType.Projects],
            cancellationToken: _cancellationToken);
        await using var projectTracker = _apiFixture.TrackForCleanup(newProject, c => c.Projects.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualProject = Assert.Single(syncResponse.Projects, p => p.Id == newProject.Id);
        Assert.Equal(newProject.Name, actualProject.Name);


        // Step 2: Share project with collaborator.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Sharing.ShareProjectAsync(newProject.Id, collaboratorInfo.Email, ProjectCollaboratorRole.ReadWrite, _cancellationToken),
            [ResourceType.Collaborators],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Collaborators, c => c.Id == collaboratorInfo.Id && c.Email == collaboratorInfo.Email);
        Assert.Contains(syncResponse.CollaboratorStates, s =>
            s.ProjectId == newProject.Id.PersistentId
            && s.UserId == collaboratorInfo.Id
            && s.State == CollaboratorStatus.Invited.ToString()
            && s.IsDeleted == false);


        // Step 3: Get invitation details from collaborator live notifications.
        var invitationNotification = await GetLatestShareInvitationNotificationAsync(newProject.Name);

        Assert.Equal(NotificationType.ShareInvitationSent, invitationNotification.NotificationType);
        Assert.Equal(newProject.Name, invitationNotification.ProjectName);
        Assert.NotNull(invitationNotification.InvitationId);
        Assert.NotNull(invitationNotification.InvitationSecret);


        // Step 4: Delete invitation.
        await _apiFixture.Client.Sharing.DeleteInvitationAsync(invitationNotification.InvitationId, _cancellationToken);

        var collaboratorsResponse = await _apiFixture.Client.Projects.GetCollaboratorsAsync(
            newProject.Id.PersistentId,
            cancellationToken: _cancellationToken);

        Assert.DoesNotContain(collaboratorsResponse.Results, c => c.Email == collaboratorInfo.Email);
    }

    [Fact]
    public async Task ShareProject_AcceptInvitation_DeleteCollaborator_RejectInvitation_Succeeds()
    {
        var collaboratorInfo = await _apiFixture.GetCollaboratorUserInfoAsync();

        var newProject = new AddProject($"SharedProject_{Guid.NewGuid():N}");


        // Step 1: Create project.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Projects.AddAsync(newProject, _cancellationToken),
            [ResourceType.Projects],
            cancellationToken: _cancellationToken);
        await using var projectTracker = _apiFixture.TrackForCleanup(newProject, c => c.Projects.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualProject = Assert.Single(syncResponse.Projects, p => p.Id == newProject.Id);
        Assert.Equal(newProject.Name, actualProject.Name);


        // Step 2: Share project and verify invited collaborator state.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Sharing.ShareProjectAsync(newProject.Id, collaboratorInfo.Email, ProjectCollaboratorRole.ReadWrite, _cancellationToken),
            [ResourceType.Collaborators],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Collaborators, c => c.Id == collaboratorInfo.Id && c.Email == collaboratorInfo.Email);
        Assert.Contains(syncResponse.CollaboratorStates, s =>
            s.ProjectId == newProject.Id.PersistentId
            && s.UserId == collaboratorInfo.Id
            && s.State == CollaboratorStatus.Invited.ToString()
            && s.IsDeleted == false);


        // Step 3: Accept invitation on collaborator account.
        var invitationNotification = await GetLatestShareInvitationNotificationAsync(newProject.Name);

        var collaboratorSyncResponse = await _apiFixture.CollaborationClient.ExecuteTransactionAndSyncAsync(
            t => t.Sharing.AcceptInvitationAsync(invitationNotification.InvitationId, invitationNotification.InvitationSecret, _cancellationToken),
            [ResourceType.Collaborators],
            cancellationToken: _cancellationToken);

        Assert.All(collaboratorSyncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        Assert.Contains(collaboratorSyncResponse.CollaboratorStates, s =>
            s.ProjectId == newProject.Id.PersistentId
            && s.UserId == collaboratorInfo.Id
            && s.State == CollaboratorStatus.Active.ToString()
            && s.IsDeleted == false);


        // Step 4: Delete collaborator from shared project.
        await _apiFixture.Client.Sharing.DeleteCollaboratorAsync(newProject.Id, collaboratorInfo.Email, _cancellationToken);

        var collaboratorsResponse = await _apiFixture.Client.Projects.GetCollaboratorsAsync(
            newProject.Id.PersistentId,
            cancellationToken: _cancellationToken);

        Assert.DoesNotContain(collaboratorsResponse.Results, c => c.Email == collaboratorInfo.Email);


        // Step 5: Share project again and verify invited collaborator state.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Sharing.ShareProjectAsync(newProject.Id, collaboratorInfo.Email, ProjectCollaboratorRole.ReadWrite, _cancellationToken),
            [ResourceType.Collaborators],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.CollaboratorStates, s =>
            s.ProjectId == newProject.Id.PersistentId
            && s.UserId == collaboratorInfo.Id
            && s.State == CollaboratorStatus.Invited.ToString()
            && s.IsDeleted == false);


        // Step 6: Reject the new invitation on collaborator account.
        invitationNotification = await GetLatestShareInvitationNotificationAsync(newProject.Name);

        await _apiFixture.CollaborationClient.Sharing.RejectInvitationAsync(
            invitationNotification.InvitationId,
            invitationNotification.InvitationSecret,
            _cancellationToken);

        collaboratorsResponse = await _apiFixture.Client.Projects.GetCollaboratorsAsync(
            newProject.Id.PersistentId,
            cancellationToken: _cancellationToken);

        Assert.DoesNotContain(collaboratorsResponse.Results, c => c.Email == collaboratorInfo.Email);
    }

    private async Task<Notification> GetLatestShareInvitationNotificationAsync(string projectName)
    {
        List<Notification> matchingNotifications = [];

        for (int attempt = 0; attempt < 5; attempt++)
        {
            var notificationsSyncResponse = await _apiFixture.CollaborationClient.Notifications.SyncAsync(cancellationToken: _cancellationToken);

            matchingNotifications = notificationsSyncResponse.Data
                .Where(n => n.NotificationType == NotificationType.ShareInvitationSent
                    && n.ProjectName == projectName
                    && !n.IsDeleted)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            if (matchingNotifications.Count > 0)
            {
                return matchingNotifications[0];
            }

            await Task.Delay(TimeSpan.FromSeconds(1), _cancellationToken);
        }

        Assert.NotEmpty(matchingNotifications);
        return matchingNotifications[0];
    }
}