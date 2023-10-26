using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHVUK_API_Handler.Classes;
using SHVUK_API_Handler.Interfaces; 

namespace SHVUK_API_Handler.DTO.TestAutomationService_DTO
{
    /// <summary>
    /// A Data Transfer Object for the VerifyCurrentRoutingFunction method that belongs to the TestAutomation API service.
    /// </summary>
    public class VerifyCurrentRoutingFunction_DTO : IDataTransferObject
    {   
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("FromFunction")]
        public string FromFunction { get; set; }

        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("FunctionMatches")]
        public bool FunctionMatches { get; set; }

        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }

        ///<summary>
        /// Method that self validates the DTO.
        ///</summary>
        public void Validate()
        {
            if (ErrorMessage != null && FromFunction == null && !FunctionMatches) // there is an error message, but no from function or function matches.
            {
                throw new SHVUKApiException($"Error from VerifyCurrentRoutingFunction endpoint. Data was returned but this error message is present: {ErrorMessage}");
            }
            else if (ErrorMessage == null && FromFunction == null && !FunctionMatches) // ruck knows what has happened here!
            {
                throw new SHVUKApiException($"API must be working very strangely, all fields returned null...");
            }
        }
    }
}
