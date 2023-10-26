using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// Built in Exception library to control and identify errors with the data returned by the APIs when endpoints are successfully reached.
    /// </summary>
    public class SHVUKApiException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SHVUKApiException() : base() { }

        /// <summary>
        /// Gets message from the base class.
        /// </summary>
        /// <param name="message">Exception message</param>
        public SHVUKApiException(string message) : base(message) { }

        /// <summary>
        /// Gets message and inner exception from the base class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">The thrown inner exception</param>
        public SHVUKApiException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
