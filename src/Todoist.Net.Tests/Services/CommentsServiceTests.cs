namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
[Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
public class CommentsServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public CommentsServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    public async Task CreateProjectComment_Update_GetById_GetByProject_Delete_Succeeds()
    {
        var project = await _apiFixture.GetPlaygroundProjectAsync();

        var newComment = new Comment($"ProjectComment_{Guid.NewGuid():N}");


        // Step 1: Create project comment.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Comments.AddToProjectAsync(newComment, project.Id, _cancellationToken),
            [ResourceType.Comments],
            cancellationToken: _cancellationToken);
        await using var commentTracker = _apiFixture.TrackForCleanup(newComment, c => c.Comments.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.ProjectComments, c => c.Id == newComment.Id && c.Content == newComment.Content);

        var actualNewComment = Assert.Single(syncResponse.ProjectComments, c => c.Id == newComment.Id);
        Assert.Equal(project.Id, actualNewComment.ProjectId);


        // Step 2: Update project comment.
        newComment.Content = $"UpdatedProjectComment_{Guid.NewGuid():N}";

        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Comments.UpdateAsync(newComment, _cancellationToken),
            [ResourceType.Comments],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.ProjectComments, c => c.Id == newComment.Id && c.Content == newComment.Content);

        var actualUpdatedComment = Assert.Single(syncResponse.ProjectComments, c => c.Id == newComment.Id);
        Assert.Equal(project.Id, actualUpdatedComment.ProjectId);


        // Step 3: Get comment by id.
        var actualComment = await _apiFixture.Client.Comments.GetAsync(newComment.Id.PersistentId, _cancellationToken);

        Assert.Equal(newComment.Id, actualComment.Id);
        Assert.Equal(newComment.Content, actualComment.Content);
        Assert.Equal(project.Id, actualComment.ProjectId);


        // Step 4: Get comments by project.
        var commentsResponse = await _apiFixture.Client.Comments.GetAsync(
            new CommentsPaginationQuery(projectId: project.Id.PersistentId),
            _cancellationToken);

        Assert.Contains(commentsResponse.Results, c => c.Id == newComment.Id && c.Content == newComment.Content);


        // Step 5: Delete project comment.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Comments.DeleteAsync(newComment.Id, _cancellationToken),
            [ResourceType.Comments],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.ProjectComments, c => c.Id == newComment.Id && c.IsDeleted == true);

        commentTracker.StopTracking();
    }

    [Fact]
    public async Task CreateTaskComment_GetByTask_Delete_Succeeds()
    {
        var project = await _apiFixture.GetPlaygroundProjectAsync();

        var newTask = new AddTask($"CommentTask_{Guid.NewGuid():N}", project.Id);
        var newComment = new Comment($"TaskComment_{Guid.NewGuid():N}");


        // Step 1: Create task and add task comment.
        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            async t =>
            {
                await t.Tasks.AddAsync(newTask, _cancellationToken);
                await t.Comments.AddToTaskAsync(newComment, newTask.Id, _cancellationToken);
            },
            [ResourceType.Tasks, ResourceType.Comments],
            cancellationToken: _cancellationToken);
        await using var taskTracker = _apiFixture.TrackForCleanup(newTask, c => c.Tasks.DeleteAsync);
        await using var commentTracker = _apiFixture.TrackForCleanup(newComment, c => c.Comments.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualNewTask = Assert.Single(syncResponse.Tasks, t => t.Id == newTask.Id);
        Assert.Equal(newTask.Content, actualNewTask.Content);
        Assert.Equal(project.Id, actualNewTask.ProjectId);

        Assert.Contains(syncResponse.Comments, c => c.Id == newComment.Id && c.Content == newComment.Content);

        var actualNewComment = Assert.Single(syncResponse.Comments, c => c.Id == newComment.Id);
        Assert.Equal(newTask.Id, actualNewComment.TaskId);


        // Step 2: Get comments by task.
        var commentsResponse = await _apiFixture.Client.Comments.GetAsync(
            new CommentsPaginationQuery(taskId: newTask.Id.PersistentId),
            _cancellationToken);

        Assert.Contains(commentsResponse.Results, c => c.Id == newComment.Id && c.Content == newComment.Content);


        // Step 3: Delete task comment.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Comments.DeleteAsync(newComment.Id, _cancellationToken),
            [ResourceType.Comments],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.Comments, c => c.Id == newComment.Id && c.IsDeleted == true);

        commentTracker.StopTracking();
    }

    [Fact]
    public async Task UploadFile_AttachToProjectComment_GetById_GetByProject_DeleteComment_Succeeds()
    {
        var project = await _apiFixture.GetPlaygroundProjectAsync();

        var fileName = $"CommentAttachment_{Guid.NewGuid():N}.png";
        var uploadFile = new UploadFile(TestData.Files.GreenPng10x10, fileName);
        var newComment = new Comment($"AttachmentComment_{Guid.NewGuid():N}");

        // Step 1: Upload file.
        var uploadedAttachment = await _apiFixture.Client.Uploads.UploadAsync(uploadFile, project.Id.PersistentId, _cancellationToken);
        await using var uploadTracker = _apiFixture.TrackForCleanup(
            (c, ct) => c.Uploads.DeleteAsync(uploadedAttachment.FileUrl, ct),
            $"uploaded file with URL {uploadedAttachment.FileUrl}");

        Assert.Equal(fileName, uploadedAttachment.FileName);
        Assert.Equal("image/png", uploadedAttachment.FileType);
        Assert.False(string.IsNullOrWhiteSpace(uploadedAttachment.FileUrl));


        // Step 2: Attach uploaded file to project comment.
        newComment.FileAttachment = uploadedAttachment;

        var syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Comments.AddToProjectAsync(newComment, project.Id, _cancellationToken),
            [ResourceType.Comments],
            cancellationToken: _cancellationToken);
        await using var commentTracker = _apiFixture.TrackForCleanup(newComment, c => c.Comments.DeleteAsync);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());

        var actualNewComment = Assert.Single(syncResponse.ProjectComments, c => c.Id == newComment.Id);
        Assert.Equal(newComment.Content, actualNewComment.Content);
        Assert.Equal(project.Id, actualNewComment.ProjectId);
        Assert.NotNull(actualNewComment.FileAttachment);
        Assert.Equal(uploadedAttachment.FileName, actualNewComment.FileAttachment.FileName);
        Assert.Equal(uploadedAttachment.FileType, actualNewComment.FileAttachment.FileType);
        Assert.False(string.IsNullOrWhiteSpace(actualNewComment.FileAttachment.FileUrl));


        // Step 3: Get comment by id.
        var actualComment = await _apiFixture.Client.Comments.GetAsync(newComment.Id.PersistentId, _cancellationToken);

        Assert.Equal(newComment.Content, actualComment.Content);
        Assert.Equal(project.Id, actualComment.ProjectId);
        Assert.NotNull(actualComment.FileAttachment);
        Assert.Equal(uploadedAttachment.FileName, actualComment.FileAttachment.FileName);


        // Step 4: Get comments by project.
        var query = new CommentsPaginationQuery(projectId: project.Id.PersistentId);

        var commentsResponse = await _apiFixture.Client.Comments.GetAsync(query, _cancellationToken);

        actualNewComment = Assert.Single(commentsResponse.Results, c => c.Id == newComment.Id);
        Assert.Equal(project.Id, actualNewComment.ProjectId);
        Assert.NotNull(actualNewComment.FileAttachment);
        Assert.Equal(uploadedAttachment.FileName, actualNewComment.FileAttachment.FileName);


        // Step 5: Delete project comment.
        syncResponse = await _apiFixture.Client.ExecuteTransactionAndSyncAsync(
            t => t.Comments.DeleteAsync(newComment.Id, _cancellationToken),
            [ResourceType.Comments],
            syncResponse.SyncToken,
            _cancellationToken);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        Assert.Contains(syncResponse.ProjectComments, c => c.Id == newComment.Id && c.IsDeleted == true);

        commentTracker.StopTracking();
        uploadTracker.StopTracking();
    }

    [Fact]
    public async Task UploadFile_Delete_Succeeds()
    {
        var project = await _apiFixture.GetPlaygroundProjectAsync();

        var fileName = $"StandaloneAttachment_{Guid.NewGuid():N}.png";
        var uploadFile = new UploadFile(TestData.Files.GreenPng10x10, fileName);


        // Step 1: Upload file.
        var uploadedAttachment = await _apiFixture.Client.Uploads.UploadAsync(uploadFile, project.Id.PersistentId, _cancellationToken);
        await using var uploadTracker = _apiFixture.TrackForCleanup(
            (c, ct) => c.Uploads.DeleteAsync(uploadedAttachment.FileUrl, ct),
            $"uploaded file with URL {uploadedAttachment.FileUrl}");

        Assert.Equal(fileName, uploadedAttachment.FileName);
        Assert.Equal("image/png", uploadedAttachment.FileType);
        Assert.False(string.IsNullOrWhiteSpace(uploadedAttachment.FileUrl));


        // Step 2: Delete uploaded file.
        await _apiFixture.Client.Uploads.DeleteAsync(uploadedAttachment.FileUrl, _cancellationToken);

        uploadTracker.StopTracking();
    }
}
