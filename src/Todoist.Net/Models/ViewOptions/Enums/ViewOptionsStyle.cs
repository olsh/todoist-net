namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents view options style values.
    /// </summary>
    public class ViewOptionsStyle : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewOptionsStyle"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private ViewOptionsStyle(string value) : base(value) { }

        /// <summary>Gets list view style.</summary>
        public static ViewOptionsStyle List { get; } = new ViewOptionsStyle("list");
        
        /// <summary>Gets board view style.</summary>
        public static ViewOptionsStyle Board { get; } = new ViewOptionsStyle("board");
        
        /// <summary>Gets calendar view style.</summary>
        public static ViewOptionsStyle Calendar { get; } = new ViewOptionsStyle("calendar");
    }
}