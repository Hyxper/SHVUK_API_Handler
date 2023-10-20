using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHVUK_API_Handler.Classes;
using SHVUK_API_Handler.Interfaces;
using Xunit;
using Moq;
using Newtonsoft.Json;

namespace HandlerTests
{
    public class JsonResponseProcessorTests
    {
        private readonly JsonResponseProcessor _processor;

        public JsonResponseProcessorTests()
        {
            _processor = new JsonResponseProcessor();
        }
        private class SampleDTO
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        [Fact]
        public void Process_DeserializesToSampleDTOSuccessfully()
        {
            // Arrange
            var json = "{\"Name\":\"John\", \"Age\":30}";
            var expectedOutput = new SampleDTO { Name = "John", Age = 30 };

            // Act
            var result = _processor.Process<SampleDTO>(json);

            // Assert
            Assert.Equal(expectedOutput.Name, result.Name);
            Assert.Equal(expectedOutput.Age, result.Age);
        }        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Process_ThrowsArgumentNullExceptionForNullOrEmptyRawContent(string rawContent)
        {
            Assert.Throws<ArgumentNullException>(() => _processor.Process<dynamic>(rawContent));
        }

        [Fact]
        public void Process_ThrowsJsonExceptionForMalformedJson()
        {
            var malformedJson = "{\"name\":\"John";
            Assert.Throws<JsonException>(() => _processor.Process<dynamic>(malformedJson));
        }

        [Fact]
        public void Process_ThrowsJsonExceptionForMismatchedJsonToObject()
        {
            var json = "{\"name\":\"John\"}";
            Assert.Throws<JsonException>(() => _processor.Process<int>(json));
        }

        [Theory]
        [InlineData("application/json", true)]
        [InlineData("application/json; charset=utf-8", true)]
        [InlineData("text/html", false)]
        public void CanProcess_ReturnsExpectedResults(string contentType, bool expected)
        {
            var result = _processor.CanProcess(contentType);
            Assert.Equal(expected, result);
        }
    }
}
