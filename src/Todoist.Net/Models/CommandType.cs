namespace Todoist.Net.Models
{
    internal class CommandType
    {
        private CommandType(string command)
        {
            Command = command;
        }

        public static CommandType AddItem { get; } = new CommandType("item_add");

        public static CommandType AddLabel { get; } = new CommandType("label_add");

        public static CommandType AddNote { get; } = new CommandType("note_add");

        public static CommandType AddProject { get; } = new CommandType("project_add");

        public static CommandType ArchiveProject { get; } = new CommandType("project_archive");

        public static CommandType CloseItem { get; } = new CommandType("item_close");

        public static CommandType DeleteItem { get; } = new CommandType("item_delete");

        public static CommandType DeleteLabel { get; } = new CommandType("label_delete");

        public static CommandType DeleteNote { get; } = new CommandType("note_delete");

        public static CommandType DeleteProject { get; } = new CommandType("project_delete");

        public static CommandType MoveItem { get; } = new CommandType("item_move");

        public static CommandType CompleteItem { get; } = new CommandType("item_complete");

        public static CommandType SetLastReadNotification { get; } = new CommandType("live_notifications_set_last_read");

        public static CommandType UnarchiveProject { get; } = new CommandType("project_unarchive");

        public static CommandType UpdateItem { get; } = new CommandType("item_update");

        public static CommandType UpdateLabel { get; } = new CommandType("label_update");

        public static CommandType UpdateNote { get; } = new CommandType("note_update");

        public static CommandType UpdateOrderIndentsProject { get; } = new CommandType("project_update_orders_indents");

        public static CommandType UpdateOrderLabel { get; } = new CommandType("label_update_orders");

        public static CommandType UpdateProject { get; } = new CommandType("project_update");

        public string Command { get; }
    }
}
