namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents color values.
    /// </summary>
    public class Color : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private Color(string value) : base(value) { }

        /// <summary>Gets berry red color (<c>#B8255F</c>).</summary>
        public static Color BerryRed { get; } = new Color("berry_red");

        /// <summary>Gets red color (<c>#DC4C3E</c>).</summary>
        public static Color Red { get; } = new Color("red");

        /// <summary>Gets orange color (<c>#C77100</c>).</summary>
        public static Color Orange { get; } = new Color("orange");

        /// <summary>Gets yellow color (<c>#B29104</c>).</summary>
        public static Color Yellow { get; } = new Color("yellow");

        /// <summary>Gets olive green color (<c>#949C31</c>).</summary>
        public static Color OliveGreen { get; } = new Color("olive_green");

        /// <summary>Gets lime green color (<c>#65A33A</c>).</summary>
        public static Color LimeGreen { get; } = new Color("lime_green");

        /// <summary>Gets green color (<c>#369307</c>).</summary>
        public static Color Green { get; } = new Color("green");

        /// <summary>Gets mint green color (<c>#42A393</c>).</summary>
        public static Color MintGreen { get; } = new Color("mint_green");

        /// <summary>Gets teal color (<c>#148FAD</c>).</summary>
        public static Color Teal { get; } = new Color("teal");

        /// <summary>Gets sky blue color (<c>#319DC0</c>).</summary>
        public static Color SkyBlue { get; } = new Color("sky_blue");

        /// <summary>Gets light blue color (<c>#6988A4</c>).</summary>
        public static Color LightBlue { get; } = new Color("light_blue");

        /// <summary>Gets blue color (<c>#4180FF</c>).</summary>
        public static Color Blue { get; } = new Color("blue");

        /// <summary>Gets grape color (<c>#692EC2</c>).</summary>
        public static Color Grape { get; } = new Color("grape");

        /// <summary>Gets violet color (<c>#CA3FEE</c>).</summary>
        public static Color Violet { get; } = new Color("violet");

        /// <summary>Gets lavender color (<c>#A4698C</c>).</summary>
        public static Color Lavender { get; } = new Color("lavender");

        /// <summary>Gets magenta color (<c>#E05095</c>).</summary>
        public static Color Magenta { get; } = new Color("magenta");

        /// <summary>Gets salmon color (<c>#C9766F</c>).</summary>
        public static Color Salmon { get; } = new Color("salmon");

        /// <summary>Gets charcoal color (<c>#808080</c>).</summary>
        public static Color Charcoal { get; } = new Color("charcoal");

        /// <summary>Gets grey color (<c>#999999</c>).</summary>
        public static Color Grey { get; } = new Color("grey");

        /// <summary>Gets taupe color (<c>#8F7A69</c>).</summary>
        public static Color Taupe { get; } = new Color("taupe");
    }
}
