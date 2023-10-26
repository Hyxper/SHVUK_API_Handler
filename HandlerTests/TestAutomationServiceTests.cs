using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SHVUK_API_Handler.Classes;
using SHVUK_API_Handler.Interfaces;
using SHVUK_API_Handler;
using SHVUK_API_Handler.DTO.TestAutomationService_DTO;
using Moq;
using Newtonsoft.Json;
using System.Net.Http;

namespace HandlerTests
{
    public class TestAutomationServiceTests
    {
        private readonly Mock<IApiHandler> _mockApiHandler;
        private readonly ITestAutomationService _service;

        public TestAutomationServiceTests()
        {
            _mockApiHandler = new Mock<IApiHandler>();
            _service = new TestAutomationService(_mockApiHandler.Object);
        }

        [Fact]
        public void IsServiceOnline_ReturnTrue()
        {
            Assert.True(TestAutomationService.IsServiceOnline);
        }

        [Fact]
        public void VerifyCurrentRoutingFunction_OperatesAsExpectedWithFullDetails()
        {
            //Arrange
            IApiHandler apiHandler = new ApiHandler(new HttpService(HttpClientFactory.CreateJSONClient()));
            ITestAutomationService testAutomationService = new TestAutomationService(apiHandler);
            string serialNumber = "111129818"; //exists in both test and live.
            string function = "TEST EQ";
    
            //Act
            var testObj = testAutomationService.VerifyCurrentRoutingFunction(serialNumber, function); // this may need changing.
            //Asert
            Assert.Equal(function, testObj.FromFunction);
            Assert.True(testObj.FunctionMatches);
            Assert.Null(testObj.ErrorMessage);
        }

        [Fact]
        public void VerifyCurrentRoutingFunction_OperatesAsExpectedWithJustSerialNumber()
        {
            //Arrange
            IApiHandler apiHandler = new ApiHandler(new HttpService(HttpClientFactory.CreateJSONClient()));
            ITestAutomationService testAutomationService = new TestAutomationService(apiHandler);
            string serialNumber = "111129818"; //exists in both test and live.
            

            //Act
            var testObj = testAutomationService.VerifyCurrentRoutingFunction(serialNumber); // this may need changing.
            //Asert
            Assert.False(testObj.FunctionMatches);
            Assert.Null(testObj.ErrorMessage);
            Assert.True(testObj.FromFunction != null);
        }

        [Fact]
        public void VerifyCurrentRoutingFunction_RunTwiceToLearnFunction()
        {
            //Arrange
            IApiHandler apiHandler = new ApiHandler(new HttpService(HttpClientFactory.CreateJSONClient()));
            ITestAutomationService testAutomationService = new TestAutomationService(apiHandler);
            string serialNumber = "111129818"; //exists in both test and live.
            string function = "TEST EQ";
            //Act
            var firstTestObj = testAutomationService.VerifyCurrentRoutingFunction(serialNumber); // this may need changing.
            var secondTestObj = testAutomationService.VerifyCurrentRoutingFunction(serialNumber, firstTestObj.FromFunction);

            Assert.Equal(function, secondTestObj.FromFunction);
            Assert.True(secondTestObj.FunctionMatches);
            Assert.Null(secondTestObj.ErrorMessage);

        }

        //exception throwers

        [Fact]
        public void VerifyCurrentRoutingFunction_ThrowsOnJsonProcessingError()
        {
            _mockApiHandler.Setup(a => a.Get(It.IsAny<string>()))
                           .Throws<JsonException>();

            Assert.Throws<SHVUKApiException>(() => _service.VerifyCurrentRoutingFunction("testSerial", "testFunction"));
        }

        [Fact]
        public void VerifyCurrentRoutingFunction_ThrowsOnApiEndpointError()
        {
            _mockApiHandler.Setup(a => a.Get(It.IsAny<string>()))
                           .Throws<HttpRequestException>();

            Assert.Throws<SHVUKApiException>(() => _service.VerifyCurrentRoutingFunction("testSerial", "testFunction"));
        }

        [Fact]
        public void VerifyCurrentRoutingFunction_ThrowsOnApiParameterError()
        {
            _mockApiHandler.Setup(a => a.Get(It.IsAny<string>()))
                           .Throws<ApiParameterException>();

            Assert.Throws<SHVUKApiException>(() => _service.VerifyCurrentRoutingFunction("testSerial", "testFunction"));
        }

        [Fact]
        public void VerifyCurrentRoutingFunction_ThrowsOnApiInvocationError()
        {
            _mockApiHandler.Setup(a => a.Get(It.IsAny<string>()))
                           .Throws<ApplicationException>();

            Assert.Throws<SHVUKApiException>(() => _service.VerifyCurrentRoutingFunction("testSerial", "testFunction"));
        }

        [Fact]
        public void VerifyCurrentRoutingFunction_ThrowsOnApiResponseValidationError()
        {
            var fakeDto = new VerifyCurrentRoutingFunction_DTO
            {
                ErrorMessage = "Some Error"
                // Set up other properties as needed for validation to fail.
            };

            _mockApiHandler.Setup(a => a.Get(It.IsAny<string>()))
                           .Returns(new Dictionary<string, string> { { "ContentType", "application/json" }, { "Body", JsonConvert.SerializeObject(fakeDto) } });

            Assert.Throws<SHVUKApiException>(() => _service.VerifyCurrentRoutingFunction("testSerial", "testFunction"));
        }

        //End of VerifyCurrentRoutingFunction tests

        [Fact]
        public void GetWOInfo_WorksAsExpected()
        {
            //Arrange
            IApiHandler apiHandler = new ApiHandler(new HttpService(HttpClientFactory.CreateJSONClient()));
            ITestAutomationService testAutomationService = new TestAutomationService(apiHandler);
            string serialNumber = "111129818"; //exists in both test and live.
            string ccn = "26";

            //Act
            var testObj = testAutomationService.GetWOInfo(serialNumber,ccn); // this may need changing.
            //Asert
            Assert.True(testObj.DidSucceed);
            Assert.Equal(serialNumber, testObj.BaseSerialNumber.Trim());
            Assert.Equal(ccn, testObj.Ccn.Trim()); //BARE IN MIND THIS MIGHT BE BOLLOCKS.
            Assert.True(testObj.MasterLocation != null);
            Assert.Null(testObj.ErrorMessage);
        }
    }
}