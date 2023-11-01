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
        public void CreateItemCompleteGetCloseAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var item = new Item("temp task");
            transaction.Items.AddAsync(item).Wait();
            transaction.Notes.AddToItemAsync(new Note("test note"), item.Id).Wait();
            transaction.Items.CloseAsync(item.Id).Wait();

            transaction.CommitAsync().Wait();

            var completedTasks =
                client.Items.GetCompletedAsync(
                    new ItemFilter() { AnnotateNotes = true, Limit = 5, Since = DateTime.Today.AddDays(-1) }).Result;

            Assert.True(completedTasks.Items.Count > 0);

            client.Items.DeleteAsync(item.Id).Wait();
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public void CreateItemCompleteUncompleteAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var item = new Item("demo task");
            var itemId = transaction.Items.AddAsync(item).Result;
            transaction.Items.CompleteAsync(new CompleteItemArgument(itemId));

            transaction.CommitAsync().Wait();

            var itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.True(itemInfo.Item.IsChecked == true);

            client.Items.UncompleteAsync(itemId).Wait();

            var anotherItem = client.Items.GetAsync().Result.First(i => i.Id != item.Id);
            client.Items.MoveAsync(ItemMoveArgument.CreateMoveToParent(item.Id, anotherItem.Id))
                .Wait();

            itemInfo = client.Items.GetAsync(item.Id).Result;
            Assert.Equal(anotherItem.Id.PersistentId, itemInfo.Item.ParentId);

            client.Items.CompleteAsync(new CompleteItemArgument(itemId)).Wait();
            itemInfo = client.Items.GetAsync(item.Id).Result;
            Assert.True(itemInfo.Item.IsChecked);

            client.Items.UncompleteAsync(itemId).Wait();
            itemInfo = client.Items.GetAsync(item.Id).Result;
            Assert.False(itemInfo.Item.IsChecked);

            client.Items.DeleteAsync(item.Id).Wait();
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public void CreateItemClearDueDateAndDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = new Item("demo task") { DueDate = DueDate.FromText("22 Dec 2021", Language.English) };
            client.Items.AddAsync(item).Wait();

            var itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.True(itemInfo.Item.Content == item.Content);
            Assert.Equal("2021-12-22", itemInfo.Item.DueDate.StringDate);

            itemInfo.Item.DueDate = null;
            client.Items.UpdateAsync(itemInfo.Item).Wait();

            itemInfo = client.Items.GetAsync(item.Id).Result;
            Assert.Null(itemInfo.Item.DueDate);

            client.Items.DeleteAsync(item.Id).Wait();
        }


        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public void CreateItem_InvalidPDueDate_ThrowsException()
        {
            var client = TodoistClientFactory.Create(_outputHelper);
            var item = new Item("bad task");
            item.DueDate = DueDate.FromText("Invalid date string");

            var aggregateException = Assert.ThrowsAsync<AggregateException>(
                async () =>
                    {
                        await client.Items.AddAsync(item);
                    }).Result;

            Assert.IsType<TodoistException>(aggregateException.InnerExceptions.First());
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task MoveItemsToProject_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = new Item("demo task");
            client.Items.AddAsync(item).Wait();

            item.DueDate = DueDate.FromText("every fri");
            await client.Items.UpdateAsync(item);

            var project = new Project(Guid.NewGuid().ToString());
            await client.Projects.AddAsync(project);

            var itemInfo = await client.Items.GetAsync(item.Id);

            Assert.True(project.Id != itemInfo.Project.Id);

            await client.Items.MoveAsync(ItemMoveArgument.CreateMoveToProject(itemInfo.Item.Id, project.Id));
            itemInfo = await client.Items.GetAsync(itemInfo.Item.Id);

            Assert.True(project.Id == itemInfo.Project.Id);

            await client.Items.DeleteAsync(itemInfo.Item.Id);
            await client.Projects.DeleteAsync(project.Id);
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public void QuickAddAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = client.Items.QuickAddAsync(new QuickAddItem("Demo task every fri")).Result;

            Assert.NotNull(item);

            client.Items.CompleteRecurringAsync(new CompleteRecurringItemArgument(item.Id, DueDate.CreateFloating(DateTime.UtcNow.AddMonths(1)))).Wait();
            client.Items.CompleteRecurringAsync(item.Id).Wait();

            client.Items.DeleteAsync(item.Id).Wait();
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public void UpdateOrders_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = client.Items.QuickAddAsync(new QuickAddItem("Demo task every fri")).Result;

            var firstProject = client.Projects.GetAsync().Result.First();
            client.Items.MoveAsync(ItemMoveArgument.CreateMoveToProject(item.Id, firstProject.Id)).Wait();
            client.Items.UpdateDayOrdersAsync(new OrderEntry(item.Id, 2));

            client.Items.DeleteAsync(item.Id).Wait();
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public void CreateNewItem_DueDateIsLocal_DueDateNotChanged()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = new Item("New task") { DueDate = DueDate.CreateFloating(DateTime.Now.AddYears(1).Date) };
            var taskId = client.Items.AddAsync(item).Result;

            var itemInfo = client.Items.GetAsync(taskId).Result;

            Assert.Equal(item.DueDate.Date, itemInfo.Item.DueDate.Date);

            client.Items.DeleteAsync(item.Id).Wait();
        }


        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
        public void CreateItemClearDurationAndDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var item = new Item("duration task")
            {
                DueDate = DueDate.FromText("22 Dec 2021 at 9:15", Language.English),
                Duration = new Duration(45, DurationUnit.Minute)
            };
            client.Items.AddAsync(item).Wait();

            var itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.True(itemInfo.Item.Content == item.Content);
            Assert.Equal("2021-12-22T09:15:00", itemInfo.Item.DueDate.StringDate);

            Assert.Equal(item.Duration.Amount, itemInfo.Item.Duration.Amount);
            Assert.Equal(item.Duration.Unit, itemInfo.Item.Duration.Unit);

            itemInfo.Item.Duration = null;
            client.Items.UpdateAsync(itemInfo.Item).Wait();

            itemInfo = client.Items.GetAsync(item.Id).Result;
            Assert.Null(itemInfo.Item.Duration);

            client.Items.DeleteAsync(item.Id).Wait();
        }

    }
}
