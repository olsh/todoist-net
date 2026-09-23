using System;
using System.Threading;
using System.Threading.Tasks;

namespace Todoist.Net
{
    internal static class TaskExtensions
    {
        public static async Task<T> WithCancellationAsync<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            // Equivalent of Task.WaitAsync(CancellationToken), which is not available on netstandard2.0/net462.
            // Cancelling only abandons this caller's wait; the underlying task keeps running.
            var cancellationCompletion = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            using (cancellationToken.Register(() => cancellationCompletion.TrySetResult(true)))
            {
                if (task != await Task.WhenAny(task, cancellationCompletion.Task).ConfigureAwait(false))
                {
                    throw new OperationCanceledException(cancellationToken);
                }
            }

            return await task.ConfigureAwait(false);
        }
    }
}
