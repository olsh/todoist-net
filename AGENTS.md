# AGENTS.md

Guidance for AI coding agents working in this repository. For human-facing documentation see [README.md](README.md); for contribution details see [CONTRIBUTING.md](CONTRIBUTING.md); for the Sync API v9 → Unified API v1 migration history see [MigrationNotes.md](MigrationNotes.md).

## Project overview

- **Todoist.Net.APIv1** — a strongly-typed .NET client for the [Todoist Unified API v1](https://developer.todoist.com/api/v1/), forked from [olsh/todoist-net](https://github.com/olsh/todoist-net). NuGet package ID: `Todoist.Net.APIv1`.
- Solution `Todoist.Net.sln` contains two projects plus a NUKE build project:
  - `src/Todoist.Net` — the library; multi-targets `netstandard2.0` and `net462` (the `net462` target requires Windows or reference assemblies).
  - `src/Todoist.Net.Tests` — xUnit **v3** tests, targets `net10.0` only.
  - `build/` — NUKE build (`_build.csproj`); bootstrappers: `build.ps1`, `build.sh`, `build.cmd`.
- **.NET 10 SDK is required** to build and test.

## Build commands

```powershell
dotnet build Todoist.Net.sln                                    # full build
dotnet pack src/Todoist.Net/Todoist.Net.csproj -c Release       # pack manually

./build.ps1            # NUKE default: Compile + UnitTest + NugetPack
./build.ps1 Compile    # build only
./build.ps1 UnitTest   # unit tests only (filter: trait=unit)
./build.ps1 Test       # all tests except OAuth-refresh ones (trait!=integration-refreshable)
./build.ps1 NugetPack  # package into artifacts/
```

- Both projects have `TreatWarningsAsErrors=true` — the build must stay warning-free.
- The library generates XML documentation; missing XML docs on new public APIs will warn/fail.

## Test commands and configuration

```powershell
dotnet test src/Todoist.Net.Tests --filter "trait=unit"                       # unit only (default safe run)
dotnet test src/Todoist.Net.Tests --filter "trait!=integration-refreshable"   # everything except OAuth refresh
```

- **Test categorization is trait-only** — there are intentionally no `Skip` attributes. Trait values (constants in `src/Todoist.Net.Tests/Extensions/Constants.cs`): `unit`, `integration-free`, `integration-premium`, `integration-collaboration`, `integration-refreshable`, `integration-root`. Apply at class level when uniform, otherwise at method level.
- To run integration tests, copy `.runsettings.example` → `.runsettings` at the repo root (auto-discovered via `RunSettingsFilePath`) and set `TODOIST_TOKEN` (Premium account for premium tests), optionally `TODOIST_TOKEN_SECONDARY` (different account, enables collaboration tests) and `TODOIST_TOKEN_TERTIARY`. A Doppler-based OAuth mode (`DOPPLER_TOKEN`) replaces static tokens and auto-refreshes/persists tokens; see the comments in `.runsettings.example` and [CONTRIBUTING.md](CONTRIBUTING.md) for the full setup including required OAuth scopes.
- Combine filters with `&`, e.g. `trait!=integration-refreshable & trait!=integration-collaboration`.
- **Always build after adding/modifying tests** so test-discovery issues surface early. Prefer the IDE test runner when available (it picks up `.runsettings` automatically).

## Architecture essentials (read before editing)

- **Two-layer services**: each domain has `I{Entity}CommandService` (mutations implemented as sync commands via `CommandServiceBase`) and `I{Entity}Service` (reads: sync wrappers and/or paginated REST GETs). Command methods must work both directly (immediate execution, `throwOnError: true`) and queued inside an `ITransaction` (collected and flushed by `CommitAsync`, `throwOnError: false`). `src/Todoist.Net/Services/` is organized by domain folder.
- **Flattened models namespace**: model files live in per-domain folders under `src/Todoist.Net/Models/` but all share `namespace Todoist.Net.Models`. Never introduce per-folder namespaces.
- **Identifiers**: use `ComplexId` (persistent `string` or temp `Guid`) for all entity IDs. Add-commands return a temp ID; after execution the argument entity's `Id` is updated in place and `SyncTransactionResponse.TempIdMappings` maps temp → persistent IDs. `IWithRelationsArgument` implementations get related temp IDs swapped automatically.
- **Serialization is centralized** in `TodoistClient` (`SerializerOptions` + converters in `Serialization/` + resolver modifiers `JsonResolverModifiers`). Extend these rather than creating per-call `JsonSerializerOptions`. JSON wire names must stay API-compatible (`items`, `notes`, `item_id`, …). Read models use `internal set;` properties populated via the resolver modifiers.
- **String-valued enums** derive from `StringEnum` with static readonly members (`Color`, `Language`, `ResourceType`, …). Never use plain C# enums for API string values.
- **PATCH semantics**: update models implement `IUnsettableProperties`; `null` properties are omitted unless explicitly marked with the `Unset(x => x.Prop)` extension.
- **Errors**: command failures throw `TodoistException` (or `AggregateException` for multi-command with `throwOnError`); HTTP failures throw `HttpRequestException`. Use the internal `ThrowHelper` for argument guards. The test project has `InternalsVisibleTo` — keep helpers `internal` and test them directly instead of widening the public API.
- **Cancellation**: every public async method takes a trailing `CancellationToken cancellationToken = default`.
- **Pagination**: REST list endpoints use `PaginatedResponse<T>` (`Results`, `NextCursor`, `HasMore`) with per-endpoint query models deriving from `PaginationQuery`/`PaginatedSearchQuery`.
- **Sync responses**: `SyncResourcesResponse` (all resource collections) / `SyncTransactionResponse` (adds `SyncStatus`, `TempIdMappings`) / `SyncResponse<T>` / `EntitySyncResponse<T>`.

## Known quirks (don't "fix" without discussion)

- `ITasksService.QuickAddAsync` returns `Task` (response body discarded) although the XML doc mentions returning the created task.
- `IWorkspaceFiltersService` does **not** inherit `IWorkspaceFiltersCommandService` (unlike other paired services); `IUploadsService` intentionally exposes only upload/delete.
- `Priority` values are inverted vs. the Todoist UI: `Priority1 = 4` is *urgent* (p1), `Priority4 = 1` is natural.
- REST get-by-ID endpoints may not return `404` immediately after a sync deletion (labels do this) — assert deleted state via sync resources in tests.
- Sync command type strings intentionally keep legacy names (`item_add`, `note_add`) for API compatibility.
- The DI extension `AddTodoistClient` is `netstandard2.0`-only (`#if NETSTANDARD2_0`).

## Code style

- Allman braces, 4-space indentation, block-scoped namespaces, `_camelCase` private fields, `#region` blocks for interface implementations in large classes (see `TodoistClient`).
- Full XML documentation on all public APIs: `<summary>`, `<param>`, `<returns>`, `<exception>`, plus `<remarks>` for Premium-only features.
- The library has no NRT annotations; the test project uses `Nullable` + `ImplicitUsings` — match each project's existing style.
- Global usings for tests are declared in `src/Todoist.Net.Tests/Todoist.Net.Tests.csproj` — don't re-add them to test files.

## Test authoring conventions

- xUnit v3 with plain `Assert` (`Assert.Equivalent`, `Assert.Single`, `Assert.Contains`, `Assert.ThrowsAsync`). No FluentAssertions, no mocking frameworks. For protocol-level unit tests use `StubTodoistRestClient` (`Helpers/`) with `RespondToGetJson`/`RespondToPostJson`; see `TodoistClientProtocolTests`. Use `FakeLocalTimeZone` in an `IDisposable` scope for timezone-sensitive tests.
- Capture `private readonly CancellationToken _cancellationToken = TestContext.Current.CancellationToken;` in each test class constructor and pass it to API calls.
- Integration tests: class named `{Service}ServiceTests` in `Todoist.Net.Tests/Services`, decorated with `[Collection(TodoistApiTestCollection.Name)]`. Get clients **only** from `TodoistApiFixture` (`Client`, `PremiumClient`, `CollaborationClient`) — all are rate-limit-aware (auto-retry on 429/5xx). Reuse shared fixture state (`GetPlaygroundProjectAsync()`, etc.) instead of creating your own scaffolding.
- **Register cleanup immediately after creating a remote entity** via `_apiFixture.TrackForCleanup(...)` in an `await using`; call `tracker.StopTracking()` after a successful in-test delete. Name multiple trackers by entity/role (`labelTracker`, `taskTracker`), never generic `tracker`.
- Naming: integration methods are scenario chains ending in `_Succeeds` (e.g. `CreateChildProject_MoveToRoot_Reorder_Delete_Succeeds_Get_ThrowsNotFound`); unit tests are `Method_Condition_Expectation`. Structure integration tests with numbered `// Step N:` comments (`// Step 0:` for setup), not Arrange/Act/Assert comments.
- Variables: `syncResponse` for transaction/sync results, `actual{Entity}` / `expected{Entity}` for assertions. Use `TestData` factories and `Expected*` anonymous objects with `Assert.Equivalent`. Suffix created entity names with a GUID (`$"NewLabel_{Guid.NewGuid():N}"`).
- Assert sync commands with `Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess())` — never raw `IsSuccess` checks. When commands affect synced resources, include the `ResourceType`s in the same request (`ExecuteTransactionAndSyncAsync`) and assert on synced resources instead of extra follow-up GETs.
- Prefer direct service calls for single commands; use `ExecuteTransactionAndSyncAsync` when batching or when synced resources are needed. Give each retrieval method one dedicated verification step rather than repeating it after every mutation. Skip `Sync*` wrapper tests when the behavior is already covered via `SyncResourcesAsync`/`ExecuteTransactionAndSyncAsync`.
- When an integration test fails, investigate the API/sync payload (inspect `syncResponse.SyncStatus`) before assuming the test code is wrong.

## PR instructions

- Keep PRs focused; split large changes into reviewable phases. Update `README.md` for user-facing changes and XML docs for API changes.
- Before committing: `dotnet build Todoist.Net.sln` must be clean (warnings are errors) and `dotnet test src/Todoist.Net.Tests --filter "trait=unit"` must pass. Add/update tests with the correct trait for any behavior change.
- CI runs the NUKE build with SonarCloud analysis; the quality gate must pass.

## Security considerations

- **Never commit tokens or secrets.** `.runsettings` is git-ignored; only `.runsettings.example` (with placeholders) is committed. Doppler tokens, OAuth client secrets, and Todoist tokens must never appear in code, test data, docs, or commit messages.
- Integration tests mutate a real Todoist account — run them only against dedicated test accounts, and rely on `TrackForCleanup` to delete created entities.
