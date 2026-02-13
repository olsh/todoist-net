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
    public class CommentsServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public CommentsServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task AddCommentGetAndDelete_Success()
        {
            var todoistClient = TodoistClientFactory.Create(_outputHelper);

            var project = new Project(Guid.NewGuid().ToString());
            await todoistClient.Projects.AddAsync(project);
            try
            {
                var comment = new Comment("Hello");
                await todoistClient.Comments.AddToProjectAsync(comment, project.Id.PersistentId);

                var commentsInfo = await todoistClient.Comments.GetAsync();
                Assert.Contains(commentsInfo.ProjectComments, c => c.Id == comment.Id);
            }
            finally
            {
                await todoistClient.Projects.DeleteAsync(project.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task AddCommentToNewProjectAndUpdateIt_Success()
        {
            var todoistClient = TodoistClientFactory.Create(_outputHelper);

            var project = new Project(Guid.NewGuid().ToString());
            await todoistClient.Projects.AddAsync(project);
            try
            {
                var comment = new Comment("Hello");
                await todoistClient.Comments.AddToProjectAsync(comment, project.Id.PersistentId);

                comment.Content = "Updated";
                await todoistClient.Comments.UpdateAsync(comment);
            }
            finally
            {
                await todoistClient.Projects.DeleteAsync(project.Id);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
        public async Task AddCommentToNewProjectAttachFileAndDeleteIt_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var project = new Project(Guid.NewGuid().ToString());
            await client.Projects.AddAsync(project);
            try
            {
                var comment = new Comment("Hello");
                var fileName = "test.txt";
                var upload = await client.Uploads.UploadAsync(fileName, Encoding.UTF8.GetBytes("hello"));
                comment.FileAttachment = upload;

                await client.Comments.AddToProjectAsync(comment, project.Id.PersistentId);

                var commentsInfo = await client.Comments.GetAsync();
                var attachedComment = commentsInfo.ProjectComments.FirstOrDefault(c => c.ProjectId == project.Id);
                Assert.NotNull(attachedComment);
                Assert.True(attachedComment.FileAttachment.FileName == fileName);

                await client.Comments.DeleteAsync(attachedComment.Id);

                commentsInfo = await client.Comments.GetAsync();
                Assert.DoesNotContain(commentsInfo.ProjectComments, c => c.ProjectId == project.Id && c.Id == attachedComment.Id);
            }
            finally
            {
                await client.Projects.DeleteAsync(project.Id);
            }
        }
    }
}
