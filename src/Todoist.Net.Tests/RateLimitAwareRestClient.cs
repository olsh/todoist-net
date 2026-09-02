using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Todoist.Net.Tests
{
    public sealed class RateLimitAwareRestClient : ITodoistRestClient
    {
        private const int MaxRetryCount = 60;
        private const string RateLimitResetHeaderName = "x-ratelimit-reset";

        private static readonly TimeSpan _defaultCooldown = TimeSpan.FromSeconds(30);

        private readonly ITestOutputHelper? _outputHelper;
        private readonly TodoistRestClient _restClient;

        public RateLimitAwareRestClient(string token, ITestOutputHelper? outputHelper = null)
        {
            _outputHelper = outputHelper;
            _restClient = new TodoistRestClient(token);
        }

        public void Dispose()
        {
            _restClient?.Dispose();
        }

        public Task<HttpResponseMessage> GetAsync(string resource, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default)
        {
            return ExecuteRequest(ct => _restClient.GetAsync(resource, queryParams, ct), cancellationToken);
        }

        public Task<HttpResponseMessage> PostAsync(string resource, Dictionary<string, string>? formParams = null, CancellationToken cancellationToken = default)
        {
            return ExecuteRequest(ct => _restClient.PostAsync(resource, formParams, ct), cancellationToken);
        }

        public Task<HttpResponseMessage> PostFilesAsync(string resource, UploadFile[] files, Dictionary<string, string>? formParams = null, CancellationToken cancellationToken = default)
        {
            return ExecuteRequest(ct => _restClient.PostFilesAsync(resource, files, formParams, ct), cancellationToken);
        }

        public Task<HttpResponseMessage> PostJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            return ExecuteRequest(ct => _restClient.PostJsonAsync(resource, jsonContent, ct), cancellationToken);
        }

        public Task<HttpResponseMessage> PutAsync(string resource, CancellationToken cancellationToken = default)
        {
            return ExecuteRequest(ct => _restClient.PutAsync(resource, ct), cancellationToken);
        }

        public Task<HttpResponseMessage> PutJsonAsync(string resource, string jsonContent, CancellationToken cancellationToken = default)
        {
            return ExecuteRequest(ct => _restClient.PutJsonAsync(resource, jsonContent, ct), cancellationToken);
        }

        public Task<HttpResponseMessage> DeleteAsync(string resource, Dictionary<string, string>? queryParams = null, CancellationToken cancellationToken = default)
        {
            return ExecuteRequest(ct => _restClient.DeleteAsync(resource, queryParams, ct), cancellationToken);
        }


        private async Task<HttpResponseMessage> ExecuteRequest(Func<CancellationToken, Task<HttpResponseMessage>> request, CancellationToken cancellationToken = default)
        {
            // For each user, you can make a maximum of 450 requests within a 15 minute period.
            int retryCount = 0;
            while (true)
            {
                var result = await request(cancellationToken).ConfigureAwait(false);

                // TooManyRequests (429) and randomly-occurring server errors (5xx) are retriable, others are not.
                if ((int)result.StatusCode is not 429 and < 500)
                {
                    return result;
                }

                retryCount++;
                if (retryCount > MaxRetryCount)
                {
                    LoggerMessage("[{0:G}] Stopping retries after max retry count ({1}).", DateTime.UtcNow, MaxRetryCount);
                    return result;
                }
                var cooldown = await GetRateLimitCooldownAsync(result, cancellationToken).ConfigureAwait(false);

                LoggerMessage("[{0:G}] Received [{1}] status code from Todoist API, retry #{2} in {3}", DateTime.UtcNow, result.StatusCode, retryCount, cooldown);

                result.Dispose();
                await Task.Delay(cooldown, cancellationToken).ConfigureAwait(false);
            }
        }

        private static async Task<TimeSpan> GetRateLimitCooldownAsync(HttpResponseMessage response, CancellationToken cancellationToken = default)
        {
            if (response.Headers.RetryAfter?.Delta is { } retryAfterDelta)
            {
                return retryAfterDelta;
            }

            if (response.Headers.TryGetValues(RateLimitResetHeaderName, out var resetValues))
            {
                var resetValue = resetValues.FirstOrDefault();
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
                    var errorResponse = await response.Content.ReadFromJsonAsync<RateLimitErrorResponse>(cancellationToken: cancellationToken)
                        .ConfigureAwait(false);

                    var retryAfterSeconds = errorResponse?.ErrorExtra?.RetryAfter ?? _defaultCooldown.TotalSeconds;
                    return TimeSpan.FromSeconds(retryAfterSeconds);
                }
                catch
                {
                    return _defaultCooldown;
                }
            }

            // Default cooldown
            return _defaultCooldown;
        }

        private void LoggerMessage(string format, params object[] args)
        {
            if (_outputHelper is not null)
            {
                _outputHelper.WriteLine(format, args);
                return;
            }
            TestContext.Current.SendDiagnosticMessage(format, args);
        }


        private sealed record RateLimitErrorResponse
        {
            [JsonPropertyName("error_extra")]
            public ErrorExtraInfo? ErrorExtra { get; init; }
        }

        private sealed record ErrorExtraInfo
        {
            [JsonPropertyName("retry_after")]
            public double? RetryAfter { get; init; }
        }
    }
}
