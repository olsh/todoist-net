using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Web;

namespace Todoist.Net.Tests.Helpers;

/// <summary>
/// A request received by <see cref="FakeOAuthServer" />.
/// </summary>
internal sealed record RecordedRequest(
    HttpMethod Method,
    string Uri,
    AuthenticationHeaderValue? Authorization,
    IReadOnlyDictionary<string, string> Headers,
    string? Body)
{
    /// <summary>
    /// Gets the body parsed as form data.
    /// </summary>
    public NameValueCollection Form => HttpUtility.ParseQueryString(Body ?? string.Empty);
}
