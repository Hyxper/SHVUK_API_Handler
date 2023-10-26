using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using SHVUK_API_Handler.Interfaces;
using System.Net.Http.Headers;
using System.Net;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// Handles API requests and processes the results.
    /// </summary>
    /// <remarks>
    /// This class is responsible for communicating with external APIs. By depending on the IHttpService interface, 
    /// it decouples itself from the actual HTTP mechanism, ensuring that changes in HTTP request methods or 
    /// testing requirements do not necessitate modifications to this class.
    /// </remarks>
    public class ApiHandler : IApiHandler
    {

        private readonly IHttpService _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiHandler"/> class.
        /// </summary>
        /// <param name="httpClient">Provides the HTTP service for fetching data from APIs.</param>
        /// <exception cref="ArgumentNullException">Thrown if httpService is null.</exception>
        public ApiHandler(IHttpService httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }


        /// <summary>
        /// Sends a GET request to the specified URI and processes the result.
        /// </summary>
        /// <param name="uri">The URI to which the GET request is sent.</param>
        /// <returns>A dictionary containing the content type and body of the response.</returns>
        /// <exception cref="ArgumentException">Thrown if the provided URI is null or empty.</exception>
        /// <exception cref="ApplicationException">Thrown if there is an error fetching data from the URI or if an unexpected error occurs.</exception>
        public Dictionary<string, string> Get(string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentException("URI cannot be null or empty.", nameof(uri));
            }

            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(uri).Result;

                if (response.StatusCode == HttpStatusCode.Unauthorized ||
                    response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new UnauthorizedAccessException("Unauthorized or access denied.");
                }

                if (response.StatusCode == (HttpStatusCode)429)
                {
                    throw new InvalidOperationException("Too many requests. Rate limit exceeded.");
                }

                response.EnsureSuccessStatusCode();
                var result = new Dictionary<string, string>
        {
            {"ContentType", response.Content.Headers.ContentType.MediaType},
            {"Body", response.Content.ReadAsStringAsync().Result},
        };

                return result;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new ApplicationException($"Unable to reach endpoint.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (TaskCanceledException ex)
            {
                throw new ApplicationException($"Request to {uri} timed out.", ex);
            }
            catch (UriFormatException ex)
            {
                throw new ApplicationException($"Invalid URI format: {uri}", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException($"Error fetching data from {uri}: {ex.Message}", ex);
            }
            catch (AggregateException ex)
            {
                throw new ApplicationException($"Multiple errors occurred. First error: {ex.InnerExceptions[0].Message}", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Unexpected error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Sends a POST request to the specified URL and processes the result. NOTE, THIS METHOD OPERATES EXACTLY THE SAME AS THE GET, IT IS JUST A FORMALITY AT THE MOMENT.
        /// </summary>
        /// <param name="uri">Address to query</param>
        /// <param name="content">content to post. Note that is "" by default as SHV APIs do not expect to be sent data.</param>
        /// <returns>Dictionary containing the Content-Type, and also raw API content.</returns>
        public Dictionary<string, string> Post(string uri, string content = "")
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentException("URI cannot be null or empty.", nameof(uri));
            }

            try
            {
                HttpResponseMessage response = _httpClient.PostAsync(uri, content).Result;
                // Similar error handling as your GET method

                if (response.StatusCode == HttpStatusCode.Unauthorized ||
                    response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new UnauthorizedAccessException("Unauthorized or access denied.");
                }

                if (response.StatusCode == (HttpStatusCode)429)
                {
                    throw new InvalidOperationException("Too many requests. Rate limit exceeded.");
                }

                response.EnsureSuccessStatusCode();

                var result = new Dictionary<string, string>
                {
                    {"ContentType", response.Content.Headers.ContentType.MediaType},
                    {"Body", response.Content.ReadAsStringAsync().Result}
                };

                return result;

            }
            catch (UnauthorizedAccessException ex)
            {
                throw new ApplicationException($"Unable to reach endpoint.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (TaskCanceledException ex)
            {
                throw new ApplicationException($"Request to {uri} timed out.", ex);
            }
            catch (UriFormatException ex)
            {
                throw new ApplicationException($"Invalid URI format: {uri}", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException($"Error fetching data from {uri}: {ex.Message}", ex);
            }
            catch (AggregateException ex)
            {
                throw new ApplicationException($"Multiple errors occurred. First error: {ex.InnerExceptions[0].Message}", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Unexpected error: {ex.Message}", ex);
            }
        }
    }
}

