using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests.Models
{
    [Trait(Constants.TraitName, Constants.UnitTraitValue)]
    public class StringEnumTests
    {
        [Fact]
        public void TryParse_InvalidValue_Fail()
        {
            Assert.False(StringEnum.TryParse("all1", out ResourceType result));
            Assert.Null(result);
        }

        [Fact]
        public void TryParse_ValidValue_Success()
        {
            Assert.True(StringEnum.TryParse("all", out ResourceType result));
            Assert.NotNull(result);
        }
    }
}
