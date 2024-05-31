using System;
using System.Threading.Tasks;

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
        public async Task CreateProjectAndCreateNote_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var project = new Project(Guid.NewGuid().ToString());
            var projectId = await transaction.Project.AddAsync(project);
            var note = new Note("Demo note");
            await transaction.Notes.AddToProjectAsync(note, projectId);

            var syncToken = await transaction.CommitAsync();
            try
            {
                var projectInfo = await client.Projects.GetAsync(project.Id);

                Assert.True(projectInfo.Notes.Count > 0);
                Assert.NotNull(syncToken);
            }
            finally
            {
                var deleteTransaction = client.CreateTransaction();

                await deleteTransaction.Notes.DeleteAsync(note.Id);
                await deleteTransaction.Project.DeleteAsync(project.Id);

                await deleteTransaction.CommitAsync();
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateProjectAndCreateItem_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var project = new Project("Shopping List");
            var projectId = await transaction.Project.AddAsync(project);

            var item = new AddItem("Buy milk")
            {
                ProjectId = projectId
            };
            await transaction.Items.AddAsync(item);

            await transaction.CommitAsync();
            try
            {
                Assert.False(string.IsNullOrEmpty(project.Id.PersistentId));
                Assert.False(string.IsNullOrEmpty(item.Id.PersistentId));


                var itemInfo = await client.Items.GetAsync(item.Id);

                Assert.Equal(itemInfo.Item.Content, item.Content);
                Assert.Equal(itemInfo.Project.Name, project.Name);
                Assert.Equal(itemInfo.Project.Id.PersistentId, project.Id.PersistentId);
            }
            finally
            {
                await client.Projects.DeleteAsync(project.Id);
            }
            var projects = await client.Projects.GetAsync();

            Assert.DoesNotContain(projects, p => p.Id.PersistentId == project.Id.PersistentId);
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateProjectAndCreateItemWithPredefinedTempId_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var project = new Project("Shopping List")
            {
                Id = new ComplexId(Guid.NewGuid()) // predefined temp id
            };
            var item = new AddItem("Buy milk")
            {
                ProjectId = project.Id // predefined temp id
            };

            var transaction = client.CreateTransaction();

            await transaction.Project.AddAsync(project);
            await transaction.Items.AddAsync(item);

            await transaction.CommitAsync();
            try
            {
                Assert.False(string.IsNullOrEmpty(project.Id.PersistentId));
                Assert.False(string.IsNullOrEmpty(item.Id.PersistentId));


                var itemInfo = await client.Items.GetAsync(item.Id);

                Assert.Equal(itemInfo.Item.Content, item.Content);
                Assert.Equal(itemInfo.Project.Name, project.Name);
                Assert.Equal(itemInfo.Project.Id.PersistentId, project.Id.PersistentId);
            }
            finally
            {
                await client.Projects.DeleteAsync(project.Id);
            }
            var projects = await client.Projects.GetAsync();

            Assert.DoesNotContain(projects, p => p.Id.PersistentId == project.Id.PersistentId);
        }

    }
}
