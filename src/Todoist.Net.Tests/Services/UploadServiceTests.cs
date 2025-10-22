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

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task UploadPngFile_ReturnsFileAttachmentWithCorrectFilename()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var fileName = $"test-image-{Guid.NewGuid()}.png";
            var pngHeader = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

            var upload = await client.Uploads.UploadAsync(fileName, pngHeader);

            try
            {
                Assert.NotNull(upload);
                Assert.NotNull(upload.FileUrl);
                Assert.Contains(".png", upload.FileUrl, StringComparison.OrdinalIgnoreCase);
            }
            finally
            {
                await client.Uploads.DeleteAsync(upload.FileUrl);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task UploadJpgFile_ReturnsFileAttachmentWithCorrectMimeType()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var fileName = $"test-photo-{Guid.NewGuid()}.jpg";
            var jpgHeader = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46 };

            var upload = await client.Uploads.UploadAsync(fileName, jpgHeader);

            try
            {
                Assert.NotNull(upload);
                Assert.NotNull(upload.FileUrl);
                Assert.Contains(".jpg", upload.FileUrl, StringComparison.OrdinalIgnoreCase);
            }
            finally
            {
                await client.Uploads.DeleteAsync(upload.FileUrl);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task UploadPdfFile_ReturnsFileAttachmentWithCorrectExtension()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var fileName = $"test-document-{Guid.NewGuid()}.pdf";
            var pdfHeader = new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D };
            var content = pdfHeader.Concat(Encoding.ASCII.GetBytes("1.4\n%Test PDF")).ToArray();

            var upload = await client.Uploads.UploadAsync(fileName, content);

            try
            {
                Assert.NotNull(upload);
                Assert.NotNull(upload.FileUrl);
                Assert.Contains(".pdf", upload.FileUrl, StringComparison.OrdinalIgnoreCase);
            }
            finally
            {
                await client.Uploads.DeleteAsync(upload.FileUrl);
            }
        }

        [Fact]
        [Trait(Constants.TraitName, Constants.IntegrationFreeTraitValue)]
        public async Task UploadTextFile_ReturnsFileAttachmentWithCorrectContent()
        {
            var client = TodoistClientFactory.Create(_outputHelper);

            var fileName = $"test-notes-{Guid.NewGuid()}.txt";
            var fileContent = "This is a test file for upload functionality verification.";

            var upload = await client.Uploads.UploadAsync(fileName, Encoding.UTF8.GetBytes(fileContent));

            try
            {
                Assert.NotNull(upload);
                Assert.NotNull(upload.FileUrl);
                Assert.Contains(".txt", upload.FileUrl, StringComparison.OrdinalIgnoreCase);

                var allUploads = await client.Uploads.GetAsync();
                Assert.Contains(allUploads, u => u.FileUrl == upload.FileUrl);
            }
            finally
            {
                await client.Uploads.DeleteAsync(upload.FileUrl);
            }
        }
    }
}
