using System;

namespace Todoist.Net.Tests.Settings
{
    public static class SettingsProvider
    {
        public static string GetToken()
        {
            return Environment.GetEnvironmentVariable("todoist:token");
        }
    }
}
