using System;
using System.IO;
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

namespace Todoist.Net
{
    internal static class TodoistSerializer 
    { 
        public static JsonSerializerOptions SerializerOptions { get; } = new JsonSerializerOptions
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


        public static string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, SerializerOptions);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, SerializerOptions);
        }

        public static T Deserialize<T>(Stream utf8Json)
        {
            return JsonSerializer.Deserialize<T>(utf8Json, SerializerOptions);
        }

        public static ValueTask<T> DeserializeAsync<T>(Stream utf8Json, CancellationToken cancellationToken = default)
        {
            return JsonSerializer.DeserializeAsync<T>(utf8Json, SerializerOptions, cancellationToken);
        }

        public static async Task<T> DeserializeResponseAsync<T>(
            HttpResponseMessage response,
            CancellationToken cancellationToken = default)
        {
            using (var responseStream = await response.Content.ReadAsStreamAsync()
                       .ConfigureAwait(false))
            {
                return await DeserializeAsync<T>(responseStream, cancellationToken)
                    .ConfigureAwait(false);
            }
        }


        public static async Task ProcessRequestAsync(
            Func<CancellationToken, Task<HttpResponseMessage>> requestFunc,
            CancellationToken cancellationToken)
        {
            using (var response = await requestFunc(cancellationToken)
                .ConfigureAwait(false))
            {
                await EnsureSuccessResponseAsync(response, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        public static async Task<T> ProcessRequestAsync<T>(
            Func<CancellationToken, Task<HttpResponseMessage>> requestFunc,
            CancellationToken cancellationToken)
        {
            using (var response = await requestFunc(cancellationToken)
                       .ConfigureAwait(false))
            {
                await EnsureSuccessResponseAsync(response, cancellationToken)
                    .ConfigureAwait(false);

                return await DeserializeResponseAsync<T>(response, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        public static async Task<string> ProcessTextRequestAsync(
            Func<CancellationToken, Task<HttpResponseMessage>> requestFunc,
            CancellationToken cancellationToken)
        {
            using (var response = await requestFunc(cancellationToken)
                       .ConfigureAwait(false))
            {
                await EnsureSuccessResponseAsync(response, cancellationToken)
                    .ConfigureAwait(false);

                return await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);
            }
        }

        public static async Task ProcessJsonRequestAsync<TReq>(
            TReq content,
            Func<string, CancellationToken, Task<HttpResponseMessage>> requestFunc,
            CancellationToken cancellationToken)
        {
            var jsonContent = Serialize(content);

            using (var response = await requestFunc(jsonContent, cancellationToken)
                       .ConfigureAwait(false))
            {
                await EnsureSuccessResponseAsync(response, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        public static async Task<TRes> ProcessJsonRequestAsync<TReq, TRes>(
            TReq content,
            Func<string, CancellationToken, Task<HttpResponseMessage>> requestFunc,
            CancellationToken cancellationToken)
        {
            var jsonContent = Serialize(content);

            using (var response = await requestFunc(jsonContent, cancellationToken)
                       .ConfigureAwait(false))
            {
                await EnsureSuccessResponseAsync(response, cancellationToken)
                    .ConfigureAwait(false);

                return await DeserializeResponseAsync<TRes>(response, cancellationToken)
                    .ConfigureAwait(false);
            }
        }


        private static async Task EnsureSuccessResponseAsync(
            HttpResponseMessage response,
            CancellationToken cancellationToken = default)
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
    }
}
