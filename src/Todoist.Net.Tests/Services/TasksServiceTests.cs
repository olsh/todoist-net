namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
public class TasksServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public TasksServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    public async Task CreateTask_GetByQuery_UpdateToRecurring_CompleteRecurring_Delete_Succeeds()
    {
        var project = await _apiFixture.GetPlaygroundProjectAsync();

        var newTask = TestData.Tasks.AddTask(project.Id, $"RecurringTask_{Guid.NewGuid():N}");
        var expectedNewTask = TestData.Tasks.ExpectedAddTask(project.Id, newTask.Content);


        // Step 1: Create task.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Tasks.AddAsync(newTask, _cancellationToken),
            [ResourceType.Tasks],
            cancellationToken: _cancellationToken);
        await using var taskTracker = _apiFixture.TrackForCleanup(newTask, c => c.Tasks.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualNewTask = Assert.Single(syncResponse.Tasks, t => t.Id == newTask.Id);
        Assert.Equivalent(expectedNewTask, actualNewTask);


        // Step 2: Get task by query.
        var tasksResponse = await _apiFixture.Client.Tasks.GetAsync(
            new TasksPaginationQuery
            {
                ProjectId = project.Id.PersistentId,
                Ids = [newTask.Id.PersistentId]
            },
            _cancellationToken);

        actualNewTask = Assert.Single(tasksResponse.Results, t => t.Id == newTask.Id);
        Assert.Equal(newTask.Content, actualNewTask.Content);


        // Step 3: Update task to recurring.
        var updateTask = TestData.Tasks.UpdateTask(newTask.Id, $"RecurringTaskUpdated_{Guid.NewGuid():N}");
        var expectedUpdatedTask = TestData.Tasks.ExpectedUpdateTask(newTask.Id, updateTask.Content);

        updateTask.DueDate = DueDate.FromText("every day", Language.English);

        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Tasks.UpdateAsync(updateTask, _cancellationToken),
            [ResourceType.Tasks],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualUpdatedTask = Assert.Single(syncResponse.Tasks, t => t.Id == newTask.Id);
        Assert.Equivalent(expectedUpdatedTask, actualUpdatedTask);
        Assert.NotNull(actualUpdatedTask.DueDate);
        Assert.True(actualUpdatedTask.DueDate.IsRecurring);
        Assert.NotNull(actualUpdatedTask.DueDate.Date);

        var previousRecurringDate = actualUpdatedTask.DueDate.Date.Value;


        // Step 4: Complete recurring task.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Tasks.CompleteRecurringAsync(new(newTask.Id), _cancellationToken),
            [ResourceType.Tasks],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualRecurringTask = Assert.Single(syncResponse.Tasks, t => t.Id == newTask.Id);
        Assert.NotNull(actualRecurringTask.DueDate);
        Assert.True(actualRecurringTask.DueDate.IsRecurring);
        Assert.NotNull(actualRecurringTask.DueDate.Date);
        Assert.True(actualRecurringTask.DueDate.Date.Value > previousRecurringDate);
        Assert.False(actualRecurringTask.IsChecked ?? true);
        Assert.False(actualRecurringTask.IsDeleted ?? true);


        // Step 5: Delete task.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Tasks.DeleteAsync(newTask.Id, _cancellationToken),
            [ResourceType.Tasks],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Tasks, t => t.Id == newTask.Id && t.IsDeleted == true);

        taskTracker.StopTracking();
    }

    [Fact]
    public async Task CreateSiblingTasks_MoveToParent_Reorder_DeleteHierarchy_Succeeds()
    {
        var project = await _apiFixture.GetPlaygroundProjectAsync();

        var parentTask = TestData.Tasks.AddTask(project.Id, $"ParentTask_{Guid.NewGuid():N}");
        var firstSiblingTask = TestData.Tasks.AddTask(project.Id, $"FirstSiblingTask_{Guid.NewGuid():N}");
        var secondSiblingTask = TestData.Tasks.AddTask(project.Id, $"SecondSiblingTask_{Guid.NewGuid():N}");


        // Step 1: Create parent and sibling tasks.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Tasks.AddAsync(parentTask, _cancellationToken);
                await t.Tasks.AddAsync(firstSiblingTask, _cancellationToken);
                await t.Tasks.AddAsync(secondSiblingTask, _cancellationToken);
            },
            [ResourceType.Tasks],
            cancellationToken: _cancellationToken);
        await using var parentTaskTracker = _apiFixture.TrackForCleanup(parentTask, c => c.Tasks.DeleteAsync);
        await using var firstSiblingTaskTracker = _apiFixture.TrackForCleanup(firstSiblingTask, c => c.Tasks.DeleteAsync);
        await using var secondSiblingTaskTracker = _apiFixture.TrackForCleanup(secondSiblingTask, c => c.Tasks.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Tasks, t => t.Id == parentTask.Id && t.ProjectId == project.Id.PersistentId);
        Assert.Contains(syncResponse.Tasks, t => t.Id == firstSiblingTask.Id && t.ProjectId == project.Id.PersistentId);
        Assert.Contains(syncResponse.Tasks, t => t.Id == secondSiblingTask.Id && t.ProjectId == project.Id.PersistentId);


        // Step 2: Move sibling tasks under the parent task.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Tasks.MoveAsync(MoveTaskArgument.CreateMoveToParent(firstSiblingTask.Id, parentTask.Id), _cancellationToken);
                await t.Tasks.MoveAsync(MoveTaskArgument.CreateMoveToParent(secondSiblingTask.Id, parentTask.Id), _cancellationToken);
            },
            [ResourceType.Tasks],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Tasks, t => t.Id == firstSiblingTask.Id && t.ParentId == parentTask.Id.PersistentId);
        Assert.Contains(syncResponse.Tasks, t => t.Id == secondSiblingTask.Id && t.ParentId == parentTask.Id.PersistentId);


        // Step 3: Reorder sibling tasks under the parent task.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Tasks.ReorderAsync(new(new Dictionary<ComplexId, int>
            {
                { firstSiblingTask.Id, 20 },
                { secondSiblingTask.Id, 10 }
            }), _cancellationToken),
            [ResourceType.Tasks],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Tasks, t => t.Id == firstSiblingTask.Id && t.ChildOrder == 20);
        Assert.Contains(syncResponse.Tasks, t => t.Id == secondSiblingTask.Id && t.ChildOrder == 10);


        // Step 4: Delete the task hierarchy.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Tasks.DeleteAsync(parentTask.Id, _cancellationToken),
            [ResourceType.Tasks],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Tasks, t => t.Id == parentTask.Id && t.IsDeleted == true);
        Assert.Contains(syncResponse.Tasks, t => t.Id == firstSiblingTask.Id && t.IsDeleted == true);
        Assert.Contains(syncResponse.Tasks, t => t.Id == secondSiblingTask.Id && t.IsDeleted == true);

        parentTaskTracker.StopTracking();
        firstSiblingTaskTracker.StopTracking();
        secondSiblingTaskTracker.StopTracking();
    }

    [Fact]
    public async Task QuickAdd_Filter_Close_Uncomplete_GetById_Succeeds()
    {
        var quickAddTaskContent = $"QuickAddTask_{Guid.NewGuid():N}";
        var quickAddTask = new QuickAddTask($"{quickAddTaskContent} tomorrow");


        // Step 1: Quick add task.
        await _apiFixture.Client.Tasks.QuickAddAsync(quickAddTask, _cancellationToken);


        // Step 2: Find the quick-added task via filter.
        var filterResponse = await _apiFixture.Client.Tasks.GetByFilterAsync(
            new($"search: {quickAddTaskContent}", Language.English),
            _cancellationToken);

        var actualQuickAddedTask = Assert.Single(filterResponse.Results, t => t.Content == quickAddTaskContent);
        await using var quickAddedTaskTracker = _apiFixture.TrackForCleanup(actualQuickAddedTask, c => c.Tasks.DeleteAsync);


        // Step 3: Close task and assert synced state.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Tasks.CloseAsync(actualQuickAddedTask.Id, _cancellationToken),
            [ResourceType.Tasks],
            cancellationToken: _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.DoesNotContain(syncResponse.Tasks, t => t.Id == actualQuickAddedTask.Id);


        // Step 4: Uncomplete task and assert synced state.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Tasks.UncompleteAsync(actualQuickAddedTask.Id, _cancellationToken),
            [ResourceType.Tasks],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualUncompletedTask = Assert.Single(syncResponse.Tasks, t => t.Id == actualQuickAddedTask.Id);
        Assert.False(actualUncompletedTask.IsChecked ?? true);
        Assert.False(actualUncompletedTask.IsDeleted ?? true);


        // Step 5: Get task by id.
        actualQuickAddedTask = await _apiFixture.Client.Tasks.GetAsync(actualQuickAddedTask.Id.PersistentId, _cancellationToken);

        Assert.Equal(quickAddTaskContent, actualQuickAddedTask.Content);
        Assert.False(actualQuickAddedTask.IsChecked ?? true);
    }

    [Fact]
    public async Task CreateCompletedTasks_GetCompletedByCompletionDate_GetCompletedByDueDate_Succeeds()
    {
        var project = await _apiFixture.GetPlaygroundProjectAsync();
        var currentUtcDate = DateTime.UtcNow;

        var firstTask = TestData.Tasks.AddTask(project.Id, $"CompletedTaskOne_{Guid.NewGuid():N}");
        var secondTask = TestData.Tasks.AddTask(project.Id, $"CompletedTaskTwo_{Guid.NewGuid():N}");
        var thirdTask = TestData.Tasks.AddTask(project.Id, $"CompletedTaskThree_{Guid.NewGuid():N}");

        firstTask.DueDate = DueDate.CreateFullDay(currentUtcDate.Date.AddDays(2));
        secondTask.DueDate = DueDate.CreateFullDay(currentUtcDate.Date.AddDays(4));
        thirdTask.DueDate = DueDate.CreateFullDay(currentUtcDate.Date.AddDays(10));

        var firstCompletedAt = currentUtcDate.AddHours(-3);
        var secondCompletedAt = currentUtcDate.AddHours(-2);
        var thirdCompletedAt = currentUtcDate.AddDays(-3);


        // Step 1: Create three tasks with different due dates.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Tasks.AddAsync(firstTask, _cancellationToken);
                await t.Tasks.AddAsync(secondTask, _cancellationToken);
                await t.Tasks.AddAsync(thirdTask, _cancellationToken);
            },
            [ResourceType.Tasks],
            cancellationToken: _cancellationToken);
        await using var firstTaskTracker = _apiFixture.TrackForCleanup(firstTask, c => c.Tasks.DeleteAsync);
        await using var secondTaskTracker = _apiFixture.TrackForCleanup(secondTask, c => c.Tasks.DeleteAsync);
        await using var thirdTaskTracker = _apiFixture.TrackForCleanup(thirdTask, c => c.Tasks.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Tasks, t => t.Id == firstTask.Id && t.ProjectId == project.Id.PersistentId);
        Assert.Contains(syncResponse.Tasks, t => t.Id == secondTask.Id && t.ProjectId == project.Id.PersistentId);
        Assert.Contains(syncResponse.Tasks, t => t.Id == thirdTask.Id && t.ProjectId == project.Id.PersistentId);


        // Step 2: Complete tasks with different completion dates.
        var transactionResponse = await _apiFixture.Client.ExecuteTransactionAsync(
            async t =>
            {
                await t.Tasks.CompleteAsync(new(firstTask.Id, firstCompletedAt), _cancellationToken);
                await t.Tasks.CompleteAsync(new(secondTask.Id, secondCompletedAt), _cancellationToken);
                await t.Tasks.CompleteAsync(new(thirdTask.Id, thirdCompletedAt), _cancellationToken);
            },
            _cancellationToken);

        Assert.All(transactionResponse.SyncStatus.Values, cr => cr.AssertSuccess());


        // Step 3: Get completed tasks by completion date.
        var completedTasksResponse = await _apiFixture.Client.Tasks.GetCompletedByCompletionDateAsync(
            new CompletedTasksPaginationQuery(currentUtcDate.AddHours(-4), currentUtcDate.AddHours(-1))
            {
                ProjectId = project.Id.PersistentId
            },
            _cancellationToken);

        Assert.Contains(completedTasksResponse.Items, t => t.Id == firstTask.Id);
        Assert.Contains(completedTasksResponse.Items, t => t.Id == secondTask.Id);
        Assert.DoesNotContain(completedTasksResponse.Items, t => t.Id == thirdTask.Id);


        // Step 4: Get completed tasks by due date.
        completedTasksResponse = await _apiFixture.Client.Tasks.GetCompletedByDueDateAsync(
            new CompletedTasksPaginationQuery(DateTime.UtcNow.Date.AddDays(1), DateTime.UtcNow.Date.AddDays(5))
            {
                ProjectId = project.Id.PersistentId
            },
            _cancellationToken);

        Assert.Contains(completedTasksResponse.Items, t => t.Id == firstTask.Id);
        Assert.Contains(completedTasksResponse.Items, t => t.Id == secondTask.Id);
        Assert.DoesNotContain(completedTasksResponse.Items, t => t.Id == thirdTask.Id);

        firstTaskTracker.StopTracking();
        secondTaskTracker.StopTracking();
        thirdTaskTracker.StopTracking();
    }
}