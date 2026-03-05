using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Todoist.Net.Exceptions;
using Todoist.Net.Models;

namespace Todoist.Net
{
    internal interface IAdvancedTodoistClient : ITodoistClient
    {
        /// <summary>
        /// Sends a <c>GET</c> request, and handles response asynchronously.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task GetAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>GET</c> request, and handles response asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> containing the response data.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<T> GetAsync<T>(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default);


        /// <summary>
        /// Sends a <c>POST</c> request with form data, and handles response asynchronously.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="formParams">The form parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task PostAsync(string resource, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>POST</c> request with form data, and handles response asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="formParams">The form parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> containing the response data.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<T> PostAsync<T>(string resource, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>POST</c> request with multipart form data, and handles response asynchronously.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="files">The files to upload.</param>
        /// <param name="formParams">The form parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task PostFilesAsync(string resource, UploadFile[] files, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>POST</c> request with multipart form data, and handles response asynchronously.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="files">The files to upload.</param>
        /// <param name="formParams">The form parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> containing the response data.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<T> PostFilesAsync<T>(string resource, UploadFile[] files, Dictionary<string, string> formParams = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>POST</c> request with a JSON body, and handles response asynchronously.
        /// </summary>
        /// <typeparam name="TReq">The type of the request content.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="content">The JSON body object.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task PostJsonAsync<TReq>(string resource, TReq content, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>POST</c> request with a JSON body, and handles response asynchronously.
        /// </summary>
        /// <typeparam name="TReq">The type of the request content.</typeparam>
        /// <typeparam name="TRes">The type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="content">The JSON body object.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> containing the response data.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<TRes> PostJsonAsync<TReq, TRes>(string resource, TReq content, CancellationToken cancellationToken = default);


        /// <summary>
        /// Sends a <c>PUT</c> request with form data, and handles response asynchronously.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task PutAsync(string resource, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>PUT</c> request with form data, and handles response asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> containing the response data.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<T> PutAsync<T>(string resource, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>PUT</c> request with a JSON body, and handles response asynchronously.
        /// </summary>
        /// <typeparam name="TReq">The type of the request content.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="content">The JSON body object.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task PutJsonAsync<TReq>(string resource, TReq content, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>PUT</c> request with a JSON body, and handles response asynchronously.
        /// </summary>
        /// <typeparam name="TReq">The type of the request content.</typeparam>
        /// <typeparam name="TRes">The type of the result.</typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="content">The JSON body object.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> containing the response data.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<TRes> PutJsonAsync<TReq, TRes>(string resource, TReq content, CancellationToken cancellationToken = default);


        /// <summary>
        /// Sends a <c>DELETE</c> request with query parameters, and handles response asynchronously.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task DeleteAsync(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a <c>DELETE</c> request with query parameters, and handles response asynchronously.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" /> containing the response data.</returns>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<T> DeleteAsync<T>(string resource, Dictionary<string, string> queryParams = null, CancellationToken cancellationToken = default);
        
        
        /// <summary>
        /// Executes the commands asynchronously.
        /// </summary>
        /// <param name="commands">The commands.</param>
        /// <param name="includedResources">The resources to include.</param>
        /// <param name="syncToken">The sync token.</param>
        /// <param name="throwOnError">Indicates whether to throw an exception on error.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <remarks>
        /// <para>
        /// Any <c>ComplexId</c> values in the commands will be resolved to actual IDs after successful execution using the
        /// <see cref="SyncTransactionResponse.TempIdMappings"/> dictionary in the response.
        /// </para>
        /// <para>
        /// When <paramref name="throwOnError"/> is set to <c>true</c> the method will throw a <see cref="TodoistException"/> containing details of the failing command when a single command fails, 
        /// or an <see cref="AggregateException"/> containing the <see cref="TodoistException"/> of each failing command when multiple commands fail. 
        /// </para>
        /// <para>
        /// When <paramref name="throwOnError"/> is set to <c>false</c> the method will not throw, and any errors will be included in the <see cref="SyncTransactionResponse"/> result for each command.
        /// </para>
        /// </remarks>
        /// <returns>
        /// Returns <see cref="Task{TResult}" />. The task object representing the asynchronous operation
        /// that at completion returns the transaction response.
        /// </returns>
        /// <exception cref="ArgumentNullException">Value cannot be null - commands.</exception>
        /// <exception cref="ArgumentException">Value cannot be an empty collection.</exception>
        /// <exception cref="HttpRequestException">API exception.</exception>
        Task<SyncTransactionResponse> SyncCommandsAsync(
            Command[] commands, 
            ResourceType[] includedResources = null,
            string syncToken = null, 
            bool throwOnError = false,
            CancellationToken cancellationToken = default);
    }
}
