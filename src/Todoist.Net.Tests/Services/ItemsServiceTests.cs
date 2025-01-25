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
    public class ItemsServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public ItemsServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
        public async Task CreateItemCompleteGetCloseAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var item = new AddItem("temp task");
            await transaction.Items.AddAsync(item);
            await transaction.Notes.AddToItemAsync(new Note("test note"), item.Id);
            await transaction.Items.CloseAsync(item.Id);

            await transaction.CommitAsync();
            try
            {
                var completedTasks =
                    await client.Items.GetCompletedAsync(
                        new ItemFilter()
                        {
                            AnnotateItems = true,
                            AnnotateNotes = true,
                            Limit = 5,
                            Since = DateTime.Today.AddDays(-1)
                        });

                Assert.True(completedTasks.Items.Count > 0);
                Assert.All(completedTasks.Items, i => Assert.NotNull(i.ItemObject));
            }
            finally
            {
                await client.Items.DeleteAsync(item.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateItemCompleteUncompleteAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var item = new AddItem("demo task");
            var itemId = await transaction.Items.AddAsync(item);
            await transaction.Items.CompleteAsync(new CompleteItemArgument(itemId));

            await transaction.CommitAsync();
            try
            {
                var itemInfo = await client.Items.GetAsync(item.Id);

                Assert.True(itemInfo.Item.IsChecked);

                await client.Items.UncompleteAsync(itemId);

                var anotherItem = (await client.Items.GetAsync()).First(i => i.Id != item.Id);
                await client.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(item.Id, anotherItem.Id));

                itemInfo = await client.Items.GetAsync(item.Id);
                Assert.Equal(anotherItem.Id.PersistentId, itemInfo.Item.ParentId);

                await client.Items.CompleteAsync(new CompleteItemArgument(itemId));
                itemInfo = await client.Items.GetAsync(item.Id);
                Assert.True(itemInfo.Item.IsChecked);

                await client.Items.UncompleteAsync(itemId);
                itemInfo = await client.Items.GetAsync(item.Id);
                Assert.False(itemInfo.Item.IsChecked);
            }
            finally
            {
                await client.Items.DeleteAsync(item.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateItemClearDueDateAndDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = new AddItem("demo task") { DueDate = DueDate.FromText("22 Dec 2021", Language.English) };
            await client.Items.AddAsync(item);
            try
            {
                var itemInfo = await client.Items.GetAsync(item.Id);

                Assert.True(itemInfo.Item.Content == item.Content);
                Assert.Equal("2021-12-22", itemInfo.Item.DueDate.StringDate);

                itemInfo.Item.Unset(i => i.DueDate);
                await client.Items.UpdateAsync(itemInfo.Item);

                itemInfo = await client.Items.GetAsync(item.Id);
                Assert.Null(itemInfo.Item.DueDate);
            }
            finally
            {
                await client.Items.DeleteAsync(item.Id);
            }
        }


        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateItem_InvalidPDueDate_ThrowsException()
        {
            var client = TodoistClientFactory.Create(_outputHelper);
            var item = new AddItem("bad task")
            {
                DueDate = DueDate.FromText("Invalid date string")
            };

            var aggregateException = await Assert.ThrowsAsync<AggregateException>(
                async () =>
                    {
                        await client.Items.AddAsync(item);
                    });

            Assert.IsType<TodoistException>(aggregateException.InnerExceptions.First());
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task MoveItemsToProject_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var addItem = new AddItem("demo task");
            var itemId = await client.Items.AddAsync(addItem);
            try
            {
                var updateItem = new UpdateItem(itemId) { DueDate = DueDate.FromText("every fri") };
                await client.Items.UpdateAsync(updateItem);

                var project = new Project(Guid.NewGuid().ToString());
                await client.Projects.AddAsync(project);
                try
                {
                    var itemInfo = await client.Items.GetAsync(itemId);

                    Assert.True(project.Id != itemInfo.Project.Id);

                    await client.Items.MoveAsync(ItemMoveArgument.CreateMoveToProject(itemInfo.Item.Id, project.Id));
                    itemInfo = await client.Items.GetAsync(itemInfo.Item.Id);

                    Assert.True(project.Id == itemInfo.Project.Id);
                }
                finally
                {
                    await client.Projects.DeleteAsync(project.Id);
                }
            }
            finally
            {
                await client.Items.DeleteAsync(itemId);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task QuickAddAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = await client.Items.QuickAddAsync(new QuickAddItem("Demo task every fri"));
            try
            {
                Assert.NotNull(item);

                await client.Items.CompleteRecurringAsync(new CompleteRecurringItemArgument(item.Id, DueDate.CreateFloating(DateTime.UtcNow.AddMonths(1))));
                await client.Items.CompleteRecurringAsync(item.Id);
            }
            finally
            {
                await client.Items.DeleteAsync(item.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task UpdateOrders_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = await client.Items.QuickAddAsync(new QuickAddItem("Demo task every fri"));
            try
            {
                var firstProject = (await client.Projects.GetAsync()).First();
                await client.Items.MoveAsync(ItemMoveArgument.CreateMoveToProject(item.Id, firstProject.Id));
                await client.Items.UpdateDayOrdersAsync(new OrderEntry(item.Id, 2));
            }
            finally
            {
                await client.Items.DeleteAsync(item.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateNewItem_DueDateIsLocal_DueDateNotChanged()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = new AddItem("New task") { DueDate = DueDate.CreateFloating(DateTime.Now.AddYears(1).Date) };
            var taskId = await client.Items.AddAsync(item);
            try
            {
                var itemInfo = await client.Items.GetAsync(taskId);

                Assert.Equal(item.DueDate.Date, itemInfo.Item.DueDate.Date);
            }
            finally
            {
                await client.Items.DeleteAsync(item.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateNewItem_DeadlineIsLocal_DeadlaneNotChanged()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = new AddItem("New task") { Deadline = new Deadline(DateTime.Now.AddYears(1).Date) };
            var taskId = await client.Items.AddAsync(item);
            try
            {
                var itemInfo = await client.Items.GetAsync(taskId);

                Assert.Equal(item.Deadline.Date, itemInfo.Item.Deadline.Date);
            }
            finally
            {
                await client.Items.DeleteAsync(item.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
        public async Task CreateItemClearDurationAndDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = new AddItem("duration task")
            {
                DueDate = DueDate.FromText("22 Dec 2021 at 9:15", Language.English),
                Duration = new Duration(45, DurationUnit.Minute)
            };
            await client.Items.AddAsync(item);
            try
            {
                var itemInfo = await client.Items.GetAsync(item.Id);

                Assert.True(itemInfo.Item.Content == item.Content);
                Assert.Equal("2021-12-22T09:15:00", itemInfo.Item.DueDate.StringDate);

                Assert.Equal(item.Duration.Amount, itemInfo.Item.Duration.Amount);
                Assert.Equal(item.Duration.Unit, itemInfo.Item.Duration.Unit);

                itemInfo.Item.Unset(i => i.Duration);
                await client.Items.UpdateAsync(itemInfo.Item);

                itemInfo = await client.Items.GetAsync(item.Id);
                Assert.Null(itemInfo.Item.Duration);
            }
            finally
            {
                await client.Items.DeleteAsync(item.Id);
            }
        }
    }
}
