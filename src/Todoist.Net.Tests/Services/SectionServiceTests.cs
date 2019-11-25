using System.Threading.Tasks;

using Todoist.Net.Models;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class SectionServiceTests
    {
        [Fact]
        public async Task CreateArchiveUnarchiveDelete_Success()
        {
            var client = TodoistClientFactory.Create();

            var projectId = await client.Projects.AddAsync(new Project("test"));
            var sectionId = await client.Sections.AddAsync(new Section("test", projectId));
            await client.Sections.ArchiveAsync(sectionId);
            await client.Sections.UnarchiveAsync(sectionId);

            var projectTwoId = await client.Projects.AddAsync(new Project("test2"));
            var sectionMoveArgument = new SectionMoveArgument(sectionId, projectTwoId);
            await client.Sections.MoveAsync(sectionMoveArgument);

            await client.Sections.DeleteAsync(sectionId);
            await client.Projects.DeleteAsync(projectId);
            await client.Projects.DeleteAsync(projectTwoId);
        }

        [Fact]
        public async Task Reorder_Success()
        {
            var client = TodoistClientFactory.Create();

            var projectId = await client.Projects.AddAsync(new Project("test"));
            var firstId = await client.Sections.AddAsync(new Section("test", projectId));
            var secondId = await client.Sections.AddAsync(new Section("test2", projectId));

            await client.Sections.ReorderAsync(new SectionOrderEntry(secondId, 1), new SectionOrderEntry(firstId, 2));

            await client.Projects.DeleteAsync(projectId);
        }
    }
}
