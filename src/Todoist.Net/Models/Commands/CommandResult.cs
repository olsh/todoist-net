namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the result of a command execution.
    /// </summary>
    /// <remarks>
    /// This class indicates whether a command succeeded or failed, and provides error details if applicable.
    /// </remarks>
    public class CommandResult
    {
        /// <summary>
        /// The value indicating a successful command execution.
        /// </summary>
        internal const string SuccessValue = "ok";

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResult"/> class.
        /// </summary>
        private CommandResult()
        { }

        /// <summary>
        /// Gets the extra information about the command execution error or long-running operation, or <c>null</c> if the command succeeded immediately.
        /// </summary>
        public CommandResultBody CommandBody { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the command execution was successful.
        /// </summary>
        /// <value><c>true</c> if the command succeeded; otherwise, <c>false</c>.</value>
        public bool IsSuccess => CommandBody == null || !CommandBody.IsError;


        /// <summary>
        /// Gets a <see cref="CommandResult"/> instance representing a successful command execution.
        /// </summary>
        public static CommandResult Success { get; } = new CommandResult();

        /// <summary>
        /// Creates a <see cref="CommandResult"/> instance from a <see cref="CommandResultBody"/>.
        /// </summary>
        /// <param name="body">The body information describing the command result.</param>
        /// <returns>A <see cref="CommandResult"/> instance with the specified body.</returns>
        public static CommandResult FromBody(CommandResultBody body) => new CommandResult
        {
            CommandBody = body
        };
    }
}
