using Xunit.Sdk;

namespace Todoist.Net.Tests.Extensions
{
    public class IntegrationFree : CustomTraitBase, ITraitAttribute
    {
        public IntegrationFree()
        {
            Name = "integration-free";
        }
    }
}
