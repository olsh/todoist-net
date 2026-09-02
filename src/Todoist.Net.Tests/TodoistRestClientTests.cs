using System.Net;

namespace Todoist.Net.Tests;

[Trait(Constants.TraitName, Constants.UnitTraitValue)]
public class TodoistRestClientTests
{
    [Fact]
    public async Task PostFiles_SendingTheSameFileTwice_KeepsTheCallerOwnedStreamUsable()
    {
        var messageHandler = new RecordingHttpMessageHandler();
        using var httpClient = new HttpClient(messageHandler);
        using var restClient = new TodoistRestClient("token", httpClient);

        var file = new UploadFile(TestData.Files.GreenPng10x10, "green.png");


        // Step 1: Send the same file twice, the way a retried request would.
        (await restClient.PostFilesAsync("uploads", [file], cancellationToken: TestContext.Current.CancellationToken)).Dispose();
        (await restClient.PostFilesAsync("uploads", [file], cancellationToken: TestContext.Current.CancellationToken)).Dispose();


        // Step 2: Assert the stream outlived the requests and the whole file was sent every time.
        Assert.True(file.ContentStream.CanRead);
        Assert.Equal(2, messageHandler.RequestBodies.Count);
        Assert.Equal(messageHandler.RequestBodies[0].Length, messageHandler.RequestBodies[1].Length);
        Assert.All(
            messageHandler.RequestBodies,
            body => Assert.True(body.Length > TestData.Files.GreenPng10x10.Length));
    }

    private sealed class RecordingHttpMessageHandler : HttpMessageHandler
    {
        public List<byte[]> RequestBodies { get; } = [];

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestBodies.Add(request.Content is null
                ? []
                : await request.Content.ReadAsByteArrayAsync(cancellationToken));

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
