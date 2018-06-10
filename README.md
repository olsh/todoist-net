# Todoist.Net
[![Build status](https://ci.appveyor.com/api/projects/status/r5ylbxtpjya9ayk2?svg=true)](https://ci.appveyor.com/project/olsh/todoist-net)
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=todoist-net&metric=alert_status)](https://sonarcloud.io/dashboard?id=todoist-net)
[![codecov](https://codecov.io/gh/olsh/todoist-net/branch/master/graph/badge.svg)](https://codecov.io/gh/olsh/todoist-net)
[![NuGet](https://img.shields.io/nuget/v/Todoist.Net.svg)](https://www.nuget.org/packages/Todoist.Net/)

A [Todoist API v7](https://developer.todoist.com/) client for .NET.
## Installation

The library is available as a [Nuget package](https://www.nuget.org/packages/Todoist.Net/).
```
Install-Package Todoist.Net
```

## Get started

### Creating Todoist client

With token (preferred way).
```csharp
ITodoistClient client = new TodoistClient("API token");
```

With email and password.
```csharp
ITodoistTokenlessClient tokenlessClient = new TodoistTokenlessClient();
ITodoistClient client = await tokenlessClient.LoginAsync("email", "password");
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

## Known issues
At the moment implemented all APIs except [Business](https://developer.todoist.com/?shell#business).
