using System;
using System.Linq;
using System.Text;

using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests.Services
{
    public class UploadServiceTests
    {
        [Fact]
        [IntegrationFree]
        public void CreateGetDeleteAsync_Success()
        {
            var client = TodoistClientFactory.Create();

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
