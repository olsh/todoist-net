namespace Todoist.Net.Models
{
    internal class CommandType : StringEnum
    {
        private CommandType(string command)
            : base(command)
        {
        }

        public static CommandType AddWorkspace { get; } = new CommandType("workspace_add");
        public static CommandType UpdateWorkspace { get; } = new CommandType("workspace_update");
        public static CommandType LeaveWorkspace { get; } = new CommandType("workspace_leave");
        public static CommandType DeleteWorkspace { get; } = new CommandType("workspace_delete");

        public static CommandType UpdateWorkspaceUser { get; } = new CommandType("workspace_update_user");
        public static CommandType UpdateWorkspaceUserSidebarPreference { get; } = new CommandType("workspace_update_user_sidebar_preference");
        public static CommandType DeleteWorkspaceUser { get; } = new CommandType("workspace_delete_user");
        public static CommandType InviteWorkspaceUsers { get; } = new CommandType("workspace_invite");

        public static CommandType AddWorkspaceFilter { get; } = new CommandType("workspace_filter_add");
        public static CommandType UpdateWorkspaceFilter { get; } = new CommandType("workspace_filter_update");
        public static CommandType DeleteWorkspaceFilter { get; } = new CommandType("workspace_filter_delete");
        public static CommandType UpdateWorkspaceFilterOrders { get; } = new CommandType("workspace_filter_update_orders");

        public static CommandType SetViewOptions { get; } = new CommandType("view_options_set");
        public static CommandType DeleteViewOptions { get; } = new CommandType("view_options_delete");
        public static CommandType SetProjectViewOptionsDefaults { get; } = new CommandType("project_view_options_defaults_set");

        public static CommandType UpdateUser { get; } = new CommandType("user_update");
        public static CommandType UpdateKarmaGoals { get; } = new CommandType("update_goals");
        public static CommandType UpdateUserSettings { get; } = new CommandType("user_settings_update");

        public static CommandType ShareProject { get; } = new CommandType("share_project");
        public static CommandType DeleteCollaborator { get; } = new CommandType("delete_collaborator");
        public static CommandType AcceptInvitation { get; } = new CommandType("accept_invitation");
        public static CommandType RejectInvitation { get; } = new CommandType("reject_invitation");
        public static CommandType DeleteInvitation { get; } = new CommandType("delete_invitation");
        
        public static CommandType AddSection { get; } = new CommandType("section_add");
        public static CommandType UpdateSection { get; } = new CommandType("section_update");
        public static CommandType MoveSection { get; } = new CommandType("section_move");
        public static CommandType ReorderSections { get; } = new CommandType("section_reorder");
        public static CommandType DeleteSection { get; } = new CommandType("section_delete");
        public static CommandType ArchiveSection { get; } = new CommandType("section_archive");
        public static CommandType UnarchiveSection { get; } = new CommandType("section_unarchive");

        public static CommandType AddReminder { get; } = new CommandType("reminder_add");
        public static CommandType UpdateReminder { get; } = new CommandType("reminder_update");
        public static CommandType DeleteReminder { get; } = new CommandType("reminder_delete");
        public static CommandType ClearLocations { get; } = new CommandType("clear_locations");

        public static CommandType AddProject { get; } = new CommandType("project_add");
        public static CommandType UpdateProject { get; } = new CommandType("project_update");
        public static CommandType MoveProject { get; } = new CommandType("project_move");
        public static CommandType MoveProjectToWorkspace { get; } = new CommandType("project_move_to_workspace");
        public static CommandType MoveProjectToPersonal { get; } = new CommandType("project_move_to_personal");
        public static CommandType LeaveProject { get; } = new CommandType("project_leave");
        public static CommandType DeleteProject { get; } = new CommandType("project_delete");
        public static CommandType ArchiveProject { get; } = new CommandType("project_archive");
        public static CommandType UnarchiveProject { get; } = new CommandType("project_unarchive");
        public static CommandType ReorderProjects { get; } = new CommandType("project_reorder");
        public static CommandType ChangeProjectRole { get; } = new CommandType("project_change_role");

        public static CommandType AddComment { get; } = new CommandType("note_add");
        public static CommandType UpdateComment { get; } = new CommandType("note_update");
        public static CommandType DeleteComment { get; } = new CommandType("note_delete");

        public static CommandType SetLastReadNotification { get; } = new CommandType("live_notifications_set_last_read");
        public static CommandType MarkReadNotification { get; } = new CommandType("live_notifications_mark_read");
        public static CommandType MarkAllReadNotification { get; } = new CommandType("live_notifications_mark_read_all");
        public static CommandType MarkUnreadNotification { get; } = new CommandType("live_notifications_mark_unread");

        public static CommandType AddLabel { get; } = new CommandType("label_add");
        public static CommandType UpdateLabel { get; } = new CommandType("label_update");
        public static CommandType DeleteLabel { get; } = new CommandType("label_delete");
        public static CommandType RenameSharedLabel { get; } = new CommandType("label_rename");
        public static CommandType DeleteSharedLabel { get; } = new CommandType("label_delete_occurrences");
        public static CommandType UpdateLabelOrders { get; } = new CommandType("label_update_orders");

        public static CommandType AddTask { get; } = new CommandType("item_add");
        public static CommandType UpdateTask { get; } = new CommandType("item_update");
        public static CommandType MoveTask { get; } = new CommandType("item_move");
        public static CommandType ReorderTasks { get; } = new CommandType("item_reorder");
        public static CommandType DeleteTask { get; } = new CommandType("item_delete");
        public static CommandType CompleteTask { get; } = new CommandType("item_complete");
        public static CommandType UncompleteTask { get; } = new CommandType("item_uncomplete");
        public static CommandType CompleteRecurringTask { get; } = new CommandType("item_update_date_complete");
        public static CommandType CloseTask { get; } = new CommandType("item_close");
        public static CommandType UpdateDayOrderTask { get; } = new CommandType("item_update_day_orders");

        public static CommandType AddFilter { get; } = new CommandType("filter_add");
        public static CommandType UpdateFilter { get; } = new CommandType("filter_update");
        public static CommandType DeleteFilter { get; } = new CommandType("filter_delete");
        public static CommandType UpdateFilterOrders { get; } = new CommandType("filter_update_orders");
    }
}
