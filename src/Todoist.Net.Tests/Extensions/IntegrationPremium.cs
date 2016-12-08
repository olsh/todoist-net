using Xunit.Sdk;

namespace Todoist.Net.Tests.Extensions
{
    public class IntegrationPremium : CustomTraitBase, ITraitAttribute
    {
        public IntegrationPremium()
        {
            Name = "integration-premium";
        }
    }
}
