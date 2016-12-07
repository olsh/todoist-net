using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a base Todoist user.
    /// </summary>
    public class UserBase : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserBase"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="password">The password.</param>
        public UserBase(string email, string fullName, string password)
        {
            Email = email;
            FullName = fullName;
            Password = password;
        }

        internal UserBase()
        {
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        [JsonProperty("lang")]
        public Language Language { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        internal ICollection<KeyValuePair<string, string>> ToParameters()
        {
            LinkedList<KeyValuePair<string, string>> parameters = new LinkedList<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(Email))
            {
                parameters.AddLast(new KeyValuePair<string, string>("email", Email));
            }

            if (!string.IsNullOrEmpty(Password))
            {
                parameters.AddLast(new KeyValuePair<string, string>("password", Password));
            }

            if (!string.IsNullOrEmpty(FullName))
            {
                parameters.AddLast(new KeyValuePair<string, string>("full_name", FullName));
            }

            if (Language != null)
            {
                parameters.AddLast(new KeyValuePair<string, string>("lang", Language.Value));
            }

            if (TimeZone != null)
            {
                parameters.AddLast(new KeyValuePair<string, string>("timezone", TimeZone));
            }

            return parameters;
        }
    }
}
