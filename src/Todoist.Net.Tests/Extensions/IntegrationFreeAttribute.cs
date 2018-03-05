using Xunit.Sdk;

namespace Todoist.Net.Tests.Extensions
{
    public class IntegrationFreeAttribute : CustomTraitBaseAttribute, ITraitAttribute
    {
        public IntegrationFreeAttribute()
        {
            Name = "integration-free";
        }
    }
}
