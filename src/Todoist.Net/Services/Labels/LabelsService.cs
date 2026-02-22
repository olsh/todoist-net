using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains operations for labels management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.LabelsCommandService" />
    /// <seealso cref="Todoist.Net.Services.ILabelsService" />
    internal class LabelsService : LabelsCommandService, ILabelsService
    {
        internal LabelsService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<SyncResponse<Label>> SyncAsync(string syncToken = "*", CancellationToken cancellationToken = default)
        {
            return SyncResourceAsync(ResourceType.Labels, r => r.Labels, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<Label>> SearchAsync(PaginatedSearchQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<Label>>("labels/search", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<Label>> GetAsync(PaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<Label>>("labels", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<PaginatedResponse<string>> GetSharedAsync(SharedLabelsPaginationQuery query = null, CancellationToken cancellationToken = default)
        {
            return TodoistClient.GetAsync<PaginatedResponse<string>>("labels/shared", query?.ToParameters(), cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Label> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id, nameof(id));

            return TodoistClient.GetAsync<Label>($"labels/{id}", cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Label> AddAndReturnAsync(Label label, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(label, nameof(label));

            return TodoistClient.PostJsonAsync<Label, Label>("labels", label, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Label> UpdateAndReturnAsync(string id, Label label, CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNullOrEmpty(id, nameof(id));
            ThrowHelper.ThrowIfNull(label, nameof(label));

            return TodoistClient.PostJsonAsync<Label, Label>($"labels/{id}", label, cancellationToken);
        }
    }
}
