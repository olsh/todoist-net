using System;
using System.Linq;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;
using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class ProjectsServiceTests
    {
        [Fact]
        [IntegrationFree]
        public void CreateGetAndDelete_Success()
        {
            var client = TodoistClientFactory.Create();

            var projectName = Guid.NewGuid().ToString();
            client.Projects.AddAsync(new Project(projectName)).Wait();

            var projects = client.Projects.GetAsync().Result;
            var project = projects.FirstOrDefault(p => p.Name == projectName);

            Assert.True(project != null);

            client.Projects.DeleteAsync(project.Id).Wait();

            projects = client.Projects.GetAsync().Result;
            project = projects.FirstOrDefault(p => p.Name == projectName);

            Assert.True(project == null);
        }

        [Fact]
        [IntegrationFree]
        public void CreateProjectWithNote_Success()
        {
            var client = TodoistClientFactory.Create();

            var projectName = Guid.NewGuid().ToString();
            client.Projects.AddAsync(new Project(projectName)).Wait();

            var projects = client.Projects.GetAsync().Result;
            var project = projects.FirstOrDefault(p => p.Name == projectName);

            Assert.True(project != null);

            client.Projects.DeleteAsync(project.Id).Wait();

            projects = client.Projects.GetAsync().Result;
            project = projects.FirstOrDefault(p => p.Name == projectName);

            Assert.True(project == null);
        }

        [Fact]
        [IntegrationFree]
        public void CreateUpdateIndentAndDelete_Success()
        {
            var client = TodoistClientFactory.Create();

            var projectName = Guid.NewGuid().ToString();
            var project = new Project(projectName);            
            client.Projects.AddAsync(project).Wait();

            Assert.True(project.Id != default(int));

            project.Name = "u_" + Guid.NewGuid();

            client.Projects.UpdateAsync(project).Wait();

            client.Projects.UpdateMultipleOrdersIndentsAsync(new OrderIndentEntry(project.Id, 1, 2)).Wait();

            client.Projects.DeleteAsync(project.Id).Wait();
        }

        [Fact]
        [IntegrationFree]
        public void CreateArchiveAndDelete_Success()
        {
            var client = TodoistClientFactory.Create();

            var projectName = Guid.NewGuid().ToString();
            var newProject = new Project(projectName);
            client.Projects.AddAsync(newProject).Wait();

            client.Projects.ArchiveAsync(newProject.Id).Wait();
            var projectInfo = client.Projects.GetAsync(newProject.Id).Result;
            Assert.True(projectInfo.Project.IsArchived);


            client.Projects.UnarchiveAsync(newProject.Id).Wait();
            projectInfo = client.Projects.GetAsync(newProject.Id).Result;
            Assert.False(projectInfo.Project.IsArchived);

            client.Projects.DeleteAsync(projectInfo.Project.Id).Wait();
        }
    }
}
