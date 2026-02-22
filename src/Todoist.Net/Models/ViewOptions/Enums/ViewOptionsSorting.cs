namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents view options sorting values.
    /// </summary>
    public class ViewOptionsSorting : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewOptionsSorting"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private ViewOptionsSorting(string value) : base(value) { }

        /// <summary>Gets list view sorting.</summary>
        public static ViewOptionsSorting List { get; } = new ViewOptionsSorting("due_date");

        /// <summary>Gets created_at view sorting.</summary>
        public static ViewOptionsSorting CreatedAt { get; } = new ViewOptionsSorting("created_at");

        /// <summary>Gets task_order view sorting.</summary>
        public static ViewOptionsSorting TaskOrder { get; } = new ViewOptionsSorting("task_order");

        /// <summary>Gets assignee view sorting.</summary>
        public static ViewOptionsSorting Assignee { get; } = new ViewOptionsSorting("assignee");

        /// <summary>Gets alphabetically view sorting.</summary>
        public static ViewOptionsSorting Alphabetically { get; } = new ViewOptionsSorting("alphabetically");

        /// <summary>Gets priority view sorting.</summary>
        public static ViewOptionsSorting Priority { get; } = new ViewOptionsSorting("priority");
    }
}