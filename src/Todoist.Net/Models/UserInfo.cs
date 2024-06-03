using System;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Class UserInfo.
    /// </summary>
    public class UserInfo : User
    {
        [JsonConstructor]
        internal UserInfo()
        {
        }

        /// <summary>
        /// Gets the avatar big.
        /// </summary>
        /// <value>The avatar big.</value>
        [JsonPropertyName("avatar_big")]
        public string AvatarBig { get; internal set; }

        /// <summary>
        /// Gets the avatar medium.
        /// </summary>
        /// <value>The avatar medium.</value>
        [JsonPropertyName("avatar_medium")]
        public string AvatarMedium { get; internal set; }

        /// <summary>
        /// Gets the avatar S640.
        /// </summary>
        /// <value>The avatar S640.</value>
        [JsonPropertyName("avatar_s640")]
        public string AvatarS640 { get; internal set; }

        /// <summary>
        /// Gets the avatar small.
        /// </summary>
        /// <value>The avatar small.</value>
        [JsonPropertyName("avatar_small")]
        public string AvatarSmall { get; internal set; }

        /// <summary>
        /// Gets the business account identifier.
        /// </summary>
        /// <value>The business account identifier.</value>
        [JsonPropertyName("business_account_id")]
        public string BusinessAccountId { get; internal set; }

        /// <summary>
        /// Gets the completed count.
        /// </summary>
        /// <value>The completed count.</value>
        [JsonPropertyName("completed_count")]
        public int CompletedCount { get; internal set; }

        /// <summary>
        /// Gets the completed today.
        /// </summary>
        /// <value>The completed today.</value>
        [JsonPropertyName("completed_today")]
        public int CompletedToday { get; internal set; }

        /// <summary>
        /// Whether the user has a password set on the account.
        /// It will be false if they have only authenticated without a password (e.g. using Google, Facebook, etc.)
        /// </summary>
        /// <value>
        ///   <c>true</c> if the user has password; otherwise, <c>false</c>.
        /// </value>
        [JsonPropertyName("has_password")]
        public bool HasPassword { get; set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the image identifier.
        /// </summary>
        /// <value>The image identifier.</value>
        [JsonPropertyName("image_id")]
        public string ImageId { get; internal set; }

        /// <summary>
        /// Gets the inbox project.
        /// </summary>
        /// <value>The inbox project.</value>
        [JsonPropertyName("inbox_project_id")]
        public string InboxProjectId { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is biz admin.
        /// </summary>
        /// <value><c>true</c> if this instance is biz admin; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_biz_admin")]
        public bool IsBizAdmin { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is premium.
        /// </summary>
        /// <value><c>true</c> if this instance is premium; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_premium")]
        public bool IsPremium { get; internal set; }

        /// <summary>
        /// Gets the join date.
        /// </summary>
        /// <value>The join date.</value>
        [JsonPropertyName("joined_at")]
        public DateTime? JoinedAt { get; internal set; }

        /// <summary>
        /// Gets the karma.
        /// </summary>
        /// <value>The karma.</value>
        [JsonPropertyName("karma")]
        public double Karma { get; internal set; }

        /// <summary>
        /// Gets the karma trend.
        /// </summary>
        /// <value>The karma trend.</value>
        [JsonPropertyName("karma_trend")]
        public string KarmaTrend { get; internal set; }

        /// <summary>
        /// Gets the premium until.
        /// </summary>
        /// <value>The premium until.</value>
        [JsonPropertyName("premium_until")]
        public DateTime? PremiumUntil { get; internal set; }

        /// <summary>
        /// Gets the team inbox identifier.
        /// </summary>
        /// <value>
        /// The team inbox identifier.
        /// </value>
        [JsonPropertyName("team_inbox_id")]
        public string TeamInboxId { get; internal set; }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>The token.</value>
        [JsonPropertyName("token")]
        public string Token { get; internal set; }

        /// <summary>
        /// Gets the tz information.
        /// </summary>
        /// <value>The tz information.</value>
        [JsonPropertyName("tz_info")]
        public TimeZoneInfo TzInfo { get; internal set; }
    }
}
