using System;
using System.Linq;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    public class TasksServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public TasksServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
        public async Task CreateTaskCompleteGetCloseAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var task = new AddTask("temp task");
            await transaction.Tasks.AddAsync(task);
            await transaction.Comments.AddToTaskAsync(new Comment("test comment"), task.Id);
            await transaction.Tasks.CloseAsync(task.Id);

            await transaction.CommitAsync();
            try
            {
                var completedTasks =
                    await client.Tasks.GetCompletedAsync(
                        new TaskFilter()
                        {
                            AnnotateTasks = true,
                            AnnotateComments = true,
                            Limit = 5,
                            Since = DateTime.Today.AddDays(-1)
                        });

                Assert.True(completedTasks.Tasks.Count > 0);
                Assert.All(completedTasks.Tasks, t => Assert.NotNull(t.TaskObject));
            }
            finally
            {
                await client.Tasks.DeleteAsync(task.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateTaskCompleteUncompleteAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var task = new AddTask("demo task");
            var taskId = await transaction.Tasks.AddAsync(task);
            await transaction.Tasks.CompleteAsync(new CompleteTaskArgument(taskId));

            await transaction.CommitAsync();
            try
            {
                var taskInfo = await client.Tasks.GetAsync(task.Id);

                Assert.True(taskInfo.Task.IsChecked);

                await client.Tasks.UncompleteAsync(taskId);

                var anotherTask = (await client.Tasks.GetAsync()).First(t => t.Id != task.Id);
                await client.Tasks.MoveAsync(TaskMoveArgument.CreateMoveToParent(task.Id, anotherTask.Id));

                taskInfo = await client.Tasks.GetAsync(task.Id);
                Assert.Equal(anotherTask.Id.PersistentId, taskInfo.Task.ParentId);

                await client.Tasks.CompleteAsync(new CompleteTaskArgument(taskId));
                taskInfo = await client.Tasks.GetAsync(task.Id);
                Assert.True(taskInfo.Task.IsChecked);

                await client.Tasks.UncompleteAsync(taskId);
                taskInfo = await client.Tasks.GetAsync(task.Id);
                Assert.False(taskInfo.Task.IsChecked);
            }
            finally
            {
                await client.Tasks.DeleteAsync(task.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateTaskClearDueDateAndDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var task = new AddTask("demo task") { DueDate = DueDate.FromText("22 Dec 2021", Language.English) };
            await client.Tasks.AddAsync(task);
            try
            {
                var taskInfo = await client.Tasks.GetAsync(task.Id);

                Assert.True(taskInfo.Task.Content == task.Content);
                Assert.Equal("2021-12-22", taskInfo.Task.DueDate.StringDate);

                taskInfo.Task.Unset(t => t.DueDate);
                await client.Tasks.UpdateAsync(taskInfo.Task);

                taskInfo = await client.Tasks.GetAsync(task.Id);
                Assert.Null(taskInfo.Task.DueDate);
            }
            finally
            {
                await client.Tasks.DeleteAsync(task.Id);
            }
        }


        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateTask_InvalidDueDate_ThrowsException()
        {
            var client = TodoistClientFactory.Create(_outputHelper);
            var task = new AddTask("bad task")
            {
                DueDate = DueDate.FromText("Invalid date string")
            };

            var aggregateException = await Assert.ThrowsAsync<AggregateException>(
                async () =>
                    {
                        await client.Tasks.AddAsync(task);
                    });

            Assert.IsType<TodoistException>(aggregateException.InnerExceptions.First());
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task MoveTasksToProject_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var addTask = new AddTask("demo task");
            var taskId = await client.Tasks.AddAsync(addTask);
            try
            {
                var updateTask = new UpdateTask(taskId) { DueDate = DueDate.FromText("every fri") };
                await client.Tasks.UpdateAsync(updateTask);

                var project = new Project(Guid.NewGuid().ToString());
                await client.Projects.AddAsync(project);
                try
                {
                    var taskInfo = await client.Tasks.GetAsync(taskId);

                    Assert.True(project.Id != taskInfo.Project.Id);

                    await client.Tasks.MoveAsync(TaskMoveArgument.CreateMoveToProject(taskInfo.Task.Id, project.Id));
                    taskInfo = await client.Tasks.GetAsync(taskInfo.Task.Id);

                    Assert.True(project.Id == taskInfo.Project.Id);
                }
                finally
                {
                    await client.Projects.DeleteAsync(project.Id);
                }
            }
            finally
            {
                await client.Tasks.DeleteAsync(taskId);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task QuickAddAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var task = await client.Tasks.QuickAddAsync(new QuickAddTask("Demo task every fri"));
            try
            {
                Assert.NotNull(task);

                await client.Tasks.CompleteRecurringAsync(new CompleteRecurringTaskArgument(task.Id, DueDate.CreateFloating(DateTime.UtcNow.AddMonths(1))));
                await client.Tasks.CompleteRecurringAsync(task.Id);
            }
            finally
            {
                await client.Tasks.DeleteAsync(task.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task UpdateOrders_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var task = await client.Tasks.QuickAddAsync(new QuickAddTask("Demo task every fri"));
            try
            {
                var firstProject = (await client.Projects.GetAsync()).First();
                await client.Tasks.MoveAsync(TaskMoveArgument.CreateMoveToProject(task.Id, firstProject.Id));
                await client.Tasks.UpdateDayOrdersAsync(new OrderEntry(task.Id, 2));
            }
            finally
            {
                await client.Tasks.DeleteAsync(task.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateNewTask_DueDateIsLocal_DueDateNotChanged()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var task = new AddTask("New task") { DueDate = DueDate.CreateFloating(DateTime.Now.AddYears(1).Date) };
            var taskId = await client.Tasks.AddAsync(task);
            try
            {
                var taskInfo = await client.Tasks.GetAsync(taskId);

                Assert.Equal(task.DueDate.Date, taskInfo.Task.DueDate.Date);
            }
            finally
            {
                await client.Tasks.DeleteAsync(task.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateNewTask_DeadlineIsLocal_DeadlineNotChanged()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var task = new AddTask("New task") { Deadline = new Deadline(DateTime.Now.AddYears(1).Date) };
            var taskId = await client.Tasks.AddAsync(task);
            try
            {
                var taskInfo = await client.Tasks.GetAsync(taskId);

                Assert.Equal(task.Deadline.Date, taskInfo.Task.Deadline.Date);
            }
            finally
            {
                await client.Tasks.DeleteAsync(task.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
        public async Task CreateTaskClearDurationAndDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var task = new AddTask("duration task")
            {
                DueDate = DueDate.FromText("22 Dec 2021 at 9:15", Language.English),
                Duration = new Duration(45, DurationUnit.Minute)
            };
            await client.Tasks.AddAsync(task);
            try
            {
                var taskInfo = await client.Tasks.GetAsync(task.Id);

                Assert.True(taskInfo.Task.Content == task.Content);
                Assert.Equal("2021-12-22T09:15:00", taskInfo.Task.DueDate.StringDate);

                Assert.Equal(task.Duration.Amount, taskInfo.Task.Duration.Amount);
                Assert.Equal(task.Duration.Unit, taskInfo.Task.Duration.Unit);

                taskInfo.Task.Unset(t => t.Duration);
                await client.Tasks.UpdateAsync(taskInfo.Task);

                taskInfo = await client.Tasks.GetAsync(task.Id);
                Assert.Null(taskInfo.Task.Duration);
            }
            finally
            {
                await client.Tasks.DeleteAsync(task.Id);
            }
        }
    }
}
