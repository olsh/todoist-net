using System.Collections.Generic;

using Newtonsoft.Json;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents karma settings.
    /// </summary>
    public class KarmaGoals : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KarmaGoals"/> class.
        /// </summary>
        public KarmaGoals()
        {
            IgnoreDays = new HashSet<DayOfWeek>();
        }

        /// <summary>
        /// Gets or sets the daily goal.
        /// </summary>
        /// <value>The daily goal.</value>
        [JsonProperty("daily_goal")]
        public int? DailyGoal { get; set; }

        /// <summary>
        /// Gets the ignore days.
        /// </summary>
        /// <value>The ignore days.</value>
        [JsonProperty("ignore_days")]
        public ICollection<DayOfWeek> IgnoreDays { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether [karma disabled].
        /// </summary>
        /// <value><c>true</c> if [karma disabled]; otherwise, <c>false</c>.</value>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("karma_disabled")]
        public bool KarmaDisabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [vacation mode].
        /// </summary>
        /// <value><c>true</c> if [vacation mode]; otherwise, <c>false</c>.</value>
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("vacation_mode")]
        public bool VacationMode { get; set; }

        /// <summary>
        /// Gets or sets the weekly goal.
        /// </summary>
        /// <value>The weekly goal.</value>
        [JsonProperty("weekly_goal")]
        public int? WeeklyGoal { get; set; }
    }
}
