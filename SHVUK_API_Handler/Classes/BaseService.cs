using SHVUK_API_Handler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// Base class for all services
    /// <sumamry>
    public abstract class BaseService
    {

        /// <summary>
        /// Api Handler for handling all API requests
        /// </summary>
        protected IApiHandler _apiHandler;
        /// <summary>
        /// Data processor for handling all data processing for the service. Will have its own default ResponseProcessors at instantiation, however more can be added as required with AddProcessor().
        /// </summary>
        protected  IDataProcessor _dataProcessor;

        protected BaseService(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }


        /// <summary>
        /// Checks the endpoint using the static method in ServiceChecker
        /// </summary>
        /// <param name="url">url of service you are wanting to reach</param>
        /// <returns>returns True if endpoint is reached and responds otherwise throws HttpRequestException</returns>
        protected internal virtual bool CheckEndpoint(string url)
        {
            return ServiceChecker.IsOnline(url);
        }
  

        /// <summary>
        /// Adds a processor to the data processor stored alongside each service. May be required in the event more than the classes default processor is required.
        /// </summary>
        /// <param name="processor">type of processor you wish to store and perhaps use.</param>

        public void AddProcessor(IResponseProcessor processor)
        {
            _dataProcessor.AddProcessor(processor);
        }

        /// <summary>
        /// returns a list of currently stored processors that the class is using.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IResponseProcessor> StoredProcessors()
        {
            return _dataProcessor.ResponseProcessors;
        }

        /// <summary>
        /// Builds the API url for the service, replacing any placeholders with the values in the args object
        /// </summary>
        /// <param name="args">an instantiated object that has translated simple string arguments to a object to build url</param>
        /// <returns>Returns a formatted string the be used in the HTTP request</returns>

        protected internal virtual string BuildApiUrl(IApiArugments args,string rawUrl)
        {
            if (string.IsNullOrEmpty(rawUrl))
                throw new ArgumentNullException(nameof(rawUrl), "Raw API URL cannot be null or empty.");
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args), "ApiArgs cannot be null.");
            }

            try
            {
                // Loop through each key-value pair in the args
                foreach (var pair in args.GetAllValues())
                {
                    // Check if the URL contains the placeholder
                    string placeholder = $"{{{pair.Key}}}";
                    if (!rawUrl.Contains(placeholder))
                    {
                        throw new ApiParameterException(placeholder, rawUrl, ApiParameterException.ParameterErrorType.Missing);
                    }

                    // Replace placeholders in URL with actual values
                    rawUrl = rawUrl.Replace(placeholder, pair.Value);
                }

                // Check if there are any unreplaced placeholders left, or rogue brackets leftover
                var regex = new Regex("{[^}]+}");
                Match match = regex.Match(rawUrl);
                if (match.Success)
                {
                    throw new ApiParameterException(match.Value,rawUrl,ApiParameterException.ParameterErrorType.Unreplaced);
                }

                // Then, check for rogue brackets
                var rogueBracketRegex = new Regex("{{|}}|^{[^}]+?}|{[^}]+?}$|^[^{]+?}|{|}\"}");
                Match rogueBracketMatch = rogueBracketRegex.Match(rawUrl);
                if (rogueBracketMatch.Success)
                {
                    throw new ApiParameterException(rogueBracketMatch.Value, rawUrl, ApiParameterException.ParameterErrorType.RogueBrackets);
                }

                // Return the modified URL
                return rawUrl;
            }
            catch (NullReferenceException ex)
            {
                throw new ArgumentException("args should not be empty.", nameof(args), ex);
            }
        }
    }
}
