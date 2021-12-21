using System;
using System.Linq;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class ItemsServiceTests
    {
        [Fact]
        [IntegrationPremium]
        public void CreateItemCompleteGetCloseAsync_Success()
        {
            var client = TodoistClientFactory.Create();

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
        [IntegrationFree]
        public void CreateItemCompleteUncompleteAsync_Success()
        {
            var client = TodoistClientFactory.Create();

            var transaction = client.CreateTransaction();

            var item = new Item("demo task");
            var itemId = transaction.Items.AddAsync(item).Result;
            transaction.Items.CompleteAsync(new CompleteItemArgument(itemId));

            transaction.CommitAsync().Wait();

            var itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.True(itemInfo.Item.IsChecked == true);

            client.Items.UnArchiveAsync(itemId).Wait();
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
        [IntegrationFree]
        public void CreateItemClearDueDateAndDelete_Success()
        {
            var client = TodoistClientFactory.Create();

            var item = new Item("demo task") { DueDate = new DueDate("22 Dec 2021", null, Language.English) };
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
        [IntegrationFree]
        public void CreateItem_InvalidPDueDate_ThrowsException()
        {
            var client = TodoistClientFactory.Create();
            var item = new Item("bad task");
            item.DueDate = new DueDate("Invalid date string");

            var aggregateException = Assert.ThrowsAsync<AggregateException>(
                async () =>
                    {
                        await client.Items.AddAsync(item);
                    }).Result;

            Assert.IsType<TodoistException>(aggregateException.InnerExceptions.First());
        }

        [Fact]
        [IntegrationFree]
        public void MoveItemsToProject_Success()
        {
            var client = TodoistClientFactory.Create();

            var item = new Item("demo task");
            client.Items.AddAsync(item).Wait();

            item.DueDate = new DueDate("every fri");
            client.Items.UpdateAsync(item).Wait();

            var project = new Project(Guid.NewGuid().ToString());
            client.Projects.AddAsync(project);

            var itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.True(project.Id != itemInfo.Project.Id);

            client.Items.MoveAsync(ItemMoveArgument.CreateMoveToProject(itemInfo.Item.Id, project.Id)).Wait();
            itemInfo = client.Items.GetAsync(itemInfo.Item.Id).Result;

            Assert.True(project.Id == itemInfo.Project.Id);

            client.Projects.DeleteAsync(project.Id).Wait();
            client.Items.DeleteAsync(itemInfo.Item.Id).Wait();
        }

        [Fact]
        [IntegrationFree]
        public void QuickAddAsync_Success()
        {
            var client = TodoistClientFactory.Create();

            var item = client.Items.QuickAddAsync(new QuickAddItem("Demo task every fri")).Result;

            Assert.NotNull(item);

            client.Items.CompleteRecurringAsync(new CompleteRecurringItemArgument(item.Id, new DueDate(DateTime.UtcNow.AddMonths(1)))).Wait();
            client.Items.CompleteRecurringAsync(item.Id).Wait();

            client.Items.DeleteAsync(item.Id).Wait();
        }

        [Fact]
        [IntegrationFree]
        public void UpdateOrders_Success()
        {
            var client = TodoistClientFactory.Create();

            var item = client.Items.QuickAddAsync(new QuickAddItem("Demo task every fri")).Result;

            var firstProject = client.Projects.GetAsync().Result.First();
            client.Items.MoveAsync(ItemMoveArgument.CreateMoveToProject(item.Id, firstProject.Id)).Wait();
            client.Items.UpdateDayOrdersAsync(new OrderEntry(item.Id, 2));

            client.Items.DeleteAsync(item.Id).Wait();
        }

        [Fact]
        [IntegrationFree]
        public void CreateNewItem_DueDateIsLocal_DueDateNotChanged()
        {
            var client = TodoistClientFactory.Create();

            var item = new Item("New task") { DueDate = new DueDate(DateTime.Now.AddYears(1).Date) };
            var taskId = client.Items.AddAsync(item).Result;

            var itemInfo = client.Items.GetAsync(taskId).Result;

            Assert.Equal(item.DueDate.Date, itemInfo.Item.DueDate.Date.Value.ToLocalTime());

            client.Items.DeleteAsync(item.Id).Wait();
        }
    }
}
