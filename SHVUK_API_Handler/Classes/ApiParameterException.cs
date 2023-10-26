using System;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// An exception designed to explicity handle errors related to API parameters.
    /// </summary>
    public class ApiParameterException : Exception
    {
        /// <summary>
        /// Parameter that caused the issue
        /// </summary>
        public string ProblematicParameter { get; private set; }

        /// <summary>
        /// The problematic Url
        /// </summary>
        public string RawUrl { get; private set; }

        /// <summary>
        ///  An enum indicating the type of issue: Missing or Unreplaced
        /// </summary>
        public ParameterErrorType ErrorType { get; private set; }

        /// <summary>
        /// The timestamp when the exception occurred
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// An enumeration to indicate the type of parameter error
        /// </summary>
        public enum ParameterErrorType
        {
            /// <summary>
            /// Enum describing a missing parameter
            /// </summary>
            Missing,
            /// <summary>
            /// Enum describing an unreplaced parameter
            /// </summary>
            Unreplaced,
            /// <summary>
            /// Enum describing the identification of Rogue or Incomplete brackets
            /// </summary>
            RogueBrackets
        }

        /// <summary>
        /// Constructor with details
        /// </summary>
        /// <param name="problematicParameter">Parameter that caused the issue</param>
        /// <param name="problematicUrl">The problematic Url</param>
        /// <param name="errorType">An enum indicating the type of issue: Missing or Unreplaced</param>
        public ApiParameterException(string problematicParameter, string problematicUrl, ParameterErrorType errorType)
            : base(GenerateMessage(problematicParameter, problematicUrl, errorType))
        {
            ProblematicParameter = problematicParameter;
            RawUrl = problematicUrl;
            ErrorType = errorType;
            Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Default constructor for testing purposes
        /// </summary>
        public ApiParameterException() : base("A parameter-related API error occurred.")
        {
            Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// A utility method to generate the appropriate message based on the error type
        /// </summary>
        private static string GenerateMessage(string parameter, string url, ParameterErrorType type)
        {
            switch (type)
            {
                case ParameterErrorType.Missing:
                    return $"The parameter '{parameter}' was not found in the URL: {url}";
                case ParameterErrorType.Unreplaced:
                    return $"The URL '{url}' contains unreplaced placeholder: '{parameter}'";
                default:
                    return "An unexpected error related to API parameters occurred.";
            }
        }

        /// <summary>
        /// Override ToString() for a detailed exception message
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{Timestamp}] {base.ToString()} - Problem with Parameter: {ProblematicParameter} in URL: {RawUrl}";
        }


    }
}
