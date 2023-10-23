﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using SHVUK_API_Handler.Interfaces;
using System.Net.Http.Headers;

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
        /// <param name="httpService">Provides the HTTP service for fetching data from APIs.</param>
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
                response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status is an error
                var result = new Dictionary<string, string>
            {
                {"ContentType", response.Content.Headers.ContentType.MediaType},
                {"Body", response.Content.ReadAsStringAsync().Result},
            };

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException($"Error fetching data from {uri}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An unexpected error occurred: {ex.Message}", ex);
            }
        }
    }
}
