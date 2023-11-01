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
    /// The fake time zone info that has been set as local.
    /// </summary>
    public TimeZoneInfo FakeTimeZoneInfo { get; }


    /// <summary>
    /// Initializes a new instance of the <see cref="FakeLocalTimeZone"/> class.
    /// </summary>
    private FakeLocalTimeZone(TimeZoneInfo fakeTimeZoneInfo)
    {
        FakeTimeZoneInfo = fakeTimeZoneInfo;

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


    /// <summary>
    /// Changes the local time zone to the given <paramref name="fakeTimeZoneInfo"/>.
    /// </summary>
    /// <remarks>
    /// Disposal of the returned object resets the local time zone to the original one.
    /// </remarks>
    /// <param name="fakeTimeZoneInfo">The time zone to set as local until disposal.</param>
    /// <returns>
    /// A <see cref="FakeLocalTimeZone"/> instance that represents the time zone change,
    /// and used to reset it back to original at disposal.
    /// </returns>
    public static FakeLocalTimeZone ChangeLocalTimeZone(TimeZoneInfo fakeTimeZoneInfo)
    {
        return new FakeLocalTimeZone(fakeTimeZoneInfo);
    }

    /// <summary>
    /// Changes the local time zone to a custom time zone with a <paramref name="baseUtcOffset"/>.
    /// </summary>
    /// <remarks>
    /// Disposal of the returned object resets the local time zone to the original one.
    /// </remarks>
    /// <param name="baseUtcOffset">UTC offset of the custom time zone.</param>
    /// <returns>
    /// A <see cref="FakeLocalTimeZone"/> instance that represents the time zone change,
    /// and used to reset it back to original at disposal.
    /// </returns>
    public static FakeLocalTimeZone ChangeLocalTimeZone(TimeSpan baseUtcOffset)
    {
        var fakeId = "Fake TimeZone";
        var fakeDisplayName = $"(UTC+{baseUtcOffset:hh':'mm})";

        var fakeTimeZoneInfo = TimeZoneInfo.CreateCustomTimeZone(fakeId, baseUtcOffset, fakeDisplayName, fakeDisplayName);

        return new FakeLocalTimeZone(fakeTimeZoneInfo);
    }

}
