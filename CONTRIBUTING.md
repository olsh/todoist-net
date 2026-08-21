# Contributing to Todoist.Net.APIv1

This document covers the development setup, the testing infrastructure (including integration tests against the live Todoist API), and the coding conventions used in this repository.

## Repository layout

```
Todoist.Net/
├── src/
│   ├── Todoist.Net/           # The library (targets netstandard2.0 and net462)
│   │   ├── Authorization/     # OAuth tokens, client credentials, auth context
│   │   ├── Exceptions/        # TodoistException, ThrowHelper guards
│   │   ├── Extensions/        # Unset(), DI registration (netstandard2.0 only)
│   │   ├── Models/            # Domain models, organized by entity folder
│   │   ├── RestClients/       # HTTP plumbing (token and OAuth-refreshable clients)
│   │   ├── Serialization/     # System.Text.Json converters and resolver modifiers
│   │   └── Services/          # Domain services, organized by entity folder
│   └── Todoist.Net.Tests/     # xUnit v3 test project (targets net10.0)
├── build/                     # NUKE build project
├── .runsettings.example       # Template for test-run configuration (copy to .runsettings)
├── build.ps1 / build.sh / build.cmd  # NUKE build bootstrappers
└── MigrationNotes.md          # Notes on the Sync API v9 → Unified API v1 migration
```

## Prerequisites

