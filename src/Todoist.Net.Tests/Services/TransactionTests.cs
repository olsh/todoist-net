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

            transaction.CommitAsync().Wait();

            var projectInfo = client.Projects.GetAsync(project.Id).Result;

            Assert.True(projectInfo.Notes.Count > 0);

            var deleteTransaction = client.CreateTransaction();

            deleteTransaction.Notes.DeleteAsync(note.Id).Wait();
            deleteTransaction.Project.DeleteAsync(project.Id).Wait();

            deleteTransaction.CommitAsync().Wait();
        }
    }
}
