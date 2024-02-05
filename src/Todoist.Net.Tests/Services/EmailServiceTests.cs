using System;
using System.Threading.Tasks;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    public class EmailServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public EmailServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationPremiumTraitValue)]
        public async Task GetOrCreateAsyncDisable_NewProject_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var project = new Project(Guid.NewGuid().ToString());
            var projectId = await client.Projects.AddAsync(project);

            var emailInfo = await client.Emails.GetOrCreateAsync(ObjectType.Project, projectId);

            Assert.NotNull(emailInfo);
            Assert.NotNull(emailInfo.Email);

            await client.Emails.DisableAsync(ObjectType.Project, projectId);
            await client.Projects.DeleteAsync(projectId);
        }
    }
}
