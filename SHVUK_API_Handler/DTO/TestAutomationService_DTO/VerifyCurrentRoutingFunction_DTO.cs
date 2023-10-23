using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.DTO.TestAutomationService_DTO
{
    /// <summary>
    /// A Data Transfer Object for the VerifyCurrentRoutingFunction method that belongs to the TestAutomation API service.
    /// </summary>
    public class VerifyCurrentRoutingFunction_DTO
    {   
            [JsonProperty("fromFunction")]
            public object FromFunction { get; set; }

            [JsonProperty("functionMatches")]
            public bool FunctionMatches { get; set; }

            [JsonProperty("errorMessage")]
            public string ErrorMessage { get; set; }    
    }
}
