using SHVUK_API_Handler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// Processes JSON content responses.
    /// </summary>
    internal class JsonResponseProcessor : IResponseProcessor
    {


        /// <summary>
        /// Determines if the given content type is of type "application/json".
        /// </summary>
        /// <param name="contentType">The content type to check.</param>
        /// <returns>True if the content type matches; otherwise, false.</returns>
        public bool CanProcess(string contentType)
        {
            return contentType.Contains("application/json");
        }

        /// <summary>
        /// Deserializes the provided JSON content to an object of type T.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON to.</typeparam>
        /// <param name="rawContent">The raw JSON content.</param>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="JsonException">Thrown when there are issues reading or deserializing the JSON.</exception>
        /// <exception cref="Exception">Thrown for unforeseen errors.</exception>
        public T Process<T>(string rawContent)
        {
            if (string.IsNullOrEmpty(rawContent))
            {
                throw new ArgumentNullException(nameof(rawContent), "Provided content was null or empty.");
            }
            try
            {
                T deserializedContent = JsonConvert.DeserializeObject<T>(rawContent); // using Newtonsoft.Json

              

               
                
                    return deserializedContent;
                
            }
            catch (JsonReaderException ex) // JsonReaderException can occur if the JSON is malformed
            {
                throw new JsonException($"Failed to read JSON content: {ex.Message}", ex);
            }
            catch (JsonSerializationException ex) // Happens if there's an issue with type mapping during deserialization
            {
                throw new JsonException($"Failed to deserialize JSON content: {ex.Message}", ex);
            }
            catch (Exception ex) // Generic exception handling for unforeseen errors
            {
                throw new Exception($"Unexpected error while processing JSON: {ex.Message}", ex);
            }
        }
    }
}
