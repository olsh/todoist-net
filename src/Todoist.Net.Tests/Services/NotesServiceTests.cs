using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    public class NotesServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public NotesServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task AddNoteGetAndDelete_Success()
        {
            var todoistClient = TodoistClientFactory.Create(_outputHelper);

            var project = new Project(Guid.NewGuid().ToString());
            await todoistClient.Projects.AddAsync(project);
            try
            {
                var note = new Note("Hello");
                await todoistClient.Notes.AddToProjectAsync(note, project.Id.PersistentId);

                var notesInfo = await todoistClient.Notes.GetAsync();
                Assert.Contains(notesInfo.ProjectNotes, n => n.Id == note.Id);
            }
            finally
            {
                await todoistClient.Projects.DeleteAsync(project.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task AddNoteToNewProjectAndUpdateIt_Success()
        {
            var todoistClient = TodoistClientFactory.Create(_outputHelper);

            var project = new Project(Guid.NewGuid().ToString());
            await todoistClient.Projects.AddAsync(project);
            try
            {
                var note = new Note("Hello");
                await todoistClient.Notes.AddToProjectAsync(note, project.Id.PersistentId);

                note.Content = "Updated";
                await todoistClient.Notes.UpdateAsync(note);
            }
            finally
            {
                await todoistClient.Projects.DeleteAsync(project.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
        public async Task AddNoteToNewProjectAttachFileAndDeleteIt_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var project = new Project(Guid.NewGuid().ToString());
            await client.Projects.AddAsync(project);
            try
            {
                var note = new Note("Hello");
                var fileName = "test.txt";
                var upload = await client.Uploads.UploadAsync(fileName, Encoding.UTF8.GetBytes("hello"));
                note.FileAttachment = upload;

                await client.Notes.AddToProjectAsync(note, project.Id.PersistentId);

                var projectInfo = await client.Projects.GetAsync(project.Id);
                var attachedNote = projectInfo.Notes.FirstOrDefault();
                Assert.NotNull(attachedNote);
                Assert.True(attachedNote.FileAttachment.FileName == fileName);

                await client.Notes.DeleteAsync(attachedNote.Id);

                projectInfo = await client.Projects.GetAsync(project.Id);
                Assert.Empty(projectInfo.Notes);
            }
            finally
            {
                await client.Projects.DeleteAsync(project.Id);
            }
        }
    }
}
