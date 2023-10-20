using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHVUK_API_Handler.Interfaces;
using SHVUK_API_Handler.Configurations;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// A class that stores the arguments to be used in the API call. Will be used to replace {} parts of template API URL addresses 
    /// with a method inside BaseService class. This object will be instantiated and built within the service classes once a top level method is called.
    /// </summary>
    public class ApiArgs : IApiArugments
    {
        /// <summary>
        /// Dictionary that stores the arguments to be used in the API call internally.
        /// </summary>
        private readonly Dictionary<ApiParamKeys, string> _args;

        /// <summary>
        /// Constructor that takes a list of tuples and converts them to a dictionary.
        /// </summary>
        /// <param name="args">Takes an array of ApiParamKeys enums, and a string value.</param>
        public ApiArgs(params (ApiParamKeys key, string value)[] args)
        {
            _args = args.ToDictionary(t => t.key, t => t.value);
        }

        /// <summary>
        /// Returns all values in the dictionary.
        /// </summary>
        /// <returns>Dicitonary of KvP representing stored arguments</returns>
        /// <exception cref="NullReferenceException">Thrown if dictionary is empty.</exception>

        public Dictionary<ApiParamKeys, string> GetAllValues()
        {
            if(_args.Count > 0)
            {
                return _args;
            }
            else
            {
                throw new NullReferenceException("No arguments have been added to the dictionary in this ApiArgs.");
            }
        }

        /// <summary>
        /// Tries to return a value with an assoicated key parameter (ApiParamKey enum). If it fails, it throws a NullReferenceException.
        /// </summary>
        /// <param name="key">A key of the value you wish to retrive.</param>
        /// <returns>value requested from the passed key.</returns>
        /// <exception cref="NullReferenceException"> Thrown if no match can be found</exception>
        public string GetValue(ApiParamKeys key) => _args.TryGetValue(key, out var value) ? value : throw new NullReferenceException($"'{key}' does not exist in argument list.");

    }
}
