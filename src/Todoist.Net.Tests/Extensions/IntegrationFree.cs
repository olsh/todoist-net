using Xunit.Sdk;

namespace Todoist.Net.Tests.Extensions
{
    public class IntegrationFree : CustomTraitBaseAttribute, ITraitAttribute
    {
        public IntegrationFree()
        {
            Name = "integration-free";
        }
    }
}
