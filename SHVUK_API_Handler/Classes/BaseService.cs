using SHVUK_API_Handler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// Base class for all services
    /// <sumamry>
    public abstract class BaseService
    {
        /// <summary>
        /// Returns a dictionary of commands for the service
        /// </summary>
        public abstract Dictionary<string, string> Commands { get; }

        /// <summary>
        /// Checks the endpoint using the static method in ServiceChecker
        /// </summary>
        /// <param name="url">url of service you are wanting to reach</param>
        /// <returns>returns True if endpoint is reached and responds otherwise throws HttpRequestException</returns>
        internal virtual bool CheckEndpoint(string url)
        {
            return ServiceChecker.IsOnline(url);
        }

        /// <summary>
        /// Builds the API url for the service, replacing any placeholders with the values in the args object
        /// </summary>
        /// <param name="args">an instantiated object that has translated simple string arguments to a object to build url</param>
        /// <returns>Returns a formatted string the be used in the HTTP request</returns>
        
        internal virtual string BuildApiUrl(ApiArgs args,string url)
        {
            throw new Exception("Not implemented");
        }
    }
}
