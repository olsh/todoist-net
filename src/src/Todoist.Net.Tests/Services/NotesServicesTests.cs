using System;
using System.Linq;

using Todoist.Net.Models;
using Todoist.Net.Tests.Constants;
using Todoist.Net.Tests.Helpers;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class NotesServicesTests
    {
        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void AddNoteToNewProjectAndUpdateIt_Success()
        {
            var todoistClient = ClientFactory.Create();

            var project = new Project(Guid.NewGuid().ToString());
            todoistClient.Projects.AddAsync(project).Wait();

            var note = new Note("Hello");
            todoistClient.Notes.AddToProjectAsync(note, project.Id.PersistentId).Wait();

            note.Content = "Updated";
            todoistClient.Notes.UpdateAsync(note).Wait();

            todoistClient.Projects.DeleteAsync(project.Id).Wait();
        }

        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void AddNoteToNewProjectAndDeleteIt_Success()
        {
            var todoistClient = ClientFactory.Create();

            var project = new Project(Guid.NewGuid().ToString());
            todoistClient.Projects.AddAsync(project).Wait();

            var note = new Note("Hello");
            todoistClient.Notes.AddToProjectAsync(note, project.Id.PersistentId).Wait();

            var projectInfo = todoistClient.Projects.GetAsync(project.Id).Result;
            Assert.True(projectInfo.Notes.Any());

            todoistClient.Notes.DeleteAsync(note.Id).Wait();

            projectInfo = todoistClient.Projects.GetAsync(project.Id).Result;
            Assert.True(!projectInfo.Notes.Any());

            todoistClient.Projects.DeleteAsync(project.Id).Wait();            
        }
    }
}
