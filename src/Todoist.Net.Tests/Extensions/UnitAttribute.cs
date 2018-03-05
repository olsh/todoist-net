using Xunit.Sdk;

namespace Todoist.Net.Tests.Extensions
{
    public class UnitAttribute : CustomTraitBaseAttribute, ITraitAttribute
    {
        public UnitAttribute()
        {
            Name = "unit";
        }
    }
}
