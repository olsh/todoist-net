# Todoist.Net
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=todoist-net&metric=alert_status)](https://sonarcloud.io/dashboard?id=todoist-net)
[![NuGet](https://img.shields.io/nuget/v/Todoist.Net.svg)](https://www.nuget.org/packages/Todoist.Net/)

A [Todoist Sync API](https://developer.todoist.com/sync/v9/) client for .NET.
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
var quickAddItem = new QuickAddItem("Task title @Label1 #Project1 +ExampleUser");
var task = await client.Items.QuickAddAsync(quickAddItem);
```

### Simple API calls
```csharp
// Get all resources (labels, projects, tasks, notes etc.).
var resources = await client.GetResourcesAsync();

// Get only projects and labels.
var projectsAndLabels = await client.GetResourcesAsync(ResourceType.Projects, ResourceType.Labels);

// Get only projects.
var projectsOnly = await client.GetResourcesAsync(ResourceType.Projects);

// Alternatively you can use this API to get projects.
var projects = await client.Projects.GetAsync();

// Add a task with a note.
var taskId = await client.Items.AddAsync(new Item("New task"));
await client.Notes.AddToItemAsync(new Note("Task description"), taskId);
```

### Transactions (Batching)
Batching: reading and writing of multiple resources can be done in a single HTTP request.

Add a new project, task and note in one request.
```csharp
// Create a new transaction.
var transaction = client.CreateTransaction();

// These requests are queued and will be executed later.
var projectId = await transaction.Project.AddAsync(new Project("New project"));
var taskId = await transaction.Items.AddAsync(new Item("New task", projectId));
await transaction.Notes.AddToItemAsync(new Note("Task description"), taskId);

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
var task = new UpdateItem("TASK_ID");
task.Unset(t => t.DueDate);

await client.Items.UpdateAsync(task);
```
