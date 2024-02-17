using System;

using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests.Helpers;

[Trait(Constants.TraitName, Constants.UnitTraitValue)]
public class FakeLocalTimeZoneTests
{

    [Fact]
    public void FakeLocalTimeZone_ShouldChangeLocalTimeZoneWithinScope_AndResetItBackOutsideScope()
    {
        var offsetDifference = TimeZoneInfo.Local.BaseUtcOffset < TimeSpan.FromHours(10) ? 2 : -2;
        var fakeTimeZoneOffset = TimeZoneInfo.Local.BaseUtcOffset + TimeSpan.FromHours(offsetDifference);

        var fakeLocalTimeZone = FakeLocalTimeZone.ChangeLocalTimeZone(fakeTimeZoneOffset);
        using (fakeLocalTimeZone)
        {
            Assert.Equal(fakeLocalTimeZone.FakeTimeZoneInfo, TimeZoneInfo.Local);
        }
        Assert.NotEqual(fakeLocalTimeZone.FakeTimeZoneInfo, TimeZoneInfo.Local);
    }

}
