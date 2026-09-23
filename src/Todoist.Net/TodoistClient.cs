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
using Todoist.Net.OAuth;
using Todoist.Net.Serialization.Converters;
using Todoist.Net.Serialization.Resolvers;
using Todoist.Net.Services;

namespace Todoist.Net
{
    /// <summary>
    /// A Todoist client.
    /// </summary>
    /// <seealso cref="Todoist.Net.IAdvancedTodoistClient" />
    public sealed class TodoistClient : IAdvancedTodoistClient
    {
        #region Constructors and Fields

        private const string SyncEndpoint = "sync";

        private const string SyncTokenParameterName = "sync_token";

        private const string ResourceTypesParameterName = "resource_types";

        private const string CommandsParameterName = "commands";

        internal static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
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

        private readonly TodoistOAuthHandler _oauthHandler;

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
            ThrowHelper.ThrowIfNullOrEmpty(token, nameof(token));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistClient" /> class which authorizes with OAuth tokens
        /// and refreshes them when they expire or get rejected.
        /// </summary>
        /// <param name="options">The credentials of the application the tokens were issued to.</param>
        /// <param name="tokens">The OAuth tokens of the user.</param>
        /// <param name="onTokensRefreshed">
        /// The callback which stores the refreshed tokens. Todoist rotates the refresh token on every refresh,
        /// so the tokens passed to it replace the stored ones. It is required when <paramref name="tokens" /> include a refresh token.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="options" />, <paramref name="tokens" /> or a required <paramref name="onTokensRefreshed" /> is null.</exception>
        /// <exception cref="ArgumentException">The client ID in <paramref name="options" /> is null or empty.</exception>
        public TodoistClient(
            TodoistOAuthOptions options,
            TodoistTokens tokens,
            Func<TodoistTokens, Task> onTokensRefreshed)
            : this(options, tokens, onTokensRefreshed, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistClient" /> class which authorizes with OAuth tokens
        /// and refreshes them when they expire or get rejected.
        /// </summary>
        /// <param name="options">The credentials of the application the tokens were issued to.</param>
        /// <param name="tokens">The OAuth tokens of the user.</param>
        /// <param name="onTokensRefreshed">
        /// The callback which stores the refreshed tokens. Todoist rotates the refresh token on every refresh,
        /// so the tokens passed to it replace the stored ones. It is required when <paramref name="tokens" /> include a refresh token.
        /// </param>
        /// <param name="proxy">The proxy.</param>
        /// <exception cref="ArgumentNullException"><paramref name="options" />, <paramref name="tokens" /> or a required <paramref name="onTokensRefreshed" /> is null.</exception>
        /// <exception cref="ArgumentException">The client ID in <paramref name="options" /> is null or empty.</exception>
        public TodoistClient(
            TodoistOAuthOptions options,
            TodoistTokens tokens,
            Func<TodoistTokens, Task> onTokensRefreshed,
            IWebProxy proxy)
            : this(
                new TodoistOAuthHandler(options, tokens, onTokensRefreshed)
                    { InnerHandler = TodoistRestClient.CreateHttpClientHandler(proxy) })
        {
        }

        private TodoistClient(TodoistOAuthHandler oauthHandler)
            : this(new TodoistRestClient(null, new HttpClient(oauthHandler), disposeHttpClient: true), oauthHandler)
        {
        }

        internal TodoistClient(ITodoistRestClient restClient, TodoistOAuthHandler oauthHandler)
            : this(restClient)
        {
            _oauthHandler = oauthHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistClient" /> class.
        /// </summary>
        /// <param name="restClient">The rest client.</param>
        /// <exception cref="System.ArgumentException">Value cannot be null or empty - restClient</exception>
        public TodoistClient(ITodoistRestClient restClient)
        {
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));

            Ids = new IdsService(this);
            Workspaces = new WorkspacesService(this);
            WorkspaceFilters = new WorkspaceFiltersService(this);
            Projects = new ProjectsService(this);
            Comments = new CommentsService(this);
            Templates = new TemplatesService(this);
            Sections = new SectionsService(this);
            Tasks = new TasksService(this);
            Labels = new LabelsService(this);
            Uploads = new UploadsService(this);
            Filters = new FiltersService(this);
            Reminders = new RemindersService(this);
            User = new UserService(this);
            Activity = new ActivityService(this);
            Backups = new BackupsService(this);
            Emails = new EmailsService(this);
            ViewOptions = new ViewOptionsService(this);
            Sharing = new SharingService(this);
            Notifications = new NotificationsService(this);
            Calendars = new CalendarsService(this);
        }

