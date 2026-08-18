using System.Net;
using System.Text;

namespace Todoist.Net.Tests.Helpers;

internal sealed class StubTodoistRestClient : ITodoistRestClient
{
    private Func<string, Dictionary<string, string>, CancellationToken, Task<HttpResponseMessage>>? _getAsyncHandler;
    private Func<string, Dictionary<string, string>, CancellationToken, Task<HttpResponseMessage>>? _postAsyncHandler;

    public string LastResource { get; private set; } = string.Empty;

    public Dictionary<string, string> LastQueryParams { get; private set; } = [];
    public Dictionary<string, string> LastFormParams { get; private set; } = [];


    public void RespondToGetJson(HttpStatusCode statusCode, string json)
    {
        _getAsyncHandler = (_, _, _) => Task.FromResult(CreateJsonResponse(statusCode, json));
    }

    public void RespondToPostJson(HttpStatusCode statusCode, string json)
    {
        _postAsyncHandler = (_, _, _) => Task.FromResult(CreateJsonResponse(statusCode, json));
    }


    public Task<HttpResponseMessage> GetAsync(string resource, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default)
    {
        LastResource = resource;
        LastQueryParams = queryParams is null ? [] : new Dictionary<string, string>(queryParams);

        return _getAsyncHandler?.Invoke(resource, LastQueryParams, cancellationToken)
            ?? throw new NotSupportedException("No GET handler configured.");
    }

    public Task<HttpResponseMessage> PostAsync(string resource, Dictionary<string, string>? formParams = null, CancellationToken cancellationToken = default)
    {
        LastResource = resource;
        LastFormParams = formParams is null ? [] : new Dictionary<string, string>(formParams);

        return _postAsyncHandler?.Invoke(resource, LastFormParams, cancellationToken)
            ?? throw new NotSupportedException("No POST handler configured.");
    }

    public Task<HttpResponseMessage> PostFilesAsync(string resource, UploadFile[] files, Dictionary<string, string>? formParams = null, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }

    public Task<HttpResponseMessage> PostJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }

    public Task<HttpResponseMessage> PutAsync(string resource, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }

    public Task<HttpResponseMessage> PutJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }

    public Task<HttpResponseMessage> DeleteAsync(string resource, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }

    public void Dispose()
    {
    }


    private static HttpResponseMessage CreateJsonResponse(HttpStatusCode statusCode, string json)
    {
        return new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
    }
}