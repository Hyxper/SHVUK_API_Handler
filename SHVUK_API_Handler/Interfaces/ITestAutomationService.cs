using System.Collections.Generic;
using SHVUK_API_Handler.Classes;
using SHVUK_API_Handler.DTO.TestAutomationService_DTO;

namespace SHVUK_API_Handler.Interfaces
{
    /// <summary>
    /// Interface for the Scanstar API service
    /// </summary>
    public interface ITestAutomationService : IService
    {
        ///<summary>
        /// Verifies the current routing function of a serial number. NOTE: In the event the incorrect routing function is supplied (or is unknown by the calling body), the API will return the correct routing function if
        /// a match is found against the serial number. In this vein, you could call this method twice to first learn what the routing function is, and then call it again with the correct routing function.
        ///</summary>
        /// <param name="serialNumber">Serial number of the unit</param>
        /// <param name="routingFunction">Routing location of the unit. if routing fucntion is not supplied, default argument "UNKNOWN" used instead.</param>
        /// <returns>Returns a list of string demonstrating returned data. or a SHVUKApiException in the event of an issue (all exceptions are propegated, so internal excepetions are available).</returns>
        ///<exception cref="SHVUKApiException">Will throw any exception regarding operation as this type. Inner exceptions are availble for view if desired or required.</exception>

        VerifyCurrentRoutingFunction_DTO VerifyCurrentRoutingFunction(string serialNumber, string routingFunction = "UNKNOWN");

        /// <summary>
        /// Saves the overall status of a test in Glovia, and moves the unit to the next routing location.
        /// ***NEED TO DEBUG THIS FULLY, MAY REQUIRE SOME TEMPLATE GLOVIA SERIAL NUMBERS***
        /// </summary>
        /// <param name="serialNumber">Unit serial number</param>
        /// <param name="testResult">Overall status of the unit (test stage maybe?) need to confirm.</param>
        /// <param name="employeeId">Employee Id Number (example being 0010670, however may need some formatting)</param>
        /// <param name="ccn">CCN site number of UUT. (May require some additonal calls in order to understand current location)</param>
        /// <returns></returns>
        Dictionary<string,string> SaveTestStatusAndMove(string serialNumber, bool testResult, string employeeId, string ccn); // this is a POST request

        /// <summary>
        /// Gets all information about a works order.
        /// </summary>
        /// <param name="serialNumber">Serial number of the unit</param>
        /// <param name="ccn">CCN site number of UUT. (May require some additonal calls in order to understand current location)</param>
        /// <returns></returns>
        /// 

        //need to triM fields when returned
        //real sn and no location - RETURNS INFORMATION
        //fake or dud sn - RETURNS ALL NULLS EXCEPT "Not a Valid Serial Number" ERROR MESSAGE
        //dont think lang and timezone matter too much (optional anyway, and lanugage default to english?)
        //Location is also required, however I dont think thatis essential either...

        GetWOInfo_DTO GetWOInfo(string serialNumber, string ccn = "UNKNOWN");

    }
}
