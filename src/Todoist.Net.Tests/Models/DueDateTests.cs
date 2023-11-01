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
            _fakeLocalTimeZone = FakeLocalTimeZone.ChangeLocalTimeZone(TimeSpan.FromHours(5));
        }

        public void Dispose()
        {
            _fakeLocalTimeZone?.Dispose();
        }


        [Fact]
        public void DateTimeAssignment_FullDayEvent_Success()
        {
            var date = new DateTime(2018, 2, 5);

            var dueDate = DueDate.CreateFullDay(date);

            Assert.Equal("2018-02-05", dueDate.StringDate);
            Assert.True(dueDate.IsFullDay);
        }

        [Fact]
        public void DateTimeAssignment_FloatingDueDateEvent_Success()
        {
            var date = new DateTime(2018, 2, 5);

            var dueDate = DueDate.CreateFloating(date);

            Assert.Equal("2018-02-05T00:00:00", dueDate.StringDate);
            Assert.False(dueDate.IsFullDay);
        }

        [Fact]
        public void DateTimeAssignment_FixedTimeZoneDueDateEvent_Success()
        {
            var date = new DateTime(2018, 2, 5);

            var dueDate = DueDate.CreateFixedTimeZone(date, "Asia/Jakarta");

            Assert.Equal("2018-02-05T00:00:00Z", dueDate.StringDate);
            Assert.False(dueDate.IsFullDay);
        }


        [Fact]
        public void StringDateProperty_ShouldReturnExactAssignedValue_WhenValueIsFullDayDate()
        {
            var dueDate = new DueDate();
            string initialValue = "2016-12-01";

            dueDate.StringDate = initialValue;
            string returnedValue = dueDate.StringDate;
            dueDate.StringDate = returnedValue;

            Assert.Equal(returnedValue, dueDate.StringDate);
        }

        [Fact]
        public void StringDateProperty_ShouldReturnExactAssignedValue_WhenValueIsFloatingDate()
        {
            var dueDate = new DueDate();
            string initialValue = "2016-12-03T12:00:00";

            dueDate.StringDate = initialValue;
            string returnedValue = dueDate.StringDate;
            dueDate.StringDate = returnedValue;

            Assert.Equal(returnedValue, dueDate.StringDate);
        }

        [Fact]
        public void StringDateProperty_ShouldReturnExactAssignedValue_WhenValueIsFixedDate()
        {
            var dueDate = new DueDate();
            string initialValue = "2016-12-06T13:00:00Z";

            dueDate.StringDate = initialValue;
            dueDate.Timezone = "Asia/Jakarta";

            string returnedValue = dueDate.StringDate;
            dueDate.StringDate = returnedValue;

            Assert.Equal(returnedValue, dueDate.StringDate);
        }
    }
}
