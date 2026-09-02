namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
public class UserServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public UserServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    public async Task UpdateUser_GetInfo_Succeeds()
    {
        // Step 1: Get current user info and register restore cleanup.
        var originalUserInfo = await _apiFixture.Client.User.GetInfoAsync(_cancellationToken);

        var updatedStartPage = originalUserInfo.StartPage == "today"
            ? "inbox"
            : "today";
        var updatedDateFormat = originalUserInfo.DateFormat == DateFormat.DdMmYyyy
            ? DateFormat.MmDdYyyy
            : DateFormat.DdMmYyyy;

        await using var userInfoTracker = _apiFixture.TrackForCleanup(
            (client, ct) => client.User.UpdateAsync(new UpdateUser
            {
                StartPage = originalUserInfo.StartPage,
                DateFormat = originalUserInfo.DateFormat
            }, ct),
            "current user info");


        // Step 2: Update a small set of safe user properties.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.User.UpdateAsync(new UpdateUser
            {
                StartPage = updatedStartPage,
                DateFormat = updatedDateFormat
            }, _cancellationToken),
            [ResourceType.User],
            cancellationToken: _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualUserInfo = syncResponse.UserInfo;
        Assert.NotNull(actualUserInfo);
        Assert.Equal(originalUserInfo.Id, actualUserInfo.Id);
        Assert.Equal(updatedStartPage, actualUserInfo.StartPage);
        Assert.Equal(updatedDateFormat, actualUserInfo.DateFormat);


        // Step 3: Get current user info and validate the updated values.
        actualUserInfo = await _apiFixture.Client.User.GetInfoAsync(_cancellationToken);

        Assert.Equal(originalUserInfo.Id, actualUserInfo.Id);
        Assert.Equal(updatedStartPage, actualUserInfo.StartPage);
        Assert.Equal(updatedDateFormat, actualUserInfo.DateFormat);
    }

    [Fact]
    public async Task UpdateKarmaGoals_GetStats_Succeeds()
    {
        // Step 1: Get current productivity stats and register restore cleanup.
        var originalStats = await _apiFixture.Client.User.GetStatsAsync(_cancellationToken);

        var updatedDailyGoal = originalStats.Goals.DailyGoal + 1;
        var updatedWeeklyGoal = Math.Max(originalStats.Goals.WeeklyGoal + 1, updatedDailyGoal + 1);

        await using var karmaGoalsTracker = _apiFixture.TrackForCleanup(
            (client, ct) => client.User.UpdateKarmaGoalsAsync(new UpdateKarmaGoals
            {
                DailyGoal = originalStats.Goals.DailyGoal,
                WeeklyGoal = originalStats.Goals.WeeklyGoal
            }, ct),
            "current user karma goals");


        // Step 2: Update karma goals.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.User.UpdateKarmaGoalsAsync(new UpdateKarmaGoals
            {
                DailyGoal = updatedDailyGoal,
                WeeklyGoal = updatedWeeklyGoal
            }, _cancellationToken),
            [ResourceType.User],
            cancellationToken: _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualUserInfo = syncResponse.UserInfo;
        Assert.NotNull(actualUserInfo);
        Assert.Equal(updatedDailyGoal, actualUserInfo.DailyGoal);
        Assert.Equal(updatedWeeklyGoal, actualUserInfo.WeeklyGoal);


        // Step 3: Get current productivity stats and validate updated goals.
        var actualStats = await _apiFixture.Client.User.GetStatsAsync(_cancellationToken);

        Assert.Equal(updatedDailyGoal, actualStats.Goals.DailyGoal);
        Assert.Equal(updatedWeeklyGoal, actualStats.Goals.WeeklyGoal);
        Assert.True(actualStats.CompletedCount >= 0);
    }

    [Fact]
    public async Task UpdateUserSettings_UpdateNotificationSetting_Succeeds()
    {
        // Step 1: Get current user settings and notification settings, then register restore cleanup.
        var settingsAndNotificationsSyncResponse = await _apiFixture.Client.SyncResourcesAsync(
            [ResourceType.UserSettings, ResourceType.NotificationSettings],
            cancellationToken: _cancellationToken);

        var originalUserSettings = settingsAndNotificationsSyncResponse.UserSettings;
        var originalNotificationSettings = settingsAndNotificationsSyncResponse.NotificationSettings;
        Assert.NotNull(originalUserSettings);
        Assert.NotNull(originalNotificationSettings);
        Assert.NotEmpty(originalNotificationSettings);

        var notificationSettingEntry = originalNotificationSettings.FirstOrDefault(
            kvp => kvp.Key == NotificationType.ItemAssigned, 
            originalNotificationSettings.First());

        var notificationType = notificationSettingEntry.Key;
        var originalNotificationSetting = notificationSettingEntry.Value;

        var updatedCompletedSoundDesktop = !originalUserSettings.CompletedSoundDesktop;
        var updatedNotifyEmail = !originalNotificationSetting.NotifyEmail;

        await using var userSettingsTracker = _apiFixture.TrackForCleanup(
            (client, ct) => client.User.UpdateSettingsAsync(new UpdateUserSettings
            {
                CompletedSoundDesktop = originalUserSettings.CompletedSoundDesktop
            }, ct),
            "current user settings");
        await using var notificationSettingTracker = _apiFixture.TrackForCleanup(
            (client, ct) => client.User.UpdateNotificationSettingAsync(new NotificationSettingUpdate
            {
                NotificationType = notificationType,
                Service = NotificationService.Email,
                DontNotify = !originalNotificationSetting.NotifyEmail
            }, ct),
            $"notification setting '{notificationType}'");


        // Step 2: Update user settings.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.User.UpdateSettingsAsync(new UpdateUserSettings
            {
                CompletedSoundDesktop = updatedCompletedSoundDesktop
            }, _cancellationToken),
            [ResourceType.UserSettings],
            settingsAndNotificationsSyncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualUserSettings = syncResponse.UserSettings;
        Assert.NotNull(actualUserSettings);
        Assert.Equal(updatedCompletedSoundDesktop, actualUserSettings.CompletedSoundDesktop);


        // Step 3: Update a notification setting and sync the changed notification settings.
        await _apiFixture.Client.User.UpdateNotificationSettingAsync(new NotificationSettingUpdate
        {
            NotificationType = notificationType,
            Service = NotificationService.Email,
            DontNotify = originalNotificationSetting.NotifyEmail
        }, _cancellationToken);

        var notificationSettingsSyncResponse = await _apiFixture.Client.SyncResourcesAsync(
            [ResourceType.NotificationSettings],
            syncResponse.SyncToken,
            _cancellationToken);

        var actualNotificationSettingEntries = notificationSettingsSyncResponse.NotificationSettings
            .Where(kvp => kvp.Key == notificationType)
            .ToList();

        Assert.Single(actualNotificationSettingEntries);
        var actualNotificationSetting = actualNotificationSettingEntries[0].Value;

        Assert.Equal(updatedNotifyEmail, actualNotificationSetting.NotifyEmail);
        Assert.Equal(originalNotificationSetting.NotifyPush, actualNotificationSetting.NotifyPush);
    }
}