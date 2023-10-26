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

        [Fact]
        public void Get_ThrowsApplicationException_WhenRequestTimesOut()
        {
            // Arrange
            var testUri = "https://timeouturi.com";
            _mockHttpService.Setup(x => x.GetAsync(testUri)).Throws<TaskCanceledException>();

            // Act & Assert
            var ex = Assert.Throws<ApplicationException>(() => _apiHandler.Get(testUri));
            Assert.Contains("timed out", ex.Message);
        }

        [Fact]
        public void Get_ThrowsApplicationException_WhenUriIsInvalid()
        {
            // Arrange
            var testUri = "invalid uri";
            _mockHttpService.Setup(x => x.GetAsync(testUri)).Throws<UriFormatException>();

            // Act & Assert
            var ex = Assert.Throws<ApplicationException>(() => _apiHandler.Get(testUri));
            Assert.Contains("Invalid URI format", ex.Message);
        }

        [Fact]
        public void Get_ThrowsUnauthorizedAccessException_WhenApiResponseIsUnauthorizedOrForbidden()
        {
            // Arrange
            var testUri = "https://unauthorizeduri.com";
            var unauthorizedResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            _mockHttpService.Setup(x => x.GetAsync(testUri)).ReturnsAsync(unauthorizedResponse);

            // Act & Assert
            Assert.Throws<ApplicationException>(() => _apiHandler.Get(testUri));
        }

        [Fact]
        public void Get_ThrowsInvalidOperationException_WhenRateLimitIsExceeded()
        {
            // Arrange
            var testUri = "https://httpbin.org/status/429";
            var rateLimitedResponse = new HttpResponseMessage((HttpStatusCode)429); // Too Many Requests
            _mockHttpService.Setup(x => x.GetAsync(testUri)).ReturnsAsync(rateLimitedResponse);

            // Act & Assert
            var ex = Assert.Throws<ApplicationException>(() => _apiHandler.Get(testUri));
            Assert.Contains("Rate limit exceeded", ex.Message);
        }

        [Fact]
        public void Post_ThrowsArgumentException_WhenUriIsNullOrEmpty()
        {
            Assert.Throws<ArgumentException>(() => _apiHandler.Post(string.Empty));
            Assert.Throws<ArgumentException>(() => _apiHandler.Post(null));
        }

        [Fact]
        public void Post_ReturnsExpectedResult_WhenApiCallIsSuccessful()
        {
            // Arrange
            var testUri = "https://testuri.com";
            var testContent = "Test content";
            var testResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Test response")
            };
            testResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _mockHttpService.Setup(x => x.PostAsync(testUri, testContent)).ReturnsAsync(testResponse);

            // Act
            var result = _apiHandler.Post(testUri, testContent);

            // Assert
            Assert.Equal("application/json", result["ContentType"]);
            Assert.Equal("Test response", result["Body"]);
        }

        [Fact]
        public void Post_ThrowsApplicationException_WhenApiCallReturnsError()
        {
            // Arrange
            var testUri = "https://erroruri.com";
            var testContent = "Test content";
            _mockHttpService.Setup(x => x.PostAsync(testUri, testContent)).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound));

            // Act & Assert
            Assert.Throws<ApplicationException>(() => _apiHandler.Post(testUri, testContent));
        }

        [Fact]
        public void Post_ThrowsApplicationException_WhenUnexpectedErrorOccurs()
        {
            // Arrange
            var testUri = "https://testuri.com";
            var testContent = "Test content";
            _mockHttpService.Setup(x => x.PostAsync(testUri, testContent)).Throws<InvalidOperationException>();

            // Act & Assert
            Assert.Throws<ApplicationException>(() => _apiHandler.Post(testUri, testContent));
        }

        [Fact]
        public void Post_ThrowsApplicationException_WhenRequestTimesOut()
        {
            // Arrange
            var testUri = "https://timeouturi.com";
            var testContent = "Test content";
            _mockHttpService.Setup(x => x.PostAsync(testUri, testContent)).Throws<TaskCanceledException>();

            // Act & Assert
            var ex = Assert.Throws<ApplicationException>(() => _apiHandler.Post(testUri, testContent));
            Assert.Contains("timed out", ex.Message);
        }

        [Fact]
        public void Post_ThrowsApplicationException_WhenUriIsInvalid()
        {
            // Arrange
            var testUri = "invalid uri";
            var testContent = "Test content";
            _mockHttpService.Setup(x => x.PostAsync(testUri, testContent)).Throws<UriFormatException>();

            // Act & Assert
            var ex = Assert.Throws<ApplicationException>(() => _apiHandler.Post(testUri, testContent));
            Assert.Contains("Invalid URI format", ex.Message);
        }

        [Fact]
        public void Post_ThrowsUnauthorizedAccessException_WhenApiResponseIsUnauthorizedOrForbidden()
        {
            // Arrange
            var testUri = "https://unauthorizeduri.com";
            var testContent = "Test content";
            var unauthorizedResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            _mockHttpService.Setup(x => x.PostAsync(testUri, testContent)).ReturnsAsync(unauthorizedResponse);

            // Act & Assert
            Assert.Throws<ApplicationException>(() => _apiHandler.Post(testUri, testContent));
        }

        [Fact]
        public void Post_ThrowsInvalidOperationException_WhenRateLimitIsExceeded()
        {
            // Arrange
            var testUri = "https://httpbin.org/status/429";
            var testContent = "Test content";
            var rateLimitedResponse = new HttpResponseMessage((HttpStatusCode)429); // Too Many Requests
            _mockHttpService.Setup(x => x.PostAsync(testUri, testContent)).ReturnsAsync(rateLimitedResponse);

            // Act & Assert
            var ex = Assert.Throws<ApplicationException>(() => _apiHandler.Post(testUri, testContent));
            Assert.Contains("Rate limit exceeded", ex.Message);
        }

    }
}

