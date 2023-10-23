using SHVUK_API_Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HandlerTests
{
    public class HttpClientFactoryTests
    {
        [Fact]
        public void CreateDefaultClient_ReturnsHttpClient()
        {
            var httpClient = HttpClientFactory.CreateDefaultClient();

            Assert.NotNull(httpClient);
        }

        [Fact]
        public void CreateJSONClient_ReturnsHttpClientWithJsonHeader()
        {
            var httpClient = HttpClientFactory.CreateJSONClient();
            var acceptHeader = httpClient.DefaultRequestHeaders.Accept.First();

            Assert.NotNull(httpClient);
            Assert.Equal("application/json", acceptHeader.MediaType);
        }

        [Fact]
        public void CreateXmlClient_ReturnsHttpClientWithXmlHeader()
        {
            var httpClient = HttpClientFactory.CreateXmlClient();
            var acceptHeader = httpClient.DefaultRequestHeaders.Accept.First();

            Assert.NotNull(httpClient);
            Assert.Equal("application/xml", acceptHeader.MediaType);
        }

        [Theory]
        [InlineData("application/custom", "application/custom")]
        [InlineData("text/plain", "text/plain")]
        public void CreateCustomClient_ReturnsHttpClientWithCustomHeader(string mediaType, string expectedMediaType)
        {
            var httpClient = HttpClientFactory.CreateCustomClient(mediaType);
            var acceptHeader = httpClient.DefaultRequestHeaders.Accept.First();

            Assert.NotNull(httpClient);
            Assert.Equal(expectedMediaType, acceptHeader.MediaType);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateCustomClient_ThrowsExceptionWithInvalidMediaType(string mediaType)
        {
            Assert.Throws<ArgumentException>(() => HttpClientFactory.CreateCustomClient(mediaType));
        }
    }
}
