using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Interfaces
{
    /// <summary>
    /// Interface that defines the work an API handler must do.
    /// </summary>
    internal interface IApiHandler
    {
        //ADDITIONAL METHODS TO BE ADDED AS NEEDED, MUST ALSO BE ADDED TO HTTPSERVICE.CS ALONGSIDE NEW TESTS FOR EACH!!

        /// <summary>
        /// Sends a GET request to the specified URL and processes the result.
        /// </summary>
        /// <param name="uri">Address to query</param>
        /// <returns>Dictionary containing the Content-Type, and also raw API content.</returns>
        Dictionary<string,string> Get(string uri);

        //string Post()
    }
}
