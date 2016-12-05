using System;

using Todoist.Net.Models;
using Todoist.Net.Tests.Constants;
using Todoist.Net.Tests.Settings;
using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class TransactionTests
    {
        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void CreateProjectAndCreateNote_Success()
        {
            var client = CreateClient();

            var transaction = client.CreateTransaction();

            var project = new Project(Guid.NewGuid().ToString());
            var projectId = transaction.Project.AddAsync(project).Result;
            var noteId = transaction.Notes.AddToProjectAsync(new Note("Demo note"), projectId).Result;

            transaction.CommitAsync().Wait();

            var projectInfo = client.Projects.GetAsync(project.Id).Result;

            Assert.True(projectInfo.Notes.Count > 0);

            client.Notes.DeleteAsync(project.Id).Wait();
        }

        public TodoistClient CreateClient()
        {
            return new TodoistClient(SettingsProvider.GetToken());
        }
    }
}