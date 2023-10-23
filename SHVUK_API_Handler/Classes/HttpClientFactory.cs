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
    internal static class HttpClientFactory
    {
        public static HttpClient CreateDefaultClient()
        {
            return new HttpClient();
        }

        public static HttpClient CreateJSONClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }
        public static HttpClient CreateXmlClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            return httpClient;
        }
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
