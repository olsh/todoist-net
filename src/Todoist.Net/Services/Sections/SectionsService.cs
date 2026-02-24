using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for sections management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ISectionsService" />
    /// <seealso cref="Todoist.Net.Services.SectionsCommandService" />
    internal class SectionsService : SectionsCommandService, ISectionsService
    {
        internal SectionsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<SyncResponse<Section>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.Sections, r => r.Sections, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<Section>> SearchAsync(SectionsSearchQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<Section>>("sections/search", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<Section>> GetAsync(SectionsPaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<Section>>("sections", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Section> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id, nameof(id));

            return TodoistClient.GetAsync<Section>($"sections/{id}", cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Section> AddAndReturnAsync(AddSection section, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(section, nameof(section));

            return TodoistClient.PostJsonAsync<AddSection, Section>("sections", section, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Section> UpdateAndReturnAsync(string id, UpdateSection section, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id, nameof(id));
            ThrowHelper.ThrowIfNull(section, nameof(section));

            return TodoistClient.PostJsonAsync<UpdateSection, Section>($"sections/{id}", section, cancellationToken);
        }
    }
}
