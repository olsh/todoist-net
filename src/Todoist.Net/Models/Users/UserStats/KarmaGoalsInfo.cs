using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents goal settings and streak information.
    /// </summary>
    public class KarmaGoalsInfo
    {
        /// <summary>
        /// Gets the user id.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; internal set; }

        /// <summary>
        /// Gets daily goal value.
        /// </summary>
        [JsonPropertyName("daily_goal")]
        public int DailyGoal { get; internal set; }

        /// <summary>
        /// Gets vacation mode flag.
        /// </summary>
        [JsonPropertyName("vacation_mode")]
        public bool VacationMode { get; internal set; }

        /// <summary>
        /// Gets karma disabled flag.
        /// </summary>
        [JsonPropertyName("karma_disabled")]
        public bool KarmaDisabled { get; internal set; }

        /// <summary>
        /// Gets weekly goal value.
        /// </summary>
        [JsonPropertyName("weekly_goal")]
        public int WeeklyGoal { get; internal set; }

        /// <summary>
        /// Gets ignored days of week.
        /// </summary>
        [JsonPropertyName("ignore_days")]
        public IReadOnlyCollection<DayOfWeek> IgnoreDays { get; internal set; }

        /// <summary>
        /// Gets current daily streak details.
        /// </summary>
        [JsonPropertyName("current_daily_streak")]
        public KarmaGoalStreakItem CurrentDailyStreak { get; internal set; }

        /// <summary>
        /// Gets current weekly streak details.
        /// </summary>
        [JsonPropertyName("current_weekly_streak")]
        public KarmaGoalStreakItem CurrentWeeklyStreak { get; internal set; }

        /// <summary>
        /// Gets last daily streak details.
        /// </summary>
        [JsonPropertyName("last_daily_streak")]
        public KarmaGoalStreakItem LastDailyStreak { get; internal set; }

        /// <summary>
        /// Gets last weekly streak details.
        /// </summary>
        [JsonPropertyName("last_weekly_streak")]
        public KarmaGoalStreakItem LastWeeklyStreak { get; internal set; }

        /// <summary>
        /// Gets max daily streak details.
        /// </summary>
        [JsonPropertyName("max_daily_streak")]
        public KarmaGoalStreakItem MaxDailyStreak { get; internal set; }

        /// <summary>
        /// Gets max weekly streak details.
        /// </summary>
        [JsonPropertyName("max_weekly_streak")]
        public KarmaGoalStreakItem MaxWeeklyStreak { get; internal set; }
    }
}
