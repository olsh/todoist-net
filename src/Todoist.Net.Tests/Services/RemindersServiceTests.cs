namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
[Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
public class RemindersServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public RemindersServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    public async Task CreateAbsoluteReminder_Update_Delete_Succeeds()
    {
        var newTask = new AddTask($"ReminderTask_{Guid.NewGuid():N}")
        {
            Id = Guid.NewGuid()
        };
        var absoluteReminderDueDate = DateTime.UtcNow.AddDays(2).AddHours(3);
        var updatedReminderDueDate = absoluteReminderDueDate.AddHours(5);

        var newReminder = TestData.Reminders.AddAbsoluteReminder(newTask.Id, absoluteReminderDueDate);


        // Step 1: Create task and absolute reminder.
        var syncResponse = await _apiFixture.PremiumClient.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Tasks.AddAsync(newTask, _cancellationToken);
                await t.Reminders.AddAsync(newReminder, _cancellationToken);
            },
            [ResourceType.Reminders],
            cancellationToken: _cancellationToken);
        await using var taskTracker = _apiFixture.TrackForCleanup(newTask, c => c.Tasks.DeleteAsync, isPremium: true);
        await using var reminderTracker = _apiFixture.TrackForCleanup(newReminder, c => c.Reminders.DeleteAsync, isPremium: true);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualReminder = Assert.Single(syncResponse.Reminders, r => r.Id == newReminder.Id);
        Assert.Equal(newTask.Id, actualReminder.TaskId);
        Assert.Equal(ReminderType.Absolute, actualReminder.Type);
        Assert.NotNull(actualReminder.DueDate);
        Assert.Equal(TruncateToSecond(absoluteReminderDueDate), actualReminder.DueDate.Date);
        Assert.False(actualReminder.IsDeleted ?? true);


        // Step 2: Update reminder due date.
        actualReminder.DueDate = DueDate.CreateFloating(updatedReminderDueDate);

        syncResponse = await _apiFixture.PremiumClient.ExecuteTransactionAndSyncAsync(
            t => t.Reminders.UpdateAsync(actualReminder, _cancellationToken),
            [ResourceType.Reminders],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        actualReminder = Assert.Single(syncResponse.Reminders, r => r.Id == newReminder.Id);
        Assert.Equal(newTask.Id, actualReminder.TaskId);
        Assert.Equal(ReminderType.Absolute, actualReminder.Type);
        Assert.NotNull(actualReminder.DueDate);
        Assert.Equal(TruncateToSecond(updatedReminderDueDate), actualReminder.DueDate.Date);


        // Step 3: Delete reminder.
        syncResponse = await _apiFixture.PremiumClient.ExecuteTransactionAndSyncAsync(
            t => t.Reminders.DeleteAsync(newReminder.Id, _cancellationToken),
            [ResourceType.Reminders],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Reminders, r => r.Id == newReminder.Id && r.IsDeleted == true);

        reminderTracker.StopTracking();
    }

    [Fact]
    public async Task CreateLocationReminder_ClearLocations_Succeeds()
    {
        var newTask = new AddTask($"LocationReminderTask_{Guid.NewGuid():N}")
        {
            Id = Guid.NewGuid()
        };
        var locationName = $"ReminderLocation_{Guid.NewGuid():N}";
        const string locationLatitude = "41.148581";
        const string locationLongitude = "-8.610945000000015";

        var newReminder = TestData.Reminders.AddLocationReminder(newTask.Id, locationName, locationLatitude, locationLongitude);


        // Step 1: Create task and location reminder.
        var syncResponse = await _apiFixture.PremiumClient.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Tasks.AddAsync(newTask, _cancellationToken);
                await t.Reminders.AddAsync(newReminder, _cancellationToken);
            },
            [ResourceType.Reminders, ResourceType.RemindersLocation, ResourceType.Locations],
            cancellationToken: _cancellationToken);
        await using var taskTracker = _apiFixture.TrackForCleanup(newTask, c => c.Tasks.DeleteAsync, isPremium: true);
        await using var reminderTracker = _apiFixture.TrackForCleanup(newReminder, c => c.Reminders.DeleteAsync, isPremium: true);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var reminderCollection = syncResponse.RemindersLocation ?? syncResponse.Reminders;
        var actualReminder = Assert.Single(reminderCollection, r => r.Id == newReminder.Id);
        Assert.Equal(newTask.Id, actualReminder.TaskId);
        Assert.Equal(ReminderType.Location, actualReminder.Type);
        Assert.Equal(locationName, actualReminder.Name);
        Assert.Equal(locationLatitude, actualReminder.LocationLatitude);
        Assert.Equal(locationLongitude, actualReminder.LocationLongitude);
        Assert.Equal(LocationTrigger.OnEnter, actualReminder.LocationTrigger);
        Assert.Equal(100, actualReminder.Radius);

        var actualLocation = Assert.Single(syncResponse.Locations, l =>
            l.Length >= 3
            && l[0] == locationName
            && l[1] == locationLatitude
            && l[2] == locationLongitude);

        Assert.Equal(locationName, actualLocation[0]);
        Assert.Equal(locationLatitude, actualLocation[1]);
        Assert.Equal(locationLongitude, actualLocation[2]);


        // Step 2: Clear saved reminder locations.
        syncResponse = await _apiFixture.PremiumClient.ExecuteTransactionAndSyncAsync(
            t => t.Reminders.ClearLocationsAsync(_cancellationToken),
            [ResourceType.Locations],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Empty(syncResponse.Locations);
    }

    private static DateTime TruncateToSecond(DateTime value)
    {
        var unspecifiedValue = DateTime.SpecifyKind(value, DateTimeKind.Unspecified);
        return unspecifiedValue.AddTicks(-(unspecifiedValue.Ticks % TimeSpan.TicksPerSecond));
    }
}