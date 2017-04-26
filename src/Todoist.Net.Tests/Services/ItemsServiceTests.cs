using System;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;
using Todoist.Net.Tests.Settings;

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
            transaction.Items.CompleteAsync(ids: itemId);

            transaction.CommitAsync().Wait();

            var itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.True(itemInfo.Item.IsChecked == true);

            var itemState = new ItemState(item.Id, false, 2, false, 1);
            client.Items.UncompleteAsync(itemState).Wait();
            itemInfo = client.Items.GetAsync(item.Id).Result;
            Assert.True(itemInfo.Item.IsChecked == itemState.IsChecked);
            Assert.True(itemInfo.Item.Indent == itemState.Indent);
            Assert.True(itemInfo.Item.InHistory == itemState.InHistory);
            Assert.True(itemInfo.Item.ItemOrder == itemState.Order);

            client.Items.DeleteAsync(item.Id).Wait();
        }

        [Fact]
        [IntegrationFree]
        public void CreateItemGetByIdAndDelete_Success()
        {
            var client = TodoistClientFactory.Create();

            var item = new Item("demo task");
            client.Items.AddAsync(item).Wait();

            var itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.True(itemInfo.Item.Content == item.Content);

            client.Items.DeleteAsync(item.Id).Wait();
        }

        [Fact]
        [IntegrationFree]
        public void MoveItemsToProject_Success()
        {
            var client = TodoistClientFactory.Create();

            var item = new Item("demo task");
            client.Items.AddAsync(item).Wait();

            var project = new Project(Guid.NewGuid().ToString());
            client.Projects.AddAsync(project);

            var itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.True(project.Id != itemInfo.Project.Id);

            client.Items.MoveToProjectAsync(project.Id, itemInfo.Item).Wait();
            itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.True(project.Id == itemInfo.Project.Id);

            client.Projects.DeleteAsync(project.Id).Wait();
        }
    }
}
