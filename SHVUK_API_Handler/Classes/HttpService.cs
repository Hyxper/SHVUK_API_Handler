using SHVUK_API_Handler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// Represents a service for making HTTP requests using HttpClient.
    /// </summary>
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance used for making HTTP requests. Requires different HttpClients from the factory to ensure items are prepared for use.</param>
        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Sends a GET request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <returns>The HTTP response message.</returns>
        public Task<HttpResponseMessage> GetAsync(string uri)
        {
            return _httpClient.GetAsync(uri);
        }
    }
}
