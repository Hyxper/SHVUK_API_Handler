using System;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// An exception designed to explicity handle errors related to API parameters.
    /// </summary>
    internal class ApiParameterException : Exception
    {
        // The parameter that caused the issue (missing or unreplaced)
        public string ProblematicParameter { get; private set; }

        // The URL that is problematic
        public string RawUrl { get; private set; }

        // An enum indicating the type of issue: Missing or Unreplaced
        public ParameterErrorType ErrorType { get; private set; }

        // The timestamp when the exception occurred
        public DateTime Timestamp { get; private set; }

        // An enumeration to indicate the type of parameter error
        public enum ParameterErrorType
        {
            Missing,
            Unreplaced,
            RogueBrackets
        }

        // Constructor with details
        public ApiParameterException(string problematicParameter, string problematicUrl, ParameterErrorType errorType)
            : base(GenerateMessage(problematicParameter, problematicUrl, errorType))
        {
            ProblematicParameter = problematicParameter;
            RawUrl = problematicUrl;
            ErrorType = errorType;
            Timestamp = DateTime.UtcNow;
        }

        // A utility method to generate the appropriate message based on the error type
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

        // Override ToString() for a detailed exception message
        public override string ToString()
        {
            return $"[{Timestamp}] {base.ToString()} - Problem with Parameter: {ProblematicParameter} in URL: {RawUrl}";
        }
    }
}
