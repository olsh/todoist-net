using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents durations for tasks.
    /// </summary>
    public class Duration
    {

        private int _amount;
        private DurationUnit _unit;

        /// <summary>
        /// Initializes a new instance of the <see cref="Duration" /> class.
        /// </summary>
        /// <param name="amount">The time amount.</param>
        /// <param name="unit">The time unit.</param>
        public Duration(int amount, DurationUnit unit)
        {
            Amount = amount;
            Unit = unit;
        }

        [JsonConstructor]
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
        /// <exception cref="ArgumentOutOfRangeException">Duration amount must be greater than zero.</exception>"
        [JsonPropertyName("amount")]
        public int Amount
        {
            get => _amount;
            set => _amount = value > 0
                ? value
                : throw new ArgumentOutOfRangeException(nameof(Amount), "Duration amount must be greater than zero.");
        }

        /// <summary>
        /// Gets or sets the duration time unit.
        /// </summary>
        /// <remarks>
        /// Either <c>minute</c> or <c>day</c>.
        /// </remarks>
        /// <value>
        /// The duration unit.
        /// </value>
        /// <exception cref="ArgumentNullException">Unit</exception>
        [JsonPropertyName("unit")]
        public DurationUnit Unit
        {
            get => _unit;
            set => _unit = value ?? throw new ArgumentNullException(nameof(Unit));
        }


        /// <summary>
        /// Gets the value of the duration as a <see cref="TimeSpan"/> object.
        /// </summary>
        /// <value>
        /// The <see cref="TimeSpan"/> value of the duration.
        /// </value>
        [JsonIgnore]
        public TimeSpan TimeValue
        {
            get
            {
                switch (Unit)
                {
                    case var _ when Unit == DurationUnit.Minute:
                        return TimeSpan.FromMinutes(Amount);
                    case var _ when Unit == DurationUnit.Day:
                        return TimeSpan.FromDays(Amount);
                    default:
                        return TimeSpan.Zero; // In case 'Unit' is unset, there's no duration.
                }
            }
        }

    }
}
