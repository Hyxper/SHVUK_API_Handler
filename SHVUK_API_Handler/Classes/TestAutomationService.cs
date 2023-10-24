using SHVUK_API_Handler.Configurations;
using SHVUK_API_Handler.DTO.TestAutomationService_DTO;
using SHVUK_API_Handler.Interfaces;
using System;
using System.Net.Http;
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

        public TestAutomationService(IApiHandler apiHandler) : base(apiHandler)
        {
            _dataProcessor = new DataProcessor(new List<IResponseProcessor> {new JsonResponseProcessor()});
        }


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
             {"VerifyCurrentRoutingFunction",_baseUrl+$"VerifyCurrentRoutingFunction?SerialNumber={{{ApiParamKeys.SerialNumber}}}&function={{{ApiParamKeys.Routing_Function}}}"},
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
        public VerifyCurrentRoutingFunction_DTO VerifyCurrentRoutingFunction(string serialNumber, string routingFunction)
        {
            try
            {
                //CHECK ALL PROPEGATION WHERE APPROPIATE
                if (IsServiceOnline) //Check service is on before we do any work
                {
                    //build arguments and API string
                    IApiArugments args = new ApiArgs((ApiParamKeys.SerialNumber, serialNumber), (ApiParamKeys.Routing_Function, routingFunction));
                    string url = BuildApiUrl(args, Commands["VerifyCurrentRoutingFunction"]);
                    //actually query the API
                    Dictionary<string,string> content =_apiHandler.Get(url);
                    VerifyCurrentRoutingFunction_DTO result = _dataProcessor.Process<VerifyCurrentRoutingFunction_DTO>(content["ContentType"], content["Body"]);

                    return result;

                    //GET THIS TO RETURN A DTO INTERFACE, WE CAN RUN VALIDATE THAT WAY!!!!



                }
                else //if not lets throw an exception.
                {
                    throw new HttpRequestException("Service is offline.");
                }
            }
            catch (Exception ex)//top level exception handling. Should also handled propegated errors. This is the entrypoint for users using the API!
            {
                throw new ApplicationException($"An unexpected error occurred: {ex.Message}", ex);
            }
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

