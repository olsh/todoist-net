using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests.Models
{
    [Unit]
    public class StringEnumTests
    {
        [Fact]
        public void TryParse_InvalidValue_Fail()
        {
            StringEnum result;

            Assert.False(StringEnum.TryParse("all1", typeof(ResourceType), out result));
            Assert.True(result == null);
        }

        [Fact]
        public void TryParse_ValidValue_Success()
        {
            StringEnum result;

            Assert.True(StringEnum.TryParse("all", typeof(ResourceType), out result));
            Assert.True(result != null);
        }
    }
}
