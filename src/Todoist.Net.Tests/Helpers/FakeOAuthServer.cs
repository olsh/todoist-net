using System.Net;
using System.Text;

namespace Todoist.Net.Tests.Helpers;

/// <summary>
/// Plays the part of the Todoist API and its OAuth endpoints: the API accepts only the access token issued last,
/// and the token endpoint rotates both tokens on every refresh.
/// </summary>
internal sealed class FakeOAuthServer : HttpMessageHandler
{
    public const string TokenEndpoint = "https://api.todoist.com/oauth/access_token";

    public const string RevokeEndpoint = "https://api.todoist.com/api/v1/revoke";

    private readonly Lock _requestsLock = new();

    private readonly List<RecordedRequest> _requests = [];

    private int _issuedTokenCount;

    public FakeOAuthServer(string validAccessToken)
    {
        ValidAccessToken = validAccessToken;
    }

    /// <summary>
    /// Gets or sets the only access token the API accepts.
    /// </summary>
    public string ValidAccessToken { get; set; }

    /// <summary>
    /// Gets or sets the handler of token requests, which issues new tokens by default.
    /// </summary>
    public Func<RecordedRequest, Task<HttpResponseMessage>>? TokenEndpointHandler { get; set; }

    /// <summary>
    /// Gets or sets a callback awaited before an API request is answered, which lets a test hold the response back.
    /// </summary>
    public Func<RecordedRequest, Task>? BeforeApiResponse { get; set; }

    public IReadOnlyList<RecordedRequest> Requests
    {
        get
        {
            lock (_requestsLock)
            {
                return [.. _requests];
            }
        }
    }

    public IReadOnlyList<RecordedRequest> TokenRequests => [.. Requests.Where(request => request.Uri == TokenEndpoint)];

    public IReadOnlyList<RecordedRequest> ApiRequests =>
        [.. Requests.Where(request => request.Uri != TokenEndpoint && request.Uri != RevokeEndpoint)];

    /// <summary>
    /// Issues the next pair of tokens, "access-N" and "refresh-N", and makes the API accept the new access token.
    /// </summary>
    public HttpResponseMessage IssueTokens(bool includeRefreshToken = true)
    {
        var number = Interlocked.Increment(ref _issuedTokenCount);
        ValidAccessToken = $"access-{number}";

        var refreshToken = includeRefreshToken ? $""", "refresh_token": "refresh-{number}" """ : string.Empty;

        return CreateJsonResponse(
            HttpStatusCode.OK,
            $$"""{ "access_token": "access-{{number}}", "token_type": "Bearer", "expires_in": 3600 {{refreshToken}} }""");
    }

    public static HttpResponseMessage CreateJsonResponse(HttpStatusCode statusCode, string json)
    {
        return new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var body = request.Content is null
            ? null
            : await request.Content.ReadAsStringAsync(cancellationToken);
        var recordedRequest = new RecordedRequest(
            request.Method,
            request.RequestUri!.ToString(),
            request.Headers.Authorization,
            request.Headers.ToDictionary(header => header.Key, header => string.Join(",", header.Value)),
            body);

        lock (_requestsLock)
        {
            _requests.Add(recordedRequest);
        }

        if (recordedRequest.Uri == TokenEndpoint)
        {
            return TokenEndpointHandler is null
                ? IssueTokens()
                : await TokenEndpointHandler(recordedRequest);
        }

        if (recordedRequest.Uri == RevokeEndpoint)
        {
            return CreateJsonResponse(HttpStatusCode.OK, "null");
        }

        if (BeforeApiResponse is not null)
        {
            await BeforeApiResponse(recordedRequest);
        }

        return recordedRequest.Authorization is { Scheme: "Bearer" } authorization &&
               authorization.Parameter == ValidAccessToken
            ? CreateJsonResponse(HttpStatusCode.OK, "{}")
            : CreateJsonResponse(
                HttpStatusCode.Unauthorized,
                """{ "error": "Unauthorized", "error_code": 477, "error_tag": "UNAUTHORIZED", "http_code": 401 }""");
    }
}
