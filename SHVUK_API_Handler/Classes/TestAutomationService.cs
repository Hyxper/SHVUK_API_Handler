using SHVUK_API_Handler.Configurations;
using SHVUK_API_Handler.Interfaces;
using System;
using System.Collections.Generic;

namespace SHVUK_API_Handler.Classes
    {
     /// <summary>
     /// TestAutomationService service class, inherits from BaseService and implements ITestAutomationService. Acts as a wrapper for the Test Automation API.
     /// </summary>
    public class TestAutomationService : BaseService, ITestAutomationService
    {

        /// <summary>
        /// Represents all commands stored internally to the class. Commands are API endpoints, consisting of string kvps.
        /// </summary>
        public Dictionary<string, string> Commands => CommandSet;

        /// <summary>
        /// Represents the base url for the service. Stored internally to the class.
        /// </summary>
        public string BaseUrl => _baseUrl;

#if TEST || DEBUG
        protected static readonly string _baseUrl = $"https://testautomationapitest.spellmanhv.local/TestAutomation/";

        /// <summary>
        /// protected static readonly dictionary of commands for the service. Instantiated automatically to allow use of
        /// IsServiceOnline property.
        /// </summary>
        protected static readonly Dictionary<string, string> CommandSet = new Dictionary<string, string>
        {
             {"TestUrl",_baseUrl+$"VerifyCurrentRoutingFunction?SerialNumber=141341406&function=TEST"},
             {"VerifyCurrentRoutingFunction",_baseUrl+$"VerifyCurrentRoutingFunction?SerialNumber={{{ApiParamKeys.SerialNumber}}}&function={{{ApiParamKeys.Routing_Function}}}"},
             {"SaveTestStatusAndMove",_baseUrl+$"SaveTestStatusAndMove?SerialNumber={{{ApiParamKeys.SerialNumber}}}&TestResult={{{ApiParamKeys.TestResult}}}&badge={{{ApiParamKeys.EmployeeId}&location={ApiParamKeys.CCN}}}"},
             {"GetWoInfo",_baseUrl+$"GetWoInfo?SerialNumber={{{ApiParamKeys.SerialNumber}&location={ApiParamKeys.CCN}}}"}
        };
#else
        protected static readonly string _baseUrl = $"https://testautomationapi.spellmanhv.local/TestAutomation/";

        /// <summary>
        /// protected static readonly dictionary of commands for the service. Instantiated automatically to allow use of
        /// IsServiceOnline property.
        /// </summary>
        protected static readonly Dictionary<string, string> CommandSet = new Dictionary<string, string>
        {
             {"TestUrl",_baseUrl+$"VerifyCurrentRoutingFunction?SerialNumber=141341406&function=TEST"},
             {"VerifyCurrentRoutingFunction",_baseUrl+$"VerifyCurrentRoutingFunction?SerialNumber={{{ApiParamKeys.SerialNumber}}}&function={{{ApiParamKeys.Routing_Function}}"},
             {"SaveTestStatusAndMove",_baseUrl+$"SaveTestStatusAndMove?SerialNumber={{{ApiParamKeys.SerialNumber}}}&TestResult={{{ApiParamKeys.TestResult}}}&badge={{{ApiParamKeys.EmployeeId}&location={ApiParamKeys.CCN}}}"},
             {"GetWoInfo",_baseUrl+$"GetWoInfo?SerialNumber={{{ApiParamKeys.SerialNumber}&location={ApiParamKeys.CCN}}}"}
        };
#endif

        /// <summary>
        /// Checks if the service is online by checking the TestUrl command.
        /// Allows us to not carry out additonal work if the service is offline.
        /// </summary>
        public static bool IsServiceOnline
        {
            get
            {
                return ServiceChecker.IsOnline(CommandSet["TestUrl"]);
            }
        }

        /// <summary>
        /// Processes the response from the API, and returns "NEEDS TO FIGURE OUT WHAT". This is specialised towards 
        /// the Scanstar API, as we need to decode a HTML response to something tangible.
        /// </summary>
   

        public Dictionary<string, string> VerifyCurrentRoutingFunction(string serialNumber, string routingFunction)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> SaveTestStatusAndMove(string serialNumber, bool testResult, string employeeId, string ccn)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetWoInfo(string serialNumber, string ccn)
        {
            throw new NotImplementedException();
        }
    }
}

