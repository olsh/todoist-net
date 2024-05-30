using System;
using System.Linq;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    public class ProjectsServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public ProjectsServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateGetAndDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var projectName = Guid.NewGuid().ToString();
            await client.Projects.AddAsync(new Project(projectName));

            var projects = await client.Projects.GetAsync();
            var project = projects.FirstOrDefault(p => p.Name == projectName);

            Assert.NotNull(project);

            await client.Projects.DeleteAsync(project.Id);

            projects = await client.Projects.GetAsync();
            project = projects.FirstOrDefault(p => p.Name == projectName);

            Assert.Null(project);
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateUpdateOrderMoveAndDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var projectName = Guid.NewGuid().ToString();
            var project = new Project(projectName);
            await client.Projects.AddAsync(project);

            Assert.True(project.Id != default(string));

            project.Name = "u_" + Guid.NewGuid();

            await client.Projects.UpdateAsync(project);

            await client.Projects.ReorderAsync(new ReorderEntry(project.Id, 1));

            var parentProjectName = Guid.NewGuid().ToString();
            var parentProject = new Project(parentProjectName);
            await client.Projects.AddAsync(parentProject);

            await client.Projects.MoveAsync(new MoveArgument(project.Id, parentProject.Id));

            await client.Projects.DeleteAsync(project.Id);
            await client.Projects.DeleteAsync(parentProject.Id);
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateArchiveAndDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var projectName = Guid.NewGuid().ToString();
            var newProject = new Project(projectName);
            await client.Projects.AddAsync(newProject);

            await client.Projects.ArchiveAsync(newProject.Id);
            var projectInfo = await client.Projects.GetAsync(newProject.Id);
            Assert.True(projectInfo.Project.IsArchived);


            await client.Projects.UnarchiveAsync(newProject.Id);
            projectInfo = await client.Projects.GetAsync(newProject.Id);
            Assert.False(projectInfo.Project.IsArchived);

            await client.Projects.DeleteAsync(projectInfo.Project.Id);
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateProjectAndGetProjectData_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var transaction = client.CreateTransaction();

            var projectId = await transaction.Project.AddAsync(new Project("Test"));
            await transaction.Items.AddAsync(new AddItem("Test task", projectId));

            await transaction.CommitAsync();

            var projectData = await client.Projects.GetDataAsync(projectId);

            Assert.Single(projectData.Items);

            await client.Projects.DeleteAsync(projectId);
        }
    }
}
