using System;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents durations for tasks.
    /// </summary>
    public class Duration
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Duration" /> class.
        /// </summary>
        /// <param name="amount">The time amount.</param>
        /// <param name="unit">The time unit.</param>
        public Duration(int amount, DurationUnit unit)
        {
            Amount = amount > 0
                ? amount
                : throw new ArgumentOutOfRangeException(nameof(amount), "Parameter must be greater than zero.");
            Unit = unit
                ?? throw new ArgumentNullException(nameof(unit));
        }

        internal Duration()
        {
        }

        /// <summary>
        /// Gets or sets the duration time amount.
        /// </summary>
        /// <remarks>
        /// Must be a positive integer (greater than zero).
        /// </remarks>
        /// <value>
        /// The time amount.
        /// </value>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the duration time unit.
        /// </summary>
        /// <remarks>
        /// Either <c>minute</c> or <c>day</c>.
        /// </remarks>
        /// <value>
        /// The duration unit.
        /// </value>
        [JsonProperty("unit")]
        public DurationUnit Unit { get; set; }


        /// <summary>
        /// Gets the value of the duration as a <see cref="TimeSpan"/> object.
        /// </summary>
        /// <value>
        /// The <see cref="TimeSpan"/> value of the duration.
        /// </value>
        [JsonIgnore]
        public TimeSpan TimeValue => Unit == DurationUnit.Minute
            ? TimeSpan.FromMinutes(Amount)
            : Unit == DurationUnit.Day
            ? TimeSpan.FromDays(Amount)
            : throw new NotImplementedException();

    }
}
