namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents view options grouping values.
    /// </summary>
    public class ViewOptionsGrouping : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewOptionsGrouping"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private ViewOptionsGrouping(string value) : base(value) { }

        /// <summary>Gets due_date view grouping.</summary>
        public static ViewOptionsGrouping DueDate { get; } = new ViewOptionsGrouping("DUE_DATE");

        /// <summary>Gets deadline view grouping.</summary>
        public static ViewOptionsGrouping Deadline { get; } = new ViewOptionsGrouping("DEADLINE");

        /// <summary>Gets added_date view grouping.</summary>
        public static ViewOptionsGrouping AddedDate { get; } = new ViewOptionsGrouping("ADDED_DATE");

        /// <summary>Gets label view grouping.</summary>
        public static ViewOptionsGrouping Label { get; } = new ViewOptionsGrouping("LABEL");

        /// <summary>Gets assignee view grouping.</summary>
        public static ViewOptionsGrouping Assignee { get; } = new ViewOptionsGrouping("ASSIGNEE");

        /// <summary>Gets priority view grouping.</summary>
        public static ViewOptionsGrouping Priority { get; } = new ViewOptionsGrouping("PRIORITY");

        /// <summary>Gets project view grouping.</summary>
        public static ViewOptionsGrouping Project { get; } = new ViewOptionsGrouping("PROJECT");

        /// <summary>Gets workspace view grouping.</summary>
        public static ViewOptionsGrouping Workspace { get; } = new ViewOptionsGrouping("WORKSPACE");
    }
}
