using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;
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
        private static readonly JsonSerializerSettings SerializerSettings
            = new JsonSerializerSettings
                  {
                      DateFormatString = "ddd dd MMM yyyy HH:mm:ss +0000",
                      DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                      NullValueHandling = NullValueHandling.Ignore,
                      ContractResolver = new ConverterContractResolver()
                  };

        private readonly ITodoistRestClient _restClient;

        private readonly string _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistClient" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <exception cref="ArgumentException">Value cannot be null or empty - token</exception>
        public TodoistClient(string token)
            : this(token, new TodoistRestClient())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistClient" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="restClient">The rest client.</param>
        /// <exception cref="System.ArgumentException">Value cannot be null or empty - token</exception>
        internal TodoistClient(string token, ITodoistRestClient restClient)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(token));
            }

            _token = token;
            _restClient = restClient;

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
        }

        private TodoistClient()
        {
            _restClient = new TodoistRestClient();
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
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        /// <remarks>Filters are only available for Todoist Premium users.</remarks>
        public IEmailService Emails { get; set; }

        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        /// <value>The filters.</value>
        /// <remarks>Filters are only available for Todoist Premium users.</remarks>
        public IFiltersService Filters { get; set; }

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
        /// Logins user and returns a new instance of Todoist client.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>A new instance of Todoist client.</returns>
        /// <exception cref="System.ArgumentException">
        /// Value cannot be null or empty - email
        /// or
        /// Value cannot be null or empty - password
        /// </exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Unable to get token.</exception>
        [Obsolete("This method is scheduled for deprecation and probably will be removed in future versions.")]
        public static Task<TodoistClient> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(password));
            }

            var parameters = new[]
                                 {
                                     new KeyValuePair<string, string>("email", email),
                                     new KeyValuePair<string, string>("password", password)
                                 };

            return LoginWithCredentialsAsync("login", parameters);
        }

        /// <summary>
        /// Logins user and returns a new instance of Todoist client.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="oauthToken">The oauth token.</param>
        /// <returns>A new instance of Todoist client.</returns>
        /// <exception cref="System.ArgumentException">Value cannot be null or empty - email
        /// or
        /// Value cannot be null or empty - password</exception>
        /// <exception cref="HttpRequestException">Value cannot be null or empty - email
        /// or
        /// Value cannot be null or empty - password</exception>
        /// <exception cref="TodoistException">API exception.</exception>
        [Obsolete("This method is scheduled for deprecation and probably will be removed in future versions.")]
        public static Task<TodoistClient> LoginWithGoogleAsync(string email, string oauthToken)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(email));
            }

            if (string.IsNullOrEmpty(oauthToken))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(oauthToken));
            }

            var parameters = new[]
                                 {
                                     new KeyValuePair<string, string>("email", email),
                                     new KeyValuePair<string, string>("oauth2_token", oauthToken)
                                 };

            return LoginWithCredentialsAsync("login_with_google", parameters);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The user info.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="user"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public static async Task<UserInfo> RegisterUserAsync(UserBase user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            using (var tokenlessClient = new TodoistClient())
            {
                var userInfo =
                    await tokenlessClient.ProcessPostAsync<UserInfo>("user/register", user.ToParameters())
                        .ConfigureAwait(false);

                return userInfo;
            }
        }

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

        /// <summary>
        /// Gets the resources asynchronous.
        /// </summary>
        /// <param name="resourceTypes">The resource types.</param>
        /// <returns>
        /// All resources.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="resourceTypes"/> is <see langword="null"/></exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<Resources> GetResourcesAsync(params ResourceType[] resourceTypes)
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
            parameters.AddLast(new KeyValuePair<string, string>("sync_token", "*"));
            parameters.AddLast(
                new KeyValuePair<string, string>(
                    "resource_types",
                    JsonConvert.SerializeObject(resourceTypes, SerializerSettings)));

            return ProcessSyncAsync<Resources>(parameters);
        }

        /// <summary>
        /// Posts the asynchronous and returns a raw content.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="files">The files.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public Task<T> PostFormAsync<T>(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters,
            IEnumerable<ByteArrayContent> files)
        {
            return ProcessFormAsync<T>(resource, parameters, files);
        }

        /// <summary>
        /// Executes the commands asynchronous.
        /// </summary>
        /// <param name="commands">The commands.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">Value cannot be null - commands.</exception>
        /// <exception cref="System.AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be an empty collection.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        async Task IAdvancedTodoistClient.ExecuteCommandsAsync(params Command[] commands)
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
                new KeyValuePair<string, string>("commands", JsonConvert.SerializeObject(commands, SerializerSettings)));

            var syncResponse = await ProcessSyncAsync<SyncResponse>(parameters).ConfigureAwait(false);

            ThrowIfErrors(syncResponse);

            if (syncResponse.TempIdMappings.Any())
            {
                UpdateTempIds(commands, syncResponse.TempIdMappings);
            }
        }

        /// <summary>
        /// Posts the request asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<T> IAdvancedTodoistClient.PostAsync<T>(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters)
        {
            return ProcessPostAsync<T>(resource, parameters);
        }

        /// <summary>
        /// Posts the asynchronous and returns a raw content.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<string> IAdvancedTodoistClient.PostRawAsync(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters)
        {
            return ProcessRawPostAsync(resource, parameters);
        }

        /// <summary>
        /// Logins with credentials and returns a new instance of Todoist client.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A new instance of Todoist client.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <exception cref="TodoistException">Unable to get token.</exception>
        private static async Task<TodoistClient> LoginWithCredentialsAsync(
            string resource,
            KeyValuePair<string, string>[] parameters)
        {
            using (var tokenlessClient = new TodoistClient())
            {
                var userInfo = await tokenlessClient.ProcessPostAsync<UserInfo>(resource, parameters).ConfigureAwait(false);

                return new TodoistClient(userInfo.Token);
            }
        }

        private T DeserializeResponse<T>(string responseContent)
        {
            return JsonConvert.DeserializeObject<T>(responseContent, SerializerSettings);
        }

        /// <summary>
        /// Processes the form asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="files">The files.</param>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <returns>The response.</returns>
        private async Task<T> ProcessFormAsync<T>(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters,
            IEnumerable<ByteArrayContent> files)
        {
            TryAddToken(parameters);

            var response = await _restClient.PostFormAsync(resource, parameters, files).ConfigureAwait(false);
            var responseContent = await ReadResponseAsync(response).ConfigureAwait(false);

            return DeserializeResponse<T>(responseContent);
        }

        /// <summary>
        /// Processes the request asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        private async Task<T> ProcessPostAsync<T>(string resource, ICollection<KeyValuePair<string, string>> parameters)
        {
            var responseContent = await ProcessRawPostAsync(resource, parameters).ConfigureAwait(false);

            return DeserializeResponse<T>(responseContent);
        }

        /// <summary>
        /// Processes the request asynchronous without deserialization.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The response content.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        private async Task<string> ProcessRawPostAsync(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters)
        {
            TryAddToken(parameters);

            HttpResponseMessage response = await _restClient.PostAsync(resource, parameters).ConfigureAwait(false);

            var responseContent = await ReadResponseAsync(response).ConfigureAwait(false);
            return responseContent;
        }

        /// <summary>
        /// Processes the synchronize request asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of the response.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The response.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        private Task<T> ProcessSyncAsync<T>(ICollection<KeyValuePair<string, string>> parameters)
        {
            return ProcessPostAsync<T>("sync", parameters);
        }

        /// <summary>
        /// Reads the response asynchronous.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <exception cref="HttpRequestException">API exception.</exception>
        /// <returns>The response content.</returns>
        private async Task<string> ReadResponseAsync(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Code: {response.StatusCode}. Content: {responseContent}.");
            }

            return responseContent;
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
                var dynamicStatus = syncStatus.Value;
                var type = dynamicStatus.GetType();

                // an "ok" string which signals success of the command
                if (type == typeof(string) || dynamicStatus.error_code == null)
                {
                    continue;
                }

                if (exceptions == null)
                {
                    exceptions = new LinkedList<TodoistException>();
                }

                exceptions.AddLast(
                    new TodoistException((long)dynamicStatus.error_code, dynamicStatus.error.ToString(), dynamicStatus));
            }

            if (exceptions?.Any() == true)
            {
                throw new AggregateException(exceptions);
            }
        }

        private void TryAddToken(ICollection<KeyValuePair<string, string>> parameters)
        {
            if (!string.IsNullOrEmpty(_token))
            {
                parameters.Add(new KeyValuePair<string, string>("token", _token));
            }
        }

        private void UpdateTempIds(Command[] commands, Dictionary<Guid, long> tempIdMappings)
        {
            foreach (var command in commands)
            {
                var identifiedArgument = command.Argument as BaseEntity;
                if (identifiedArgument != null)
                {
                    long persistentId;
                    if (command.TempId.HasValue && tempIdMappings.TryGetValue(command.TempId.Value, out persistentId))
                    {
                        identifiedArgument.Id = persistentId;
                    }
                }

                var withRelations = command.Argument as IWithRelationsArgument;
                withRelations?.UpdateRelatedTempIds(tempIdMappings);
            }
        }
    }
}
