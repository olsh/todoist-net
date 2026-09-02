using System.Net;
using System.Text.Json;

namespace Todoist.Net.Tests;

[Trait(Constants.TraitName, Constants.UnitTraitValue)]
public class TodoistClientProtocolTests
{
    [Fact]
    public async Task GetSharedLabels_WithPaginationQuery_ReturnsPaginatedResponse_AndForwardsQueryParameters()
    {
        var restClient = new StubTodoistRestClient();
        restClient.RespondToGetJson(
            HttpStatusCode.OK,
            """
            {
                "results": ["sdk-tests", "shared-team"],
                "next_cursor": "cursor-2"
            }
            """);
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Get shared labels with cursor/limit pagination.
        var actualResponse = await todoistClient.Labels.GetSharedAsync(
            new SharedLabelsPaginationQuery(omitPersonal: true, cursor: "cursor-1", limit: 25),
            TestContext.Current.CancellationToken);


        // Step 2: Assert the paginated response and forwarded query parameters.
        Assert.Equal("labels/shared", restClient.LastResource);
        Assert.Equal("cursor-1", restClient.LastQueryParams["cursor"]);
        Assert.Equal("25", restClient.LastQueryParams["limit"]);
        Assert.Equal("true", restClient.LastQueryParams["omit_personal"]);

        Assert.Equal("cursor-2", actualResponse.NextCursor);
        Assert.True(actualResponse.HasMore);
        Assert.Equal(["sdk-tests", "shared-team"], actualResponse.Results);
    }

    [Fact]
    public async Task SyncCommands_WithTransactionResponseBody_PreservesOperationDetails()
    {
        var command = new Command(CommandType.AddLabel, EmptyCommand.Instance);
        var commandId = command.Uid;

        var restClient = new StubTodoistRestClient();
        restClient.RespondToPostJson(
            HttpStatusCode.OK,
            $$"""
            {
                "sync_status": {
                    "{{commandId}}": {
                        "operation": {
                            "id": "operation-123",
                            "operation_type": "label_add",
                            "status": "in_progress"
                        }
                    }
                },
                "temp_id_mapping": {},
                "sync_token": "sync-token-1",
                "full_sync": false
            }
            """);
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Execute a low-level sync command and capture its transaction response body.
        var actualResponse = await ((IAdvancedTodoistClient)todoistClient).SyncCommandsAsync(
            [command],
            includedResources: null,
            syncToken: null,
            throwOnError: false,
            cancellationToken: TestContext.Current.CancellationToken);


        // Step 2: Assert sync response metadata and operation details.
        Assert.Equal("sync", restClient.LastResource);
        Assert.True(restClient.LastFormParams.ContainsKey("commands"));

        var actualCommandResult = Assert.Single(actualResponse.SyncStatus, kvp => kvp.Key == commandId).Value;
        Assert.True(actualCommandResult.IsSuccess);
        Assert.NotNull(actualCommandResult.CommandBody);
        Assert.NotNull(actualCommandResult.CommandBody.Operation);
        Assert.Equal("operation-123", actualCommandResult.CommandBody.Operation.Id);
        Assert.Equal("label_add", actualCommandResult.CommandBody.Operation.OperationType);
        Assert.Equal("in_progress", actualCommandResult.CommandBody.Operation.Status);
        Assert.Empty(actualResponse.TempIdMappings);
        Assert.Equal("sync-token-1", actualResponse.SyncToken);
    }

