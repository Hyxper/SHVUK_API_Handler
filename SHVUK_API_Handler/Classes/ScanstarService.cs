using SHVUK_API_Handler.Configurations;
using SHVUK_API_Handler.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// Scanstar service class, inherits from BaseService and implements IScanstarService. Acts as a wrapper for the Scanstar API.
    /// 
    /// THIS CLASS IS NOT TO BE USED, AS THE SCANSTAR API IS STRICTLY INTERNAL (KEEPING IN CASE THIS CHANGES).
    /// 
    /// </summary>
    internal class ScanstarService : BaseService, IScanstarService
    {
        /// <summary>
        /// Represents all commands stored internally to the class. Commands are API endpoints, consisting of string kvps.
        /// </summary>
        public Dictionary<string, string> Commands => CommandSet;

        /// <summary>
        /// Represents the base url for the service. Stored internally to the class.
        /// </summary>
        public string BaseUrl => _baseUrl;

        protected static readonly string _baseUrl = "https://scanstar.spellmanhv.local/UK/en-GB/GMT-Standard-Time/Desktop/";

        public ScanstarService(IApiHandler apiHandler) : base(apiHandler)
        {
            //THIS IS COMPLETE FILLER, WILL REQUIRE A DIFFERENT RESPONSEPROCESSOR IF IMPLEMENTED!
           //_dataProcessor = new DataProcessor(new List<IResponseProcessor> { new JsonResponseProcessor() });
        }
        
          


        /// <summary>
        /// protected static readonly dictionary of commands for the service. Instantiated automatically to allow use of
        /// IsServiceOnline property.
        /// </summary>
        protected static readonly Dictionary<string, string> CommandSet = new Dictionary<string, string>
        {

             {"TestUrl",_baseUrl+$"GloviaDataAccess/CheckBadgeStrMatMove?id=0010670"},
             {"CheckEsd",_baseUrl+$"GloviaDataAccess/CheckESD?id={{{ApiParamKeys.EmployeeId}}}"},
             {"CheckLabour",_baseUrl+$"GloviaDataAccess/GetEmployeeInfoFromBadge?id={{{ApiParamKeys.EmployeeId}}}" },
             {"CheckMatMove", _baseUrl+$"GloviaDataAccess/CheckBadgeStrValidateMatMov?Badge={{{ApiParamKeys.EmployeeId}}}&location={{{ApiParamKeys.CCN}}}"},
             {"GetSerialWoInfo",_baseUrl+$"MatMovStartPaperless/GetSerialWOInfo?id={{{ApiParamKeys.SerialNumber}}}"},
             {"GetWoQuantityInfo",_baseUrl+$"MatMovStartPaperless/GetWOQtyInfo?id={{{ApiParamKeys.SerialNumber}}}"},
             {"GetUserInfo",_baseUrl+$"GloviaDataAccess/CheckBadgeStrMatMove?id={{{ApiParamKeys.EmployeeId}}}" },
             {"GetUserPicture",_baseUrl+$"Employee/GetUserImage?badgeID={{{ApiParamKeys.EmployeeId}}}&location={{{ApiParamKeys.CCN}}}&width={{{ApiParamKeys.ImageWidth}}}&height={{{ApiParamKeys.ImageHeight}}}"}
        };

   
        /// <summary>
        /// Checks if the service is online by checking the TestUrl command.
        /// Allows us to not carry out additonal work if the service is offline.
        /// </summary>
        public static bool IsServiceOnline
        {

            get
            {
                try
                {
                    return ServiceChecker.IsOnline(CommandSet["TestUrl"]);
                }
                catch(Exception ex)
                {
                    throw new SHVUKApiException($"Unable to check if service is online. {ex.Message}", ex);
                }
                
            }
        }

    }
}
