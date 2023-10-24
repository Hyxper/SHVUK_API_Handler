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
        public SHVUKApiException() : base() { }

        public SHVUKApiException(string message) : base(message) { }

        public SHVUKApiException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
