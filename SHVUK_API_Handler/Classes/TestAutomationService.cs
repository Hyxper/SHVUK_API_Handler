using SHVUK_API_Handler.Configurations;
using SHVUK_API_Handler.DTO.TestAutomationService_DTO;
using SHVUK_API_Handler.Interfaces;
using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

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


        /// <summary>
        /// Constructor for TestAutomationSerivce. Requires an IApiHandler to be passed, and adds a default JSON processor to the data processor.
        /// </summary>
        /// <param name="apiHandler"></param>
        public TestAutomationService(IApiHandler apiHandler) : base(apiHandler)
        {
            _dataProcessor = new DataProcessor(new List<IResponseProcessor> {new JsonResponseProcessor()});
        }


#if TEST || DEBUG
        /// <summary>
        /// Base URL used to describe the API endpoint. Stored internally to the class. Also used to build template queries in CommandSet
        /// </summary>
        protected static readonly string _baseUrl = $"https://testautomationapitest.spellmanhv.local/TestAutomation/";

        /// <summary>
        /// protected static readonly dictionary of commands for the service. Instantiated automatically to allow use of
        /// IsServiceOnline property.
        /// </summary>
        protected static readonly Dictionary<string, string> CommandSet = new Dictionary<string, string>
        {
             {"TestUrl",_baseUrl+$"VerifyCurrentRoutingFunction?SerialNumber=141341406&function=TEST"},
             {"VerifyCurrentRoutingFunction",_baseUrl+$"VerifyCurrentRoutingFunction?SerialNumber={{{ApiParamKeys.SerialNumber}}}&function={{{ApiParamKeys.Routing_Function}}}"},
             {"SaveTestStatusAndMove",_baseUrl+$"SaveTestStatusAndMove?SerialNumber={{{ApiParamKeys.SerialNumber}}}&TestResult={{{ApiParamKeys.TestResult}}}&badge={{{ApiParamKeys.EmployeeId}}}&location={{{ApiParamKeys.CCN}}}"},
             {"GetWOInfo",_baseUrl+$"GetWoInfo?SerialNumber={{{ApiParamKeys.SerialNumber}}}&location={{{ApiParamKeys.CCN}}}"}
        };

