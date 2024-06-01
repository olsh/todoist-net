using System;

using Todoist.Net.Models;
using Todoist.Net.Tests.Extensions;
using Xunit;

namespace Todoist.Net.Tests.Models
{
    [Trait(Constants.TraitName, Constants.UnitTraitValue)]
    public class DurationTests
    {
        [Fact]
        public void AmountAssignment_InvalidValue_ThrowsException()
        {
            Duration duration;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                duration = new Duration(0, DurationUnit.Minute));

            duration = new Duration(15, DurationUnit.Minute);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                duration.Amount = -5);
        }

        [Fact]
        public void UnitAssignment_InvalidValue_ThrowsException()
        {
            Duration duration;

            Assert.Throws<ArgumentNullException>(() =>
                duration = new Duration(15, null));

            duration = new Duration(15, DurationUnit.Minute);

            Assert.Throws<ArgumentNullException>(() =>
                duration.Unit = null);
        }

        [Fact]
        public void UnitInstantiation_UnsetValue_HasNoTime()
        {
            var duration = new Duration();

            Assert.Equal(duration.TimeValue, TimeSpan.Zero);

            duration.Amount = 15;

            Assert.Equal(duration.TimeValue, TimeSpan.Zero);
        }

        [Fact]
        public void TimeValueEvaluation_Success()
        {
            var duration = new Duration(15, DurationUnit.Minute);

            Assert.Equal(TimeSpan.FromMinutes(15), duration.TimeValue);

            duration.Amount = 3;
            duration.Unit = DurationUnit.Day;

            Assert.Equal(TimeSpan.FromDays(3), duration.TimeValue);
        }

    }
}
