using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
    public class SectionServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public SectionServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task CreateArchiveUnarchiveDelete_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var projectId = await client.Projects.AddAsync(new Project("test"));
            try
            {
                var sectionId = await client.Sections.AddAsync(new Section("test", projectId));
                await client.Sections.ArchiveAsync(sectionId);
                await client.Sections.UnarchiveAsync(sectionId);

                var projectTwoId = await client.Projects.AddAsync(new Project("test2"));
                try
                {
                    var sectionMoveArgument = new SectionMoveArgument(sectionId, projectTwoId);
                    await client.Sections.MoveAsync(sectionMoveArgument);

                    await client.Sections.DeleteAsync(sectionId);
                }
                finally
                {
                    await client.Projects.DeleteAsync(projectTwoId);
                }
            }
            finally
            {
                await client.Projects.DeleteAsync(projectId);
            }
        }

        [Fact]
        public async Task Reorder_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var projectId = await client.Projects.AddAsync(new Project("test"));
            try
            {
                var firstId = await client.Sections.AddAsync(new Section("test", projectId));
                var secondId = await client.Sections.AddAsync(new Section("test2", projectId));

                await client.Sections.ReorderAsync(new SectionOrderEntry(secondId, 1), new SectionOrderEntry(firstId, 2));
            }
            finally
            {
                await client.Projects.DeleteAsync(projectId);
            }
        }
    }
}
