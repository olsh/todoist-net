using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Todoist.Net.Models;

namespace Todoist.Net
{
    internal static class ApiRequestBuilder
    {
        public const string ResourcesBaseUri = "https://api.todoist.com/api/v1";
        public const string TokenRefreshUri = "https://api.todoist.com/oauth/access_token";
        public const string TokenRevokeUri = "https://api.todoist.com/api/v1/revoke";
        public const string NonReplayableRequestKey = "Todoist.Net.NonReplayableRequest";

        
        public static HttpRequestMessage BuildRefreshTokensRequest(string clientId, string clientSecret, string refreshToken)
        {
            var formParams = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "refresh_token", refreshToken },
                { "grant_type", "refresh_token" }
            };

            return new HttpRequestMessage(HttpMethod.Post, TokenRefreshUri)
            {
                Content = new FormUrlEncodedContent(formParams)
            };
        }

        public static HttpRequestMessage BuildRevokeTokenRequest(string clientId, string clientSecret, string accessToken)
        {
            var formParams = new Dictionary<string, string>
            {
                { "token", accessToken },
                { "token_type_hint", "access_token" }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, TokenRevokeUri)
            {
                Content = new FormUrlEncodedContent(formParams)
            };
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));
                    
            return request;
        }
        

        public static HttpRequestMessage BuildResourceRequest(HttpMethod method, string resource, Dictionary<string, string> queryParams = null)
        {
            var requestUri = BuildResourceUri(resource, queryParams);

            return new HttpRequestMessage(method, requestUri);
        }

        public static HttpRequestMessage BuildResourceJsonRequest(HttpMethod method, string resource, string jsonContent)
        {
            var requestUri = BuildResourceUri(resource);

            return new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };
        }

        public static HttpRequestMessage BuildResourceFormRequest(string resource, Dictionary<string, string> formParams, UploadFile[] formFiles = null)
        {
            var requestUri = BuildResourceUri(resource);
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            
            if (formFiles == null)
            {
                request.Content = new FormUrlEncodedContent(formParams);
                return request;
            }

            request.Content = new MultipartFormDataContent()
                .AddStringParts(formParams)
                .AddFileParts("file", formFiles);

            if (formFiles.Any(file => !file.ContentStream.CanSeek))
            {
                // A stream which cannot be rewound is read only once, so the request cannot be sent again with refreshed tokens.
                request.Properties[NonReplayableRequestKey] = true;
            }
            return request;
        }


        private static string BuildResourceUri(string resource, Dictionary<string, string> queryParams = null)
        {
            if (queryParams == null || queryParams.Count == 0)
            {
                return $"{ResourcesBaseUri}/{resource}";
            }
            var queryBuilder = new StringBuilder();

            foreach (var pair in queryParams)
            {
                if (queryBuilder.Length > 0)
                {
                    queryBuilder.Append('&');
                }
                queryBuilder.Append(EncodeUriData(pair.Key));
                queryBuilder.Append('=');
                queryBuilder.Append(EncodeUriData(pair.Value));
            }
            return $"{ResourcesBaseUri}/{resource}?{queryBuilder}";
        }

        private static string EncodeUriData(string data)
        {
            return string.IsNullOrEmpty(data)
                ? string.Empty
                : Uri.EscapeDataString(data).Replace("%20", "+");
        }
    }
}
