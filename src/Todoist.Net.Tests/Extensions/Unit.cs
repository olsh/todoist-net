using Xunit.Sdk;

namespace Todoist.Net.Tests.Extensions
{
    public class Unit : CustomTraitBaseAttribute, ITraitAttribute
    {
        public Unit()
        {
            Name = "unit";
        }
    }
}