    [Fact]
    public async Task SyncCommands_WhenCommandFails_ThrowsTodoistExceptionWithBodyDetails()
    {
        var command = new Command(CommandType.AddLabel, EmptyCommand.Instance);
        var commandId = command.Uid;

        var restClient = new StubTodoistRestClient();
        restClient.RespondToPostJson(
            HttpStatusCode.OK,
            $$"""
            {
                "sync_status": {
                    "{{commandId}}": {
                        "error_code": 42,
                        "error": "Command failed",
                        "error_tag": "INVALID_ARGUMENT_VALUE",
                        "http_code": 400,
                        "error_extra": {
                            "argument": "name",
                            "command": "label_add",
                            "expected": "non-empty"
                        }
                    }
                },
                "temp_id_mapping": {},
                "sync_token": "sync-token-1",
                "full_sync": false
            }
            """);
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Execute a failing low-level sync command.
        var exception = await Assert.ThrowsAsync<TodoistException>(() => ((IAdvancedTodoistClient)todoistClient).SyncCommandsAsync(
            [command],
            includedResources: null,
            syncToken: null,
            throwOnError: true,
            cancellationToken: TestContext.Current.CancellationToken));


        // Step 2: Assert error reporting details from the command body.
        Assert.StartsWith("Command failed", exception.Message);
        Assert.Equal(42, exception.Code);
        Assert.Equal("INVALID_ARGUMENT_VALUE", exception.ErrorTag);
        Assert.Equal(400, exception.HttpCode);
        Assert.NotNull(exception.ErrorExtra);
        Assert.Equal("name", exception.ErrorExtra.Argument);
        Assert.Equal("label_add", exception.ErrorExtra.Command);
        Assert.Equal("non-empty", exception.ErrorExtra.Expected);
    }

    [Fact]
    public async Task GetSharedLabels_WhenApiReturnsTodoistError_ThrowsTodoistExceptionWithErrorDetails()
    {
        var restClient = new StubTodoistRestClient();
        restClient.RespondToGetJson(
            HttpStatusCode.BadRequest,
            """
            {
                "error_code": 99,
                "error": "Bad request",
                "error_tag": "INVALID_REQUEST",
                "http_code": 400,
                "error_extra": {
                    "retry_after": 3,
                    "event_id": "evt-123"
                }
            }
            """);
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Execute a paginated request that returns an API error body.
        var exception = await Assert.ThrowsAsync<TodoistException>(() => todoistClient.Labels.GetSharedAsync(
            new SharedLabelsPaginationQuery(cursor: "cursor-1", limit: 10),
            TestContext.Current.CancellationToken));


        // Step 2: Assert Todoist-specific error reporting details.
        Assert.StartsWith("Bad request", exception.Message);
        Assert.Equal(99, exception.Code);
        Assert.Equal("INVALID_REQUEST", exception.ErrorTag);
        Assert.Equal(400, exception.HttpCode);
        Assert.NotNull(exception.ErrorExtra);
        Assert.Equal(3, exception.ErrorExtra.RetryAfter);
        Assert.Equal("evt-123", exception.ErrorExtra.EventId);
    }

    [Fact]
    public async Task AddProject_OutsideOfATransaction_ReturnsTheIdAssignedByTheApi()
    {
        var tempId = Guid.NewGuid();
        var project = new AddProject("Sdk tests") { Id = tempId };

        var restClient = new StubTodoistRestClient();
        restClient.RespondToPostJson(
            HttpStatusCode.OK,
            $$"""
            {
                "sync_status": {},
                "temp_id_mapping": { "{{tempId}}": "6X7rM8997g3RQmvh" },
                "sync_token": "sync-token-1",
                "full_sync": false
            }
            """);
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Add a project outside of a transaction, so the command is sent immediately.
        var actualId = await todoistClient.Projects.AddAsync(project, TestContext.Current.CancellationToken);


        // Step 2: Assert the persistent ID is returned instead of the temporary one.
        Assert.Equal("6X7rM8997g3RQmvh", actualId.PersistentId);
        Assert.Equal(project.Id, actualId);
    }

    [Fact]
    public async Task ExecuteTransactionAndSync_WithoutResourceTypes_SyncsAllResources()
    {
        var restClient = new StubTodoistRestClient();
        restClient.RespondToPostJson(
            HttpStatusCode.OK,
            """
            {
                "sync_status": {},
                "temp_id_mapping": {},
                "sync_token": "sync-token-1",
                "full_sync": true
            }
            """);
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Commit a transaction without naming any resource type.
        await todoistClient.ExecuteTransactionAndSyncAsync(
            transaction => transaction.Projects.AddAsync(new AddProject("Sdk tests")),
            resourceTypes: null,
            cancellationToken: TestContext.Current.CancellationToken);


        // Step 2: Assert all resources were requested back.
        Assert.Equal("[\"all\"]", restClient.LastFormParams["resource_types"]);
    }

