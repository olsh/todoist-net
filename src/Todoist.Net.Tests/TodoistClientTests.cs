using System;
using System.Linq;

using Todoist.Net.Models;
using Todoist.Net.Models.Types;
using Todoist.Net.Tests.Constants;
using Todoist.Net.Tests.Settings;

using Xunit;

namespace Todoist.Net.Tests
{
    public class TodoistClientTests
    {
        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void ExecuteCommandsAsync_AddAndUpdateProject_Success()
        {
            var client = CreateClient();

            var newProjectName = Guid.NewGuid().ToString();
            client.ExecuteCommandsAsync(new Command(CommandType.AddProject, new Project(newProjectName))).Wait();

            var resources = client.GetResourcesAsync(ResourceType.Projects).Result;

            var project = resources.Projects.FirstOrDefault(p => p.Name == newProjectName);
            Assert.True(project != null);

            project.Name = "u_" + Guid.NewGuid();
            client.ExecuteCommandsAsync(new Command(CommandType.UpdateProject, project)).Wait();

            resources = client.GetResourcesAsync(ResourceType.Projects).Result;
            var updatedProject = resources.Projects.FirstOrDefault(p => p.Name == project.Name);
            Assert.True(updatedProject != null);

            DeleteProject(project.Id, client);
        }

        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void ExecuteCommandsAsync_AddProjectAndAddItemInOneTransaction_Success()
        {
            var client = CreateClient();

            var newProjectName = Guid.NewGuid().ToString();
            var tempProjectId = Guid.NewGuid();
            var projectCommand = new Command(CommandType.AddProject, new Project(newProjectName), tempProjectId);

            var itemContent = Guid.NewGuid().ToString();
            var itemCommand = new Command(
                                  CommandType.AddItem,
                                  new Item(itemContent) { ProjectId = new ComplexId(tempProjectId) });

            client.ExecuteCommandsAsync(projectCommand, itemCommand).Wait();

            var resources = client.GetResourcesAsync(ResourceType.Projects, ResourceType.Items).Result;
            var project = resources.Projects.FirstOrDefault(p => p.Name == newProjectName);
            Assert.True(project != null);

            var item = resources.Items.FirstOrDefault(i => i.Content == itemContent);
            Assert.True(item != null);

            Assert.True(project.Id == item.ProjectId);

            DeleteProject(project.Id, client);
        }

        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void ExecuteCommandsAsync_UpdateUnexistingProject_ThrownsException()
        {
            var client = CreateClient();

            var newProjectName = Guid.NewGuid().ToString();

            Assert.Throws<AggregateException>(
                () =>
                    {
                        client.ExecuteCommandsAsync(
                            new Command(CommandType.UpdateProject, new Project(newProjectName), null)).Wait();
                    });
        }

        [Fact]
        [Trait(TraitConstants.Category, TraitConstants.Integration)]
        public void GetAllResources_HasProjects()
        {
            var client = CreateClient();

            var resources = client.GetResourcesAsync(ResourceType.All).Result;

            Assert.True(resources.Projects.Length > 0);
            Assert.True(resources.Items.Length > 0);
        }

        private static ITodoistClient CreateClient()
        {
            return new TodoistClient(SettingsProvider.GetToken());
        }

        private static void DeleteProject(ComplexId id, ITodoistClient client)
        {
            var idsArgument = new IdsArgument();
            idsArgument.Ids.Add(id);
            var deleteProjectCommand = new Command(CommandType.DeleteProject, idsArgument);
            client.ExecuteCommandsAsync(deleteProjectCommand).Wait();
        }
    }
}