        #endregion

        #region IDisposable implementation

        /// <inheritdoc/>
        public void Dispose()
        {
            _restClient?.Dispose();
        }

        #endregion

        #region ITodoistClient implementation

        /// <inheritdoc/>
        public IIdsService Ids { get; }

        /// <inheritdoc/>
        public IWorkspacesService Workspaces { get; }

        /// <inheritdoc/>
        public IWorkspaceFiltersService WorkspaceFilters { get; }

        /// <inheritdoc/>
        public IProjectsService Projects { get; }

        /// <inheritdoc/>
        public ICommentsService Comments { get; }

        /// <inheritdoc/>
        public ITemplatesService Templates { get; }

        /// <inheritdoc/>
        public ISectionsService Sections { get; }

        /// <inheritdoc/>
        public ITasksService Tasks { get; }

        /// <inheritdoc/>
        public ILabelsService Labels { get; }

        /// <inheritdoc/>
        public IUploadsService Uploads { get; }

        /// <inheritdoc/>
        public IFiltersService Filters { get; }

        /// <inheritdoc/>
        public IRemindersService Reminders { get; }

        /// <inheritdoc/>
        public IUserService User { get; }

        /// <inheritdoc/>
        public IActivityService Activity { get; }

        /// <inheritdoc/>
        public IBackupsService Backups { get; }

        /// <inheritdoc/>
        public IEmailsService Emails { get; }

        /// <inheritdoc/>
        public IViewOptionsService ViewOptions { get; }

        /// <inheritdoc/>
        public ISharingService Sharing { get; }

        /// <inheritdoc/>
        public INotificationsService Notifications { get; }

        /// <inheritdoc/>
        public ICalendarsService Calendars { get; }

        /// <inheritdoc/>
        public ITransaction CreateTransaction()
        {
            return new Transaction(this);
        }

