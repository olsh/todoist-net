namespace Todoist.Net.Models
{
    internal class CommandResult
    {
        internal const string SuccessValue = "ok";


        private CommandResult()
        { }

        public CommandError CommandError { get; private set; }


        public bool IsSuccess => CommandError == null;

        public static CommandResult Success { get; } = new CommandResult();
        public static CommandResult Fail(CommandError error) => new CommandResult
        {
            CommandError = error
        };
    }
}
