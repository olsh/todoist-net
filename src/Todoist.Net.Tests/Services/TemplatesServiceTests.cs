using System.Text;

namespace Todoist.Net.Tests.Services;

[Collection(TodoistApiTestCollection.Name)]
[Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
public class TemplatesServiceTests
{
    private readonly TodoistApiFixture _apiFixture;
    private readonly CancellationToken _cancellationToken;

    public TemplatesServiceTests(TodoistApiFixture apiFixture)
    {
        _apiFixture = apiFixture;
        _cancellationToken = TestContext.Current.CancellationToken;
    }

    [Fact]
    public async Task ExportAsFile_ImportIntoProjectFromFile_ExportAsUrl_Succeeds()
    {
        var sourceProject = TestData.Projects.AddProject($"TemplateSource_{Guid.NewGuid():N}");
        var destinationProject = TestData.Projects.AddProject($"TemplateDestination_{Guid.NewGuid():N}");
        const string expectedTaskContent = "Template seed task";


        // Step 1: Create source project and seed it with a task.
        await _apiFixture.PremiumClient.Projects.AddAsync(sourceProject, _cancellationToken);
        await using var sourceProjectTracker = _apiFixture.TrackForCleanup(sourceProject, c => c.Projects.DeleteAsync, isPremium: true);

        var seedTask = TestData.Tasks.AddTask(sourceProject.Id, expectedTaskContent);
        await _apiFixture.PremiumClient.Tasks.AddAsync(seedTask, _cancellationToken);


        // Step 2: Export the source project as a template file.
        var templateCsv = await _apiFixture.PremiumClient.Templates.ExportAsFileAsync(
            sourceProject.Id.PersistentId,
            cancellationToken: _cancellationToken);

        Assert.False(string.IsNullOrWhiteSpace(templateCsv));
        Assert.Contains(expectedTaskContent, templateCsv);


        // Step 3: Create the destination project.
        await _apiFixture.PremiumClient.Projects.AddAsync(destinationProject, _cancellationToken);
        await using var destinationProjectTracker = _apiFixture.TrackForCleanup(destinationProject, c => c.Projects.DeleteAsync, isPremium: true);


        // Step 4: Import the exported template file into the destination project.
        using var templateFileContent = new FileContent(Encoding.UTF8.GetBytes(templateCsv));

        var importResult = await _apiFixture.PremiumClient.Templates.ImportIntoProjectAsync(
            destinationProject.Id.PersistentId,
            templateFileContent,
            _cancellationToken);

        Assert.Equal("ok", importResult.Status);
        Assert.NotEmpty(importResult.Tasks);
        Assert.Contains(importResult.Tasks, t => t.Content == expectedTaskContent);


        // Step 5: Export the source project as a shareable template URL.
        var actualTemplateFile = await _apiFixture.PremiumClient.Templates.ExportAsUrlAsync(
            sourceProject.Id.PersistentId,
            cancellationToken: _cancellationToken);

        Assert.False(string.IsNullOrWhiteSpace(actualTemplateFile.FileName));
        Assert.False(string.IsNullOrWhiteSpace(actualTemplateFile.FileUrl));
    }

    [Fact]
    public async Task CreateProjectFromFileInWorkspace_Succeeds()
    {
        var workspace = TestData.Workspaces.AddWorkspace($"TemplateWorkspace_{Guid.NewGuid():N}");
        var sourceProject = TestData.Projects.AddProject($"TemplateSource_{Guid.NewGuid():N}");
        var newProjectName = $"CreatedFromTemplate_{Guid.NewGuid():N}";
        const string expectedTaskContent = "Template workspace seed task";


        // Step 1: Create a premium-owned workspace, create a source project, and seed it with a task.
        var syncResponse = await _apiFixture.PremiumClient.ExecuteTransactionAndSyncAsync(
            t => t.Workspaces.AddAsync(workspace, _cancellationToken),
            [ResourceType.Workspaces],
            cancellationToken: _cancellationToken);
        await using var workspaceTracker = _apiFixture.TrackForCleanup(workspace, c => c.Workspaces.DeleteAsync, isPremium: true);

        Assert.All(syncResponse.SyncStatus.Values, cr => cr.AssertSuccess());
        var actualWorkspace = Assert.Single(syncResponse.Workspaces, w => w.Id == workspace.Id);
        Assert.Equal(workspace.Name, actualWorkspace.Name);

        await _apiFixture.PremiumClient.Projects.AddAsync(sourceProject, _cancellationToken);
        await using var sourceProjectTracker = _apiFixture.TrackForCleanup(sourceProject, c => c.Projects.DeleteAsync, isPremium: true);

        await _apiFixture.PremiumClient.Projects.MoveToWorkspaceAsync(
            new(sourceProject.Id, workspace.Id),
            _cancellationToken);

        var seedTask = TestData.Tasks.AddTask(sourceProject.Id, expectedTaskContent);
        await _apiFixture.PremiumClient.Tasks.AddAsync(seedTask, _cancellationToken);


        // Step 2: Export the source project as a template file.
        var templateCsv = await _apiFixture.PremiumClient.Templates.ExportAsFileAsync(
            sourceProject.Id.PersistentId,
            cancellationToken: _cancellationToken);

        Assert.False(string.IsNullOrWhiteSpace(templateCsv));
        Assert.Contains(expectedTaskContent, templateCsv);


        // Step 3: Create a new workspace project from the exported template file.
        using var templateFileContent = new FileContent(Encoding.UTF8.GetBytes(templateCsv));

        var createResult = await _apiFixture.PremiumClient.Templates.CreateProjectFromFileAsync(
            newProjectName,
            templateFileContent,
            workspace.Id.PersistentId,
            _cancellationToken);

        Assert.Equal("ok", createResult.Status);
        Assert.False(string.IsNullOrWhiteSpace(createResult.ProjectId));

        var actualProject = Assert.Single(createResult.Projects, p => p.Id.PersistentId == createResult.ProjectId);
        await using var createdProjectTracker = _apiFixture.TrackForCleanup(
            (c, ct) => c.Projects.DeleteAsync(actualProject.Id, ct),
            $"Project with ID {actualProject.Id}",
            isPremium: true);

        Assert.Equal(newProjectName, actualProject.Name);
        Assert.Contains(createResult.Tasks, t => t.Content == expectedTaskContent);
    }
}
