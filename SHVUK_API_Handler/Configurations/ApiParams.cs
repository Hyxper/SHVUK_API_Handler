using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Configurations
{
    /// <summary>
    /// Enum to hold the API parameter keys, to ensure consistency across the application.
    /// </summary>
    public enum ApiParamKeys
    {
        /// <summary>
        /// Enum describing the API parameter key for the Serial Number.
        /// </summary>
        SerialNumber,
        /// <summary>
        /// Enum describing the API parameter key for the Time.
        /// </summary>
        Time,
        /// <summary>
        /// Enum describing the API parameter key for the CCN number (site number).
        /// </summary>
        CCN,
        /// <summary>
        /// Enum describing the API parameter key for an Employee Id Number.
        /// </summary>
        EmployeeId,
        /// <summary>
        /// Enum describing the API parameter key for Image Height.
        /// </summary>
        ImageHeight,
        /// <summary>
        /// Enum describing the API parameter key for Image Width.
        /// </summary>
        ImageWidth,
        /// <summary>
        /// Enum describing the API parameter key for the Routing Function.
        /// </summary>
        Routing_Function,
        /// <summary>
        /// Enum describing the API parameter key for a Test Result.
        /// </summary>
        TestResult
    }
}
