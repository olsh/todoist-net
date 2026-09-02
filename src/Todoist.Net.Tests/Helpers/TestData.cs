namespace Todoist.Net.Tests.Helpers;

public static class TestData
{
    public static class Files
    {
        public static byte[] GreenPng10x10 =>
            Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAFUlEQVR42mNk+M9Qz0AEYBxVSF+FAAhKDveksOjmAAAAAElFTkSuQmCC");
    }

    public static class Projects
    {
        public static AddProject AddProject(string name = "Test Project", ComplexId? parentId = null) => new()
        {
            Name = name,
            Description = "This is a test project.",
            Color = Color.BerryRed,
            IsFavorite = false,
            ViewStyle = ViewOptionsStyle.Board,
            ChildOrder = 15,
            ParentId = parentId
        };

        public static object ExpectedAddProject(string name = "Test Project", ComplexId? parentId = null)
        {
            var added = AddProject(name, parentId);
            return new
            {
                added.Name,
                added.Description,
                added.Color,
                added.IsFavorite,
                added.ViewStyle,
                added.ChildOrder,
                added.ParentId,
                IsCollapsed = false,
                IsArchived = false,
                IsDeleted = false,
                IsShared = false,
                IsFrozen = false,
                CanAssignTasks = false,
                PublicAccess = false,
                DefaultOrder = 0,
                Role = ProjectCollaboratorRole.Creator,
                Access = new
                {
                    Visibility = ProjectAccessVisibility.Restricted
                }
            };
        }

        public static UpdateProject UpdateProject(ComplexId id, string name = "Updated Project") => new(id)
        {
            Name = name,
            Description = "This is an updated project.",
            Color = Color.Grape,
            IsFavorite = true,
            IsCollapsed = true,
        };

        public static object ExpectedUpdateProject(ComplexId id, string name = "Updated Project")
        {
            var updated = UpdateProject(id, name);
            return new
            {
                updated.Id,
                updated.Name,
                updated.Description,
                updated.Color,
                updated.IsFavorite,
                updated.IsCollapsed
            };
        }

        public static ProjectViewOptionsDefaults ViewOptionsDefaults(ComplexId projectId) => new(projectId)
        {
            ViewMode = ViewOptionsStyle.Board,
            GroupedBy = ViewOptionsGrouping.Priority,
            SortedBy = ViewOptionsSorting.AddedDate,
            SortOrder = SortingOrder.Desc,
            ShowCompletedTasks = true
        };

        public static object ExpectedViewOptionsDefaults(ComplexId projectId)
        {
            var defined = ViewOptionsDefaults(projectId);
            return new
            {
                defined.ProjectId,
                defined.ViewMode,
                defined.GroupedBy,
                defined.SortedBy,
                defined.SortOrder,
                defined.ShowCompletedTasks
            };
        }
    }

    public static class ViewOptions
    {
        public static Todoist.Net.Models.ViewOptions ProjectViewOptions(ComplexId projectId) => new(projectId, ViewOptionsType.Project)
        {
            FilteredBy = "!assigned",
            GroupedBy = ViewOptionsGrouping.Priority,
            SortedBy = ViewOptionsSorting.AddedDate,
            SortOrder = SortingOrder.Desc,
            ShowCompletedTasks = true
        };

        public static object ExpectedProjectViewOptions(ComplexId projectId)
        {
            var defined = ProjectViewOptions(projectId);
            return new
            {
                defined.ObjectId,
                defined.ViewType,
                defined.FilteredBy,
                defined.GroupedBy,
                defined.SortedBy,
                defined.SortOrder,
                defined.ShowCompletedTasks,
                IsDeleted = false
            };
        }
    }

    public static class Tasks
    {
        public static AddTask AddTask(ComplexId projectId, string content = "Test Task") => new(content, projectId)
        {
            Description = "This is a test task.",
            Labels = ["sdk-tests"],
            Priority = Priority.Priority3,
            IsCollapsed = false
        };

        public static object ExpectedAddTask(ComplexId projectId, string content = "Test Task")
        {
            var added = AddTask(projectId, content);
            return new
            {
                ProjectId = projectId.PersistentId,
                added.Content,
                added.Description,
                added.Labels,
                added.Priority,
                IsCollapsed = added.IsCollapsed ?? false,
                IsDeleted = false,
                IsChecked = false
            };
        }

