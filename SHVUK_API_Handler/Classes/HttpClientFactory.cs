using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler
{
    /// <summary>
    /// Creates Http Clients configured for different media types.
    /// </summary>
    public static class HttpClientFactory
    {
        /// <summary>
        /// Creates a bog standard HttpClient.
        /// </summary>
        /// <returns>an instantiated HttpClient</returns>
        public static HttpClient CreateDefaultClient()
        {
            return new HttpClient();
        }

        /// <summary>
        /// Creates a HttpClient configured for JSON expected Results.
        /// </summary>
        /// <returns>An HttpClient that expects requests headers as "application/json".</returns>
        public static HttpClient CreateJSONClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        /// <summary>
        /// Creates a HttpClient configured for XML expected Results.
        /// </summary>
        /// <returns>An HttpClient that expects requests headers as "application/XML".</returns>
        public static HttpClient CreateXmlClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            return httpClient;
        }
        /// <summary>
        /// Creates a HttpClient configured for a custom client; ie the passed argument should describe the desired header.
        /// </summary>
        /// <returns>An HttpClient that expects requests of passed header. For example, passing "text/plain" will return a HttpClient configured for such.</returns>
        public static HttpClient CreateCustomClient(string mediaType)
        {

            if (string.IsNullOrWhiteSpace(mediaType))
            {
                throw new ArgumentException("Media type cannot be null or empty.", nameof(mediaType));
            }

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            return httpClient;
        }
    }
}
