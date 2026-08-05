using System.Net;

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
        Assert.Equal("Command failed", exception.Message);
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
        Assert.Equal("Bad request", exception.Message);
        Assert.Equal(99, exception.Code);
        Assert.Equal("INVALID_REQUEST", exception.ErrorTag);
        Assert.Equal(400, exception.HttpCode);
        Assert.NotNull(exception.ErrorExtra);
        Assert.Equal(3, exception.ErrorExtra.RetryAfter);
        Assert.Equal("evt-123", exception.ErrorExtra.EventId);
    }
}