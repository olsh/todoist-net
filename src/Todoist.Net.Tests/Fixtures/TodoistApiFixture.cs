namespace Todoist.Net.Tests;

public sealed class TodoistApiFixture : IAsyncLifetime
{
    // Start the first instance with 0 or 1 randomly to distribute single-test runs across different accounts when available.
    private static int _globalInstanceCounter = Random.Shared.Next(0, 2);
    private static readonly Lock _instantiationLock = new();

    private readonly int _instanceNumber = 0;
    private readonly SemaphoreSlim _creationGate;
    private readonly SemaphoreSlim _fetchGate;

    private bool _disposed = false;

    private ProjectInfo? _playgroundProject;
    private WorkspaceInfo? _playgroundWorkspace;

    private UserInfo? _mainUserInfo;
    private UserInfo? _collaboratorUserInfo;

    private ITodoistClient? _primaryClient;
    private ITodoistClient? _secondaryClient;

    public TodoistApiFixture()
    {
        lock (_instantiationLock)
        {
            _instanceNumber = ++_globalInstanceCounter;
        }
        _creationGate = new SemaphoreSlim(1, 1);
        _fetchGate = new SemaphoreSlim(1, 1);
    }

    public ITodoistClient Client => _secondaryClient ?? PremiumClient;

    public ITodoistClient CollaborationClient => Client != PremiumClient
        ? PremiumClient
        : throw new InvalidOperationException("Secondary client is not available. Make sure the token is set in the environment variables.");

    public ITodoistClient PremiumClient => _primaryClient
        ?? throw new InvalidOperationException("The fixture has not been initialized yet");


