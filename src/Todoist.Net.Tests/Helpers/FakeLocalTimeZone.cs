using System;
using System.Reflection;

namespace Todoist.Net.Tests.Helpers;

/// <summary>
/// A helper class that changes the local timezone to a fake timezone provided
/// at initialization, and resets the original local timezone when disposed.
/// </summary>
/// <remarks>
/// See this <see href="https://stackoverflow.com/questions/44413407/mock-the-country-timezone-you-are-running-unit-test-from">SO question</see> for more details.
/// </remarks>
public sealed class FakeLocalTimeZone : IDisposable
{

    /// <summary>
    /// Initializes a new instance of the <see cref="FakeLocalTimeZone"/> class
    /// and changes the local time zone to the given <paramref name="fakeTimeZoneInfo"/>
    /// until it's disposed.
    /// </summary>
    /// <param name="fakeTimeZoneInfo">The time zone to set as local until disposal.</param>
    public FakeLocalTimeZone(TimeZoneInfo fakeTimeZoneInfo)
    {
        var info = typeof(TimeZoneInfo).GetField("s_cachedData", BindingFlags.NonPublic | BindingFlags.Static);
        var cachedData = info.GetValue(null);

        var field = cachedData.GetType().GetField("_localTimeZone",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Instance);

        field.SetValue(cachedData, fakeTimeZoneInfo);
    }

    public void Dispose()
    {
        TimeZoneInfo.ClearCachedData();
    }
}
