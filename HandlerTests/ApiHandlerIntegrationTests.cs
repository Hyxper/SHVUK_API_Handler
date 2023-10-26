using SHVUK_API_Handler.Classes;
using SHVUK_API_Handler.Interfaces;
using SHVUK_API_Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Net;

namespace HandlerTests
{
    //should be moved to a seperatea project....
    public class ApiHandlerIntegrationTests
    {
        [Fact]
        public void Get_ReturnsExpectedResult_WhenUsingHttpBin()
        {
            // Arrange
            string testUri = "https://httpbin.org/get";
            HttpClient jsonClient = HttpClientFactory.CreateJSONClient();
            IHttpService httpService = new HttpService(jsonClient);  // Assuming you've created an HttpService class that implements IHttpService.
            ApiHandler apiHandler = new ApiHandler(httpService);

            // Act
            var result = apiHandler.Get(testUri);

            // Assert
            Assert.Equal("application/json", result["ContentType"]);
            Assert.Contains("httpbin.org/get", result["Body"]);
        }

        [Fact]
        public void Get_ReturnsExpectedResult_WhenUsingHttpBinWithXmlClientAndIsJSON()
        {
            // Arrange
            string testUri = "https://httpbin.org/get";
            HttpClient xmlClient = HttpClientFactory.CreateXmlClient();
            IHttpService httpService = new HttpService(xmlClient);  // Assuming you've created an HttpService class that implements IHttpService.
            ApiHandler apiHandler = new ApiHandler(httpService);

            // Act
            var result = apiHandler.Get(testUri);

            // Assert
            // Even though we set the client to expect XML, the API returns JSON, so we expect "application/json" as the content type.
            Assert.Equal("application/json", result["ContentType"]);
            Assert.Contains("httpbin.org/get", result["Body"]);
        }

        [Fact]
        public void Get_ReturnsExpectedResult_WhenUsingHttpBinWithXmlClient()
        {
            //Arrange
            string testUri = "https://httpbin.org/xml";
            HttpClient xmlClient = HttpClientFactory.CreateXmlClient();
            IHttpService httpService = new HttpService(xmlClient);  // Assuming you've created an HttpService class that implements IHttpService.
            ApiHandler apiHandler = new ApiHandler(httpService);

            //Act
            var result = apiHandler.Get(testUri);

            //Assert
            Assert.Equal("application/xml", result["ContentType"]);
            Assert.Contains("WonderWidgets", result["Body"]);
        }


        [Fact]
        public void Get_ReturnsExpectedResult_UsingDefaultClient()
        {
            // Arrange
            string testUri = "https://httpbin.org/get";
            HttpClient defaultClient = HttpClientFactory.CreateDefaultClient();
            IHttpService httpService = new HttpService(defaultClient);  // Assuming you've created an HttpService class that implements IHttpService.
            ApiHandler apiHandler = new ApiHandler(httpService);

            //Act   
            var result = apiHandler.Get(testUri);

            //Assert   
            Assert.Equal("application/json", result["ContentType"]);
            Assert.Contains("httpbin.org/get", result["Body"]);
        }

        [Fact]
        public void FetchPlainTextDataFromHttpBin_ReturnsPlainTextData()
        {
            // Arrange
            var mediaType = "text/plain";
            var testUri = "https://httpbin.org/robots.txt";

            var httpClient = HttpClientFactory.CreateCustomClient(mediaType);
            var httpService = new HttpService(httpClient);
            var apiHandler = new ApiHandler(httpService);

            // Act
            var response = apiHandler.Get(testUri);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.ContainsKey("ContentType"));
            Assert.Equal(mediaType, response["ContentType"]);
            Assert.True(response.ContainsKey("Body"));
            Assert.Contains("Disallow", response["Body"]);
        }

         [Fact]
        public void Post_ReturnsExpectedResult_WhenUsingHttpBin()
        {
            // Arrange
            string testUri = "https://httpbin.org/post";
            string content = "{content}";
            HttpClient jsonClient = HttpClientFactory.CreateJSONClient();
            IHttpService httpService = new HttpService(jsonClient);  // Assuming you've created an HttpService class that implements IHttpService.
            ApiHandler apiHandler = new ApiHandler(httpService);

            // Act
            var result = apiHandler.Post(testUri, content);

            // Assert
            Assert.Equal("application/json", result["ContentType"]);
            Assert.Contains("httpbin.org/post", result["Body"]);
        }

        [Fact]
        public void Post_ReturnsExpectedResult_WhenUsingHttpBinWithNoContent()
        {
            // Arrange
            string testUri = "https://httpbin.org/post";
            HttpClient jsonClient = HttpClientFactory.CreateJSONClient();
            IHttpService httpService = new HttpService(jsonClient);  // Assuming you've created an HttpService class that implements IHttpService.
            ApiHandler apiHandler = new ApiHandler(httpService);

            // Act
            var result = apiHandler.Post(testUri);

            // Assert
            Assert.Equal("application/json", result["ContentType"]);
            Assert.Contains("httpbin.org/post", result["Body"]);
        }
    }
}
