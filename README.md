# Todoist.Net
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=todoist-net&metric=alert_status)](https://sonarcloud.io/dashboard?id=todoist-net)
[![NuGet](https://img.shields.io/nuget/v/Todoist.Net.svg)](https://www.nuget.org/packages/Todoist.Net/)

A [Todoist API](https://developer.todoist.com) client for .NET.

> **Upgrading from 10.x?** Version 11.0.0 moves to the unified Todoist API v1 and renames much of
> the public surface (`Items` -> `Tasks`, `Notes` -> `Comments`, and more). See the
> [11.0.0 release notes](https://github.com/olsh/todoist-net/releases/tag/11.0.0) for the full
> migration table.

## Installation

The library is available as a [Nuget package](https://www.nuget.org/packages/Todoist.Net/).
```
Install-Package Todoist.Net
```

## Get started

### Creating Todoist client

```csharp
ITodoistClient client = new TodoistClient("API token");
```

### OAuth with refresh tokens

Applications authorized through [Todoist OAuth](https://developer.todoist.com/api/v1/#tag/Authorization/OAuth) get
an access token which expires after an hour, and a refresh token to get a new one. The client refreshes the tokens on
its own before they expire, or when Todoist rejects the access token, and sends the request again.

Todoist rotates the refresh token on every refresh, so the previous one stops working. Store the tokens passed to
the callback every time it is called, or the user will have to authorize the application again.
The OAuth types live in the `Todoist.Net.OAuth` namespace.

```csharp
var options = new TodoistOAuthOptions
{
    ClientId = "CLIENT_ID",
    ClientSecret = "CLIENT_SECRET"
};
var tokens = new TodoistTokens(accessToken, refreshToken, expiresAt);

using var client = new TodoistClient(options, tokens, refreshedTokens => tokenStore.SaveAsync(userId, refreshedTokens));
```

When two clients of the same user refresh with the same refresh token at once, Todoist gives the new refresh token to
the first one only. The second one gets the access token alone and does not call the callback, so it never overwrites
the stored refresh token.

Create clients from the stored tokens when you need them instead of keeping several long-lived clients for the same
user. A client holding a refresh token another client has already used, which refreshes more than a minute later,
looks like a replay attack to Todoist: it revokes all the tokens of the user, who has to authorize the application again.

The tokens can also be refreshed ahead of time, and the access token revoked (revoking requires the client secret):

```csharp
var refreshedTokens = await client.RefreshTokensAsync();

await client.RevokeTokensAsync();
```

Todoist can revoke access tokens only, so the refresh token keeps working after `RevokeTokensAsync`. Delete the stored
tokens to give up the access for good; the authorization ends when the user removes the application in the Todoist settings.

### Dependency injection

`AddTodoistClient` registers `ITodoistClientFactory`, which creates clients on top of `IHttpClientFactory`.
Configure the OAuth application to create clients with the tokens of each user through `ITodoistOAuthClientFactory`:

```csharp
services.AddTodoistClient(options =>
{
    options.ClientId = configuration["Todoist:ClientId"];
    options.ClientSecret = configuration["Todoist:ClientSecret"];
});
```

```csharp
public class TodoistSync(ITodoistOAuthClientFactory clientFactory, ITokenStore tokenStore)
{
    public async Task SyncAsync(string userId)
    {
        var tokens = await tokenStore.GetAsync(userId);

        // The callback captures the user, so the refreshed tokens are stored for the right one.
        using var client = clientFactory.CreateClient(tokens, refreshedTokens => tokenStore.SaveAsync(userId, refreshedTokens));

        var projects = await client.Projects.GetAsync();
    }
}
```

Clients created with an API token keep working as before:

```csharp
using var client = serviceProvider.GetRequiredService<ITodoistClientFactory>().CreateClient("API token");
```

### Quick add

Implementation of the Quick Add Task available in the official clients.
```csharp
var quickAddTask = new QuickAddTask("Task title @Label1 #Project1 +ExampleUser");
var task = await client.Tasks.QuickAddAsync(quickAddTask);
```

### Simple API calls
```csharp
// Get all resources (labels, projects, tasks, comments etc.).
var resources = await client.SyncResourcesAsync();

// Get only projects and labels.
var projectsAndLabels = await client.SyncResourcesAsync(new[] { ResourceType.Projects, ResourceType.Labels });

// Get only projects.
var projectsOnly = await client.SyncResourcesAsync(new[] { ResourceType.Projects });

// Alternatively you can use this API to get projects.
var projects = await client.Projects.GetAsync();

// Add a task with a comment.
var taskId = await client.Tasks.AddAsync(new AddTask("New task"));
await client.Comments.AddToTaskAsync(new Comment("Task description"), taskId);
```

### Transactions (Batching)
Batching: reading and writing of multiple resources can be done in a single HTTP request.

Add a new project, task and comment in one request.
```csharp
// Create a new transaction.
var transaction = client.CreateTransaction();

// These requests are queued and will be executed later.
var projectId = await transaction.Projects.AddAsync(new AddProject("New project"));
var taskId = await transaction.Tasks.AddAsync(new AddTask("New task", projectId));
await transaction.Comments.AddToTaskAsync(new Comment("Task description"), taskId);

// Execute all the requests in the transaction in a single HTTP request.
await transaction.CommitAsync();

```

### Sending null values when updating entities.
When updating entities, **Todoist API** only updates properties included in the request body, using a `PATCH` request style.
That's why all properties with `null` values are not included by default, to allow updating without fetching the entity first,
since including `null` properties will update them to `null`.

However, if you want to intentionally send a `null` value to the API, you need to use the `Unset` extension method, for example:
```csharp
// This code removes a task's due date.
var task = new UpdateTask("TASK_ID");
task.Unset(t => t.DueDate);

await client.Tasks.UpdateAsync(task);
```
