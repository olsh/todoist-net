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
            var projectId = await client.Projects.AddAsync(new Project(projectName));
            try
            {
                var projects = await client.Projects.GetAsync();

                Assert.Contains(projects, p => p.Name == projectName);
            }
            finally
            {
                await client.Projects.DeleteAsync(projectId);
            }
            var otherProjects = await client.Projects.GetAsync();

            Assert.DoesNotContain(otherProjects, p => p.Name == projectName);
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateUpdateOrderMoveAndDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var projectName = Guid.NewGuid().ToString();
            var project = new Project(projectName);
            await client.Projects.AddAsync(project);
            try
            {
                Assert.True(project.Id != default(string));

                project.Name = "u_" + Guid.NewGuid();

                await client.Projects.UpdateAsync(project);

                await client.Projects.ReorderAsync(new ReorderEntry(project.Id, 1));

                var parentProjectName = Guid.NewGuid().ToString();
                var parentProject = new Project(parentProjectName);
                await client.Projects.AddAsync(parentProject);
                try
                {
                    await client.Projects.MoveAsync(new MoveArgument(project.Id, parentProject.Id));
                }
                finally
                {
                    await client.Projects.DeleteAsync(parentProject.Id);
                }
            }
            finally
            {
                try
                {
                    await client.Projects.DeleteAsync(project.Id);
                }
                // Parent project removes child projects too
                // So the project may be deleted already
                catch
                {
                    // ignored
                }
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateArchiveAndDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var projectName = Guid.NewGuid().ToString();
            var newProject = new Project(projectName);
            await client.Projects.AddAsync(newProject);
            try
            {
                await client.Projects.ArchiveAsync(newProject.Id);
                var projectInfo = await client.Projects.GetAsync(newProject.Id);
                Assert.True(projectInfo.Project.IsArchived);


                await client.Projects.UnarchiveAsync(newProject.Id);
                projectInfo = await client.Projects.GetAsync(newProject.Id);
                Assert.False(projectInfo.Project.IsArchived);
            }
            finally
            {
                await client.Projects.DeleteAsync(newProject.Id);
            }
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
            try
            {
                var projectData = await client.Projects.GetDataAsync(projectId);

                Assert.Single(projectData.Items);
            }
            finally
            {
                await client.Projects.DeleteAsync(projectId);
            }
        }
    }
}
