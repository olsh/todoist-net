using System.Text.Json;

namespace Todoist.Net.Tests.Extensions;

internal static class CommandResultExtensions
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web) { WriteIndented = true };

    internal static void AssertSuccess(this CommandResult commandResult)
    {
        var commandBodyJson = commandResult.CommandBody is null
            ? null
            : JsonSerializer.Serialize(commandResult.CommandBody, _jsonSerializerOptions);

        Assert.True(
            commandResult.IsSuccess,
            $"Command failed. CommandBody={commandBodyJson}");
    }
}