using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Xunit.Abstractions;

namespace Todoist.Net.Tests
{
    public sealed class RateLimitAwareRestClient : ITodoistRestClient
    {
        private readonly ITestOutputHelper _outputHelper;

        private readonly TodoistRestClient _restClient;

        public RateLimitAwareRestClient(string token, ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _restClient = new TodoistRestClient(token);
        }

        public void Dispose()
        {
            _restClient?.Dispose();
        }

        public async Task<HttpResponseMessage> ExecuteRequest(Func<Task<HttpResponseMessage>> request)
        {
            HttpResponseMessage result;

            // For each user, you can make a maximum of 450 requests within a 15 minute period.
            const int maxRetryCount = 35;
            int retryCount = 0;
            do
            {
                result = await request().ConfigureAwait(false);
                if ((int)result.StatusCode != 429 /*Requests limit*/  &&
                    (int)result.StatusCode < 500 /*Server side errors happen randomly*/)
                {
                    return result;
                }

                var cooldown = await GetRateLimitCooldown(result).ConfigureAwait(false);
                retryCount++;

                _outputHelper.WriteLine("[{0:G}] Received [{1}] status code from Todoist API, retry #{2} in {3}", DateTime.UtcNow, result.StatusCode, retryCount, cooldown);
                await Task.Delay(cooldown);
            }
            while (retryCount < maxRetryCount);

            return result;
        }

        public async Task<HttpResponseMessage> GetAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(() => _restClient.GetAsync(resource, parameters, cancellationToken)).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(() => _restClient.PostAsync(resource, parameters, cancellationToken)).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostFormAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            IEnumerable<ByteArrayContent> files,
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(() => _restClient.PostFormAsync(resource, parameters, files, cancellationToken))
                       .ConfigureAwait(false);
        }

        public async Task<TimeSpan> GetRateLimitCooldown(HttpResponseMessage response)
        {
            var defaultCooldown = TimeSpan.FromSeconds(30);
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    JObject json = JObject.Parse(content);

                    return TimeSpan.FromSeconds(json["error_extra"]!["retry_after"]!.Value<double>());
                }
                catch
                {
                    return defaultCooldown;
                }
            }

            // Default cooldown
            return defaultCooldown;
        }
    }
}
