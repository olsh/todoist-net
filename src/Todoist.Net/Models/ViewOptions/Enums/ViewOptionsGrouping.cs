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

        /// <summary>Gets list view grouping.</summary>
        public static ViewOptionsGrouping List { get; } = new ViewOptionsGrouping("due_date");

        /// <summary>Gets created_at view grouping.</summary>
        public static ViewOptionsGrouping CreatedAt { get; } = new ViewOptionsGrouping("created_at");

        /// <summary>Gets label view grouping.</summary>
        public static ViewOptionsGrouping Label { get; } = new ViewOptionsGrouping("label");

        /// <summary>Gets assignee view grouping.</summary>
        public static ViewOptionsGrouping Assignee { get; } = new ViewOptionsGrouping("assignee");

        /// <summary>Gets priority view grouping.</summary>
        public static ViewOptionsGrouping Priority { get; } = new ViewOptionsGrouping("priority");

        /// <summary>Gets project view grouping.</summary>
        public static ViewOptionsGrouping Project { get; } = new ViewOptionsGrouping("project");
    }
}