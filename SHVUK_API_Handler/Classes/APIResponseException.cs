using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler
{
    /// <summary>
    /// A error type to represent a crap response from the API (returned data, but is poo)
    /// </summary>
    public class APIResponseException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public APIResponseException() : base() { }

        /// <summary>
        /// Gets message from the base class.
        /// </summary>
        /// <param name="message">Exception message</param>
        public APIResponseException(string message) : base(message) { }

        /// <summary>
        /// Gets message and inner exception from the base class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">The thrown inner exception</param>
        public APIResponseException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
