namespace Todoist.Net
{
    internal static class ApiConstants
    {
        public const string ApiBaseUrl = "https://api.todoist.com/api/v1/";

        public const string ResourcesEndpoint = "api/v1/";
        public const string TokenRefreshEndpoint = "oauth/access_token/";
        public const string TokenRevokeEndpoint = "api/v1/access_tokens/";

        public const string HttpClientName = "Todoist.Net.HttpClient";
    }
}
