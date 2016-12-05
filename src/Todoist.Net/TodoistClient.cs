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
    public class TodoistClient : IDisposable, ITodoistClient
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
                                                                                {
                                                                                    DateFormatString =
                                                                                        "ddd dd MMM yyyy HH:mm:ss +0000",
                                                                                    DateTimeZoneHandling =
                                                                                        DateTimeZoneHandling.Utc,
                                                                                    NullValueHandling =
                                                                                        NullValueHandling.Ignore,
                                                                                    ContractResolver =
                                                                                        ConverterContractResolver.Instance
                                                                                };

        private readonly ITodoistRestClient _restClient;

        private readonly string _token;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistClient" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="restClient">The rest client.</param>
        /// <exception cref="System.ArgumentException">Value cannot be null or empty - token</exception>
        /// <exception cref="System.ArgumentNullException">Value cannot be null - restClient</exception>
        public TodoistClient(string token, ITodoistRestClient restClient)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(token));
            }

            if (restClient == null)
            {
                throw new ArgumentNullException(nameof(restClient));
            }

            _token = token;
            _restClient = restClient;

            Projects = new ProjectService(this);
            Notes = new NotesServices(this);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistClient" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <exception cref="ArgumentException">Value cannot be null or empty - token</exception>
        /// <exception cref="ArgumentNullException">Value cannot be null - restClient</exception>
        public TodoistClient(string token)
            : this(token, new TodoistRestClient())
        {
        }

        public INotesServices Notes { get; }

        public IProjectService Projects { get; }

        /// <summary>
        /// Creates the transaction.
        /// </summary>
        /// <returns>The transaction.</returns>
        public Transaction CreateTransaction()
        {
            return new Transaction(this);
        }

        public void Dispose()
        {
            _restClient?.Dispose();
        }

        /// <summary>
        ///     Executes the commands asynchronous.
        /// </summary>
        /// <param name="commands">The commands.</param>
        /// <returns>The task.</returns>
        /// <exception cref="System.ArgumentNullException">Value cannot be null - commands.</exception>
        /// <exception cref="System.AggregateException">Command execution exception.</exception>
        /// <exception cref="ArgumentException">Value cannot be an empty collection.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task ExecuteCommandsAsync(params Command[] commands)
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
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<T> GetAsync<T>(string resource, ICollection<KeyValuePair<string, string>> parameters)
        {
            return await ProcessAsync<T>(resource, parameters, HttpMethod.Get).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the resources asynchronous.
        /// </summary>
        /// <param name="resourceTypes">The resource types.</param>
        /// <returns>
        /// All resources.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="resourceTypes"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">Value cannot be an empty collection.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        public async Task<Resources> GetResourcesAsync(params ResourceType[] resourceTypes)
        {
            if (resourceTypes == null)
            {
                throw new ArgumentNullException(nameof(resourceTypes));
            }

            if (resourceTypes.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(resourceTypes));
            }

            var parameters = new LinkedList<KeyValuePair<string, string>>();
            parameters.AddLast(new KeyValuePair<string, string>("sync_token", "*"));
            parameters.AddLast(
                new KeyValuePair<string, string>(
                    "resource_types",
                    JsonConvert.SerializeObject(resourceTypes, SerializerSettings)));

            return await ProcessSyncAsync<Resources>(parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Throws if there are error in the response.
        /// </summary>
        /// <param name="syncResponse">The synchronize response.</param>
        /// <exception cref="System.AggregateException">Command execution exception.</exception>        
        private static void ThrowIfErrors(SyncResponse syncResponse)
        {
            LinkedList<TodoistException> exceptions = null;
            foreach (var syncStatus in syncResponse.SyncStatus)
            {
                var dynamicStatus = syncStatus.Value;
                var type = dynamicStatus.GetType();

                // an "ok” string which signals success of the command
                if (type == typeof(string) || dynamicStatus.error_code == null)
                {
                    continue;
                }

                if (exceptions == null)
                {
                    exceptions = new LinkedList<TodoistException>();
                }

                exceptions.AddLast(
                    new TodoistException((int)dynamicStatus.error_code, dynamicStatus.error.ToString(), dynamicStatus));
            }

            if (exceptions?.Any() == true)
            {
                throw new AggregateException(exceptions);
            }
        }

        private static void UpdateTempIds(Command[] commands, Dictionary<Guid, int> tempIdMappings)
        {
            foreach (var command in commands)
            {
                var identifiedArgument = command.Argument as BaseEntity;
                if (identifiedArgument != null)
                {
                    int persistentId;
                    if (command.TempId.HasValue && tempIdMappings.TryGetValue(command.TempId.Value, out persistentId))
                    {
                        identifiedArgument.Id = persistentId;
                    }
                }

                var withRelations = command.Argument as IWithRelationsArgument;
                withRelations?.UpdateRelatedTempIds(tempIdMappings);
            }
        }

        /// <summary>
        /// Processes the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        private async Task<T> ProcessAsync<T>(
            string resource,
            ICollection<KeyValuePair<string, string>> parameters,
            HttpMethod method)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (!string.IsNullOrEmpty(_token))
            {
                parameters.Add(new KeyValuePair<string, string>("token", _token));
            }

            HttpResponseMessage response;
            if (method == HttpMethod.Get)
            {
                response = await _restClient.GetAsync(resource, parameters).ConfigureAwait(false);
            }
            else if (method == HttpMethod.Post)
            {
                response = await _restClient.PostAsync(resource, parameters).ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentException("Unsupported method.", nameof(method));
            }

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Code: {response.StatusCode}. Content: {responseContent}.");
            }

            return JsonConvert.DeserializeObject<T>(responseContent, SerializerSettings);
        }

        /// <summary>
        /// Processes the synchronize request asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of the response.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The response.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        private async Task<T> ProcessSyncAsync<T>(ICollection<KeyValuePair<string, string>> parameters)
        {
            return await ProcessAsync<T>("sync", parameters, HttpMethod.Post).ConfigureAwait(false);
        }
    }
}
