﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Interfaces
{/// <summary>
/// This interface is used on top level data processor classes to process the data from the API.
/// </summary>
    public interface IDataProcessor
    {
        /// <summary>
        /// Iterates through a list of response processors to find the correct one to process the data.
        /// </summary>
        /// <typeparam name="T">Type that defines DTO of data wanting to be used</typeparam>
        /// <param name="contentType">Type of data we need to decompile.</param>
        /// <param name="rawContent">Raw string of information recieved by API</param>
        /// <returns></returns>
        T Process<T>(string contentType, string rawContent);

        /// <summary>
        /// Adds a response processor to the list of processors. In the event we neec to add more processors or support different decoding formats.
        /// </summary>
        /// <param name="responseProcessor">A new type of responseProcessor</param>
        void AddProcessor(IResponseProcessor responseProcessor);

        /// <summary>
        /// Getable property for the list of response processors.
        /// </summary>
        IEnumerable<IResponseProcessor> ResponseProcessors { get; }
    }
}
