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
        //encapsulate urls inside method. MAYBE SHOULD RETURN A SUPER OBJECT, WHERE WE CAN GET PROPS BY ID
        ///<summary>
        /// Verifies the current routing function of a serial number.
        ///</summary>
        ///<remarks>
        /// API USE CASE/QUERY OUTCOMES:
        /// - Query 1: 
        ///     A real serial number (example used 142116779) and a dud function (routing location) returns:
        ///     <c>fromFunction</c> current routing location ("KITTING"), 
        ///     <c>functionMatches</c> false, 
        ///     <c>errorMessage</c> null.
        /// - Query 2: 
        ///     A real serial number (example used 142116779) and a real function (routing location) returns:
        ///     <c>fromFunction</c> current routing location ("KITTING"), 
        ///     <c>functionMatches</c> true, 
        ///     <c>errorMessage</c> null.
        /// - Query 3: 
        ///     A fake serial number and real function routing location returns:
        ///     <c>fromFunction</c> null, 
        ///     <c>functionMatches</c> false, 
        ///     <c>errorMessage</c> "Not a valid serial number".
        /// - Query 4: 
        ///     In a test with a serial number from the SSATS database (141429282) seemingly returned:
        ///     <c>fromFunction</c> null, 
        ///     <c>functionMatches</c> false, 
        ///     <c>errorMessage</c> "Exception from server Sequence contains no matching element".
        ///</remarks>
        /// <param name="serialNumber">Serial number of the unit</param>
        /// <param name="routingFunction">Routing location of the unit</param>
        /// <returns>Returns a list of string demonstrating returned data, otherwise throws x propegated exception on failure.</returns>
        VerifyCurrentRoutingFunction_DTO VerifyCurrentRoutingFunction(string serialNumber, string routingFunction);

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
        Dictionary<string,string> GetWoInfo(string serialNumber, string ccn);

    }
}
