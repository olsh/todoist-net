using Microsoft.Extensions.Configuration;

namespace Todoist.Net.Tests.Settings
{
    public static class SettingsProvider
    {
        public static string GetToken()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets();
            var configuration = builder.Build();

            return configuration["token"];
        }
    }
}
