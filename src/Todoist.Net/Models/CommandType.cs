namespace Todoist.Net.Models
{
    internal class CommandType : StringEnum
    {
        private CommandType(string command)
            : base(command)
        {
        }

        public static CommandType AcceptInvitation { get; } = new CommandType("accept_invitation");

        public static CommandType AddFilter { get; } = new CommandType("filter_add");

        public static CommandType AddTask { get; } = new CommandType("item_add");

        public static CommandType AddLabel { get; } = new CommandType("label_add");

        public static CommandType AddComment { get; } = new CommandType("note_add");

        public static CommandType AddProject { get; } = new CommandType("project_add");

        public static CommandType AddReminder { get; } = new CommandType("reminder_add");

        public static CommandType AddSection { get; } = new CommandType("section_add");

        public static CommandType ArchiveProject { get; } = new CommandType("project_archive");

        public static CommandType ArchiveSection { get; } = new CommandType("section_archive");

        public static CommandType ClearLocations { get; } = new CommandType("clear_locations");

        public static CommandType CloseTask { get; } = new CommandType("item_close");

        public static CommandType CompleteTask { get; } = new CommandType("item_complete");

        public static CommandType CompleteRecurringTask { get; } = new CommandType("item_update_date_complete");

        public static CommandType DeleteCollaborator { get; } = new CommandType("delete_collaborator");

        public static CommandType DeleteFilter { get; } = new CommandType("filter_delete");

        public static CommandType DeleteInvitation { get; } = new CommandType("delete_invitation");

        public static CommandType DeleteTask { get; } = new CommandType("item_delete");

        public static CommandType DeleteLabel { get; } = new CommandType("label_delete");

        public static CommandType DeleteComment { get; } = new CommandType("note_delete");

        public static CommandType DeleteProject { get; } = new CommandType("project_delete");

        public static CommandType DeleteReminder { get; } = new CommandType("reminder_delete");

        public static CommandType DeleteSection { get; } = new CommandType("section_delete");

        public static CommandType MoveTask { get; } = new CommandType("item_move");

        public static CommandType MoveProject { get; } = new CommandType("project_move");

        public static CommandType MoveSection { get; } = new CommandType("section_move");

        public static CommandType RejectInvitation { get; } = new CommandType("reject_invitation");

        public static CommandType ReorderTasks { get; } = new CommandType("item_reorder");

        public static CommandType ReorderProjects { get; } = new CommandType("project_reorder");

        public static CommandType ReorderSection { get; } = new CommandType("section_reorder");

        public static CommandType SetLastReadNotification { get; } =
            new CommandType("live_notifications_set_last_read");

        public static CommandType ShareProject { get; } = new CommandType("share_project");

        public static CommandType UnarchiveProject { get; } = new CommandType("project_unarchive");

        public static CommandType UnarchiveSection { get; } = new CommandType("section_unarchive");

        public static CommandType UncompleteTask { get; } = new CommandType("item_uncomplete");

        public static CommandType UpdateDayOrderTask { get; } = new CommandType("item_update_day_orders");

        public static CommandType UpdateFilter { get; } = new CommandType("filter_update");

        public static CommandType UpdateTask { get; } = new CommandType("item_update");

        public static CommandType UpdateKarmaGoals { get; } = new CommandType("update_goals");

        public static CommandType UpdateLabel { get; } = new CommandType("label_update");

        public static CommandType UpdateComment { get; } = new CommandType("note_update");

        public static CommandType UpdateOrderFilter { get; } = new CommandType("filter_update_orders");

        public static CommandType UpdateOrderLabel { get; } = new CommandType("label_update_orders");

        public static CommandType UpdateProject { get; } = new CommandType("project_update");

        public static CommandType UpdateReminder { get; } = new CommandType("reminder_update");

        public static CommandType UpdateSection { get; } = new CommandType("section_update");

        public static CommandType UpdateUser { get; } = new CommandType("user_update");
    }
}
