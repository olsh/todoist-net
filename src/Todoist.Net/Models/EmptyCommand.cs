namespace Todoist.Net.Models
{
    internal class EmptyCommand : ICommandArgument
    {
        private EmptyCommand()
        {
        }

        public static EmptyCommand Instance { get; } = new EmptyCommand();
    }
}
