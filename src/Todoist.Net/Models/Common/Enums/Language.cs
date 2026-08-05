namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a language type.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class Language : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Language" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private Language(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the chinese.
        /// </summary>
        /// <value>The chinese.</value>
        public static Language Chinese { get; } = new Language("zh");

        /// <summary>
        /// Gets the danish.
        /// </summary>
        /// <value>The danish.</value>
        public static Language Danish { get; } = new Language("da");

        /// <summary>
        /// Gets the dutch.
        /// </summary>
        /// <value>The dutch.</value>
        public static Language Dutch { get; } = new Language("nl");

        /// <summary>
        /// Gets the english.
        /// </summary>
        /// <value>The english.</value>
        public static Language English { get; } = new Language("en");

        /// <summary>
        /// Gets the french.
        /// </summary>
        /// <value>The french.</value>
        public static Language French { get; } = new Language("fr");

        /// <summary>
        /// Gets the german.
        /// </summary>
        /// <value>The german.</value>
        public static Language German { get; } = new Language("de");

        /// <summary>
        /// Gets the italian.
        /// </summary>
        /// <value>The italian.</value>
        public static Language Italian { get; } = new Language("it");

        /// <summary>
        /// Gets the japanese.
        /// </summary>
        /// <value>The japanese.</value>
        public static Language Japanese { get; } = new Language("ja");

        /// <summary>
        /// Gets the korean.
        /// </summary>
        /// <value>The korean.</value>
        public static Language Korean { get; } = new Language("ko");

        /// <summary>
        /// Gets the polish.
        /// </summary>
        /// <value>The polish.</value>
        public static Language Polish { get; } = new Language("pl");

        /// <summary>
        /// Gets the portuguese.
        /// </summary>
        /// <value>The portuguese.</value>
        public static Language Portuguese { get; } = new Language("pt");

        /// <summary>
        /// Gets the russian.
        /// </summary>
        /// <value>The russian.</value>
        public static Language Russian { get; } = new Language("ru");

        /// <summary>
        /// Gets the spanish.
        /// </summary>
        /// <value>The spanish.</value>
        public static Language Spanish { get; } = new Language("es");

        /// <summary>
        /// Gets the swedish.
        /// </summary>
        /// <value>The swedish.</value>
        public static Language Swedish { get; } = new Language("sv");
    }
}