#else
        /// <summary>
        /// Base URL used to describe the API endpoint. Stored internally to the class. Also used to build template queries in CommandSet
        /// </summary>
        protected static readonly string _baseUrl = $"https://testautomationapi.spellmanhv.local/TestAutomation/";

        /// <summary>
        /// protected static readonly dictionary of commands for the service. Instantiated automatically to allow use of
        /// IsServiceOnline property.
        /// </summary>
        protected static readonly Dictionary<string, string> CommandSet = new Dictionary<string, string>
        {
             {"TestUrl",_baseUrl+$"VerifyCurrentRoutingFunction?SerialNumber=141341406&function=TEST"},
             {"VerifyCurrentRoutingFunction",_baseUrl+$"VerifyCurrentRoutingFunction?SerialNumber={{{ApiParamKeys.SerialNumber}}}&function={{{ApiParamKeys.Routing_Function}}}"},
             {"SaveTestStatusAndMove",_baseUrl+$"SaveTestStatusAndMove?SerialNumber={{{ApiParamKeys.SerialNumber}}}&TestResult={{{ApiParamKeys.TestResult}}}&badge={{{ApiParamKeys.EmployeeId}}}&location={{{ApiParamKeys.CCN}}}"},
             {"GetWOInfo",_baseUrl+$"GetWoInfo?SerialNumber={{{ApiParamKeys.SerialNumber}}}&location={{{ApiParamKeys.CCN}}}"}
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

        ///<summary>
        /// Verifies the current routing function of a serial number. NOTE: In the event the incorrect routing function is supplied (or is unknown by the calling body), the API will return the correct routing function if
        /// a match is found against the serial number. In this vein, you could call this method twice to first learn what the routing function is, and then call it again with the correct routing function.
        ///</summary>
        ///<remarks>
        /// API USE CASE/QUERY OUTCOMES:
        /// - Query 1: 
        ///     A real serial number (example used 142116779) and a dud function (routing location) returns:
        ///     <c>FromFunction</c> current routing location ("KITTING"), 
        ///     <c>FunctionMatches</c> false, 
        ///     <c>ErrorMessage</c> null.
        /// - Query 2: 
        ///     A real serial number (example used 142116779) and a real function (routing location) returns:
        ///     <c>FromFunction</c> current routing location ("KITTING"), 
        ///     <c>FunctionMatches</c> true, 
        ///     <c>ErrorMessage</c> null.
        /// - Query 3: 
        ///     A fake serial number and real function routing location returns:
        ///     <c>FromFunction</c> null, 
        ///     <c>FunctionMatches</c> false, 
        ///     <c>ErrorMessage</c> "Not a valid serial number".
        /// - Query 4: 
        ///     In a test with a serial number from the SSATS database (141429282) seemingly returned:
        ///     <c>FromFunction</c> null, 
        ///     <c>FunctionMatches</c> false, 
        ///     <c>ErrorMessage</c> "Exception from server Sequence contains no matching element".
        ///</remarks>
        /// <param name="serialNumber">Serial number of the unit</param>
        /// <param name="routingFunction">Routing location of the unit. if routing fucntion is not supplied, default argument "UNKNOWN" used instead.</param>
        /// <returns>Returns a list of string demonstrating returned data. or a SHVUKApiException in the event of an issue (all exceptions are propegated, so internal excepetions are available).</returns>
        /// <exception cref="SHVUKApiException">Will throw any exception regarding operation as this type. Inner exceptions are availble for view if desired or required.</exception>

        public VerifyCurrentRoutingFunction_DTO VerifyCurrentRoutingFunction(string serialNumber, string routingFunction)
        {

            //May need some serial number validation here.... (Or a overall validation method for the service(s))

            try
            {
                //CHECK ALL PROPEGATION WHERE APPROPIATE
                if (IsServiceOnline) //Check service is on before we do any work
                {
                    //build arguments and API string
                    IApiArugments args = new ApiArgs((ApiParamKeys.SerialNumber, serialNumber), (ApiParamKeys.Routing_Function, routingFunction)); //shouldnt needto check for doo doo args, only error handling here is for IP
                    string url = BuildApiUrl(args, Commands["VerifyCurrentRoutingFunction"]);
                    Dictionary<string,string> content =_apiHandler.Get(url);
                    IDataTransferObject result = _dataProcessor.Process<VerifyCurrentRoutingFunction_DTO>(content["ContentType"], content["Body"]);//application exception catches here.
                    result.Validate();//validate the response to see if we need to report an error.
                    return (VerifyCurrentRoutingFunction_DTO)result;

                    //GET THIS TO RETURN A DTO INTERFACE, WE CAN RUN VALIDATE THAT WAY!!!!
                }
                else
                {
                    return null; //wont be reached, either runs code or throws an exception in IsServiceOnline
                }
            }
            catch(InvalidOperationException ex)
            {
                throw new SHVUKApiException($"An error occured when processing data from API: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new SHVUKApiException($"An error occured when processing JSON data retrived from the API: {ex.Message}", ex);
            }
            catch(HttpRequestException ex)
            {
                throw new SHVUKApiException($"An error occured when attempting to reach the API endpoint: {ex.Message}", ex);
            }
            catch(ApiParameterException ex)//thrown by the API args.
            {
                throw new SHVUKApiException($"An error occured when processing arguments for the API call: {ex.Message}", ex);
            }
            catch(ApplicationException ex) //thrown by the API handler.
            {
                throw new SHVUKApiException($"An error occured when invoking and using the API: {ex.Message}", ex);
            }
            catch (APIResponseException ex) //thrown by validate method.
            {
                throw new SHVUKApiException(ex.Message);
            }
            catch (Exception ex)//top level exception handling. Should also handled propegated errors. This is the entrypoint for users using the API! This level should handle all errors. We dont expect.
            {
                throw new SHVUKApiException($"An unexpected error occurred: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="testResult"></param>
        /// <param name="employeeId"></param>
        /// <param name="ccn"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SaveTestStatusAndMove_DTO SaveTestStatusAndMove(string serialNumber, bool testResult, string employeeId, string ccn)
        {
            //May need some serial number validation here.... (Or a overall validation method for the service(s))

            try
            {
                //CHECK ALL PROPEGATION WHERE APPROPIATE
                if (IsServiceOnline) //Check service is on before we do any work
                {
                    //build arguments and API string
                    IApiArugments args = new ApiArgs((ApiParamKeys.SerialNumber, serialNumber), (ApiParamKeys.TestResult, testResult.ToString().ToLower()), (ApiParamKeys.EmployeeId, employeeId), (ApiParamKeys.CCN,ccn)); //shouldnt needto check for doo doo args, only error handling here is for IP
                    string url = BuildApiUrl(args, Commands["SaveTestStatusAndMove"]);
                    Dictionary<string, string> content = _apiHandler.Post(url);
                    IDataTransferObject result = _dataProcessor.Process<SaveTestStatusAndMove_DTO>(content["ContentType"], content["Body"]);//application exception catches here.
                    result.Validate();//validate the response to see if we need to report an error.
                    return (SaveTestStatusAndMove_DTO)result;

                    //GET THIS TO RETURN A DTO INTERFACE, WE CAN RUN VALIDATE THAT WAY!!!!
                }
                else
                {
                    return null; //wont be reached, either runs code or throws an exception in IsServiceOnline
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new SHVUKApiException($"An error occured when processing data from API: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new SHVUKApiException($"An error occured when processing JSON data retrived from the API: {ex.Message}", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new SHVUKApiException($"An error occured when attempting to reach the API endpoint: {ex.Message}", ex);
            }
            catch (ApiParameterException ex)//thrown by the API args.
            {
                throw new SHVUKApiException($"An error occured when processing arguments for the API call: {ex.Message}", ex);
            }
            catch (ApplicationException ex) //thrown by the API handler.
            {
                throw new SHVUKApiException($"An error occured when invoking and using the API: {ex.Message}", ex);
            }
            catch (APIResponseException ex) //thrown by validate method.
            {
                throw new SHVUKApiException(ex.Message);
            }
            catch (Exception ex)//top level exception handling. Should also handled propegated errors. This is the entrypoint for users using the API! This level should handle all errors. We dont expect.
            {
                throw new SHVUKApiException($"An unexpected error occurred: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="ccn"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public GetWOInfo_DTO GetWOInfo(string serialNumber, string ccn)
        {
            //May need some serial number validation here.... (Or a overall validation method for the service(s))

            try
            {
                //CHECK ALL PROPEGATION WHERE APPROPIATE
                if (IsServiceOnline) //Check service is on before we do any work
                {
                    //build arguments and API string
                    IApiArugments args = new ApiArgs((ApiParamKeys.SerialNumber, serialNumber), (ApiParamKeys.CCN, ccn)); //shouldnt needto check for doo doo args, only error handling here is for IP
                    string url = BuildApiUrl(args, Commands["GetWOInfo"]);
                    Dictionary<string, string> content = _apiHandler.Get(url);
                    IDataTransferObject result = _dataProcessor.Process<GetWOInfo_DTO>(content["ContentType"], content["Body"]);//application exception catches here.
                    result.Validate();//validate the response to see if we need to report an error.
                    return (GetWOInfo_DTO)result;

                    //GET THIS TO RETURN A DTO INTERFACE, WE CAN RUN VALIDATE THAT WAY!!!!
                }
                else
                {
                    return null; //wont be reached, either runs code or throws an exception in IsServiceOnline
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new SHVUKApiException($"An error occured when processing data from API: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new SHVUKApiException($"An error occured when processing JSON data retrived from the API: {ex.Message}", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new SHVUKApiException($"An error occured when attempting to reach the API endpoint: {ex.Message}", ex);
            }
            catch (ApiParameterException ex)//thrown by the API args.
            {
                throw new SHVUKApiException($"An error occured when processing arguments for the API call: {ex.Message}", ex);
            }
            catch (ApplicationException ex) //thrown by the API handler.
            {
                throw new SHVUKApiException($"An error occured when invoking and using the API: {ex.Message}", ex);
            }
            catch (APIResponseException ex) //thrown by validate method.
            {
                throw new SHVUKApiException(ex.Message);
            }
            catch (Exception ex)//top level exception handling. Should also handled propegated errors. This is the entrypoint for users using the API! This level should handle all errors. We dont expect.
            {
                throw new SHVUKApiException($"An unexpected error occurred: {ex.Message}", ex);
            }
        }
    }
}

