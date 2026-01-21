using System;
using System.Collections.Generic;

using Todoist.Net.Extensions;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a completed tasks query filter.
    /// </summary>
    public class TaskFilter
    {
        /// <summary>
        /// Gets or sets a value indicating whether to [annotate tasks].
        /// </summary>
        /// <remarks>
        /// When this property is set to <c>true</c>, the <see cref="CompletedTask"/> returned will contain the
        /// full task object contained in the <see cref="CompletedTask.TaskObject"/> property.
        /// </remarks>
        /// <value><c>null</c> if [annotate tasks] contains no value, <c>true</c> if [annotate tasks]; otherwise, <c>false</c>.</value>
        public bool? AnnotateTasks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [annotate comments].
        /// </summary>
        /// <value><c>null</c> if [annotate comments] contains no value, <c>true</c> if [annotate comments]; otherwise, <c>false</c>.</value>
        public bool? AnnotateComments { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>The limit.</value>
        /// <remarks>Default is 30, and the maximum is 200.</remarks>
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

            if (AnnotateComments.HasValue)
            {
                parameters.AddLast(
                    new KeyValuePair<string, string>("annotate_notes", AnnotateComments == true ? "true" : "false"));
            }

            if (AnnotateTasks.HasValue)
            {
                parameters.AddLast(
                    new KeyValuePair<string, string>("annotate_items", AnnotateTasks == true ? "true" : "false"));
            }

            return parameters;
        }
    }
}