    public async ValueTask InitializeAsync()
    {
        _primaryClient = TodoistClientFactory.CreatePrimary();

        // Alternate between secondary and tertiary clients for each instance to maximize test distribution across different accounts.
        _secondaryClient = _instanceNumber % 2 == 0
            ? TodoistClientFactory.CreateSecondary()
            : TodoistClientFactory.CreateTertiary() ?? TodoistClientFactory.CreateSecondary();
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }
        try
        {
            await DeletePlaygroundProjectAsync();
            await DeletePlaygroundWorkspaceAsync();
        }
        finally
        {
            _creationGate.Dispose();

            _primaryClient?.Dispose();
            _secondaryClient?.Dispose();

            _disposed = true;
        }
    }


    public async Task<UserInfo> GetMainUserInfoAsync()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        await _fetchGate.WaitAsync(TestContext.Current.CancellationToken);
        try
        {
            return _mainUserInfo ??= await Client.User.GetInfoAsync(TestContext.Current.CancellationToken);
        }
        finally
        {
            _fetchGate.Release();
        }
    }

    public async Task<UserInfo> GetCollaboratorUserInfoAsync()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        await _fetchGate.WaitAsync(TestContext.Current.CancellationToken);
        try
        {
            return _collaboratorUserInfo ??= await CollaborationClient.User.GetInfoAsync(TestContext.Current.CancellationToken);
        }
        finally
        {
            _fetchGate.Release();
        }
    }

    public async Task<ProjectInfo> GetPlaygroundProjectAsync(bool freshInstance = false)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        await _creationGate.WaitAsync(TestContext.Current.CancellationToken);
        try
        {
            if (freshInstance)
            {
                await DeletePlaygroundProjectAsync();
            }
            return _playgroundProject ??= await CreatePlaygroundProjectAsync();
        }
        finally
        {
            _creationGate.Release();
        }
    }

    public async Task<WorkspaceInfo> GetPlaygroundWorkspaceAsync(bool freshInstance = false)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        await _creationGate.WaitAsync(TestContext.Current.CancellationToken);
        try
        {
            if (freshInstance)
            {
                await DeletePlaygroundWorkspaceAsync();
            }
            return _playgroundWorkspace ??= await CreatePlaygroundWorkspaceAsync();
        }
        finally
        {
            _creationGate.Release();
        }
    }

    public async Task<bool> DeletePlaygroundProjectAsync()
    {
        if (string.IsNullOrEmpty(_playgroundProject?.Id.PersistentId))
        {
            return false;
        }
        try
        {
            await Client.Projects.DeleteAsync(_playgroundProject.Id.PersistentId, TestContext.Current.CancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete playground project with ID {_playgroundProject.Id.PersistentId}: {ex}");
            throw;
        }
        _playgroundProject = null;
        return true;
    }

    public async Task<bool> DeletePlaygroundWorkspaceAsync()
    {
        if (string.IsNullOrEmpty(_playgroundWorkspace?.Id.PersistentId))
        {
            return false;
        }

        try
        {
            await Client.Workspaces.DeleteAsync(_playgroundWorkspace.Id.PersistentId, TestContext.Current.CancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete playground workspace with ID {_playgroundWorkspace.Id.PersistentId}: {ex}");
            throw;
        }
        _playgroundWorkspace = null;
        return true;
    }

    public TodoistTracker TrackForCleanup<T>(
        T entity,
        Func<ITodoistClient, Func<ComplexId, CancellationToken, Task>> deleteFunc,
        bool isPremium = false)
        where T : BaseEntity
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        var client = isPremium
            ? PremiumClient
            : Client;

        return new TodoistTracker(
            $"Entity with ID {entity.Id}",
            ct => deleteFunc(client)(entity.Id, ct));
    }

    public TodoistTracker TrackForCleanup(
        Func<ITodoistClient, CancellationToken, Task> cleanupAction,
        string trackedResourceDescription,
        bool isPremium = false)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        var client = isPremium
            ? PremiumClient
            : Client;

        return new TodoistTracker(
            trackedResourceDescription,
            ct => cleanupAction(client, ct));
    }


    private async Task<ProjectInfo> CreatePlaygroundProjectAsync()
    {
        var playgroundProject = new AddProject($"PlaygroundProject_{_instanceNumber}");

        var response = await Client.ExecuteTransactionAndSyncAsync(
            t => t.Projects.AddAsync(playgroundProject, TestContext.Current.CancellationToken),
            resourceTypes: [ResourceType.Projects],
            cancellationToken: TestContext.Current.CancellationToken);
            
        Assert.All(response.SyncStatus.Values, cr => cr.AssertSuccess());

        return response.Projects.First(p => p.Id == playgroundProject.Id);
    }

    private async Task<WorkspaceInfo> CreatePlaygroundWorkspaceAsync()
    {
        var playgroundWorkspace = new AddWorkspace($"PlaygroundWorkspace_{_instanceNumber}");

        var response = await Client.ExecuteTransactionAndSyncAsync(
            t => t.Workspaces.AddAsync(playgroundWorkspace, TestContext.Current.CancellationToken),
            resourceTypes: [ResourceType.Workspaces],
            cancellationToken: TestContext.Current.CancellationToken);

        Assert.All(response.SyncStatus.Values, cr => cr.AssertSuccess());

        return response.Workspaces.First(w => w.Id == playgroundWorkspace.Id);
    }


    public sealed class TodoistTracker : IAsyncDisposable
    {
        private readonly string _trackedResourceDescription;
        private readonly Func<CancellationToken, Task> _cleanupAction;

        private bool _trackingStopped = false;

        public TodoistTracker(string trackedResourceDescription, Func<CancellationToken, Task> cleanupAction)
        {
            _trackedResourceDescription = trackedResourceDescription;
            _cleanupAction = cleanupAction;
        }

        public async ValueTask DisposeAsync()
        {
            if (_trackingStopped)
            {
                return;
            }
            _trackingStopped = true;

            try
            {
                await _cleanupAction(TestContext.Current.CancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to clean up {_trackedResourceDescription}: {ex}");
                throw;
            }
        }

        public void StopTracking()
        {
            _trackingStopped = true;
        }
    }
}
