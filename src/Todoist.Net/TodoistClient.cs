using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;
using Todoist.Net.Serialization.Converters;
using Todoist.Net.Serialization.Resolvers;
using Todoist.Net.Services;

namespace Todoist.Net
{
    /// <summary>
    /// A Todoist client.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="Todoist.Net.IAdvancedTodoistClient" />
    public sealed class TodoistClient : IDisposable, IAdvancedTodoistClient
    {
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    JsonResolverModifiers.SerializeInternalSetters,
                    JsonResolverModifiers.FilterSerializationByType,
                    JsonResolverModifiers.IncludeUnsetProperties
                }
            },
            Converters =
            {
                new StringEnumTypeConverter(),
                new ComplexIdConverter(),
                new CommandResultConverter(),
                new CommandArgumentConverter()
            }
        };

        private readonly ITodoistRestClient _restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistClient" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <exception cref="ArgumentException">Value cannot be null or empty - token</exception>
        public TodoistClient(string token)
            : this(token, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistClient" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="proxy">The proxy.</param>
        /// <exception cref="ArgumentException">Value cannot be null or empty - token</exception>
        public TodoistClient(string token, IWebProxy proxy)
            : this(new TodoistRestClient(token, proxy))
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(token));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistClient" /> class.
        /// </summary>
        /// <param name="restClient">The rest client.</param>
        /// <exception cref="System.ArgumentException">Value cannot be null or empty - restClient</exception>
        public TodoistClient(ITodoistRestClient restClient)
        {
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));

            Projects = new ProjectsService(this);
            Templates = new TemplateService(this);
            Items = new ItemsService(this);
            Labels = new LabelsService(this);
            Notes = new NotesService(this);
            Uploads = new UploadService(this);
            Filters = new FiltersService(this);
            Activity = new ActivityService(this);
            Notifications = new NotificationsService(this);
            Backups = new BackupService(this);
            Reminders = new RemindersService(this);
            Users = new UsersService(this);
            Sharing = new SharingService(this);
            Emails = new EmailService(this);
            Sections = new SectionService(this);
        }


        /// <summary>
        /// Gets the activity service.
        /// </summary>
        /// <value>The activity service.</value>
        public IActivityService Activity { get; }

        /// <summary>
        /// Gets the backups.
        /// </summary>
        /// <value>The backups.</value>
        public IBackupService Backups { get; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        /// <remarks>Filters are only available for Todoist Premium users.</remarks>
        public IEmailService Emails { get; }

        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <value>The filters.</value>
        /// <remarks>Filters are only available for Todoist Premium users.</remarks>
        public IFiltersService Filters { get; }

        /// <summary>
        /// Gets the items service.
        /// </summary>
        /// <value>The items service.</value>
        public IItemsService Items { get; }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <value>The labels.</value>
        public ILabelsService Labels { get; }

        /// <summary>
        /// Gets the notes service.
        /// </summary>
        /// <value>The notes service.</value>
        public INotesServices Notes { get; }

        /// <summary>
        /// Gets the notifications service.
        /// </summary>
        /// <value>The notifications service.</value>
        public INotificationsService Notifications { get; }

        /// <summary>
        /// Gets the projects service.
        /// </summary>
        /// <value>The projects service.</value>
        public IProjectsService Projects { get; }

        /// <summary>
        /// Gets the reminders.
        /// </summary>
        /// <value>The reminders.</value>
        /// <remarks>Reminders are only available for Todoist Premium users.</remarks>
        public IRemindersService Reminders { get; }

        /// <summary>
        /// Gets the sections service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public ISectionService Sections { get; }

        /// <summary>
        /// Gets the sharing.
        /// </summary>
        /// <value>
        /// The sharing.
        /// </value>
        public ISharingService Sharing { get; }

        /// <summary>
        /// Gets the templates.
        /// </summary>
        /// <value>The templates.</value>
        /// <remarks>Templates are only available for Todoist Premium users.</remarks>
        public ITemplateService Templates { get; }

        /// <summary>
        /// Gets the uploads service.
        /// </summary>
        /// <value>The uploads service.</value>
        public IUploadService Uploads { get; }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>The users.</value>
        public IUsersService Users { get; }

        /// <summary>
        /// Creates the transaction.
        /// </summary>
        /// <returns>The transaction.</returns>
        public ITransaction CreateTransaction()
        {
            return new Transaction(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _restClient?.Dispose();
        }


        /// <inheritdoc/>
        public Task<Resources> GetResourcesAsync(params ResourceType[] resourceTypes) =>
            GetResourcesAsync("*", resourceTypes);

        /// <inheritdoc/>
        public Task<Resources> GetResourcesAsync(CancellationToken cancellationToken, params ResourceType[] resourceTypes) =>
            GetResourcesAsync("*", cancellationToken, resourceTypes);

        /// <inheritdoc/>
        public Task<Resources> GetResourcesAsync(string syncToken, params ResourceType[] resourceTypes) =>
            GetResourcesAsync(syncToken, CancellationToken.None, resourceTypes);

        /// <inheritdoc/>
        public Task<Resources> GetResourcesAsync(string syncToken, CancellationToken cancellationToken, params ResourceType[] resourceTypes)
        {
            if (resourceTypes == null)
            {
                throw new ArgumentNullException(nameof(resourceTypes));
            }

            if (resourceTypes.Length == 0)
            {
                resourceTypes = new[] { ResourceType.All };
            }

            var parameters = new LinkedList<KeyValuePair<string, string>>();
            parameters.AddLast(new KeyValuePair<string, string>("sync_token", syncToken));
            parameters.AddLast(
                new KeyValuePair<string, string>(
                    "resource_types",
                    JsonSerializer.Serialize(resourceTypes, _serializerOptions)));

            return ProcessSyncAsync<Resources>(parameters, cancellationToken);
        }

        /// <inheritdoc/>
        Task<string> IAdvancedTodoistClient.ExecuteCommandsAsync(params Command[] commands) =>
            ((IAdvancedTodoistClient)this).ExecuteCommandsAsync(CancellationToken.None, commands);

        /// <inheritdoc/>
        async Task<string> IAdvancedTodoistClient.ExecuteCommandsAsync(CancellationToken cancellationToken, params Command[] commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            if (commands.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(commands));
            }

            var parameters = new LinkedList<KeyValuePair<string, string>>();
            parameters.AddLast(
                new KeyValuePair<string, string>(
                    "commands",
                    JsonSerializer.Serialize(commands, _serializerOptions)));

            var syncResponse = await ProcessSyncAsync<SyncResponse>(parameters, cancellationToken)
                                   .ConfigureAwait(false);

            ThrowIfErrors(syncResponse);

            if (syncResponse.TempIdMappings.Any())
            {
                UpdateTempIds(commands, syncResponse.TempIdMappings);
            }

            return syncResponse.SyncToken;
        }


        /// <inheritdoc/>
        Task<T> IAdvancedTodoistClient.PostFormAsync<T>(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters,
            IEnumerable<ByteArrayContent> files,
            CancellationToken cancellationToken)
        {
            return ProcessFormAsync<T>(resource, parameters, files, cancellationToken);
        }

        /// <inheritdoc/>
        async Task<T> IAdvancedTodoistClient.GetAsync<T>(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters,
            CancellationToken cancellationToken)
        {
            var response = await _restClient.GetAsync(resource, parameters, cancellationToken)
                                .ConfigureAwait(false);

            var responseContent = await ReadResponseAsync(response, cancellationToken)
                                      .ConfigureAwait(false);

            return DeserializeResponse<T>(responseContent);
        }

        /// <inheritdoc/>
        Task<T> IAdvancedTodoistClient.PostAsync<T>(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters,
            CancellationToken cancellationToken)
        {
            return ProcessPostAsync<T>(resource, parameters, cancellationToken);
        }

        /// <inheritdoc/>
        Task<string> IAdvancedTodoistClient.PostRawAsync(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters,
            CancellationToken cancellationToken)
        {
            return ProcessRawPostAsync(resource, parameters, cancellationToken);
        }


        /// <summary>
        /// Processes the form asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="files">The files.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <returns>The response.</returns>
        private async Task<T> ProcessFormAsync<T>(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters,
            IEnumerable<ByteArrayContent> files,
            CancellationToken cancellationToken)
        {
            var response = await _restClient.PostFormAsync(resource, parameters, files, cancellationToken)
                                    .ConfigureAwait(false);

            var responseContent = await ReadResponseAsync(response, cancellationToken)
                                      .ConfigureAwait(false);

            return DeserializeResponse<T>(responseContent);
        }

        /// <summary>
        /// Processes the request asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        private async Task<T> ProcessPostAsync<T>(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters,
            CancellationToken cancellationToken)
        {
            var responseContent = await ProcessRawPostAsync(resource, parameters, cancellationToken)
                                      .ConfigureAwait(false);

            return DeserializeResponse<T>(responseContent);
        }

        /// <summary>
        /// Processes the request asynchronous without deserialization.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The response content.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        private async Task<string> ProcessRawPostAsync(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters,
            CancellationToken cancellationToken)
        {
            var response = await _restClient.PostAsync(resource, parameters, cancellationToken)
                                .ConfigureAwait(false);

            var responseContent = await ReadResponseAsync(response, cancellationToken)
                                      .ConfigureAwait(false);
            return responseContent;
        }

        /// <summary>
        /// Processes the synchronize request asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of the response.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The response.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        private Task<T> ProcessSyncAsync<T>(ICollection<KeyValuePair<string, string>> parameters, CancellationToken cancellationToken)
        {
            return ProcessPostAsync<T>("sync", parameters, cancellationToken);
        }

        /// <summary>
        /// Reads the response asynchronous.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <returns>The response content.</returns>
        private async Task<string> ReadResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            var responseContent = await response.Content.ReadAsStringAsync()
                                      .ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Code: {response.StatusCode}. Content: {responseContent}.");
            }

            return responseContent;
        }

        private T DeserializeResponse<T>(string responseContent)
        {
            return JsonSerializer.Deserialize<T>(responseContent, _serializerOptions);
        }

        /// <summary>
        /// Throws if there are errors in the response.
        /// </summary>
        /// <param name="syncResponse">The synchronize response.</param>
        /// <exception cref="System.AggregateException">Command execution exception.</exception>
        private void ThrowIfErrors(SyncResponse syncResponse)
        {
            LinkedList<TodoistException> exceptions = null;
            foreach (var syncStatus in syncResponse.SyncStatus)
            {
                var result = syncStatus.Value;

                // an "ok" string which signals success of the command
                if (result.IsSuccess)
                {
                    continue;
                }

                if (exceptions == null)
                {
                    exceptions = new LinkedList<TodoistException>();
                }

                exceptions.AddLast(
                    new TodoistException(
                        result.CommandError.ErrorCode,
                        result.CommandError.Error,
                        result.CommandError));
            }

            if (exceptions?.Any() == true)
            {
                throw new AggregateException(exceptions);
            }
        }

        private void UpdateTempIds(Command[] commands, IDictionary<Guid, string> tempIdMappings)
        {
            foreach (var command in commands)
            {
                if (command.Argument is BaseEntity identifiedArgument && command.TempId.HasValue
                        && tempIdMappings.TryGetValue(command.TempId.Value, out var persistentId))
                {
                    identifiedArgument.Id = persistentId;
                }

                var withRelations = command.Argument as IWithRelationsArgument;
                withRelations?.UpdateRelatedTempIds(tempIdMappings);
            }
        }
    }
}
