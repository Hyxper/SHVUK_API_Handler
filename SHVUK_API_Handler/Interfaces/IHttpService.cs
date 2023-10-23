using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Interfaces
{
    /// <summary>
    /// Represents a service for making HTTP requests.
    /// </summary>
    /// <remarks>
    /// This interface abstracts the HTTP request mechanism, allowing for easier testing and potential 
    /// future changes to the HTTP request method without affecting dependent classes. By implementing
    /// HTTP operations through this interface, we can utilize dependency injection to provide specific
    /// implementations or mock implementations, thus adhering to the Dependency Inversion Principle of SOLID.
    /// </remarks>
    public interface IHttpService
    {
        /// <summary>
        /// Sends a GET request to the specified URI and returns the response.
        /// </summary>
        /// <param name="uri">The URI of the HTTP request.</param>
        /// <returns>The HTTP response message or a propegated exception.</returns>
        Task<HttpResponseMessage> GetAsync(string uri);
    }
}
