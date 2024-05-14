namespace Todoist.Net.Models
{
    internal class CommandResult
    {
        private CommandResult()
        { }

        public CommandError CommandError { get; set; }


        public bool IsSuccess => CommandError == null;

        public static CommandResult Success { get; } = new CommandResult();
        public static CommandResult Fail(CommandError error) => new CommandResult
        {
            CommandError = error
        };
    }
}
