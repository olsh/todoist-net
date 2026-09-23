using System.Collections.Concurrent;

using Todoist.Net.OAuth;

namespace Todoist.Net.Tests.Helpers;

/// <summary>
/// Stands in for the storage of refreshed OAuth tokens and remembers every tokens it was given.
/// </summary>
internal sealed class TokensRecorder
{
    private readonly ConcurrentQueue<TodoistTokens> _tokens = new();

    public IReadOnlyList<TodoistTokens> Tokens => [.. _tokens];

    public Task StoreAsync(TodoistTokens tokens)
    {
        _tokens.Enqueue(tokens);
        return Task.CompletedTask;
    }
}
