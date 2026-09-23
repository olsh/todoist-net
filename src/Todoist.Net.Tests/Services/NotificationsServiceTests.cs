namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
[Trait(Constants.TraitName, Constants.IntegrationCollaborationTraitValue)]
public class NotificationsServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public NotificationsServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    public async Task InviteCollaborator_SetLastKnown_MarkUnread_MarkRead_MarkAllRead_Succeeds()
    {
        var workspace = await _apiFixture.GetPlaygroundWorkspaceAsync();
        var collaboratorInfo = await _apiFixture.GetCollaboratorUserInfoAsync();
        long parsedWorkspaceId = long.Parse(workspace.Id.PersistentId);


        // Step 1: Invite collaborator to workspace.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Workspaces.InviteUsersAsync(new(workspace.Id, [collaboratorInfo.Email], WorkspaceRole.Admin), _cancellationToken),
            [ResourceType.Workspaces],
            cancellationToken: _cancellationToken);
        await using var invitationTracker = _apiFixture.TrackForCleanup(
            (client, ct) => client.Workspaces.DeleteInvitationAsync(parsedWorkspaceId, collaboratorInfo.Email, ct),
            $"workspace invitation for '{collaboratorInfo.Email}'");

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualWorkspace = Assert.Single(syncResponse.Workspaces, w => w.Id == workspace.Id);
        Assert.Contains(actualWorkspace.PendingInvitations, email => email == collaboratorInfo.Email);


        // Step 2: Sync collaborator live notifications and find the invitation notification.
        var notificationsSyncResponse = await _apiFixture.CollaborationClient.Notifications.SyncAsync(cancellationToken: _cancellationToken);

        var actualNotification = GetLatestWorkspaceInvitationNotification(notificationsSyncResponse.Data, parsedWorkspaceId);

        Assert.Equal(NotificationType.WorkspaceInvitationCreated, actualNotification.NotificationType);
        Assert.Equal(parsedWorkspaceId, actualNotification.WorkspaceId);
        Assert.NotNull(actualNotification.InvitationId);
        Assert.NotNull(actualNotification.InvitationSecret);


        // Step 3: Set the last known notification.
        syncResponse = await _apiFixture.CollaborationClient.ExecuteTransactionAndSyncAsync(
            t => t.Notifications.SetLastKnownAsync(actualNotification.Id, _cancellationToken),
            [ResourceType.LiveNotifications],
            notificationsSyncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Equal(long.Parse(actualNotification.Id), syncResponse.LastReadNotificationId);


        // Step 4: Mark the notification as unread.
        await _apiFixture.CollaborationClient.Notifications.MarkUnreadAsync([actualNotification.Id], _cancellationToken);

        notificationsSyncResponse = await _apiFixture.CollaborationClient.Notifications.SyncAsync(cancellationToken: _cancellationToken);

        actualNotification = Assert.Single(notificationsSyncResponse.Data, n => n.Id == actualNotification.Id);
        Assert.True(actualNotification.IsUnread);


        // Step 5: Mark the notification as read and mark all as read.
        await _apiFixture.CollaborationClient.Notifications.MarkReadAsync([actualNotification.Id], _cancellationToken);
        await _apiFixture.CollaborationClient.Notifications.MarkAllReadAsync(_cancellationToken);
    }

    private static Notification GetLatestWorkspaceInvitationNotification(
        IReadOnlyCollection<Notification> notifications,
        long workspaceId)
    {
        var matchingNotifications = notifications
            .Where(n => n.NotificationType == NotificationType.WorkspaceInvitationCreated
                && n.WorkspaceId == workspaceId
                && !n.IsDeleted)
            .OrderByDescending(n => n.CreatedAt)
            .ToList();

        Assert.NotEmpty(matchingNotifications);

        return matchingNotifications[0];
    }
}
