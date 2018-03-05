using System;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;
using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class TransactionTests
    {
        [Fact]
        [IntegrationFree]
        public void CreateProjectAndCreateNote_Success()
        {
            var client = TodoistClientFactory.Create();

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