- **.NET 10 SDK** — the test project targets `net10.0`, and the SDK builds the library targets (`netstandard2.0`, `net462`).
- **Windows** is required to build the `net462` target (or .NET Framework reference assemblies elsewhere).
- For integration tests: at least one Todoist account token (see [Test configuration](#test-configuration)).

## Building

Standard .NET CLI works for everything:

```powershell
dotnet build Todoist.Net.sln
dotnet pack src/Todoist.Net/Todoist.Net.csproj -c Release
```

The repository also ships a [NUKE](https://nuke.build/) build. Use the bootstrapper for your platform (`build.ps1` on Windows, `build.sh` on Linux/macOS):

```powershell
./build.ps1            # default: Compile, UnitTest, NugetPack
./build.ps1 Compile    # build the solution
./build.ps1 UnitTest   # run unit tests only (trait=unit)
./build.ps1 Test       # run all tests except token-refresh tests (trait!=integration-refreshable)
./build.ps1 NugetPack  # produce the NuGet package in artifacts/
```

The `Sonar` target is used by CI (SonarCloud) and requires a `SONAR_TOKEN`.

## Testing

The test suite contains both **unit tests** (in-memory, no network) and **integration tests** that run against the live Todoist API. Everything is gated through xUnit traits — there are no `Skip` attributes in the codebase; conditionality is expressed entirely through test-case filters.

### Test categories (traits)

Every test must carry exactly one `trait` value (constants live in `Todoist.Net.Tests/Extensions/Constants.cs`):

| Trait | Meaning | Requires |
|-------|---------|----------|
| `unit` | Pure in-memory tests, no network | Nothing |
| `integration-free` | Free-tier endpoints | `TODOIST_TOKEN` |
| `integration-premium` | Premium-only endpoints (filters, reminders, templates, activity log, …) | `TODOIST_TOKEN` of a **Premium** account |
| `integration-collaboration` | Two-account scenarios (sharing, notifications) | `TODOIST_TOKEN_SECONDARY` (a *different* account) |
| `integration-refreshable` | OAuth token-refresh tests | Doppler setup (refreshable OAuth tokens) |
| `integration-root` | Tests needing a root-level API token (e.g. emails) | Static `TODOIST_TOKEN`; excluded when using Doppler |

Apply the trait at the **class level** when all tests in the class share a category, otherwise at the **method level**.

### Test configuration

Copy `.runsettings.example` to `.runsettings` in the repository root and fill in the values. The test project picks up `.runsettings` automatically (via `RunSettingsFilePath`), so IDE test runners and `dotnet test` both honor it.

Two mutually exclusive authentication modes are supported:

**1. Static tokens** (simplest):

- `TODOIST_TOKEN` — **required** for integration tests; must belong to a Premium account to run premium tests.
- `TODOIST_TOKEN_SECONDARY` — optional; a *different* account. Required for collaboration tests, and otherwise used to offload free-tier tests from the primary account.
- `TODOIST_TOKEN_TERTIARY` — optional; further distributes load to avoid rate limits.

**2. Doppler** (recommended for maintainers — short-lived OAuth tokens with automatic refresh):

- `DOPPLER_TOKEN` — a Doppler service token. When present, static Todoist tokens are ignored.
- `DOPPLER_PROJECT` / `DOPPLER_CONFIG` — defaults are `todoist-net` / `dev`.
- The Doppler config must contain OAuth secrets (`CLIENT_ID`, `CLIENT_SECRET`, and `PRIMARY`/`SECONDARY`/`TERTIARY` `_ACCESS_TOKEN` + `_REFRESH_TOKEN` pairs; key names are overridable via `*_KEY` variables).
- When creating the OAuth tokens, request these scopes to run the full suite: `data:read_write`, `data:delete`, `project:delete`, `backups:read`, `billing:read`, `user:write`, `workspaces:write`.
- Refreshed tokens are written back to Doppler automatically during test runs, so they never expire between runs.

The example file documents the `<TestCaseFilter>` recipes, e.g.:

- `trait=unit` — only unit tests (the default until tokens are configured).
- `trait!=integration-refreshable` — when no Doppler token is provided.
- `trait!=integration-premium` — when the primary account is not Premium.
- `trait!=integration-collaboration` — when no secondary token is provided.
- `trait!=integration-root` — when Doppler is used.
- Combine with `&`, e.g. `trait!=integration-refreshable & trait!=integration-collaboration`.

### Running tests

- **IDE test runner** (Visual Studio / VS Code): preferred — the `.runsettings` file is picked up automatically.
- **CLI**: `dotnet test src/Todoist.Net.Tests --filter "trait=unit"` (or any other filter expression).

Always build after writing or modifying tests so discovery issues are caught early.

### Writing tests

**Framework and style**

- xUnit v3 with plain `Assert` (`Assert.Equivalent`, `Assert.Single`, `Assert.Contains`, `Assert.ThrowsAsync`, …). No FluentAssertions, no mocking frameworks.
- Capture the cancellation token in each test class: `private readonly CancellationToken _cancellationToken = TestContext.Current.CancellationToken;` (assign in the constructor) and pass it to API calls.
- Use `ITestOutputHelper` or `TestContext.Current.SendDiagnosticMessage` for diagnostics.

**Unit tests**

- Name them `Method_Condition_Expectation` (e.g. `IncludeUnsetProperties_WithUnsetProperty_IncludeNull`).
- Use `StubTodoistRestClient` (in `Helpers/`) to test the client without network: configure canned responses with `RespondToGetJson` / `RespondToPostJson`, inject it into `new TodoistClient(restClient)`, and inspect `LastResource` / `LastQueryParams` / `LastFormParams`. See `TodoistClientProtocolTests` for the pattern.
- Use `FakeLocalTimeZone` (in `Helpers/`) inside an `IDisposable` scope for timezone-sensitive model tests (see `DueDateTests`).

**Integration tests**

- Put service tests in `Todoist.Net.Tests/Services`, named `{Service}ServiceTests`, decorated with `[Collection(TodoistApiTestCollection.Name)]` so they serialize and share the fixture.
- Obtain clients **only** from `TodoistApiFixture`: `_apiFixture.Client` (free-tier; falls back to the primary account), `_apiFixture.PremiumClient`, and `_apiFixture.CollaborationClient` (throws when no secondary token is configured). All fixture clients are wrapped in `RateLimitAwareRestClient`, which automatically retries HTTP 429/5xx with backoff derived from `Retry-After`, `x-ratelimit-reset`, or `error_extra.retry_after`.
- Use the fixture's shared state helpers where possible: `GetPlaygroundProjectAsync()`, `GetPlaygroundWorkspaceAsync()`, `GetMainUserInfoAsync()`, `GetCollaboratorUserInfoAsync()`.
- **Register cleanup immediately after creating a remote entity**:
  ```csharp
  var taskId = await _apiFixture.Client.Tasks.AddAsync(newTask);
  await using var taskTracker = _apiFixture.TrackForCleanup(newTask, c => c.Tasks.DeleteAsync);
  ```
  Call `tracker.StopTracking()` after an in-test delete so cleanup doesn't run twice. When a test tracks multiple entities, name each tracker by entity or role (`labelTracker`, `taskTracker`) — never a generic `tracker`.
- Use the `TestData` helpers (`Helpers/TestData.cs`) for inputs and `Expected*` anonymous objects, and assert payloads with `Assert.Equivalent(expectedX, actualX)`.
- Name scenario-style integration methods as chains ending in `_Succeeds` (e.g. `CreateChildProject_MoveToRoot_Reorder_Delete_Succeeds_Get_ThrowsNotFound`), and structure the body with numbered `// Step N:` comments (`// Step 0:` for setup) instead of Arrange/Act/Assert comments.
- Use descriptive variable names: `syncResponse` for transaction/sync results, `actual{Entity}` / `expected{Entity}` for assertions. Avoid ad-hoc short names like `sync` or plain `actual`.
- Make names of created entities unique with a GUID suffix (e.g. `$"NewLabel_{Guid.NewGuid():N}"`).
- **Assert sync commands** through the shared helper: `Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess())` — never raw `IsSuccess` checks, and don't assert a single entry and then re-check `SyncStatus.Values.Single()` separately.
- When commands affect synced resources, include the relevant `ResourceType`s in the same request (`ExecuteTransactionAndSyncAsync`) and assert on the synced resources instead of making extra follow-up GET calls when the synced data already proves the behavior.
- Prefer direct service calls for single commands; use `ExecuteTransactionAndSyncAsync(...)` when batching commands or when synced resources are needed for assertions; avoid `ExecuteTransactionAsync(...)` for one-command cases that don't need sync plumbing.
- When a retrieval method is part of the service contract under test, give it one dedicated verification step instead of repeating the same retrieval call after several mutations.
- Avoid duplicate happy-path coverage of REST endpoints that share the same response payload — assert the body on one representative endpoint and use sibling endpoints only for distinct behavior (e.g. 404 handling).
- Treat cross-service setup as scenario setup inside a local-service step, not as standalone asserted steps, unless the cross-service result is the necessary observable outcome.
- Skip `Sync*` wrapper methods when their successful-path behavior is already covered through `Client.SyncResourcesAsync(...)` or `Client.ExecuteTransactionAndSyncAsync(...)`.
- Do not assume REST get-by-ID endpoints return `404` immediately after a sync deletion (the labels API, for example, can still return the deleted entity) — prefer asserting deleted state from sync resources.
- Before writing service integration tests, read the relevant models, XML docs, and the official API docs so the setup matches the intended contract.
- When integration tests fail, investigate thoroughly before assuming the test code is wrong — examine the objects in `syncResponse.SyncStatus` before restructuring the test flow.

## Coding style (library)

- **C# conventions**: Allman braces, 4-space indentation, block-scoped namespaces, `_camelCase` for private fields, `#region` blocks for interface implementations in large classes (see `TodoistClient`).
- **Warnings are errors** (`TreatWarningsAsErrors`) in both projects — keep the build clean.
- **XML documentation** on all public APIs (`<summary>`, `<param>`, `<returns>`, `<exception>`, `<remarks>`), including Premium-only remarks where applicable.
- **Nullable flow**: the library does not use NRT annotations; the test project has `Nullable` and `ImplicitUsings` enabled. Match the surrounding style of each project.
- **Models** live in per-domain folders but share the flattened `Todoist.Net.Models` namespace. Keep JSON wire names (`JsonPropertyName`) compatible with the API (e.g. `items`, `notes`, `item_id`).
- **String-valued enums** derive from `StringEnum` with static readonly members (see `Color`, `Language`, `ResourceType`) — never plain enums for API string values.
- **Entity identifiers** use `ComplexId` (persistent string ID or temp `Guid`); add-commands return a temp ID that is resolved automatically after execution.
- **Services** are split into an `I{Entity}CommandService` (mutations, built on sync commands via `CommandServiceBase`) and an `I{Entity}Service` (reads: sync wrappers and/or paginated REST GETs). Command methods must work both directly (immediate execution) and inside an `ITransaction` (queued).
- **Partial updates**: models that support PATCH-style updates implement `IUnsettableProperties`; `null` properties are omitted unless marked via `Unset(...)`. Read models use `internal set;` properties populated through the JSON resolver modifiers.
- **Serialization** is `System.Text.Json`-only, configured centrally in `TodoistClient` (converters + resolver modifiers). Extend the existing converters/modifiers rather than adding per-call serializer options.
- **Guards**: use the internal `ThrowHelper` (`ThrowIfNull`, `ThrowIfNullOrEmpty`, …) for argument validation.
- **Cancellation**: every async public method accepts a `CancellationToken cancellationToken = default` last parameter.
- **Errors**: command failures throw `TodoistException` (single command) or `AggregateException` (multiple, with `throwOnError`); HTTP failures throw `HttpRequestException`.
- The test assembly has `InternalsVisibleTo` access — keep implementation details `internal` and test them directly instead of widening the public API surface.

## Pull requests

1. Keep PRs focused; large changes should be split into reviewable phases.
2. Build the solution and run `trait=unit` tests locally; add or update tests for behavioral changes, with the correct trait.
3. Document public API changes with XML docs, and update `README.md` when user-facing behavior changes.
4. The CI pipeline runs the NUKE build with SonarCloud analysis — the quality gate must pass.
