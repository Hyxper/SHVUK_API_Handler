using Moq.Protected;
using Moq;
using SHVUK_API_Handler.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HandlerTests
{
  
        public class HttpServiceTests
        {
            private readonly Mock<HttpMessageHandler> _mockHandler;
            private readonly HttpService _httpService;

            public HttpServiceTests()
            {
                _mockHandler = new Mock<HttpMessageHandler>();
                var httpClient = new HttpClient(_mockHandler.Object);
                _httpService = new HttpService(httpClient);
            }

            [Fact]
            public async Task GetAsync_ReturnsExpectedResult_WhenApiCallIsSuccessful()
            {
                // Arrange
                var testUri = "https://testuri.com";
                _mockHandler.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent("Test response")
                    });

                // Act
                var result = await _httpService.GetAsync(testUri);
                // Assert
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
                Assert.Equal("Test response", await result.Content.ReadAsStringAsync());
            }

            [Fact]
            public void GetAsync_ThrowsException_WhenApiCallFails()
            {
                // Arrange
                var testUri = "https://testuri.com";
                _mockHandler.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ThrowsAsync(new HttpRequestException("Request failed"));

                // Act & Assert
                Assert.ThrowsAsync<HttpRequestException>(() => _httpService.GetAsync(testUri));
            }
    }
}
