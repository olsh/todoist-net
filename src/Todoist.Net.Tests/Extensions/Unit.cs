using Xunit.Sdk;

namespace Todoist.Net.Tests.Extensions
{
    public class Unit : CustomTraitBase, ITraitAttribute
    {
        public Unit()
        {
            Name = "unit";
        }
    }
}
