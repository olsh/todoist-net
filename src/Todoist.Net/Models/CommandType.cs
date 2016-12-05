using System;

namespace Todoist.Net.Models
{
    public class CommandType : IEquatable<CommandType>
    {
        private CommandType(string command)
        {
            Command = command;
        }

        public static CommandType AddItem { get; } = new CommandType("item_add");

        public static CommandType AddProject { get; } = new CommandType("project_add");

        public static CommandType AddNote { get; } = new CommandType("note_add");

        public static CommandType UpdateNote { get; } = new CommandType("note_update");

        public static CommandType DeleteNote { get; } = new CommandType("note_delete");

        public static CommandType ArchiveProject { get; } = new CommandType("project_archive");

        public static CommandType DeleteProject { get; } = new CommandType("project_delete");

        public static CommandType UnarchiveProject { get; } = new CommandType("project_unarchive");

        public static CommandType UpdateItem { get; } = new CommandType("item_update");

        public static CommandType UpdateOrderIndentsProject { get; } = new CommandType("project_update_orders_indents");

        /// <summary>
        /// Gets a new project command.
        /// </summary>
        public static CommandType UpdateProject { get; } = new CommandType("project_update");

        public string Command { get; }

        public static bool operator ==(CommandType left, CommandType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CommandType left, CommandType right)
        {
            return !Equals(left, right);
        }

        public bool Equals(CommandType other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Command, other.Command);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((CommandType)obj);
        }

        public override int GetHashCode()
        {
            return Command.GetHashCode();
        }
    }
}
