using System;
using System.Linq;

using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests.Helpers;

[Trait(Constants.TraitName, Constants.UnitTraitValue)]
public class FakeLocalTimeZoneTests
{

    [Fact]
    public void FakeLocalTimeZone_ShouldChangeLocalTimeZoneWithinScope_AndResetItBackOutsideScope()
    {
        var actualTimeZoneInfo = TimeZoneInfo.Local;

        var timeZoneCollection = TimeZoneInfo
            .GetSystemTimeZones()
            .Where(t => !actualTimeZoneInfo.Equals(t))
            .ToArray();

        var randomIndex = new Random().Next(timeZoneCollection.Length);
        var fakeTimeZoneInfo = timeZoneCollection.ElementAt(randomIndex);


        Assert.NotEqual(fakeTimeZoneInfo, actualTimeZoneInfo);

        using (var fakeLocalTimeZone = new FakeLocalTimeZone(fakeTimeZoneInfo))
        {
            Assert.Equal(fakeTimeZoneInfo, TimeZoneInfo.Local);
        }
        Assert.Equal(actualTimeZoneInfo, TimeZoneInfo.Local);
    }

}
