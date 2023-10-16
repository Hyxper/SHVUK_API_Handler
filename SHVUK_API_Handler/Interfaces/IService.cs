﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Interfaces
{
    /// <summary>
    /// Interface for any API service in the package.
    /// </summary>
    internal interface IService
    {
        /// <summary>
        /// The method used in API services classes to call the API, and reformat contents as required.
        /// </summary>
        /// <typeparam name="T">Generic type to work with in the method</typeparam>
        /// <param name="apiUrl">formatted api URL to use in the HTTP request</param>
        /// <returns>A list T, represnting processed contents of the API</returns>
       // List<T> InvokeAPI<T>(string apiUrl);

 

        
    }
}