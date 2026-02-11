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
        public async Task CreateProjectAndCreateComment_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var project = new Project(Guid.NewGuid().ToString());
            var projectId = await transaction.Project.AddAsync(project);
            var comment = new Comment("Demo comment");
            await transaction.Comments.AddToProjectAsync(comment, projectId);

            var syncToken = await transaction.CommitAsync();
            try
            {
                var projectInfo = await client.Projects.GetAsync(project.Id);

                Assert.True(projectInfo.Comments.Count > 0);
                Assert.NotNull(syncToken);
            }
            finally
            {
                var deleteTransaction = client.CreateTransaction();

                await deleteTransaction.Comments.DeleteAsync(comment.Id);
                await deleteTransaction.Project.DeleteAsync(project.Id);

                await deleteTransaction.CommitAsync();
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateProjectAndCreateTask_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var project = new Project("Shopping List");
            var projectId = await transaction.Project.AddAsync(project);

            var task = new AddTask("Buy milk")
            {
                ProjectId = projectId
            };
            await transaction.Tasks.AddAsync(task);

            await transaction.CommitAsync();
            try
            {
                Assert.False(string.IsNullOrEmpty(project.Id.PersistentId));
                Assert.False(string.IsNullOrEmpty(task.Id.PersistentId));


                var taskInfo = await client.Tasks.GetAsync(task.Id);

                Assert.Equal(taskInfo.Task.Content, task.Content);
                Assert.Equal(taskInfo.Project.Name, project.Name);
                Assert.Equal(taskInfo.Project.Id.PersistentId, project.Id.PersistentId);
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
        public async Task CreateProjectAndCreateTaskWithPredefinedTempId_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var project = new Project("Shopping List")
            {
                Id = new ComplexId(Guid.NewGuid()) // predefined temp id
            };
            var task = new AddTask("Buy milk")
            {
                ProjectId = project.Id // predefined temp id
            };

            var transaction = client.CreateTransaction();

            await transaction.Project.AddAsync(project);
            await transaction.Tasks.AddAsync(task);

            await transaction.CommitAsync();
            try
            {
                Assert.False(string.IsNullOrEmpty(project.Id.PersistentId));
                Assert.False(string.IsNullOrEmpty(task.Id.PersistentId));


                var taskInfo = await client.Tasks.GetAsync(task.Id);

                Assert.Equal(taskInfo.Task.Content, task.Content);
                Assert.Equal(taskInfo.Project.Name, project.Name);
                Assert.Equal(taskInfo.Project.Id.PersistentId, project.Id.PersistentId);
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
