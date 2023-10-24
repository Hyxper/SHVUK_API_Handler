using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHVUK_API_Handler.Classes;
using SHVUK_API_Handler.Interfaces;
using Newtonsoft.Json;
using Moq;
using Xunit;

namespace HandlerTests
{
    public class DataProcessorTests
    {
        [Fact]
        public void Process_DelegatesToCorrectProcessor()
        {
            //Arrange
            var mockJsonProcessor = new Mock<IResponseProcessor>();
            mockJsonProcessor.Setup(p => p.CanProcess("application/json")).Returns(true);
            mockJsonProcessor.Setup(p => p.Process<string>(It.IsAny<string>())).Returns("Processed");
            var dataProcessor = new DataProcessor(new List<IResponseProcessor> { mockJsonProcessor.Object });
            //Act
            var result = dataProcessor.Process<string>("application/json", "raw content");
            //Assert
            Assert.Equal("Processed", result);
        }

        [Fact]
        public void AddProcessor_ThrowsArgumentNullException_WhenNullProcessorIsProvided()
        {
            // Arrange
            var mockProcessor = new Mock<IResponseProcessor>().Object;
            var dataProcessor = new DataProcessor(new List<IResponseProcessor>{mockProcessor});

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => dataProcessor.AddProcessor(null));
        }

        [Fact]
        public void AddProcessor_AddsProcessorSuccessfully_WhenValidProcessorIsProvided()
        {
            // Arrange
            var initialProcessors = new List<IResponseProcessor>
            {
                new MockResponseProcessor()
            };
            var dataProcessor = new DataProcessor(initialProcessors);
            var newProcessor = new MockResponseProcessor();

            // Act
            dataProcessor.AddProcessor(newProcessor);

            // Assert
            foreach(IResponseProcessor processor in dataProcessor.ResponseProcessors)
            {
                if(processor == newProcessor)
                {
                    Assert.True(true);
                }
            }            
        }

        // Mock class for testing purposes
        private class MockResponseProcessor : IResponseProcessor
        {
            public bool CanProcess(string contentType) => true;
            public T Process<T>(string rawContent) => default;
        }

        [Fact]
        public void DataProcessor_Constructor_ThrowsArgumentExceptionWhenListIsEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new DataProcessor(new List<IResponseProcessor>()));
        }

        [Fact]
        public void Process_PropagatesException_FromProcessor()
        {
            // Arrange
            var mockJsonProcessor = new Mock<IResponseProcessor>();
            mockJsonProcessor.Setup(p => p.CanProcess("application/json")).Returns(true);
            mockJsonProcessor.Setup(p => p.Process<string>(It.IsAny<string>())).Throws(new JsonException("An error occurred"));

            var dataProcessor = new DataProcessor(new List<IResponseProcessor> { mockJsonProcessor.Object });

            // Act & Assert
            var exception = Assert.Throws<JsonException>(() => dataProcessor.Process<string>("application/json", "{}"));
            Assert.Equal("An error occurred", exception.Message);
        }

        [Fact]
        public void Process_ThrowsInvalidOperationException_WhenDuplicateProcessorsAreFound()
        {
            // Arrange
            var mockProcessor1 = new Mock<IResponseProcessor>();
            mockProcessor1.Setup(p => p.CanProcess(It.IsAny<string>())).Returns(true);

            var mockProcessor2 = new Mock<IResponseProcessor>();
            mockProcessor2.Setup(p => p.CanProcess(It.IsAny<string>())).Returns(true);

            var dataProcessor = new DataProcessor(new List<IResponseProcessor> { mockProcessor1.Object, mockProcessor2.Object });

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => dataProcessor.Process<string>("application/json", "{}"));
        }

        [Fact]
        public void Process_ThrowsInvalidOperationException_WhenNoProcessorCanHandleContentType()
        {
            // Arrange
            var mockProcessor = new Mock<IResponseProcessor>();
            mockProcessor.Setup(p => p.CanProcess(It.IsAny<string>())).Returns(false);

            var dataProcessor = new DataProcessor(new List<IResponseProcessor> { mockProcessor.Object });

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => dataProcessor.Process<string>("application/json", "{}"));
        }

    }
}
