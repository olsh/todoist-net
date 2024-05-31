using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todoist.Net.Tests.Extensions;

using Xunit;
using Xunit.Abstractions;

namespace Todoist.Net.Tests.Services
{
    [Collection(Constants.TodoistApiTestCollectionName)]
    public class UploadServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public UploadServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task CreateGetDeleteAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var fileName = $"{Guid.NewGuid()}.txt";
            var upload = await client.Uploads.UploadAsync(fileName, Encoding.UTF8.GetBytes("hello"));
            try
            {
                var allUploads = await client.Uploads.GetAsync();
                Assert.Contains(allUploads, u => u.FileUrl == upload.FileUrl);
            }
            finally
            {
                await client.Uploads.DeleteAsync(upload.FileUrl);
            }
            var otherUploads = await client.Uploads.GetAsync();
            Assert.DoesNotContain(otherUploads, u => u.FileUrl == upload.FileUrl);
        }
    }
}
