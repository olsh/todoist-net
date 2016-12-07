using System;
using System.Collections.Generic;

using Todoist.Net.Extensions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a completed items query filter.
    /// </summary>
    public class ItemFilter
    {
        /// <summary>
        /// Gets or sets a value indicating whether [annotate notes].
        /// </summary>
        /// <value><c>null</c> if [annotate notes] contains no value, <c>true</c> if [annotate notes]; otherwise, <c>false</c>.</value>
        public bool? AnnotateNotes { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>The limit.</value>
        /// <remarks>Default is 30, and the maximum is 50.</remarks>
        public int? Limit { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public int? Offset { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the since.
        /// </summary>
        /// <value>The since.</value>
        public DateTime? Since { get; set; }

        /// <summary>
        /// Gets or sets the until.
        /// </summary>
        /// <value>The until.</value>
        public DateTime? Until { get; set; }

        internal ICollection<KeyValuePair<string, string>> ToParameters()
        {
            var parameters = new LinkedList<KeyValuePair<string, string>>();
            if (ProjectId.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("project_id", ProjectId.ToString()));
            }

            if (Limit.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("limit", Limit.ToString()));
            }

            if (Offset.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("offset", Offset.ToString()));
            }

            if (Until.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("until", Until.Value.ToFilterParameter()));
            }

            if (Since.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("since", Since.Value.ToFilterParameter()));
            }

            if (AnnotateNotes.HasValue)
            {
                parameters.AddLast(
                    new KeyValuePair<string, string>("annotate_notes", AnnotateNotes == true ? "true" : "false"));
            }

            return parameters;
        }
    }
}
