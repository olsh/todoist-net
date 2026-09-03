# AGENTS.md

Guidance for coding agents working in this repository.

## Project Overview

Todoist.Net is a .NET client library for the **Todoist unified API v1** (`https://api.todoist.com/api/v1/`, set in `TodoistRestClient`). It wraps both the batch `sync` endpoint and the plain REST v1 endpoints behind one strongly-typed client.

The repo is mid-migration from the old Sync API v9 (`11.0.0-beta.1`; see the "API v1 Migration - Phase 1/2" commits). Public naming was moved to v1 vocabulary (`Item` -> `Task`, `Note` -> `Comment`) while the sync wire protocol kept the legacy names — see [Naming: C# vs. wire](#naming-c-vs-wire) before touching models or commands.

Library targets `netstandard2.0` and `net462`; the test project targets `net8.0`.

## Build and Test Commands

NUKE is the build system. Targets are invoked in **kebab-case** on the CLI:

```bash
.\build.cmd                                    # default chain: compile, unit-test, nuget-pack
.\build.cmd unit-test                          # trait=unit only (no API token needed)
.\build.cmd test                               # everything except trait!=mfa-required (hits the live API)
.\build.cmd nuget-pack --configuration Release # package into artifacts/
```

`build.cmd` (Windows) and `build.sh` (Linux/macOS) both delegate to `build/Build.cs`. Configuration defaults to `Debug` locally, `Release` on CI.

Direct dotnet CLI:

```bash
dotnet build Todoist.Net.slnx
dotnet test src/Todoist.Net.Tests/Todoist.Net.Tests.csproj --filter "trait=unit"
dotnet test src/Todoist.Net.Tests/Todoist.Net.Tests.csproj --filter "FullyQualifiedName~TransactionTests"
```

`dotnet test` only builds the `netstandard2.0` leg. Run `dotnet build Todoist.Net.slnx` to compile `net462` too — a change can pass tests and still break that target framework.

Integration tests read the token from the **`todoist:token`** environment variable (`Settings/SettingsProvider.cs`) and mutate a real Todoist account.

## Architecture

### Two transports behind one client

`TodoistClient` implements the public `ITodoistClient` plus the internal `IAdvancedTodoistClient`. All HTTP goes through the latter, which services consume:

- **Batch/sync** — `ExecuteCommandsAsync` POSTs a `commands` array to `sync`, and `GetResourcesAsync` POSTs `sync_token` + `resource_types` to the same endpoint. Server errors arrive per-command in `sync_status` and are re-thrown as an `AggregateException` of `TodoistException`.
- **REST v1** — `GetAsync<T>` / `PostJsonAsync<T>` / `PutJsonAsync<T>` / `PostFormAsync<T>` and the raw `GetRawAsync` / `PostRawAsync` / `DeleteRawAsync` variants hit named resources (`tasks/{id}`, `projects/archived`, `uploads`, `templates/file`, …).

`IAdvancedTodoistClient` is `internal` and implemented explicitly, so the transport surface never leaks to consumers. Widening it is a deliberate decision, not a convenience.

### Command pattern and transactions

`Command` = `{ type, args, temp_id, uuid }`, where `type` is a `CommandType` and `args` any `ICommandArgument`.

`CommandServiceBase` gives every command service two modes, selected by which constructor ran:

- **Direct** — built with an `IAdvancedTodoistClient`; `ExecuteCommandAsync` fires one HTTP request immediately.
- **Queued** — built with an `ICollection<Command>`; `ExecuteCommandAsync` just appends to the transaction's list.

`Transaction` owns the `LinkedList<Command>` and constructs a queued instance of every command service. `CommitAsync` sends the batch and clears the queue (in a `finally`, so a failed commit does not leave commands to be re-sent).

```csharp
var transaction = client.CreateTransaction();
var projectId = await transaction.Project.AddAsync(new Project("New project"));
var taskId = await transaction.Tasks.AddAsync(new AddTask("New task", projectId));
await transaction.Comments.AddToTaskAsync(new Comment("Description"), taskId);
await transaction.CommitAsync();
```

### Service layer

Per resource there are two types, and **`{Resource}Service` inherits `{Resource}CommandService`** — that inheritance is why `client.Tasks` exposes reads and writes together while `transaction.Tasks` exposes only the queueable writes:

- `{Resource}CommandService : CommandServiceBase, I{Resource}CommandService` — sync commands (`AddAsync`, `UpdateAsync`, `DeleteAsync`, …); usable in both modes.
- `{Resource}Service : {Resource}CommandService, I{Resource}Service` — adds the read/REST calls; requires a client, so it is direct-mode only.

`TodoistClient`'s constructor instantiates the `*Service` variants; `Transaction`'s constructor instantiates the `*CommandService` variants. Adding a resource means wiring both.

### ID handling: ComplexId and temp IDs

`ComplexId` is a struct holding either a `PersistentId` (server string) or a `TempId` (`Guid`). `CreateAddCommand` assigns a fresh temp ID to a new entity and sends it as the command's `temp_id`, letting later commands in the same batch reference an entity that does not exist yet. After a commit, `UpdateTempIds` walks the sent commands and rewrites `BaseEntity.Id` from the response's `temp_id_mapping`; arguments that reference *other* entities implement `IWithRelationsArgument` to patch their own references.

Consequence: an entity object passed into a transaction is mutated by `CommitAsync`. Reusing it afterwards works precisely because of that rewrite.

### Serialization

One static `JsonSerializerOptions` in `TodoistClient` drives everything. `DefaultIgnoreCondition = WhenWritingNull` is what gives updates their PATCH semantics.

Converters (`Serialization/Converters/`): `StringEnumTypeConverter`, `ComplexIdConverter`, `CommandResultConverter`, `CommandArgumentConverter` (write-only; re-dispatches on the runtime type so polymorphic `args` serialize fully), plus opt-in `BoolConverter` (accepts `"1"`/`1`/`true`) and `DateOnlyConverter` (`yyyy-MM-dd`).

Resolver modifiers (`Serialization/Resolvers/JsonResolverModifiers.cs`), applied in order:

1. `SerializeInternalSetters` — lets models expose `internal set` properties and still round-trip.
2. `FilterSerializationByType` — suppresses writing `UserInfo` entirely.
3. `IncludeUnsetProperties` — the escape hatch from `WhenWritingNull`: a null property is still written if it was registered via `Unset`.

`entity.Unset(x => x.DueDate)` (`UnsettablePropertiesExtensions`) records the `PropertyInfo` on `BaseUnsetEntity`'s `IUnsettableProperties.UnsetProperties`. It throws unless `TProp` is nullable, and nulls the property itself when the setter is not internal. This is the only way to send an explicit `null` to the API.

### Models

`BaseEntity` (has `ComplexId Id`, is an `ICommandArgument`) -> `BaseUnsetEntity` (adds unset tracking) -> concrete entities. Write models are split from read models: `AddTask` / `UpdateTask` / `BaseTask` / `DetailedTask` rather than one mutable `Task` type.

`StringEnum` is a class-based enum (`CommandType`, `ResourceType`, `Priority`, `Language`, …) whose `Value` is the wire string and whose members are static properties discovered by reflection in `TryParse`.

Paginated REST responses come in two shapes — `PaginatedResponse<T>` (`results`) and `PaginatedItemsResponse<T>` (`items`) — both with `NextCursor`/`HasMore`. Pick the one matching the endpoint's actual JSON.

## Naming: C# vs. wire

The v1 migration renamed the .NET surface but **not** the sync protocol. Never assume the wire name follows the C# name:

| C# | Wire |
|---|---|
| `ITasksService`, `AddTask`, `DetailedTask` | `item_add`, `item_delete`, `items` |
| `ICommentsService`, `Comment` | `note_add`, `note_delete`, `notes`, `project_notes` |
| `ResourceType.Tasks` / `ResourceType.Comments` | `"items"` / `"notes"` |
| `Resources.Tasks` / `Resources.Comments` | `[JsonPropertyName("items")]` / `("notes")` |
| `Comment.TaskId` | `item_id` |

`CommandType` is the canonical mapping table. When adding a command, take the wire string from Todoist's sync docs verbatim — it is usually still `item_*`/`note_*`.

### Adding a sync command

1. Add a `CommandType` static property with the exact wire string.
2. Add or reuse an `ICommandArgument` model (`*Argument` types cover non-entity payloads such as `MoveArgument`, `IdsArgument`, `ReorderArgument`).
3. Add the method to `I{Resource}CommandService` + `{Resource}CommandService`, building the command via `CreateAddCommand` / `CreateEntityCommand` / `new Command(...)` and dispatching through `ExecuteCommandAsync` — never call the client directly, or the method breaks in transaction mode.
4. If the argument references other entities by ID, implement `IWithRelationsArgument` so temp IDs resolve after commit.

## Testing

xUnit, traits under the key `trait`:

- `unit` — 15 tests, no network: serialization resolvers, `DueDate`, `Duration`, `StringEnum`, timezone helper. This is the only suite CI gates on.
- `integration-free` / `integration-premium` — hit a live account; premium ones need a paid plan.
- `mfa-required` — excluded from the `test` target because MFA breaks them.

Integration test classes share `[Collection(Constants.TodoistApiTestCollectionName)]`, which serializes them so they do not race on shared account state. They create clients through `TodoistClientFactory.Create(outputHelper)`, which wraps `TodoistRestClient` in `RateLimitAwareRestClient` — it retries 429s and 5xx up to 60 times, honoring `retry_after` (Todoist allows ~450 requests / 15 min). Long, quiet test runs are usually a rate-limit backoff, not a hang.

New integration tests must clean up after themselves in a `finally` block; the account is shared and persistent.

## Constraints when editing

- `TreatWarningsAsErrors=true` on both projects, and the library sets `GenerateDocumentationFile=true` — a public member without an XML doc comment fails the build.
- `netstandard2.0`/`net462` means no `DateOnly`, no nullable reference types, no `record`. `IHttpClientFactory` support (`TodoistServiceCollectionExtensions`, `TodoistClientFactory`) is behind `#if NETSTANDARD2_0`.
- `InternalsVisibleTo("Todoist.Net.Tests")` lets tests reach internal types (`TodoistRestClient`, `CommandType`, resolvers) — prefer keeping new plumbing internal over widening the public API.
- CI (`.github/workflows/ci.yml`) runs on `windows-latest` with the .NET 10 SDK and only builds, tests and packs. SonarCloud analysis is not part of CI: the project uses SonarQube Cloud Automatic Analysis, which runs server-side via the GitHub App and covers `master` plus pull requests (including those from forks).
