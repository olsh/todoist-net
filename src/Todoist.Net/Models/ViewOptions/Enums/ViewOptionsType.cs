namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents view options type values.
    /// </summary>
    public class ViewOptionsType : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewOptionsType"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private ViewOptionsType(string value) : base(value) { }

        /// <summary>Gets today view type.</summary>
        public static ViewOptionsType Today { get; } = new ViewOptionsType("TODAY");

        /// <summary>Gets upcoming view type.</summary>
        public static ViewOptionsType Upcoming { get; } = new ViewOptionsType("UPCOMING");

        /// <summary>Gets project view type.</summary>
        public static ViewOptionsType Project { get; } = new ViewOptionsType("PROJECT");

        /// <summary>Gets label view type.</summary>
        public static ViewOptionsType Label { get; } = new ViewOptionsType("LABEL");

        /// <summary>Gets filter view type.</summary>
        public static ViewOptionsType Filter { get; } = new ViewOptionsType("FILTER");

        /// <summary>Gets workspace filter view type.</summary>
        public static ViewOptionsType WorkspaceFilter { get; } = new ViewOptionsType("WORKSPACE_FILTER");
    }
}