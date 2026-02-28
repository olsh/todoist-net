using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for projects management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.IProjectsCommandService" />
    public interface IProjectsService : IProjectsCommandService
    {
        /// <summary>
        /// Gets a read-only collection of projects that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all projects and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of projects that were synchronized.
        /// </returns>
        Task<SyncResponse<ProjectInfo>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a read-only collection of project view options defaults that were synchronized with the specified sync token.
        /// </summary>
        /// <param name="syncToken">The sync token. Use "*" to get all project view options defaults and the new sync token.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only collection of project view options defaults that were synchronized.
        /// </returns>
        Task<SyncResponse<ProjectViewOptionsDefaults>> SyncViewOptionsDefaultsAsync(string syncToken = "*", CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets projects based on a search query with cursor/limit pagination.
        /// </summary>
        /// <param name="query">The pagination query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The projects.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<ProjectInfo>> SearchAsync(PaginatedSearchQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets projects with cursor/limit pagination.
        /// </summary>
        /// <param name="query">The pagination query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The projects.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<ProjectInfo>> GetAsync(PaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets archived projects with cursor/limit pagination.
        /// </summary>
        /// <param name="query">The pagination query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The archived projects.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<ProjectInfo>> GetArchivedAsync(PaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets project collaborators with cursor/limit pagination.
        /// </summary>
        /// <param name="id">The project ID.</param>
        /// <param name="query">The pagination query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The project collaborators.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<PaginatedResponse<ProjectCollaborator>> GetCollaboratorsAsync(string id, PaginationQuery query = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of all the available roles and the associated actions they can perform in a project.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The project permissions.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ProjectPermissions> GetPermissionsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets project by ID.
        /// </summary>
        /// <param name="id">The ID of the project.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The project.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ProjectInfo> GetAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a project's full data, including uncompleted tasks.
        /// </summary>
        /// <param name="id">The ID of the project.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The project data.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ProjectData> GetDataAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Joins a workspace project by ID.
        /// </summary>
        /// <param name="id">The ID of the workspace project.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The minimal joined project data payload.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<ProjectData> JoinAsync(string id, CancellationToken cancellationToken = default);
    }
}
