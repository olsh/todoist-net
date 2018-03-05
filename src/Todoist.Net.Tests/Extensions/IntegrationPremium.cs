using Xunit.Sdk;

namespace Todoist.Net.Tests.Extensions
{
    public class IntegrationPremium : CustomTraitBaseAttribute, ITraitAttribute
    {
        public IntegrationPremium()
        {
            Name = "integration-premium";
        }
    }
}
