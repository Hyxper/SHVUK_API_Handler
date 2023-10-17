using System.Collections.Generic;

namespace SHVUK_API_Handler.Interfaces
{
    /// <summary>
    /// Interface for the Scanstar API service
    /// </summary>
    internal interface IScanstarService : IService
    {
        //Check ESD does not appear to work as expected

        /// <summary>
        /// Returns a list of strings including Employee Number, First Name and Last Name upon successful API call.
        /// </summary>
        /// <param name="employeeId">enter a string of the employee ID (0010670 for example, perhaps need to be several digits long?) for example</param>
        /// <returns>Need to figure this lil bit out</returns>
        Dictionary<string, string> ScanLabour(string employeeId);
        //Check Serial does not appear to work either
        bool CheckMatieralMove(string employeeId, string ccn_location);

    }
}
