namespace Todoist.Net
{
    internal static class ApiConstants
    {
        public const string ApiBaseUrl = "https://api.todoist.com";

        public const string ResourcesEndpoint = "/api/v1";
        public const string TokenRefreshEndpoint = "/oauth/access_token";
        public const string TokenRevokeEndpoint = "/api/v1/revoke";

        public const string HttpClientName = "Todoist.Net.HttpClient";
        public const string FlurlClientName = "Todoist.Net.FlurlClient";
    }
}
