# Todoist.Net
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=todoist-net&metric=alert_status)](https://sonarcloud.io/dashboard?id=todoist-net)
[![NuGet](https://img.shields.io/nuget/v/Todoist.Net.svg)](https://www.nuget.org/packages/Todoist.Net/)

A Todoist API client for .NET.

## Important: Todoist API migration and breaking changes

Todoist old APIs (Sync v9 / REST v2 legacy paths) are deprecated in favor of the unified Todoist API v1. You should update to the latest `Todoist.Net` package version and migrate your code accordingly.

This branch includes compatibility updates for `/api/v1` and introduces breaking changes to align with the new API behavior.

### Breaking changes summary

- IDs are now treated as opaque string IDs in more places; do not assume numeric IDs.
- Filters API surface changed: single-filter read by ID (`Filters.GetAsync(id)`) is removed; use `Filters.GetAsync()` and select the filter from the returned collection.
- Upload listing endpoint behavior is constrained by current API behavior; use upload/delete workflows accordingly.
- Activity and completed-task payload handling was updated for v1 response shapes.

### Migration notes

- Review Todoist's migration docs and endpoint renames for `/api/v1`.
- Rebuild and rerun tests after migration because service signatures and payload expectations changed.

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

### Quick add

Implementation of the Quick Add Task available in the official clients.
```csharp
var quickAddTask = new QuickAddTask("Task title @Label1 #Project1 +ExampleUser");
var task = await client.Tasks.QuickAddAsync(quickAddTask);
```

### Simple API calls
```csharp
// Get all resources (labels, projects, tasks, comments etc.).
var resources = await client.GetResourcesAsync();

// Get only projects and labels.
var projectsAndLabels = await client.GetResourcesAsync(ResourceType.Projects, ResourceType.Labels);

// Get only projects.
var projectsOnly = await client.GetResourcesAsync(ResourceType.Projects);

// Alternatively you can use this API to get projects.
var projects = await client.Projects.GetAsync();

// Add a task with a comment.
var taskId = await client.Tasks.AddAsync(new AddTask("New task"));
await client.Comments.AddToTaskAsync(new Comment("Task description"), taskId);
```

### Transactions (Batching)
Batching: reading and writing of multiple resources can be done in a single HTTP request.

Add a new project, task and note in one request.
```csharp
// Create a new transaction.
var transaction = client.CreateTransaction();

// These requests are queued and will be executed later.
var projectId = await transaction.Project.AddAsync(new Project("New project"));
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
