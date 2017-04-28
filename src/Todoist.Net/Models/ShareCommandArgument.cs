using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    internal class ShareProjectArgument : ICommandArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShareProjectArgument"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="email">The email.</param>
        internal ShareProjectArgument(ComplexId id, string email)
        {
            Id = id;
            Email = email;
        }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("project_id")]
        public ComplexId Id { get; }
    }
}
