using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Interfaces
{
    /// <summary>
    /// Interface for any API service in the package.
    /// </summary>
    public interface IService
    {
       
        ///<summary>
        ///gets the base url for all API calls.
        ///</summary>
        string BaseUrl { get; }

        /// <summary>
        /// Returns a dictionary of commands for the service
        /// </summary>
        Dictionary<string, string> Commands { get; }

        void AddProcessor(IResponseProcessor responseProcessor);

        IEnumerable<IResponseProcessor> StoredProcessors();

    }
}
