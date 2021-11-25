using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Todoist.Net.Tests
{
    public sealed class RateLimitAwareRestClient : ITodoistRestClient
    {
        private readonly TodoistRestClient restClient;

        public RateLimitAwareRestClient()
        {
            restClient = new TodoistRestClient();
        }

        public void Dispose()
        {
            restClient?.Dispose();
        }

        public async Task<HttpResponseMessage> ExecuteRequest(Func<Task<HttpResponseMessage>> request)
        {
            HttpResponseMessage result;

            // For each user, you can make a maximum of 450 requests within a 15 minute period.
            const int maxRetryCount = 35;
            const int delaySeconds = 30;

            int retryCount = 0;
            do
            {
                result = await request().ConfigureAwait(false);

                if ((int)result.StatusCode != 429 /*Requests limit*/  && 
                    (int)result.StatusCode < 500 /*Server side errors happen randomly*/)
                {
                    return result;
                }

                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                retryCount++;
            }
            while (retryCount < maxRetryCount);

            return result;
        }

        public async Task<HttpResponseMessage> PostAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters)
        {
            return await ExecuteRequest(() => restClient.PostAsync(resource, parameters)).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostFormAsync(
            string resource,
            IEnumerable<KeyValuePair<string, string>> parameters,
            IEnumerable<ByteArrayContent> files)
        {
            return await ExecuteRequest(() => restClient.PostFormAsync(resource, parameters, files))
                       .ConfigureAwait(false);
        }
    }
}
