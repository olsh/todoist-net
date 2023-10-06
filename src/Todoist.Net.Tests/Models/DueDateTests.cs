using System;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;
using Todoist.Net.Tests.Helpers;

using Xunit;

namespace Todoist.Net.Tests.Models
{
    [Trait(Constants.TraitName, Constants.UnitTraitValue)]
    public class DueDateTests : IDisposable
    {

        private readonly FakeLocalTimeZone _fakeLocalTimeZone;

        public DueDateTests()
        {
            var timeZoneCollection = System.TimeZoneInfo.GetSystemTimeZones();

            var randomIndex = new Random().Next(timeZoneCollection.Count);
            var fakeTimeZoneInfo = timeZoneCollection[randomIndex];

            _fakeLocalTimeZone = new FakeLocalTimeZone(fakeTimeZoneInfo);
        }

        public void Dispose()
        {
            _fakeLocalTimeZone.Dispose();
            GC.SuppressFinalize(this);
        }


        [Fact]
        public void DateTimeAssignment_FullDayEvent_Success()
        {
            var date = new DateTime(2018, 2, 5, 0, 0, 0, DateTimeKind.Utc);

            var dueDate = new DueDate(date, true);

            Assert.Equal("2018-02-05", dueDate.StringDate);
            Assert.True(dueDate.IsFullDay);
        }

        [Fact]
        public void DateTimeAssignment_FloatingDueDateEvent_Success()
        {
            var date = new DateTime(2018, 2, 5, 0, 0, 0, DateTimeKind.Unspecified);

            var dueDate = new DueDate(date);

            Assert.Equal("2018-02-05T00:00:00", dueDate.StringDate);
            Assert.False(dueDate.IsFullDay);
        }

        [Fact]
        public void DateTimeAssignment_FloatingDueDateWithTimezoneEvent_Success()
        {
            var date = new DateTime(2018, 2, 5, 0, 0, 0, DateTimeKind.Utc);

            var dueDate = new DueDate(date, false, "Asia/Jakarta");

            Assert.Equal("2018-02-05T00:00:00Z", dueDate.StringDate);
            Assert.False(dueDate.IsFullDay);
        }


        [Fact]
        public void StringDateProperty_ShouldReturnExactAssignedValue_WhenValueIsFullDayDate()
        {
            // Arrange
            var dueDate = new DueDate();
            string initialValue = "2016-12-01";

            // Act
            dueDate.StringDate = initialValue; // Set initial value.
            string returnedValue = dueDate.StringDate; // Get.
            dueDate.StringDate = returnedValue; // Set returned value.

            // Assert
            Assert.Equal(returnedValue, dueDate.StringDate);
        }

        [Fact]
        public void StringDateProperty_ShouldReturnExactAssignedValue_WhenValueIsFloatingDate()
        {
            // Arrange
            var dueDate = new DueDate();
            string initialValue = "2016-12-03T12:00:00";

            // Act
            dueDate.StringDate = initialValue; // Set initial value.
            string returnedValue = dueDate.StringDate; // Get.
            dueDate.StringDate = returnedValue; // Set returned value.

            // Assert
            Assert.Equal(returnedValue, dueDate.StringDate);
        }

        [Fact]
        public void StringDateProperty_ShouldReturnExactAssignedValue_WhenValueIsFixedDate()
        {
            // Arrange
            var dueDate = new DueDate();
            string initialValue = "2016-12-06T13:00:00Z";

            // Act
            dueDate.StringDate = initialValue; // Set initial value.
            string returnedValue = dueDate.StringDate; // Get.
            dueDate.StringDate = returnedValue; // Set returned value.

            // Assert
            Assert.Equal(returnedValue, dueDate.StringDate);
        }
    }
}
