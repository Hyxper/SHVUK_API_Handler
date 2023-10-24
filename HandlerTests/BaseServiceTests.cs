using SHVUK_API_Handler.Classes;
using SHVUK_API_Handler.Configurations;
using SHVUK_API_Handler.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

//ADD ABILITY TO TEST INERNAL METHODS IN THE ABSTRACT CLASS!
namespace HandlerTests
{
    public class BaseServiceTests
    {
        private class TestableBaseService : BaseService
        {        
            protected static readonly Dictionary<string, string> CommandSet = new Dictionary<string, string>
            {
               {"TestUrl","https://scanstar.spellmanhv.local/UK/en-GB/GMT-Standard-Time/Desktop//GloviaDataAccess/CheckBadgeStrMatMove?id=0010670"},
               {"CheckESD",$"https://scanstar.spellmanhv.local/UK/en-GB/GMT-Standard-Time/Desktop/GloviaDataAccess/CheckESD?id={ApiParamKeys.EmployeeId}"}
            };

            public TestableBaseService(IApiHandler apiHandler) : base(apiHandler)
            {
            }   
        }

        private readonly string _baseUrl = "https://example.com/";

        [Fact]
        public void CheckEndpoint_WhenCalledReturnsTrue()
        {
            // Arrange
            var service = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));

            // Act
            var result = service.CheckEndpoint("https://www.google.com");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckEndpoint_HttpRequestException()
        {
            // Arrange
            var service = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            // Assert
            Assert.Throws<HttpRequestException>(() => service.CheckEndpoint("https://www.google.com/404"));
        } 
        
        [Fact]
        public void CheckEndpoint_HttpRequestExceptionWithErrorCode()
        {
            // Arrange
            var service = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));

            // Act
            try
            {
                service.CheckEndpoint("https://www.google.com/404");
            }catch(HttpRequestException ex)
            {
                // Assert
                Assert.Contains("404",ex.Message);
            }
        }

        [Fact]
        public void CheckEndpoint_ArugmentNullException()
        {
            // Arrange
            var service = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));

            // Assert
            Assert.Throws<ArgumentNullException>(() => service.CheckEndpoint(null));
        }

        [Fact]
        public void BuildApiUrl_ReplacesPlaceholderWithCorrectValue()
        {
            // Arrange
            var apiArgs = new ApiArgs((ApiParamKeys.EmployeeId, "1234"));
            var rawUrl = _baseUrl + $"GloviaDataAccess/CheckESD?id={{{ApiParamKeys.EmployeeId}}}";
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));

            // Act
            var result = baseService.BuildApiUrl(apiArgs, rawUrl);

            // Assert
            Assert.Equal(_baseUrl + "GloviaDataAccess/CheckESD?id=1234", result);
        }

        [Fact]
        public void BuildApiUrl_ThrowsException_WhenPlaceholderIsMissing()
        {
            // Arrange
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            var apiArgs = new ApiArgs((ApiParamKeys.EmployeeId, "1234"));
            var rawUrl = _baseUrl + $"GloviaDataAccess/CheckESD";

            // Act & Assert
            // You might want to throw a custom exception if a placeholder is not found. 
            // For this example, I'm just assuming you'll throw a generic Exception.
            Assert.Throws<ApiParameterException>(() => baseService.BuildApiUrl(apiArgs, rawUrl));
        }

        [Fact]
        public void BuildApiUrl_ThrowsArgumentNullException_WhenRawUrlIsNull()
        {
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            Assert.Throws<ArgumentNullException>(() => baseService.BuildApiUrl(new ApiArgs((ApiParamKeys.EmployeeId, "1234")), null));
        }

        [Fact]
        public void BuildApiUrl_ThrowsArgumentNullException_WhenRawUrlIsEmpty()
        {
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            Assert.Throws<ArgumentNullException>(() => baseService.BuildApiUrl(new ApiArgs((ApiParamKeys.EmployeeId, "1234")), string.Empty));
        }

        [Fact]
        public void BuildApiUrl_ThrowsArgumentNullException_WhenArgsIsNull()
        {
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            Assert.Throws<ArgumentNullException>(() => baseService.BuildApiUrl(null, "SomeRawUrl"));
        }

        [Fact]
        public void BuildApiUrl_ReturnsModifiedUrl_WhenAllPlaceholdersAreReplaced()
        {
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            var rawUrl = "https://example.com/api?id={EmployeeId}";
            var expectedUrl = "https://example.com/api?id=1234";
            var result = baseService.BuildApiUrl(new ApiArgs((ApiParamKeys.EmployeeId, "1234")), rawUrl);
            Assert.Equal(expectedUrl, result);
        }
        [Fact]
        public void BuildApiUrl_ThrowsApiParameterException_WhenUnreplacedPlaceholder()
        {
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            var rawUrl = "https://example.com/api?id={EmployeeId}&location={Location}";
            Assert.Throws<ApiParameterException>(() => baseService.BuildApiUrl(new ApiArgs((ApiParamKeys.EmployeeId, "1234")), rawUrl));
        }

        [Fact]
        public void BuildApiUrl_ThrowsArgumentException_WhenArgsIsEmpty()
        {
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            var rawUrl = "https://example.com/api?id={EmployeeId}";
            Assert.Throws<ArgumentException>(() => baseService.BuildApiUrl(new ApiArgs(), rawUrl));
        }

        [Fact]
        public void BuildApiUrl_ThrowsApiParameterException_WhenMalformedPlaceholderPresent()
        {
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            var rawUrl = "https://example.com/api?id={{EmployeeId}";
            Assert.Throws<ApiParameterException>(() => baseService.BuildApiUrl(new ApiArgs((ApiParamKeys.EmployeeId, "1234")), rawUrl));
        }

        [Fact]
        public void BuildApiUrl_ThrowsApiParameterException_WhenEmptyPlaceholderPresent()
        {
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            var rawUrl = "https://example.com/api?id={}";
            Assert.Throws<ApiParameterException>(() => baseService.BuildApiUrl(new ApiArgs((ApiParamKeys.EmployeeId, "1234")), rawUrl));
        }

        [Fact]
        public void BuildApiUrl_ThrowsApiParameterException_WhenArgsContainsUnusedKey()
        {
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            var rawUrl = "https://example.com/api?id={EmployeeId}";
            Assert.Throws<ApiParameterException>(() => baseService.BuildApiUrl(new ApiArgs((ApiParamKeys.EmployeeId, "1234"), (ApiParamKeys.CCN, "26")), rawUrl));
        }

        [Fact]
        public void BuildApiUrl_ReplacesAllIdenticalPlaceholders()
        {
            var baseService = new TestableBaseService(new ApiHandler(new HttpService(new HttpClient())));
            var rawUrl = "https://example.com/api?id={EmployeeId}&duplicateId={EmployeeId}";
            var expectedUrl = "https://example.com/api?id=1234&duplicateId=1234";
            var result = baseService.BuildApiUrl(new ApiArgs((ApiParamKeys.EmployeeId, "1234")), rawUrl);
            Assert.Equal(expectedUrl, result);
        }

    }
}
