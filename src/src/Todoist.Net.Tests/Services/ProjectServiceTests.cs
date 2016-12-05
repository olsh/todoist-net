using System;
using System.Linq;

using Todoist.Net.Models;
using Todoist.Net.Tests.Constants;
using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class ProjectServiceTests
    {
        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void CreateGetAndDelete_Success()
        {
            var client = CreateProjectService();

            var projectName = Guid.NewGuid().ToString();
            client.Projects.AddAsync(new Project(projectName)).Wait();

            var projects = client.Projects.GetAsync().Result;
            var project = projects.FirstOrDefault(p => p.Name == projectName);

            var result = client.Projects.GetAsync(project.Id).Result;

            Assert.True(project != null);

            client.Projects.DeleteAsync(project.Id).Wait();

            projects = client.Projects.GetAsync().Result;
            project = projects.FirstOrDefault(p => p.Name == projectName);

            Assert.True(project == null);
        }

        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void CreateProjectWithNote_Success()
        {
            var client = CreateProjectService();

            var projectName = Guid.NewGuid().ToString();
            var projectId = Guid.NewGuid().ToString();
            client.Projects.AddAsync(new Project(projectName)).Wait();

            var projects = client.Projects.GetAsync().Result;
            var project = projects.FirstOrDefault(p => p.Name == projectName);

            var result = client.Projects.GetAsync(project.Id).Result;

            Assert.True(project != null);

            client.Projects.DeleteAsync(project.Id).Wait();

            projects = client.Projects.GetAsync().Result;
            project = projects.FirstOrDefault(p => p.Name == projectName);

            Assert.True(project == null);
        }

        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void CreateUpdateAndDelete_Success()
        {
            var client = CreateProjectService();

            var projectName = Guid.NewGuid().ToString();
            var project = new Project(projectName);            
            client.Projects.AddAsync(project).Wait();

            var result = client.Projects.GetAsync(project.Id).Result;

            Assert.True(project.Id != default(int));

            project.Name = "u_" + Guid.NewGuid();

            client.Projects.UpdateAsync(project).Wait();

            client.Projects.DeleteAsync(project.Id).Wait();
        }

        public TodoistClient CreateProjectService()
        {
            return new TodoistClient(SettingsProvider.GetToken());
        }
    }
}