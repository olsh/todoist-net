using System;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    public class TransactionTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public TransactionTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public void CreateProjectAndCreateNote_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var project = new Project(Guid.NewGuid().ToString());
            var projectId = transaction.Project.AddAsync(project).Result;
            var note = new Note("Demo note");
            transaction.Notes.AddToProjectAsync(note, projectId).Wait();

            var syncToken = transaction.CommitAsync().Result;

            var projectInfo = client.Projects.GetAsync(project.Id).Result;

            Assert.True(projectInfo.Notes.Count > 0);
            Assert.NotNull(syncToken);

            var deleteTransaction = client.CreateTransaction();

            deleteTransaction.Notes.DeleteAsync(note.Id).Wait();
            deleteTransaction.Project.DeleteAsync(project.Id).Wait();

            deleteTransaction.CommitAsync().Wait();
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public void CreateProjectAndCreateItem_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var project = new Project("Shopping List");
            var projectId = transaction.Project.AddAsync(project).Result;

            var item = new Item("Buy milk")
            {
                ProjectId = projectId
            };
            transaction.Items.AddAsync(item).Wait();

            transaction.CommitAsync().Wait();

            Assert.False(string.IsNullOrEmpty(project.Id.PersistentId));
            Assert.False(string.IsNullOrEmpty(item.Id.PersistentId));


            var itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.Equal(itemInfo.Item.Content, item.Content);
            Assert.Equal(itemInfo.Project.Name, project.Name);
            Assert.Equal(itemInfo.Project.Id.PersistentId, project.Id.PersistentId);


            client.Projects.DeleteAsync(project.Id).Wait();

            var projects = client.Projects.GetAsync().Result;

            Assert.DoesNotContain(projects, p => p.Id.PersistentId == project.Id.PersistentId);
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public void CreateProjectAndCreateItemWithPredefinedTempId_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var project = new Project("Shopping List")
            {
                Id = new ComplexId(Guid.NewGuid()) // predefined temp id
            };
            var item = new Item("Buy milk")
            {
                ProjectId = project.Id // predefined temp id
            };

            var transaction = client.CreateTransaction();

            transaction.Project.AddAsync(project).Wait();
            transaction.Items.AddAsync(item).Wait();

            transaction.CommitAsync().Wait();

            Assert.False(string.IsNullOrEmpty(project.Id.PersistentId));
            Assert.False(string.IsNullOrEmpty(item.Id.PersistentId));


            var itemInfo = client.Items.GetAsync(item.Id).Result;

            Assert.Equal(itemInfo.Item.Content, item.Content);
            Assert.Equal(itemInfo.Project.Name, project.Name);
            Assert.Equal(itemInfo.Project.Id.PersistentId, project.Id.PersistentId);


            client.Projects.DeleteAsync(project.Id).Wait();

            var projects = client.Projects.GetAsync().Result;

            Assert.DoesNotContain(projects, p => p.Id.PersistentId == project.Id.PersistentId);
        }

    }
}
