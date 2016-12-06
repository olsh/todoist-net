using System;

using Todoist.Net.Models;
using Todoist.Net.Tests.Constants;
using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class ItemsServiceTests
    {
        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void CreateItemGetByIdAndDelete_Success()
        {
            var client = CreateClient();

            var item = new Item("demo task");
            client.Items.AddAsync(item).Wait();

            var itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.True(itemInfo.Item.Content == item.Content);

            client.Items.DeleteAsync(item.Id).Wait();
        }

        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void MoveItemsToProject_Success()
        {
            var client = CreateClient();

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

        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void CreateItemCompleteGetCloseAsync_Success()
        {
            var client = CreateClient();

            var item = new Item("temp task");
            client.Items.AddAsync(item).Wait();
            client.Notes.AddToItemAsync(new Note("test note"), item.Id).Wait();
            client.Items.CloseAsync(item.Id).Wait();

            var completedTasks = client.Items.GetCompletedAsync(new ItemFilter() {AnnotateNotes = true, Limit = 5, Since = DateTime.Today.AddDays(-1)}).Result;

            Assert.True(completedTasks.Items.Length > 0);

            client.Items.DeleteAsync(item.Id).Wait();
        }

        private static ITodoistClient CreateClient()
        {
            return new TodoistClient(SettingsProvider.GetToken());
        }
    }
}
