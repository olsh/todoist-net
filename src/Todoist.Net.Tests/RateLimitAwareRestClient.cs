using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

using Xunit.Abstractions;

namespace Todoist.Net.Tests
{
    public sealed class RateLimitAwareRestClient : ITodoistRestClient
    {
        private readonly ITestOutputHelper _outputHelper;

        private readonly TodoistRestClient _restClient;
        private static readonly TimeSpan DefaultCooldown = TimeSpan.FromSeconds(30);
        private const int MaxRetryCount = 60;

        public RateLimitAwareRestClient(string token, ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _restClient = new TodoistRestClient(token);
        }

        public void Dispose()
        {
            _restClient?.Dispose();
        }

        public async Task<HttpResponseMessage> ExecuteRequest(Func<Task<HttpResponseMessage>> request, CancellationToken cancellationToken = default)
        {
            HttpResponseMessage result;

            // For each user, you can make a maximum of 450 requests within a 15 minute period.
            int retryCount = 0;
            do
            {
                cancellationToken.ThrowIfCancellationRequested();
                result = await request().ConfigureAwait(false);
                if ((int)result.StatusCode != 429 /*Requests limit*/  &&
                    (int)result.StatusCode < 500 /*Server side errors happen randomly*/)
                {
                    return result;
                }

                var cooldown = await GetRateLimitCooldown(result).ConfigureAwait(false);
                retryCount++;

                _outputHelper.WriteLine("[{0:G}] Received [{1}] status code from Todoist API, retry #{2} in {3}", DateTime.UtcNow, result.StatusCode, retryCount, cooldown);
                result.Dispose();
                await Task.Delay(cooldown, cancellationToken).ConfigureAwait(false);
            }
            while (retryCount < MaxRetryCount);

            _outputHelper.WriteLine("[{0:G}] Stopping retries after max retry count ({1}).", DateTime.UtcNow, MaxRetryCount);

            return result;
        }

        public async Task<HttpResponseMessage> GetAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(() => _restClient.GetAsync(resource, parameters, cancellationToken), cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(() => _restClient.PostAsync(resource, parameters, cancellationToken), cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostFormAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            IEnumerable<UploadFile> files,
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(() => _restClient.PostFormAsync(resource, parameters, files, cancellationToken), cancellationToken)
                       .ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostJsonAsync(
            string resource,
            string jsonContent,
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(() => _restClient.PostJsonAsync(resource, jsonContent, cancellationToken), cancellationToken)
                       .ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PutAsync(
            string resource,
            string jsonContent,
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(() => _restClient.PutAsync(resource, jsonContent, cancellationToken), cancellationToken)
                       .ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> DeleteAsync(
            string resource,
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(() => _restClient.DeleteAsync(resource, cancellationToken), cancellationToken)
                       .ConfigureAwait(false);
        }

        public async Task<TimeSpan> GetRateLimitCooldown(HttpResponseMessage response)
        {
            if (response.Headers.RetryAfter?.Delta is { } retryAfterDelta)
            {
                return retryAfterDelta;
            }

            if (response.Headers.TryGetValues("x-ratelimit-reset", out var resetValues))
            {
                var resetValue = System.Linq.Enumerable.FirstOrDefault(resetValues);
                if (long.TryParse(resetValue, out var resetUnixSeconds))
                {
                    var resetAt = DateTimeOffset.FromUnixTimeSeconds(resetUnixSeconds);
                    var remaining = resetAt - DateTimeOffset.UtcNow;
                    if (remaining <= TimeSpan.Zero)
                    {
                        return TimeSpan.FromSeconds(1);
                    }

                    return remaining;
                }
            }

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    JObject json = JObject.Parse(content);
                    var retryAfterSeconds = json["error_extra"]?["retry_after"]?.Value<double>() ?? DefaultCooldown.TotalSeconds;
                    return TimeSpan.FromSeconds(retryAfterSeconds);
                }
                catch
                {
                    return DefaultCooldown;
                }
            }

            // Default cooldown
            return DefaultCooldown;
        }
    }
}
