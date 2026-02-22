using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Class UserInfo.
    /// </summary>
    public class UserInfo : BaseUser
    {
        [JsonConstructor]
        internal UserInfo()
        {
        }

        /// <summary>
        /// Gets a value indicating whether the user is activated.
        /// </summary>
        [JsonPropertyName("activated_user")]
        public bool ActivatedUser { get; internal set; }

        /// <summary>
        /// Gets the avatar big.
        /// </summary>
        [JsonPropertyName("avatar_big")]
        public string AvatarBig { get; internal set; }

        /// <summary>
        /// Gets the avatar medium.
        /// </summary>
        [JsonPropertyName("avatar_medium")]
        public string AvatarMedium { get; internal set; }

        /// <summary>
        /// Gets the avatar S640.
        /// </summary>
        [JsonPropertyName("avatar_s640")]
        public string AvatarS640 { get; internal set; }

        /// <summary>
        /// Gets the avatar small.
        /// </summary>
        [JsonPropertyName("avatar_small")]
        public string AvatarSmall { get; internal set; }

        /// <summary>
        /// Gets the business account identifier.
        /// </summary>
        [JsonPropertyName("business_account_id")]
        public string BusinessAccountId { get; internal set; }

        /// <summary>
        /// Gets the daily goal.
        /// </summary>
        [JsonPropertyName("daily_goal")]
        public int DailyGoal { get; internal set; }

        /// <summary>
        /// Gets the days off array.
        /// </summary>
        [JsonPropertyName("days_off")]
        public DayOfWeek[] DaysOff { get; internal set; }

        /// <summary>
        /// Gets the deleted at timestamp.
        /// </summary>
        [JsonPropertyName("deleted_at")]
        public DateTime? DeletedAt { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the user has a password set on the account.
        /// </summary>
        /// <remarks>
        /// It will be <c>false</c> if they have only authenticated without a password (e.g. using Google, Facebook, etc.)
        /// </remarks>
        [JsonPropertyName("has_password")]
        public bool HasPassword { get; set; }

        /// <summary>
        /// Gets a value indicating whether the user has started a trial.
        /// </summary>
        [JsonPropertyName("has_started_a_trial")]
        public bool HasStartedATrial { get; set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the image identifier.
        /// </summary>
        [JsonPropertyName("image_id")]
        public string ImageId { get; internal set; }

        /// <summary>
        /// Gets the inbox project.
        /// </summary>
        [JsonPropertyName("inbox_project_id")]
        public string InboxProjectId { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this user is premium.
        /// </summary>
        [JsonPropertyName("is_premium")]
        public bool IsPremium { get; internal set; }

        /// <summary>
        /// Gets the join date.
        /// </summary>
        [JsonPropertyName("joined_at")]
        public DateTime? JoinedAt { get; internal set; }

        /// <summary>
        /// Gets the karma.
        /// </summary>
        [JsonPropertyName("karma")]
        public double Karma { get; internal set; }

        /// <summary>
        /// Gets the karma trend.
        /// </summary>
        [JsonPropertyName("karma_trend")]
        public string KarmaTrend { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether multi-factor authentication is enabled.
        /// </summary>
        [JsonPropertyName("mfa_enabled")]
        public bool MfaEnabled { get; internal set; }

        /// <summary>
        /// Gets the premium status.
        /// </summary>
        [JsonPropertyName("premium_status")]
        public string PremiumStatus { get; internal set; }

        /// <summary>
        /// Gets the premium until.
        /// </summary>
        [JsonPropertyName("premium_until")]
        public DateTime? PremiumUntil { get; internal set; }

        /// <summary>
        /// Gets the share limit.
        /// </summary>
        [JsonPropertyName("share_limit")]
        public int? ShareLimit { get; internal set; }

        /// <summary>
        /// Gets the currently selected Todoist theme.
        /// </summary>
        [JsonPropertyName("theme_id")]
        public string ThemeId { get; internal set; }

        /// <summary>
        /// Gets the token.
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; internal set; }

        /// <summary>
        /// Gets the tz information.
        /// </summary>
        [JsonPropertyName("tz_info")]
        public TimeZoneInfo TzInfo { get; internal set; }

        /// <summary>
        /// Gets the weekly goal.
        /// </summary>
        [JsonPropertyName("weekly_goal")]
        public int WeeklyGoal { get; internal set; }

        /// <summary>
        /// Gets the verification status.
        /// </summary>
        [JsonPropertyName("verification_status")]
        public string VerificationStatus { get; internal set; }

        /// <summary>
        /// Gets the websocket URL.
        /// </summary>
        [JsonPropertyName("websocket_url")]
        public string WebsocketUrl { get; internal set; }
    }
}
