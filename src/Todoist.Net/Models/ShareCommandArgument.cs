using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    internal class ShareProjectArgument : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShareProjectArgument"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="email">The email.</param>
        internal ShareProjectArgument(ComplexId id, string email)
            : base(id)
        {
            Email = email;
        }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