        public static UpdateTask UpdateTask(ComplexId id, string content = "Updated Task") => new(id)
        {
            Content = content,
            Description = "This is an updated task.",
            Labels = ["sdk-tests", "updated"],
            Priority = Priority.Priority2
        };

        public static object ExpectedUpdateTask(ComplexId id, string content = "Updated Task")
        {
            var updated = UpdateTask(id, content);
            return new
            {
                updated.Id,
                updated.Content,
                updated.Description,
                updated.Labels,
                updated.Priority
            };
        }
    }

    public static class Reminders
    {
        public static AddReminder AddAbsoluteReminder(ComplexId taskId, DateTime dateTime) => new(taskId, ReminderType.Absolute)
        {
            DueDate = DueDate.CreateFloating(dateTime)
        };

        public static AddReminder AddLocationReminder(
            ComplexId taskId,
            string name,
            string latitude,
            string longitude) => new(taskId, ReminderType.Location)
        {
            Name = name,
            LocationLatitude = latitude,
            LocationLongitude = longitude,
            LocationTrigger = LocationTrigger.OnEnter,
            Radius = 100
        };
    }

    public static class Labels
    {
        public static Label AddLabel(string name = "Test Label") => new(name)
        {
            Color = Color.BerryRed,
            ItemOrder = 15,
            IsFavorite = false
        };

        public static object ExpectedAddLabel(string name = "Test Label")
        {
            var added = AddLabel(name);
            return new
            {
                added.Name,
                added.Color,
                added.ItemOrder,
                added.IsFavorite,
                IsDeleted = false
            };
        }

        public static Label UpdateLabel(ComplexId id, string name = "Updated Label") => new(name)
        {
            Id = id,
            Color = Color.Grape,
            ItemOrder = 5,
            IsFavorite = true
        };

        public static object ExpectedUpdatedLabel(ComplexId id, string name = "Updated Label")
        {
            var updated = UpdateLabel(id, name);
            return new
            {
                updated.Id,
                updated.Name,
                updated.Color,
                updated.ItemOrder,
                updated.IsFavorite
            };
        }
    }

    public static class Filters
    {
        public static Filter AddFilter(string name = "Test Filter", string query = "today", int? itemOrder = 15) => new(name, query)
        {
            Color = Color.BerryRed,
            ItemOrder = itemOrder,
            IsFavorite = true
        };

        public static object ExpectedAddFilter(string name = "Test Filter", string query = "today", int? itemOrder = 15)
        {
            var added = AddFilter(name, query, itemOrder);
            return new
            {
                added.Name,
                added.Query,
                added.Color,
                added.ItemOrder,
                added.IsFavorite,
                IsDeleted = false
            };
        }

        public static Filter UpdateFilter(ComplexId id, string name = "Updated Filter", string query = "overdue", int? itemOrder = 5) => new(name, query)
        {
            Id = id,
            Color = Color.Grape,
            ItemOrder = itemOrder,
            IsFavorite = true
        };

        public static object ExpectedUpdateFilter(ComplexId id, string name = "Updated Filter", string query = "overdue", int? itemOrder = 5)
        {
            var updated = UpdateFilter(id, name, query, itemOrder);
            return new
            {
                updated.Id,
                updated.Name,
                updated.Query,
                updated.Color,
                updated.ItemOrder,
                updated.IsFavorite
            };
        }
    }

    public static class WorkspaceFilters
    {
        public static AddWorkspaceFilter AddWorkspaceFilter(
            ComplexId workspaceId,
            string name = "Test Workspace Filter",
            string query = "today",
            int? itemOrder = 15) => new(workspaceId, name, query)
        {
            Color = Color.BerryRed,
            ItemOrder = itemOrder
        };

        public static object ExpectedAddWorkspaceFilter(
            ComplexId workspaceId,
            string name = "Test Workspace Filter",
            string query = "today",
            int? itemOrder = 15)
        {
            var added = AddWorkspaceFilter(workspaceId, name, query, itemOrder);
            return new
            {
                added.WorkspaceId,
                added.Name,
                added.Query,
                added.Color,
                added.ItemOrder,
                IsDeleted = false
            };
        }

        public static UpdateWorkspaceFilter UpdateWorkspaceFilter(
            ComplexId id,
            string name = "Updated Workspace Filter",
            string query = "overdue",
            int? itemOrder = 5) => new(id)
        {
            Name = name,
            Query = query,
            Color = Color.Grape,
            ItemOrder = itemOrder,
            IsFavorite = true
        };

