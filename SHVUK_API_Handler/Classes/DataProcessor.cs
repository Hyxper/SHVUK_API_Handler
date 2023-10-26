using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHVUK_API_Handler.Interfaces;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// Delegates processing of response content to specific processors based on content type.
    /// </summary>
    internal class DataProcessor : IDataProcessor
    {
        public IEnumerable<IResponseProcessor> ResponseProcessors => _responseProcessors;
        private readonly IEnumerable<IResponseProcessor> _responseProcessors;

        /// <summary>
        /// Initializes a new instance of the DataProcessor class.
        /// </summary>
        /// <param name="responseProcessors">The set of processors available for processing different content types.</param>
        public DataProcessor(IEnumerable<IResponseProcessor> responseProcessors)
        {
            if(responseProcessors.Count() == 0)
            {
                throw new ArgumentException("No processors provided", nameof(responseProcessors));
            }
            _responseProcessors = responseProcessors;
        }

        public void AddProcessor(IResponseProcessor responseProcessor)
        {
            if(responseProcessor == null)
            {
                throw new ArgumentNullException(nameof(responseProcessor));
            }       
            _responseProcessors.Append(responseProcessor);
        }

     
        /// <summary>
        /// Processes the raw content based on its content type using the appropriate processor.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the content to.</typeparam>
        /// <param name="contentType">The content type of the raw content.</param>
        /// <param name="rawContent">The raw content to process.</param>
        /// <returns>The processed content as an object of type T.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no processor is found for the provided content type.</exception>
        /// <exception cref="Exception">Thrown for unforeseen errors during processing.</exception>
        public T Process<T>(string contentType, string rawContent)
        {
            // Check for duplicate processors based on content type
            var duplicateProcessors = _responseProcessors
                .GroupBy(p => p.CanProcess(contentType))
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateProcessors.Any())
            {
                throw new InvalidOperationException($"Duplicate processors found for content type: {contentType}");
            }

            var responseProcessor = _responseProcessors.FirstOrDefault(p => p.CanProcess(contentType));

            if (responseProcessor == null)
            {
                throw new InvalidOperationException($"No processor available for content type: {contentType}");
            }

            return responseProcessor.Process<T>(rawContent);
        }
    }
}

    