    [Fact]
    public async Task GetSharedLabels_WhenErrorBodyIsNotATodoistError_ThrowsHttpRequestException()
    {
        var restClient = new StubTodoistRestClient();
        restClient.RespondToGetJson(HttpStatusCode.BadGateway, """{ "message": "Bad gateway" }""");
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Execute a request which fails with a JSON body that carries no Todoist error details.
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => todoistClient.Labels.GetSharedAsync(
            cancellationToken: TestContext.Current.CancellationToken));


        // Step 2: Assert the status code is reported instead of an empty Todoist exception.
        Assert.Equal(HttpStatusCode.BadGateway, exception.StatusCode);
    }

    [Fact]
    public async Task GetSharedLabels_WhenErrorBodyOmitsHttpCode_FallsBackToTheResponseStatusCode()
    {
        var restClient = new StubTodoistRestClient();
        restClient.RespondToGetJson(
            HttpStatusCode.TooManyRequests,
            """{ "error": "Rate limit exceeded", "error_tag": "TOO_MANY_REQUESTS" }""");
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Execute a request which fails with a partial Todoist error body.
        var exception = await Assert.ThrowsAsync<TodoistException>(() => todoistClient.Labels.GetSharedAsync(
            cancellationToken: TestContext.Current.CancellationToken));


        // Step 2: Assert the missing HTTP code is taken from the response itself.
        Assert.Equal("Rate limit exceeded", exception.Message);
        Assert.Equal(429, exception.HttpCode);
    }

    [Fact]
    public async Task GetCompletedTasksByCompletionDate_ForwardsTheFilterQueryParameter()
    {
        var restClient = new StubTodoistRestClient();
        restClient.RespondToGetJson(HttpStatusCode.OK, """{ "items": [], "next_cursor": null }""");
        using var todoistClient = new TodoistClient(restClient);

        var query = new CompletedTasksPaginationQuery(
            new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc))
        {
            FilterQuery = "today",
            FilterLang = "en"
        };


        // Step 1: Request completed tasks with a filter query.
        await todoistClient.Tasks.GetCompletedByCompletionDateAsync(query, TestContext.Current.CancellationToken);


        // Step 2: Assert the filter is sent under the name the API expects.
        Assert.Equal("tasks/completed/by_completion_date", restClient.LastResource);
        Assert.Equal("today", restClient.LastQueryParams["filter_query"]);
        Assert.Equal("en", restClient.LastQueryParams["filter_lang"]);
        Assert.False(restClient.LastQueryParams.ContainsKey("filter"));
    }

    [Fact]
    public async Task GetProjectData_ReadsTasksFromTheV1PropertyName()
    {
        var restClient = new StubTodoistRestClient();
        restClient.RespondToGetJson(
            HttpStatusCode.OK,
            """
            {
                "project": { "id": "6Crcmx9Mrpphc3Qc", "name": "Shopping" },
                "tasks": [ { "id": "6X7rM8997g3RQmvh", "content": "Buy Milk" } ],
                "sections": [],
                "subprojects": [],
                "collaborators": [],
                "collaborator_states": [],
                "comments_count": 3,
                "folder": null
            }
            """);
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Get the full data of a project.
        var actualProjectData = await todoistClient.Projects.GetDataAsync(
            "6Crcmx9Mrpphc3Qc",
            TestContext.Current.CancellationToken);


        // Step 2: Assert the tasks are read. This endpoint answers with the v1 name "tasks", not the
        // "items" the sync endpoint still uses, so binding it to "items" left `Tasks` silently null.
        Assert.Equal("projects/6Crcmx9Mrpphc3Qc/full", restClient.LastResource);

        var actualTask = Assert.Single(actualProjectData.Tasks);
        Assert.Equal("Buy Milk", actualTask.Content);
        Assert.Equal("Shopping", actualProjectData.Project.Name);
        Assert.Equal(3, actualProjectData.CommentsCount);
    }

    [Fact]
    public async Task ImportTemplateIntoProject_AddressesTheTemplateIdEndpoint()
    {
        var restClient = new StubTodoistRestClient();
        restClient.RespondToJsonPost(
            HttpStatusCode.OK,
            """
            {
                "status": "ok",
                "projects": [],
                "sections": [],
                "tasks": [],
                "comments": [],
                "project_notes": []
            }
            """);
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Import a saved template into an existing project.
        await todoistClient.Templates.ImportIntoProjectAsync(
            "6X7rM8997g3RQmvh",
            "123456",
            TestContext.Current.CancellationToken);


        // Step 2: Assert the v1 route is used. The v9 `templates/import_into_project` route was split
        // into a file variant and this template ID variant, and the retired route answers every
        // request with the same generic `NOT_FOUND` error, so only the URL itself pins this down.
        Assert.Equal("templates/import_into_project_from_template_id", restClient.LastResource);

        using var requestBody = JsonDocument.Parse(restClient.LastJsonContent);
        Assert.Equal("6X7rM8997g3RQmvh", requestBody.RootElement.GetProperty("project_id").GetString());
        Assert.Equal("123456", requestBody.RootElement.GetProperty("template_id").GetString());
    }

    [Fact]
    public async Task GetOrCreateEmail_ForATask_SendsTheTaskObjectType()
    {
        var restClient = new StubTodoistRestClient();
        restClient.RespondToJsonPut(
            HttpStatusCode.OK,
            """
            {
                "email": "sdk-tests@in.todoist.com"
            }
            """);
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Get or create the email of a task.
        var actualEmail = await todoistClient.Emails.GetOrCreateAsync(
            EmailObjectType.Task,
            "6X7rM8997g3RQmvh",
            TestContext.Current.CancellationToken);


        // Step 2: Assert the object type is sent as `task`. The legacy `item` value is still accepted
        // when disabling an email, but it is rejected here.
        Assert.Equal("emails", restClient.LastResource);

        using var requestBody = JsonDocument.Parse(restClient.LastJsonContent);
        Assert.Equal("task", requestBody.RootElement.GetProperty("obj_type").GetString());
        Assert.Equal("6X7rM8997g3RQmvh", requestBody.RootElement.GetProperty("obj_id").GetString());

        Assert.Equal("sdk-tests@in.todoist.com", actualEmail.Email);
    }

    [Fact]
    public async Task DisableEmail_WithAnEmptyResponseBody_Succeeds()
    {
        var restClient = new StubTodoistRestClient();
        restClient.RespondToDeleteWithEmptyBody(HttpStatusCode.NoContent);
        using var todoistClient = new TodoistClient(restClient);


        // Step 1: Disable an object email, which the API answers without a body.
        await todoistClient.Emails.DisableAsync(
            EmailObjectType.Project,
            "6X7rM8997g3RQmvh",
            TestContext.Current.CancellationToken);


        // Step 2: Assert the request was addressed correctly.
        Assert.Equal("emails", restClient.LastResource);
        Assert.Equal("project", restClient.LastQueryParams["obj_type"]);
        Assert.Equal("6X7rM8997g3RQmvh", restClient.LastQueryParams["obj_id"]);
    }

    [Fact]
    public async Task AddCommentToTask_WithoutAComment_ThrowsArgumentNullException()
    {
        using var todoistClient = new TodoistClient(new StubTodoistRestClient());

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => todoistClient.Comments.AddToTaskAsync(
            null!,
            "6X7rM8997g3RQmvh",
            TestContext.Current.CancellationToken));

        Assert.Equal("comment", exception.ParamName);
    }

    [Fact]
    public async Task AddWorkspaceFolder_WithoutAFolder_ThrowsArgumentNullException()
    {
        using var todoistClient = new TodoistClient(new StubTodoistRestClient());

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => todoistClient.Workspaces.AddFolderAsync(
            "6X7rM8997g3RQmvh",
            null!,
            TestContext.Current.CancellationToken));

        Assert.Equal("folder", exception.ParamName);
    }
}