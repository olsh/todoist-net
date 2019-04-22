using System;

using Todoist.Net.Models;

using Xunit;

namespace Todoist.Net.Tests.Models
{
    public class DueDateTests
    {
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
            var date = new DateTime(2018, 2, 5, 0, 0, 0, DateTimeKind.Utc);

            var dueDate = new DueDate(date, false);

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
    }
}
