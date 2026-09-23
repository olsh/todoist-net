namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
public class UtilityServicesTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public UtilityServicesTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
    public async Task CreateProject_GetOrCreateEmail_Disable_Succeeds()
    {
        var newProject = TestData.Projects.AddProject($"UtilityEmailProject_{Guid.NewGuid():N}");


        // Step 1: Create project.
        await _apiFixture.PremiumClient.Projects.AddAsync(newProject, _cancellationToken);
        await using var projectTracker = _apiFixture.TrackForCleanup(newProject, c => c.Projects.DeleteAsync, isPremium: true);


        // Step 2: Get or create project email.
        var actualEmail = await _apiFixture.PremiumClient.Emails.GetOrCreateAsync(
            EmailObjectType.Project,
            newProject.Id.PersistentId,
            _cancellationToken);

        Assert.NotNull(actualEmail);
        Assert.False(string.IsNullOrWhiteSpace(actualEmail.Email));


        // Step 3: Disable project email.
        await _apiFixture.PremiumClient.Emails.DisableAsync(
            EmailObjectType.Project,
            newProject.Id.PersistentId,
            _cancellationToken);
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
    public async Task CreateTask_GetOrCreateEmail_Disable_Succeeds()
    {
        var newProject = TestData.Projects.AddProject($"UtilityEmailTaskProject_{Guid.NewGuid():N}");
        var newTask = TestData.Tasks.AddTask(newProject.Id, $"UtilityEmailTask_{Guid.NewGuid():N}");


        // Step 1: Create project and task.
        await _apiFixture.PremiumClient.Projects.AddAsync(newProject, _cancellationToken);
        await using var projectTracker = _apiFixture.TrackForCleanup(newProject, c => c.Projects.DeleteAsync, isPremium: true);

        await _apiFixture.PremiumClient.Tasks.AddAsync(newTask, _cancellationToken);
        await using var taskTracker = _apiFixture.TrackForCleanup(newTask, c => c.Tasks.DeleteAsync, isPremium: true);


        // Step 2: Get or create task email. The API only accepts the "task" object type here,
        // the legacy "item" value is rejected.
        var actualEmail = await _apiFixture.PremiumClient.Emails.GetOrCreateAsync(
            EmailObjectType.Task,
            newTask.Id.PersistentId,
            _cancellationToken);

        Assert.NotNull(actualEmail);
        Assert.False(string.IsNullOrWhiteSpace(actualEmail.Email));


        // Step 3: Disable task email.
        await _apiFixture.PremiumClient.Emails.DisableAsync(
            EmailObjectType.Task,
            newTask.Id.PersistentId,
            _cancellationToken);
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
    public async Task CreateTask_GetIdMappings_Succeeds()
    {
        var newProject = TestData.Projects.AddProject($"UtilityActivityProject_{Guid.NewGuid():N}");
        var newTask = TestData.Tasks.AddTask(newProject.Id, $"UtilityTask_{Guid.NewGuid():N}");


        // Step 1: Create project and task.
        await _apiFixture.PremiumClient.Projects.AddAsync(newProject, _cancellationToken);
        await using var projectTracker = _apiFixture.TrackForCleanup(newProject, c => c.Projects.DeleteAsync, isPremium: true);

        await _apiFixture.PremiumClient.Tasks.AddAsync(newTask, _cancellationToken);
        await using var taskTracker = _apiFixture.TrackForCleanup(newTask, c => c.Tasks.DeleteAsync, isPremium: true);


        // Step 2: Update task to generate additional activity.
        var updateTask = TestData.Tasks.UpdateTask(newTask.Id, $"UtilityTaskUpdated_{Guid.NewGuid():N}");

        await _apiFixture.PremiumClient.Tasks.UpdateAsync(updateTask, _cancellationToken);


        // Step 3: Get ID mappings for the created task.
        var actualMappings = await _apiFixture.PremiumClient.Ids.GetMappingsAsync(
            MappingObjectName.Tasks,
            [newTask.Id.PersistentId],
            _cancellationToken);

        var actualMapping = Assert.Single(actualMappings);
        Assert.True(
            actualMapping.OldId == newTask.Id.PersistentId || actualMapping.NewId == newTask.Id.PersistentId,
            $"Expected one side of the ID mapping to match task ID '{newTask.Id.PersistentId}', but got old_id='{actualMapping.OldId}' and new_id='{actualMapping.NewId}'.");
    }

    [Fact]
    [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
    public async Task GetActivityLogs_Succeeds()
    {
        // Step 1: Get recent activity logs.
        var activityLogs = await _apiFixture.PremiumClient.Activity.GetAsync(
            new LogsPaginationQuery
            {
                Limit = 100,
                AnnotateParents = true,
            },
            _cancellationToken);

        Assert.NotNull(activityLogs);
        Assert.NotEmpty(activityLogs.Results);

        Assert.Contains(
            activityLogs.Results,
            l => !string.IsNullOrWhiteSpace(l.ObjectId)
                && !string.IsNullOrWhiteSpace(l.ObjectType.ToString())
                && !string.IsNullOrWhiteSpace(l.EventType.ToString()));

        var actualActivityLog = activityLogs.Results.First(
            l => !string.IsNullOrWhiteSpace(l.ObjectId)
                && !string.IsNullOrWhiteSpace(l.ObjectType.ToString())
                && !string.IsNullOrWhiteSpace(l.EventType.ToString()));

        Assert.NotEqual(default, actualActivityLog.EventDate);
    }
}
