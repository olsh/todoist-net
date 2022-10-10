using System;
using System.Linq;
using System.Text;

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
        [IntegrationFree]
        public void CreateGetDeleteAsync_Success()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var fileName = $"{Guid.NewGuid().ToString()}.txt";
            var upload = client.Uploads.UploadAsync(fileName, Encoding.UTF8.GetBytes("hello")).Result;

            var allUploads = client.Uploads.GetAsync().Result;
            Assert.Contains(allUploads, u => u.FileUrl == upload.FileUrl);

            client.Uploads.DeleteAsync(upload.FileUrl).Wait();

            allUploads = client.Uploads.GetAsync().Result;
            Assert.True(allUploads.All(u => u.FileUrl != upload.FileUrl));
        }
    }
}
