using Newtonsoft.Json;
using SHVUK_API_Handler.Classes;
using SHVUK_API_Handler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.DTO.TestAutomationService_DTO
{
    /// <summary>
    /// DTO for the GetWOInfo method that belongs to the TestAutomation API service.
    /// </summary>
    public class GetWOInfo_DTO : IDataTransferObject
    {
        /// <summary>
        /// Validates the DTOs data once instantiated.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Validate()
        {
            if (ErrorMessage != null)
            {
                throw new APIResponseException($"Error from GetWOInfo endpoint. Data was returned but this error message is present: {ErrorMessage}");
               
            }
            else if (!DidSucceed)
            {
                throw new APIResponseException($"Endpoint GetWOInfo reported an unsuccessful transaction on return.");
            }
        }

        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("didSucceed")]
        public bool DidSucceed { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("ccn")]
        public string Ccn { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("mas_loc")]
        public string MasterLocation { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("wo_num")]
        public string WoNumber { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("wo_line")]
        public string WoLine { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("c_unit")]
        public string CUnit { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("base_serial_num")]
        public string BaseSerialNumber { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("orig_item")]
        public string OriginalItem { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("current_item")]
        public string CurrentItem { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("orig_item_desc")]
        public string OriginalItemDescription { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("current_item_desc")]
        public string CurrentItemDescription { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("wo_desc")]
        public string WoDescription { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("orig_rev")]
        public string OriginalRevision { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("current_rev")]
        public string CurrentRevision { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("glovia_late_rev")]
        public string GloviaLateRevision { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("is_rework")]
        public string IsRework { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("is_serial")]
        public string IsSerial { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("is_dependent_wo")]
        public string IsDependentWo { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("is_service_wo")]
        public bool IsServiceWo { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("prim_base_serial_num")]
        public string PrimBaseSerialNumber { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("vendor_supplied_sn")]
        public string VendorSuppliedSerialNumber { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("halt_code")]
        public string HaltCode { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("halt_description")]
        public string HaltDescription { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("is_lot")]
        public string IsLot { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("isECNPending")]
        public bool IsEcnPending { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("ecnNumber")]
        public string EcnNumber { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("isOpenDeviation")]
        public bool IsOpenDeviation { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("openDeviationNumber")]
        public string OpenDeviationNumber { get; set; }
        /// <summary>
        /// DTO property representing an API field
        /// </summary>
        [JsonProperty("openDeviationApproverName")]
        public string OpenDeviationApproverName { get; set; }

        
    }
}
