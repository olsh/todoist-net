# Todoist.Net
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=todoist-net&metric=alert_status)](https://sonarcloud.io/dashboard?id=todoist-net)
[![NuGet](https://img.shields.io/nuget/v/Todoist.Net.svg)](https://www.nuget.org/packages/Todoist.Net/)

A [Todoist API](https://developer.todoist.com) client for .NET.

> 💡 **Upgrading from 10.x?** Version 11.0.0 moves to the unified Todoist API v1 and renames much of the public surface (`Items` -> `Tasks`, `Notes` -> `Comments`, and more).
> See the [11.0.0 release notes](https://github.com/olsh/todoist-net/releases/tag/11.0.0) for the full migration table.

## Features

- **Complete Unified API v1 coverage** — 20 domain services: tasks, projects, sections, labels, comments, reminders, filters, workspaces, workspace filters, view options, calendars, notifications, sharing, activity log, backups, uploads, emails, templates, user profile, and legacy ID mappings.
- **Dual API style** — resource-oriented REST endpoints for reads, plus the sync/commands engine for writes and incremental synchronization.
- **Transactions (batching)** — queue multiple commands and execute them in a single HTTP request, with automatic temp-ID resolution.
- **Cursor-based pagination** — a uniform `PaginatedResponse<T>` model (`Results`, `NextCursor`, `HasMore`) across all list endpoints.
- **OAuth 2.0 support** — automatic access-token refresh (proactive and on `401`), token revocation, and refresh callbacks.
- **Dependency injection ready** — `IServiceCollection` integration via `IHttpClientFactory` (available on the `netstandard2.0` target).
- **Cross-platform targets** — `netstandard2.0` and `net462`.

## Installation

The library is available as a [NuGet package](https://www.nuget.org/packages/Todoist.Net/).

```powershell
# Package Manager
Install-Package Todoist.Net

# .NET CLI
dotnet add package Todoist.Net
```

## Get started

### Creating a client

Use your API token from the [Todoist integrations settings](https://app.todoist.com/app/settings/integrations/developer):

```csharp
using Todoist.Net;

ITodoistClient client = new TodoistClient("API token");

// Optionally, behind a proxy:
// IWebProxy proxy = ...;
// ITodoistClient client = new TodoistClient("API token", proxy);
```

### OAuth 2.0 with automatic token refresh

For OAuth applications, provide your client credentials and the user's tokens. The client refreshes the access token automatically when it is about to expire, and also retries a request once after refreshing on a `401 Unauthorized` response:

```csharp
var credentials = new ClientCredentials("client-id", "client-secret");
var tokens = new TodoistTokens("access-token", "refresh-token", expirationTimeUtc);

var authContext = new TodoistAuthenticationContext(credentials, tokens, onRefresh: async (res, state, ct) =>
{
    // Persist the rotated tokens (Todoist rotates the refresh token on each refresh).
    await SaveTokensAsync(res.AccessToken, res.RefreshToken);
});

ITodoistClient client = new TodoistClient(authContext);
```

You can also control the token lifecycle manually (supported only by OAuth-based clients):

```csharp
await client.RefreshTokensAsync(); // force a token refresh
await client.RevokeTokensAsync();  // log the user out and invalidate both tokens
```

### Dependency injection

For ASP.NET Core and other DI-enabled applications (`netstandard2.0` target only):

```csharp
builder.Services.AddTodoistClient((serviceProvider, options) =>
{
    // Required to create OAuth-refreshable clients from TodoistTokens:
    options.Credentials.ClientId = builder.Configuration["Todoist:ClientId"];
    options.Credentials.ClientSecret = builder.Configuration["Todoist:ClientSecret"];

    options.OnRefresh = async (res, state, ct) =>
    {
        // You can utilize the state parameter to pass contextual information to the refresh callback, for example the user ID.
        var userId = state as string;
        
        // Todoist options configuration action provides an IServiceProvider that you can use to resolve services in the callback.
        await using var scope = serviceProvider.CreateAsyncScope();

        // Persist the rotated tokens (Todoist rotates the refresh token on each refresh).
        await scope.ServiceProvider
            .GetRequiredService<IUserTokenRepository>()
            .SaveTodoistTokensAsync(userId, res.AccessToken, res.RefreshToken, res.ExpiresIn, ct);
    };
});
```

```csharp
public class MyService
{
    private readonly IUserTokenRepository _tokenRepository;
    private readonly ITodoistClientFactory _clientFactory;

    public MyService(IUserTokenRepository tokenRepository, ITodoistClientFactory clientFactory) 
    {
        _tokenRepository = tokenRepository;
        _clientFactory = clientFactory;
    }

    public async Task DoWorkAsync(string userId, CancellationToken cancellationToken = default)
    {
        TodoistTokens todoistTokens = await _tokenRepository.GetTodoistTokensAsync(userId, cancellationToken);

        // Here, the userId is passed as the refreshState parameter to the factory method. 
        // It will be available in the OnRefresh callback when the access token is refreshed.
        using var client = _clientFactory.CreateClient(todoistTokens, refreshState: userId);

        var projectsPage = await client.Projects.GetAsync(cancellationToken: cancellationToken);
        // ...
    }
}
```

`AddTodoistClient` returns an `IHttpClientBuilder`, so you can chain additional handlers or resilience policies onto the named `HttpClient`.

## Tasks

```csharp
// Add a task (goes to the user's Inbox by default).
var task = new AddTask("Prepare release notes")
{
    Description = "Include migration notes",
    Priority = Priority.Priority1, // p1/urgent in the official clients
    DueDate = DueDate.FromText("next monday"),
    Duration = new Duration(60, DurationUnit.Minute), // Premium
    Labels = ["releases"]
};

await client.Tasks.AddAsync(task);
// The entity instance is updated in place with the persistent ID:
Console.WriteLine(task.Id.PersistentId);

// Get a task by ID.
var taskInfo = await client.Tasks.GetAsync(task.Id.PersistentId);

// Update a task (only non-null properties are sent — PATCH semantics).
await client.Tasks.UpdateAsync(new UpdateTask(task.Id)
{
    Content = "Updated title",
    Priority = Priority.Priority2
});

// Complete / uncomplete.
await client.Tasks.CloseAsync(task.Id);
await client.Tasks.UncompleteAsync(task.Id);

// Delete.
await client.Tasks.DeleteAsync(task.Id);
```

### Quick Add

Implementation of the Quick Add Task available in the official clients — natural-language parsing of due dates, labels, projects, and assignees:

```csharp
var quickAddTask = new QuickAddTask("Task title tomorrow @Label1 #Project1 +ExampleUser")
{
    Comment = "Note attached to the task",
    Reminder = "tomorrow at 9am"
};
await client.Tasks.QuickAddAsync(quickAddTask);
```

### Completing tasks: the three flavors

- `CloseAsync(id)` — mirrors what the official clients do: a regular task is completed and moved to history, a subtask is checked (marked done, not moved to history), and a recurring task is moved forward to its next occurrence.
- `CompleteAsync(new CompleteTaskArgument(id, dateCompleted))` — full control over completion, optionally without moving the task to history.
- `CompleteRecurringAsync(new CompleteRecurringTaskArgument(id, dueDate, isForward, resetSubtasks))` — advanced completion of recurring tasks.

### Moving and reordering

```csharp
await client.Tasks.MoveAsync(MoveTaskArgument.CreateMoveToProject(taskId, projectId));
await client.Tasks.MoveAsync(MoveTaskArgument.CreateMoveToSection(taskId, sectionId));
await client.Tasks.MoveAsync(MoveTaskArgument.CreateMoveToParent(taskId, parentTaskId));
```

### Listing, filtering, and completed tasks

All list endpoints are cursor-paginated (see [Pagination](#pagination)):

```csharp
// Active tasks, optionally scoped to a project, section, parent, or label.
var page = await client.Tasks.GetAsync(new TasksPaginationQuery(limit: 50)
{
    ProjectId = projectId,
    Label = "releases"
});

// Tasks matching a filter query.
var filtered = await client.Tasks.GetByFilterAsync(new TasksFilterQuery("today | overdue", Language.English));

// Completed tasks (Premium), by completion date or by due date range.
var completed = await client.Tasks.GetCompletedByCompletionDateAsync(
    new CompletedTasksPaginationQuery(since: DateTime.UtcNow.AddDays(-7), until: DateTime.UtcNow));
```

## Due dates, priorities, durations, and deadlines

```csharp
// Natural language (recurring supported), optionally with a language hint.
DueDate.FromText("every friday", Language.English);

// Full-day date.
DueDate.CreateFullDay(new DateTime(2026, 12, 25));

// Floating date-time (always interpreted in the user's timezone).
DueDate.CreateFloating(DateTime.Now.AddDays(1));

// Fixed time zone (IANA time zone name).
DueDate.CreateFixedTimeZone(DateTime.UtcNow.AddHours(2), "America/New_York");
```

> **Priority numbering:** the API inverts the UI numbering — `Priority.Priority1` (value `4`) is *urgent* (p1) in the official clients, while `Priority.Priority4` (value `1`) is natural priority.

- `Duration` (Premium): `new Duration(60, DurationUnit.Minute)` — units are `DurationUnit.Minute` and `DurationUnit.Day`.
- `Deadline`: `new Deadline(new DateTime(2026, 12, 31))` — date-only task deadlines.

## Projects

```csharp
// Add a project (optionally nested, or inside a workspace/folder).
var project = new AddProject("My project")
{
    Color = Color.Red,
    IsFavorite = true,
    ViewStyle = ViewOptionsStyle.Board
};
await client.Projects.AddAsync(project);

// Update, archive (Premium), unarchive, delete.
await client.Projects.UpdateAsync(new UpdateProject(project.Id) { Name = "Renamed" });
await client.Projects.ArchiveAsync(project.Id);
await client.Projects.UnarchiveAsync(project.Id);
await client.Projects.DeleteAsync(project.Id); // deletes all descendants too

// Reads: paginated lists, single fetch, and full project data.
var projectsPage = await client.Projects.GetAsync(new PaginationQuery(limit: 50));
var archivedPage = await client.Projects.GetArchivedAsync();
var projectInfo = await client.Projects.GetAsync(project.Id.PersistentId);
var projectData = await client.Projects.GetDataAsync(project.Id.PersistentId); // incl. uncompleted tasks

// Search, collaborators, and permissions.
var found = await client.Projects.SearchAsync(new PaginatedSearchQuery("release"));
var collaborators = await client.Projects.GetCollaboratorsAsync(project.Id.PersistentId);
var permissions = await client.Projects.GetPermissionsAsync();

// Workspace hierarchy management.
await client.Projects.MoveToWorkspaceAsync(new MoveProjectToWorkspaceArgument(project.Id, workspaceId));
await client.Projects.MoveToPersonalAsync(project.Id);
await client.Projects.LeaveAsync(project.Id);
var joined = await client.Projects.JoinAsync("project-id");
```

## Sections

```csharp
// Add a section to a project.
var section = new AddSection("In Progress", projectId);
await client.Sections.AddAsync(section);

// Add a task to the section.
await client.Tasks.AddAsync(new AddTask("Section task")
{
    ProjectId = projectId,
    SectionId = section.Id
});

// Update, move to another project, archive/unarchive, delete.
await client.Sections.UpdateAsync(new UpdateSection(section.Id, "Done"));
await client.Sections.ArchiveAsync(section.Id); // archives all descendant tasks too
```

Sections also support paginated reads (`GetAsync` with `SectionsPaginationQuery`), search (`SearchAsync`), reordering (`ReorderAsync`), and get-by-ID.

## Labels and shared labels

```csharp
// Create a label and use it on a task.
var label = new Label("urgent", Color.Red) { IsFavorite = true };
await client.Labels.AddAsync(label);

var task = new AddTask("Important task");
task.Labels.Add("urgent");
await client.Tasks.AddAsync(task);

// Search personal labels, or list shared label names from active tasks.
var found = await client.Labels.SearchAsync(new PaginatedSearchQuery("urg"));
var sharedNames = await client.Labels.GetSharedAsync();

// Shared-label maintenance.
await client.Labels.RenameSharedAsync("urgent", "critical");
await client.Labels.DeleteSharedAsync("critical");

// Delete a personal label but keep it on tasks as a shared label.
await client.Labels.DeleteAsync(label.Id, keepAsShared: true);
```

## Comments and attachments

```csharp
// Add a comment to a task or a project.
await client.Comments.AddToTaskAsync(new Comment("Task description"), taskId);
await client.Comments.AddToProjectAsync(new Comment("Project note"), projectId);

// Upload a file and attach it to a comment.
await using var stream = File.OpenRead("receipt.png");
var attachment = await client.Uploads.UploadAsync(new UploadFile(stream, "receipt.png"), projectId);

var comment = new Comment("See the attached receipt")
{
    FileAttachment = new FileAttachment(attachment.FileName, attachment.FileUrl)
};
await client.Comments.AddToTaskAsync(comment, taskId);
```

The uploads service intentionally exposes only upload and delete operations (`client.Uploads.UploadAsync`, `client.Uploads.DeleteAsync(fileUrl)`), aligned with the API v1 surface.

## Pagination

All REST list endpoints use cursor-based pagination with a uniform response shape — `Results`, `NextCursor`, and the computed `HasMore` flag:

```csharp
var query = new PaginationQuery(limit: 50);
while (true)
{
    var page = await client.Projects.GetAsync(query);
    foreach (var projectInfo in page.Results)
    {
        Console.WriteLine(projectInfo.Name);
    }

    if (!page.HasMore)
    {
        break;
    }

    query = new PaginationQuery(cursor: page.NextCursor, limit: 50);
}
```

Each endpoint has a dedicated query model: `PaginationQuery`, `PaginatedSearchQuery`, `TasksPaginationQuery`, `TasksFilterQuery`, `CompletedTasksPaginationQuery`, `CommentsPaginationQuery`, `SectionsPaginationQuery`, `SharedLabelsPaginationQuery`, `LogsPaginationQuery`, and `WorkspaceUsersQuery`.

## Syncing resources

The sync engine returns all resources, or only what changed since the previous sync:

```csharp
// Full sync of every resource type.
var resources = await client.SyncResourcesAsync();

// Only projects and labels.
var subset = await client.SyncResourcesAsync(new[] { ResourceType.Projects, ResourceType.Labels });

// Incremental sync — only changes since the previous call.
var changes = await client.SyncResourcesAsync(new[] { ResourceType.Tasks }, resources.SyncToken);

// Per-service sync wrappers are also available, e.g.:
var projects = await client.Projects.SyncAsync(resources.SyncToken);
```

## Transactions (batching)

Reading and writing multiple resources can be done in a single HTTP request. Add a new project, task, and comment in one request:

```csharp
// Create a new transaction.
var transaction = client.CreateTransaction();

// These requests are queued and will be executed later. 
// Upon queuing, the entities are assigned temporary IDs to allow referencing them in subsequent commands.
var project = new AddProject("New project");
var tempProjectId = await transaction.Projects.AddAsync(project);
var tempTaskId = await transaction.Tasks.AddAsync(new AddTask("New task") { ProjectId = tempProjectId });
await transaction.Comments.AddToTaskAsync(new Comment("Task description"), tempTaskId);

// Execute all the queued commands in a single HTTP request.
var result = await transaction.CommitAsync();

// Temp IDs are resolved automatically:
Console.WriteLine(project.Id.PersistentId);              // the argument is updated in place
Console.WriteLine(result.TempIdMappings[tempTaskId.TempId]); // or via the mapping table
```

References between queued commands (for example, a task pointing at a not-yet-created project via its temp ID) are resolved automatically after the commit.

You can also commit and sync the affected resources in the same request, or skip the explicit transaction object entirely:

```csharp
// Sync resources after committing the transaction in one API call.
var result = await transaction.CommitAndSyncAsync(new[] { ResourceType.Projects, ResourceType.Tasks }, syncToken);

// Or use the ExecuteTransactionAndSyncAsync helper to create, commit, and sync in one call:
var syncResponse = await client.ExecuteTransactionAndSyncAsync(async t =>
{
    await t.Labels.AddAsync(new Label("urgent", Color.Red));
    await t.Tasks.AddAsync(new AddTask("Urgent task"));
},
new[] { ResourceType.Labels, ResourceType.Tasks }, syncToken);
```

Transactions do not throw on individual command failures — inspect the per-command results instead:

```csharp
foreach (var (commandId, commandResult) in result.SyncStatus)
{
    if (!commandResult.IsSuccess)
    {
        Console.WriteLine($"Command {commandId} failed: {commandResult.CommandBody}");
    }
}
```

## Sending null values when updating entities

When updating entities, the **Todoist API** only updates properties included in the request body, using a `PATCH` request style. That's why all properties with `null` values are excluded by default — this allows updating without fetching the entity first, since including `null` properties would update them to `null`.

However, if you want to intentionally send a `null` value to the API, use the `Unset` extension method:

```csharp
// This code removes a task's due date.
var task = new UpdateTask("TASK_ID");
task.Unset(t => t.DueDate);

await client.Tasks.UpdateAsync(task);
```

`Unset` works on every entity implementing the unsettable-properties pattern (for example `UpdateTask`, `UpdateProject`, and `Comment`).

## Premium features

Some features require a Todoist Premium subscription:

| Feature | Service |
|---------|---------|
| Reminders | `client.Reminders` |
| Filters | `client.Filters` |
| Templates | `client.Templates` |
| Activity log | `client.Activity` |
| Completed tasks | `client.Tasks.GetCompletedByCompletionDateAsync` / `GetCompletedByDueDateAsync` |
| Task duration | `Duration` property on tasks |
| Project archiving | `client.Projects.ArchiveAsync` / `UnarchiveAsync` |

```csharp
// Reminders (Premium) — absolute, relative, and location-based.
await client.Reminders.AddAsync(new AddReminder(taskId, ReminderType.Absolute)
{
    DueDate = DueDate.CreateFloating(DateTime.Now.AddHours(1))
});
await client.Reminders.AddAsync(new AddReminder(taskId, ReminderType.Relative) { MinuteOffset = 30 });

// Filters (Premium).
await client.Filters.AddAsync(new Filter("Urgent & today", "p1 & today") { Color = Color.BerryRed });

// Templates (Premium) — export a project, or import/create from a template.
var csv = await client.Templates.ExportAsFileAsync(projectId);
var importResult = await client.Templates.ImportIntoProjectAsync(projectId, "template-id");

// Activity log (Premium) — paginated, with rich filtering via LogsPaginationQuery.
var logsPage = await client.Activity.GetAsync();
```

## Collaboration: sharing and notifications

```csharp
// Share a project with a collaborator role.
await client.Sharing.ShareProjectAsync(projectId, "teammate@example.com", ProjectCollaboratorRole.ReadWrite);

// Invitations are accepted or rejected using the ID and secret from the live notification.
await client.Sharing.AcceptInvitationAsync(invitationId, invitationSecret);
await client.Sharing.DeleteCollaboratorAsync(projectId, "teammate@example.com");

// Live notifications.
var notifications = await client.Notifications.SyncAsync();
await client.Notifications.MarkAllReadAsync();
await client.Notifications.MarkUnreadAsync(new[] { "notification-id" });
```

## Workspaces

```csharp
var workspaces = await client.Workspaces.SyncAsync();
var workspaceUsers = await client.Workspaces.GetUsersAsync(new WorkspaceUsersQuery(workspaceId: 123456));
var activeProjects = await client.Workspaces.GetActiveProjectsAsync(workspaceId);
var planDetails = await client.Workspaces.GetPlanDetailsAsync(workspaceId);

// Invitations and joining.
var pendingInvitations = await client.Workspaces.GetInvitationDetailsAsync(workspaceId);
var joinResult = await client.Workspaces.JoinByCodeAsync("invite-code");
```

The workspaces service also covers workspace CRUD, member and role management, project sort preferences, workspace folders, and workspace logo upload — all available on transactions as well. Workspace filters (`client.WorkspaceFilters`) provide CRUD, ordering, and sync for workspace-scoped filters.

## User profile, settings, and other services

```csharp
// Current user info and productivity stats.
var userInfo = await client.User.GetInfoAsync();
var userStats = await client.User.GetStatsAsync();

// Backups (Todoist creates daily backups; pass an MFA token for MFA-enabled accounts).
var backups = await client.Backups.GetAsync();

// Email gateway — get (or create) the address that posts comments to a task by email.
var objectEmail = await client.Emails.GetOrCreateAsync(EmailObjectType.Item, taskId);
await client.Emails.DisableAsync(EmailObjectType.Item, taskId);

// Calendars (read-only, sync-based).
var calendars = await client.Calendars.SyncAsync();
var calendarAccounts = await client.Calendars.SyncAccountsAsync();

// View options (sync + set/delete per view scope).
var viewOptions = await client.ViewOptions.SyncAsync();

// ID mappings between legacy (v9) and unified (v1) identifiers.
var mappings = await client.Ids.GetMappingsAsync(MappingObjectName.Projects, new[] { "old-id-1", "old-id-2" });
foreach (var mapping in mappings)
{
    Console.WriteLine($"{mapping.OldId} -> {mapping.NewId}");
}
```

## Available services

| Service | Description |
|---------|-------------|
| `client.Tasks` | Tasks management, quick add, filtering, completed tasks |
| `client.Projects` | Projects management, archiving, collaborators, workspace moves |
| `client.Sections` | Sections management |
| `client.Labels` | Labels management, shared labels |
| `client.Comments` | Comments/notes management with attachments |
| `client.Uploads` | File uploads and deletion |
| `client.Reminders` | Reminders management (Premium) |
| `client.Filters` | Filters management (Premium) |
| `client.Templates` | Project templates export/import (Premium) |
| `client.Activity` | Activity log (Premium) |
| `client.User` | User info, settings, karma goals, stats, plan limits |
| `client.Sharing` | Project sharing and invitations |
| `client.Notifications` | Live notifications and notification settings |
| `client.Workspaces` | Workspaces, members, folders, invitations, plan details |
| `client.WorkspaceFilters` | Workspace filters management |
| `client.ViewOptions` | View options per view scope |
| `client.Calendars` | Calendars and calendar accounts (read-only) |
| `client.Backups` | Account backups |
| `client.Emails` | Email gateway settings |
| `client.Ids` | Legacy v9 ↔ v1 ID mappings |

## Error handling

Direct (non-transactional) command calls throw a `TodoistException` when the API reports a command error; transport-level and non-command API errors surface as `HttpRequestException`:

```csharp
using Todoist.Net.Exceptions;

try
{
    await client.Tasks.AddAsync(new AddTask("New task"));
}
catch (TodoistException ex)
{
    // A command failed on the API side.
    Console.WriteLine($"[{ex.ErrorTag}] {ex.Message} (HTTP {ex.HttpCode}, code {ex.Code})");

    // Extra details, when available (e.g. rate limiting):
    if (ex.ErrorExtra?.RetryAfter != null)
    {
        // Back off and retry later.
    }
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"HTTP error: {ex.Message}");
}
```

## Cancellation support

All async methods support cancellation:

```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

try
{
    var projects = await client.Projects.GetAsync(cancellationToken: cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation was cancelled");
}
```

## Migrating from Todoist.Net (v10 and earlier)

This package is a major, breaking evolution of the original `Todoist.Net` package. The most visible renames are `Item` → `TaskInfo`/`AddTask`/`UpdateTask`, `Note` → `Comment`, `client.Items` → `client.Tasks`, and `client.Notes` → `client.Comments`. See the [11.0.0 release notes](https://github.com/olsh/todoist-net/releases/tag/11.0.0) for the full migration table.

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.
