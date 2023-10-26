using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Interfaces
{
    /// <summary>
    /// Interface that is to be used on specialsed data processors, unique by their type. e.g JSON, XML, etc.
    /// </summary>
    public interface IResponseProcessor
    {
        /// <summary>
        /// Validates a string to see if it can be processed by the processor.
        /// </summary>
        /// <param name="contentType">content type to process</param>
        /// <returns>True if can, false if not.</returns>
        bool CanProcess(string contentType);

        /// <summary>
        /// Processes the string into a type T (A DTO).
        /// </summary>
        /// <typeparam name="T">DTO object that is being transformed</typeparam>
        /// <param name="content">Api response data to be processed by the data processor</param>
        /// <returns>the processed data of type T (DTO)</returns>
        T Process<T>(string content);
    }
}
