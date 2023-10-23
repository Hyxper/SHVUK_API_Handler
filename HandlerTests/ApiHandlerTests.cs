using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using SHVUK_API_Handler.Classes;
using System.ComponentModel.Design;
using SHVUK_API_Handler.Interfaces;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HandlerTests
{
    public class ApiHandlerTests
    {
        private readonly Mock<IHttpService> _mockHttpService;
        private readonly ApiHandler _apiHandler;

        public ApiHandlerTests()
        {
            _mockHttpService = new Mock<IHttpService>();
            _apiHandler = new ApiHandler(_mockHttpService.Object);
        }

        [Fact]
        public void Get_ThrowsArgumentException_WhenUriIsNullOrEmpty()
        {
            Assert.Throws<ArgumentException>(() => _apiHandler.Get(string.Empty));
            Assert.Throws<ArgumentException>(() => _apiHandler.Get(null));
        }

        [Fact]
        public void Get_ReturnsExpectedResult_WhenApiCallIsSuccessful()
        {
            // Arrange
            var testUri = "https://testuri.com";
            var testResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Test response")
            };
            testResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _mockHttpService.Setup(x => x.GetAsync(testUri)).ReturnsAsync(testResponse);

            // Act
            var result = _apiHandler.Get(testUri);

            // Assert
            Assert.Equal("application/json", result["ContentType"]);
            Assert.Equal("Test response", result["Body"]);
        }

        [Fact]
        public void Get_ThrowsApplicationException_WhenApiCallReturnsError()
        {
            // Arrange
            var testUri = "https://erroruri.com";
            _mockHttpService.Setup(x => x.GetAsync(testUri)).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound));

            // Act & Assert
            Assert.Throws<ApplicationException>(() => _apiHandler.Get(testUri));
        }

        [Fact]
        public void Get_ThrowsApplicationException_WhenUnexpectedErrorOccurs()
        {
            // Arrange
            var testUri = "https://testuri.com";
            _mockHttpService.Setup(x => x.GetAsync(testUri)).Throws<InvalidOperationException>();

            // Act & Assert
            Assert.Throws<ApplicationException>(() => _apiHandler.Get(testUri));
        }
    }
}