        /// <inheritdoc/>
        public Task<SyncResourcesResponse> SyncResourcesAsync(
            ResourceType[] resourceTypes = null,
            string syncToken = "*",
            CancellationToken cancellationToken = default)
        {
            return SyncResourcesAsync<SyncResourcesResponse>(resourceTypes, syncToken, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<T> SyncResourcesAsync<T>(
            ResourceType[] resourceTypes = null,
            string syncToken = "*",
            CancellationToken cancellationToken = default)
            where T : BaseSyncResponse
        {
            if (resourceTypes == null || resourceTypes.Length == 0)
            {
                resourceTypes = new[] { ResourceType.All };
            }

            var serializedResourceTypes = JsonSerializer.Serialize(resourceTypes, SerializerOptions);
            syncToken = syncToken ?? "*";

            var parameters = new Dictionary<string, string>
            {
                { SyncTokenParameterName, syncToken },
                { ResourceTypesParameterName, serializedResourceTypes }
            };

            return ProcessSyncAsync<T>(parameters, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<SyncTransactionResponse> ExecuteTransactionAsync(
            Func<ITransaction, Task> transactionActions,
            CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(transactionActions, nameof(transactionActions));

            var transaction = new Transaction(this);
            await transactionActions(transaction)
                .ConfigureAwait(false);

            return await transaction.CommitAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<SyncTransactionResponse> ExecuteTransactionAndSyncAsync(
            Func<ITransaction, Task> transactionActions,
            ResourceType[] resourceTypes,
            string syncToken = "*",
            CancellationToken cancellationToken = default)
        {
            ThrowHelper.ThrowIfNull(transactionActions, nameof(transactionActions));

            var transaction = new Transaction(this);
            await transactionActions(transaction)
                .ConfigureAwait(false);

            return await transaction.CommitAndSyncAsync(resourceTypes, syncToken, cancellationToken)
                .ConfigureAwait(false);
        }

        #endregion

        #region OAuth

        /// <summary>
        /// Refreshes the OAuth tokens ahead of their expiration.
        /// </summary>
        /// <remarks>
        /// The client refreshes the tokens on its own when they expire or get rejected, so calling this method is optional.
        /// The refreshed tokens are passed to the callback given when the client was created, the same way as after an automatic refresh.
        /// </remarks>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The refreshed tokens.</returns>
        /// <exception cref="InvalidOperationException">The client was not created with OAuth tokens, or they include no refresh token.</exception>
        /// <exception cref="TodoistException">Todoist rejected the refresh, e.g. because the refresh token was revoked.</exception>
        public Task<TodoistTokens> RefreshTokensAsync(CancellationToken cancellationToken = default)
        {
            return GetOAuthHandler()
                .RefreshTokensAsync(cancellationToken);
        }

        /// <summary>
        /// Revokes the OAuth access token, and stops the client from refreshing the tokens.
        /// </summary>
        /// <remarks>
        /// <para>Revoking requires the client secret of the application.</para>
        /// <para>
        /// Todoist can revoke access tokens only, so the refresh token keeps working: delete the stored tokens to give up
        /// the access for good. The authorization itself ends when the user removes the application in the Todoist settings.
        /// </para>
        /// </remarks>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">The client was not created with OAuth tokens, or without the client secret.</exception>
        public Task RevokeTokensAsync(CancellationToken cancellationToken = default)
        {
            return GetOAuthHandler()
                .RevokeTokensAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the handler which authorizes requests with OAuth tokens, or <c>null</c> when the client was not created with OAuth tokens.
        /// </summary>
        internal TodoistOAuthHandler OAuthHandler => _oauthHandler;

        internal static TodoistClient CreateOAuthClient(
            TodoistOAuthOptions options,
            TodoistTokens tokens,
            Func<TodoistTokens, Task> onTokensRefreshed,
            HttpMessageHandler innerHandler)
        {
            return new TodoistClient(
                new TodoistOAuthHandler(options, tokens, onTokensRefreshed) { InnerHandler = innerHandler });
        }

        private TodoistOAuthHandler GetOAuthHandler()
        {
            return _oauthHandler ??
                   throw new InvalidOperationException("The client was not created with OAuth tokens.");
        }

        #endregion

        #region IAdvancedTodoistClient implementation

        /// <inheritdoc/>
        async Task<SyncTransactionResponse> IAdvancedTodoistClient.SyncCommandsAsync(
            Command[] commands,
            ResourceType[] includedResources,
            string syncToken,
            bool throwOnError,
            CancellationToken cancellationToken)
        {
            ThrowHelper.ThrowIfNullOrEmpty(commands, nameof(commands));

            var serializedCommands = JsonSerializer.Serialize(commands, SerializerOptions);

            var parameters = new Dictionary<string, string>
            {
                { CommandsParameterName, serializedCommands }
            };

            if (includedResources != null && includedResources.Length > 0)
            {
                parameters[ResourceTypesParameterName] = JsonSerializer.Serialize(includedResources, SerializerOptions);
            }

            if (!string.IsNullOrEmpty(syncToken))
            {
                parameters[SyncTokenParameterName] = syncToken;
            }

            var syncResponse = await ProcessSyncAsync<SyncTransactionResponse>(parameters, cancellationToken)
                .ConfigureAwait(false);

            if (throwOnError)
            {
                ThrowIfErrors(syncResponse);
            }

            if (syncResponse.TempIdMappings.Count > 0)
            {
                UpdateTempIds(commands, syncResponse.TempIdMappings);
            }

            return syncResponse;
        }

        /// <inheritdoc/>
        Task IAdvancedTodoistClient.GetAsync(
            string resource,
            Dictionary<string, string> queryParams,
            CancellationToken cancellationToken)
        {
            return ProcessRequestAsync(ct => _restClient.GetAsync(resource, queryParams, ct), cancellationToken);
        }

        /// <inheritdoc/>
        Task<T> IAdvancedTodoistClient.GetAsync<T>(
            string resource,
            Dictionary<string, string> queryParams,
            CancellationToken cancellationToken)
        {
            return ProcessRequestAsync<T>(ct => _restClient.GetAsync(resource, queryParams, ct), cancellationToken);
        }

        /// <inheritdoc/>
        Task<string> IAdvancedTodoistClient.GetStringAsync(
            string resource,
            Dictionary<string, string> queryParams,
            CancellationToken cancellationToken)
        {
            return ProcessTextRequestAsync(ct => _restClient.GetAsync(resource, queryParams, ct), cancellationToken);
        }

        /// <inheritdoc/>
        Task IAdvancedTodoistClient.PostAsync(
            string resource,
            Dictionary<string, string> formParams,
            CancellationToken cancellationToken)
        {
            return ProcessRequestAsync(ct => _restClient.PostAsync(resource, formParams, ct), cancellationToken);
        }

        /// <inheritdoc/>
        Task<T> IAdvancedTodoistClient.PostAsync<T>(
            string resource,
            Dictionary<string, string> formParams,
            CancellationToken cancellationToken)
        {
            return ProcessRequestAsync<T>(ct => _restClient.PostAsync(resource, formParams, ct), cancellationToken);
        }

        /// <inheritdoc/>
        Task IAdvancedTodoistClient.PostFilesAsync(
            string resource,
            UploadFile[] files,
            Dictionary<string, string> formParams,
            CancellationToken cancellationToken)
        {
            return ProcessRequestAsync(
                ct => _restClient.PostFilesAsync(resource, files, formParams, ct),
                cancellationToken);
        }

        /// <inheritdoc/>
        Task<T> IAdvancedTodoistClient.PostFilesAsync<T>(
            string resource,
            UploadFile[] files,
            Dictionary<string, string> formParams,
            CancellationToken cancellationToken)
        {
            return ProcessRequestAsync<T>(
                ct => _restClient.PostFilesAsync(resource, files, formParams, ct),
                cancellationToken);
        }

        /// <inheritdoc/>
        Task IAdvancedTodoistClient.PostJsonAsync<TReq>(
            string resource,
            TReq content,
            CancellationToken cancellationToken)
        {
            return ProcessJsonRequestAsync(resource, content, _restClient.PostJsonAsync, cancellationToken);
        }

        /// <inheritdoc/>
        Task<TRes> IAdvancedTodoistClient.PostJsonAsync<TReq, TRes>(
            string resource,
            TReq content,
            CancellationToken cancellationToken)
        {
            return ProcessJsonRequestAsync<TReq, TRes>(resource, content, _restClient.PostJsonAsync, cancellationToken);
        }

        /// <inheritdoc/>
        Task IAdvancedTodoistClient.PutAsync(string resource, CancellationToken cancellationToken)
        {
            return ProcessRequestAsync(ct => _restClient.PutAsync(resource, ct), cancellationToken);
        }

        /// <inheritdoc/>
        Task<T> IAdvancedTodoistClient.PutAsync<T>(string resource, CancellationToken cancellationToken)
        {
            return ProcessRequestAsync<T>(ct => _restClient.PutAsync(resource, ct), cancellationToken);
        }

        /// <inheritdoc/>
        Task IAdvancedTodoistClient.PutJsonAsync<TReq>(
            string resource,
            TReq content,
            CancellationToken cancellationToken)
        {
            return ProcessJsonRequestAsync(resource, content, _restClient.PutJsonAsync, cancellationToken);
        }

        /// <inheritdoc/>
        Task<TRes> IAdvancedTodoistClient.PutJsonAsync<TReq, TRes>(
            string resource,
            TReq content,
            CancellationToken cancellationToken)
        {
            return ProcessJsonRequestAsync<TReq, TRes>(resource, content, _restClient.PutJsonAsync, cancellationToken);
        }

        /// <inheritdoc/>
        Task IAdvancedTodoistClient.DeleteAsync(
            string resource,
            Dictionary<string, string> queryParams,
            CancellationToken cancellationToken)
        {
            return ProcessRequestAsync(ct => _restClient.DeleteAsync(resource, queryParams, ct), cancellationToken);
        }

        /// <inheritdoc/>
        Task<T> IAdvancedTodoistClient.DeleteAsync<T>(
            string resource,
            Dictionary<string, string> queryParams,
            CancellationToken cancellationToken)
        {
            return ProcessRequestAsync<T>(ct => _restClient.DeleteAsync(resource, queryParams, ct), cancellationToken);
        }

        #endregion

        #region Private helper methods

        private Task<T> ProcessSyncAsync<T>(Dictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            return ProcessRequestAsync<T>(ct => _restClient.PostAsync(SyncEndpoint, parameters, ct), cancellationToken);
        }

        private static async Task ProcessRequestAsync(
            Func<CancellationToken, Task<HttpResponseMessage>> restCall,
            CancellationToken cancellationToken)
        {
            var response = await restCall(cancellationToken)
                .ConfigureAwait(false);

            await EnsureSuccessResponseAsync(response, cancellationToken)
                .ConfigureAwait(false);
        }

        private static async Task<T> ProcessRequestAsync<T>(
            Func<CancellationToken, Task<HttpResponseMessage>> restCall,
            CancellationToken cancellationToken)
        {
            var response = await restCall(cancellationToken)
                .ConfigureAwait(false);

            await EnsureSuccessResponseAsync(response, cancellationToken)
                .ConfigureAwait(false);

            return await DeserializeResponseAsync<T>(response, cancellationToken)
                .ConfigureAwait(false);
        }

        private static async Task<string> ProcessTextRequestAsync(
            Func<CancellationToken, Task<HttpResponseMessage>> restCall,
            CancellationToken cancellationToken)
        {
            var response = await restCall(cancellationToken)
                .ConfigureAwait(false);

            await EnsureSuccessResponseAsync(response, cancellationToken)
                .ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
        }

        private static async Task ProcessJsonRequestAsync<TReq>(
            string resource,
            TReq content,
            Func<string, string, CancellationToken, Task<HttpResponseMessage>> restCall,
            CancellationToken cancellationToken)
        {
            var jsonContent = JsonSerializer.Serialize(content, SerializerOptions);

            var response = await restCall(resource, jsonContent, cancellationToken)
                .ConfigureAwait(false);

            await EnsureSuccessResponseAsync(response, cancellationToken)
                .ConfigureAwait(false);
        }

        private static async Task<TRes> ProcessJsonRequestAsync<TReq, TRes>(
            string resource,
            TReq content,
            Func<string, string, CancellationToken, Task<HttpResponseMessage>> restCall,
            CancellationToken cancellationToken)
        {
            var jsonContent = JsonSerializer.Serialize(content, SerializerOptions);

            var response = await restCall(resource, jsonContent, cancellationToken)
                .ConfigureAwait(false);

            await EnsureSuccessResponseAsync(response, cancellationToken)
                .ConfigureAwait(false);

            return await DeserializeResponseAsync<TRes>(response, cancellationToken)
                .ConfigureAwait(false);
        }

        internal static async Task EnsureSuccessResponseAsync(
            HttpResponseMessage response,
            CancellationToken cancellationToken)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            TodoistError errorContent;
            try
            {
                errorContent = await DeserializeResponseAsync<TodoistError>(response, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch
            {
                // If deserialization fails, we can still throw a generic exception with status code and reason.
                errorContent = null;
            }

            // Any JSON object deserializes into a `TodoistError` instance, so a body which is not an
            // actual Todoist error would produce an exception without a single populated property.
            if (errorContent != null && HasErrorDetails(errorContent))
            {
                errorContent.HttpCode = errorContent.HttpCode ?? (int)response.StatusCode;

                throw new TodoistException(errorContent);
            }

            response.EnsureSuccessStatusCode();
        }

        private static bool HasErrorDetails(TodoistError error)
        {
            return error.Error != null
                   || error.ErrorCode.HasValue
                   || error.ErrorTag != null
                   || error.HttpCode.HasValue
                   || error.ErrorExtra != null;
        }

        internal static async Task<T> DeserializeResponseAsync<T>(
            HttpResponseMessage response,
            CancellationToken cancellationToken)
        {
            using (var responseStream = await response.Content.ReadAsStreamAsync()
                       .ConfigureAwait(false))
            {
                return await JsonSerializer.DeserializeAsync<T>(responseStream, SerializerOptions, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        private static void ThrowIfErrors(SyncTransactionResponse syncResponse)
        {
            var exceptions = syncResponse.SyncStatus
                .Where(kvp => !kvp.Value.IsSuccess)
                .Select(kvp => new TodoistException(kvp.Value.CommandBody))
                .ToList();

            if (exceptions.Count > 1)
            {
                throw new AggregateException(exceptions);
            }

            if (exceptions.Count == 1)
            {
                throw exceptions[0];
            }
        }

        private static void UpdateTempIds(Command[] commands, Dictionary<Guid, string> tempIdMappings)
        {
            foreach (var command in commands)
            {
                if (command.Argument is BaseEntity identifiedArgument
                    && command.TempId.HasValue
                    && tempIdMappings.TryGetValue(command.TempId.Value, out var persistentId))
                {
                    identifiedArgument.Id = persistentId;
                }

                var withRelations = command.Argument as IWithRelationsArgument;
                withRelations?.UpdateRelatedTempIds(tempIdMappings);
            }
        }

        #endregion
    }
}