        public static object ExpectedUpdateWorkspaceFilter(
            ComplexId id,
            string name = "Updated Workspace Filter",
            string query = "overdue",
            int? itemOrder = 5)
        {
            var updated = UpdateWorkspaceFilter(id, name, query, itemOrder);
            return new
            {
                updated.Id,
                updated.Name,
                updated.Query,
                updated.Color,
                updated.ItemOrder,
                updated.IsFavorite
            };
        }
    }

    public static class Sections
    {
        public static AddSection AddSection(ComplexId projectId, string name = "Test Section", int? sectionOrder = null) =>
            new(name, projectId, sectionOrder);

        public static object ExpectedAddSection(ComplexId projectId, string name = "Test Section", int? sectionOrder = null)
        {
            var added = AddSection(projectId, name, sectionOrder);
            return new
            {
                added.Name,
                added.ProjectId,
                added.SectionOrder,
                IsArchived = false,
                IsDeleted = false,
                IsCollapsed = false
            };
        }

        public static UpdateSection UpdateSection(ComplexId id, string name = "Updated Section", bool? isCollapsed = true) =>
            new(id, name, isCollapsed);

        public static object ExpectedUpdateSection(ComplexId id, string name = "Updated Section", bool? isCollapsed = true)
        {
            var updated = UpdateSection(id, name, isCollapsed);
            return new
            {
                updated.Id,
                updated.Name,
                IsCollapsed = updated.IsCollapsed ?? false
            };
        }
    }

    public static class Workspaces
    {
        public static AddWorkspace AddWorkspace(string name = "Test Workspace") => new(name)
        {
            Description = "This is a test workspace.",
            IsLinkSharingEnabled = true,
            IsGuestAllowed = true,
            Properties = new()
            {
                Industry = WorkspaceIndustry.InformationTechnology,
                Department = WorkspaceDepartment.ProductDevelopment,
                OrganizationSize = WorkspaceOrganizationSize.Size2To10,
                CreatorRole = WorkspaceCreatorRole.Leader,
                Region = WorkspaceRegion.Europe,
                DefaultAccessLevel = WorkspaceDefaultAccessLevel.Team
            }
        };

        public static object ExpectedAddWorkspace(string name = "Test Workspace")
        {
            var added = AddWorkspace(name);
            return new
            {
                added.Name,
                added.Description,
                added.IsLinkSharingEnabled,
                added.IsGuestAllowed,
                Properties = new
                {
                    added.Properties.Industry,
                    added.Properties.Department,
                    added.Properties.OrganizationSize,
                    added.Properties.CreatorRole,
                    added.Properties.Region,
                    added.Properties.DefaultAccessLevel
                },
                Role = WorkspaceRole.Admin,
                IsDeleted = false,
                IsCollapsed = false,
                CurrentMemberCount = 1,
                CurrentActiveProjects = 0,
                CurrentTemplateCount = 0,
                MemberCountByType = new WorkspaceMemberCountByType
                {
                    AdminCount = 1,
                    MemberCount = 0,
                    GuestCount = 0
                },
                PendingInvitesByType = new WorkspaceMemberCountByType
                {
                    AdminCount = 0,
                    MemberCount = 0,
                    GuestCount = 0
                }
            };
        }

        public static UpdateWorkspace UpdateWorkspace(ComplexId id, string name = "Updated Workspace") => new(id)
        {
            Name = name,
            Description = "This is an updated workspace.",
            IsLinkSharingEnabled = true,
            IsGuestAllowed = true,
            Properties = new()
            {
                Industry = WorkspaceIndustry.Education,
                Department = WorkspaceDepartment.Administration,
                Region = WorkspaceRegion.Europe,
                DefaultAccessLevel = WorkspaceDefaultAccessLevel.Team
            },
            IsCollapsed = true
        };

        public static object ExpectedUpdateWorkspace(ComplexId id, string name = "Updated Workspace")
        {
            var updated = UpdateWorkspace(id, name);
            return new
            {
                updated.Id,
                updated.Name,
                updated.Description,
                updated.IsLinkSharingEnabled,
                updated.IsGuestAllowed,
                updated.IsCollapsed,
                Properties = new
                {
                    updated.Properties.Industry,
                    updated.Properties.Department,
                    updated.Properties.Region,
                    updated.Properties.DefaultAccessLevel
                }
            };
        }
    }
}
