using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHVUK_API_Handler.Classes;

namespace SHVUK_API_Handler.DTO.TestAutomationService_DTO
{
    /// <summary>
    /// A Data Transfer Object for the VerifyCurrentRoutingFunction method that belongs to the TestAutomation API service.
    /// </summary>
    public class VerifyCurrentRoutingFunction_DTO
    {   
        [JsonProperty("fromFunction")]
        public string fromFunction { get; set; }

        [JsonProperty("functionMatches")]
        public bool functionMatches { get; set; }

        [JsonProperty("errorMessage")]
        public string errorMessage { get; set; }

        public void Validate()
        {
            if (errorMessage != null && fromFunction == null && !functionMatches) // there is an error message, but no from function or function matches.
            {
                throw new SHVUKApiException($"Error from API: {errorMessage}");
            }
            else if (errorMessage == null && fromFunction == null && !functionMatches) // fuck knows what has happened here!
            {
                throw new SHVUKApiException($"API must be working very strangely, all fields returned null...");
            }
        }
    }
}
