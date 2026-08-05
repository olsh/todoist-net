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

        /// <summary>Gets due_date view sorting.</summary>
        public static ViewOptionsSorting DueDate { get; } = new ViewOptionsSorting("DUE_DATE");

        /// <summary>Gets deadline view sorting.</summary>
        public static ViewOptionsSorting Deadline { get; } = new ViewOptionsSorting("DEADLINE");

        /// <summary>Gets added_date view sorting.</summary>
        public static ViewOptionsSorting AddedDate { get; } = new ViewOptionsSorting("ADDED_DATE");

        /// <summary>Gets task_order view sorting.</summary>
        public static ViewOptionsSorting Label { get; } = new ViewOptionsSorting("LABEL");

        /// <summary>Gets manual view sorting.</summary>
        public static ViewOptionsSorting Manual { get; } = new ViewOptionsSorting("MANUAL");

        /// <summary>Gets assignee view sorting.</summary>
        public static ViewOptionsSorting Assignee { get; } = new ViewOptionsSorting("ASSIGNEE");

        /// <summary>Gets alphabetically view sorting.</summary>
        public static ViewOptionsSorting Alphabetically { get; } = new ViewOptionsSorting("ALPHABETICALLY");

        /// <summary>Gets priority view sorting.</summary>
        public static ViewOptionsSorting Priority { get; } = new ViewOptionsSorting("PRIORITY");

        /// <summary>Gets project view sorting.</summary>
        public static ViewOptionsSorting Project { get; } = new ViewOptionsSorting("PROJECT");

        /// <summary>Gets workspace view sorting.</summary>
        public static ViewOptionsSorting Workspace { get; } = new ViewOptionsSorting("WORKSPACE");
    }
}
