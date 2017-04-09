using System;
using System.Linq;
using System.Text;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class NotesServiceTests
    {
        [Fact]
        [IntegrationFree]
        public void AddNoteGetByIdAndDelete_Success()
        {
            var todoistClient = TodoistClientFactory.Create();

            var project = new Project(Guid.NewGuid().ToString());
            todoistClient.Projects.AddAsync(project).Wait();

            var note = new Note("Hello");
            todoistClient.Notes.AddToProjectAsync(note, project.Id.PersistentId).Wait();

            var noteInfo = todoistClient.Notes.GetAsync(note.Id).Result;
            Assert.True(noteInfo.Note.Content == note.Content);

            todoistClient.Projects.DeleteAsync(project.Id).Wait();
        }

        [Fact]
        [IntegrationFree]
        public void AddNoteToNewProjectAndUpdateIt_Success()
        {
            var todoistClient = TodoistClientFactory.Create();

            var project = new Project(Guid.NewGuid().ToString());
            todoistClient.Projects.AddAsync(project).Wait();

            var note = new Note("Hello");
            todoistClient.Notes.AddToProjectAsync(note, project.Id.PersistentId).Wait();

            note.Content = "Updated";
            todoistClient.Notes.UpdateAsync(note).Wait();

            todoistClient.Projects.DeleteAsync(project.Id).Wait();
        }

        [Fact]
        [IntegrationPremium]
        public void AddNoteToNewProjectAttachFileAndDeleteIt_Success()
        {
            var client = TodoistClientFactory.Create();

            var project = new Project(Guid.NewGuid().ToString());
            client.Projects.AddAsync(project).Wait();

            var note = new Note("Hello");
            var fileName = "test.txt";
            var upload = client.Uploads.UploadAsync(fileName, Encoding.UTF8.GetBytes("hello")).Result;
            note.FileAttachment = upload;

            client.Notes.AddToProjectAsync(note, project.Id.PersistentId).Wait();

            var projectInfo = client.Projects.GetAsync(project.Id).Result;
            var attachedNote = projectInfo.Notes.FirstOrDefault();
            Assert.True(attachedNote != null);
            Assert.True(attachedNote.FileAttachment.FileName == fileName);

            client.Notes.DeleteAsync(attachedNote.Id).Wait();

            projectInfo = client.Projects.GetAsync(project.Id).Result;
            Assert.True(!projectInfo.Notes.Any());

            client.Projects.DeleteAsync(project.Id).Wait();
        }
    }
}
