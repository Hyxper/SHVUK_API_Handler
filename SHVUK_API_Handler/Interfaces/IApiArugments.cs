using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHVUK_API_Handler.Configurations;

namespace SHVUK_API_Handler.Interfaces
{
    /// <summary>
    /// Laying down the foundation for the API arguments.
    /// </summary>
    public interface IApiArugments
    {
        /// <summary>
        /// Returns the value assoicated with a given key that represents an API argument.
        /// </summary>
        /// <param name="key">value of parameter requested</param>
        /// <returns>the assoicated value, or throws an NullReferenceException if key does not exist.</returns>
        string GetValue(ApiParamKeys key);
        /// <summary>
        /// Returns all key value pairs stored in the dictionary, otherwise throws an NullReferenceException if empty.
        /// </summary>
        Dictionary<ApiParamKeys, string> GetAllValues();
    }
}
