using Newtonsoft.Json;
using SHVUK_API_Handler.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHVUK_API_Handler.Interfaces;

namespace SHVUK_API_Handler.DTO.TestAutomationService_DTO
{
    /// <summary>
    /// A Data Transfer Object for the SaveTestStatusAndMove method that belongs to the TestAutomation API service.
    /// </summary>
    public class SaveTestStatusAndMove_DTO : IDataTransferObject
    {
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("didSucceed")]
        public bool DidSucceed { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("validationMsg")]
        public string ValidationMsg { get; set; }

        ///<summary>
        /// Method that self validates the DTO.
        ///</summary>
        ///
        public void Validate()
        {
            if (!DidSucceed)
            {
                throw new SHVUKApiException($"The API reported a problem moving the serial number through routing. Field: {FieldName} Message: {ValidationMsg}");
            }
        }
    }
}
