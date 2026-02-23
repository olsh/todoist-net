using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for workspace management which can be executed in a transaction.
    /// </summary>
    public interface IWorkspacesCommandService
    {
        /// <summary>
        /// Adds a new workspace.
        /// </summary>
        /// <param name="workspace">The workspace payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The temporary ID of the workspace.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="workspace"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task<ComplexId> AddAsync(AddWorkspace workspace, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing workspace.
        /// </summary>
        /// <param name="workspace">The workspace payload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="workspace"/> is <see langword="null"/>.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task UpdateAsync(UpdateWorkspace workspace, CancellationToken cancellationToken = default);

        /// <summary>
        /// Leaves a workspace.
        /// </summary>
        /// <param name="id">The workspace identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task LeaveAsync(ComplexId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a workspace.
        /// </summary>
        /// <param name="id">The workspace identifier.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task DeleteAsync(ComplexId id, CancellationToken cancellationToken = default);


        /// <summary>
        /// Changes the role of a user in a workspace.
        /// </summary>
        /// <param name="userRoleArgs">The argument containing the workspace identifier, user email, and new role.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task ChangeUserRoleAsync(ChangeWorkspaceUserRoleArgument userRoleArgs, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the sidebar sorting preference for a workspace.
        /// </summary>
        /// <param name="sidebarArgs">The argument containing the workspace identifier and new sidebar sorting preference.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task UpdateSidebarPreferenceAsync(UpdateSidebarPreferenceArgument sidebarArgs, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Deletes a user from a workspace.
        /// </summary>
        /// <param name="userArgs">The argument containing the workspace identifier and the email of the user to be deleted.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task DeleteUserAsync(DeleteWorkspaceUserArgument userArgs, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Invites users to a workspace.
        /// </summary>
        /// <param name="userArgs">The argument containing the workspace identifier, the list of emails of the users to be invited, and the role to be assigned.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Command execution exception.</exception>
        Task InviteUsersAsync(InviteWorkspaceUsersArgument userArgs, CancellationToken cancellationToken = default);
    }
}
