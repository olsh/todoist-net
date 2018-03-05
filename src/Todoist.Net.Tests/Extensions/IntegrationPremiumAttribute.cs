using Xunit.Sdk;

namespace Todoist.Net.Tests.Extensions
{
    public class IntegrationPremiumAttribute : CustomTraitBaseAttribute, ITraitAttribute
    {
        public IntegrationPremiumAttribute()
        {
            Name = "integration-premium";
        }
    }
}
