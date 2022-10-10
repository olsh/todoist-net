using System;
using System.Linq;
using System.Text;

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
        public void ExportAndImportTemplate_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var firstProject = client.Projects.GetAsync().Result.First();

            var template = client.Templates.ExportAsFileAsync(firstProject.Id).Result;

            var tempProject = new Project(Guid.NewGuid().ToString());
            client.Projects.AddAsync(tempProject).Wait();

            client.Templates.ImportIntoProjectAsync(tempProject.Id, Encoding.UTF8.GetBytes(template)).Wait();

            client.Projects.DeleteAsync(tempProject.Id);
        }

        [Fact]
        public void ExportAsShareableUrl_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var firstProject = client.Projects.GetAsync().Result.First();
            var template = client.Templates.ExportAsShareableUrlAsync(firstProject.Id).Result;

            Assert.True(!string.IsNullOrEmpty(template.FileUrl));
        }
    }
}
