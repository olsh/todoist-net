using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
    public class TemplateServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public TemplateServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task ExportAndImportTemplate_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var firstProject = (await client.Projects.GetAsync()).First();

            var template = await client.Templates.ExportAsFileAsync(firstProject.Id);

            var tempProject = new Project(Guid.NewGuid().ToString());
            await client.Projects.AddAsync(tempProject);
            try
            {
                await client.Templates.ImportIntoProjectAsync(tempProject.Id, Encoding.UTF8.GetBytes(template));
            }
            finally
            {
                await client.Projects.DeleteAsync(tempProject.Id);
            }
        }

        [Fact]
        public async Task ExportAsShareableUrl_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var firstProject = (await client.Projects.GetAsync()).First();
            var template = await client.Templates.ExportAsShareableUrlAsync(firstProject.Id);

            Assert.False(string.IsNullOrEmpty(template.FileUrl));
        }
    }
}
