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

namespace HandlerTests
{
    public class TestAutomationServiceTests
    {
        [Fact]
        public void IsServiceOnline_ReturnTrue()
        {
            Assert.True(TestAutomationService.IsServiceOnline);
        }

        //NEED SOME PROPER TESTS. TEST DICTIONARY, AND ONCE WHOLE THING IS FLESHED OUT, TEST EVERY STEP. TEST BY INTERFACE TYPE WHERE APPROPIATE.
        //ALSO NEED TO CHECK EACH API INDIVIDUALLY FOR USE CASE STUFF ETC.... USING TESTS WILL HELP ME UNDERSTAND THE FUNCTION OF EACH API!
        //SHOULD ALSO BE ABLE TO USE PREPROCESSOR DIRECTIVES TO DICTATE WHAT TESTS ARE RUN ETC.....
        //MENTION ABOUT HOW WE CAN MAKE FUNCTION AN MISSED FIELD, THAT WAY WE CAN CHECK WHERE THE SERIAL NUMBER IS.

        [Fact]
        public void VerifyCurrentRoutingFunction_OperatesAsExpected()
        {
            //Arrange
            IApiHandler apiHandler = new ApiHandler(new HttpService(HttpClientFactory.CreateJSONClient()));
            ITestAutomationService testAutomationService = new TestAutomationService(apiHandler);
          
    
            //Act
            var testObj = testAutomationService.VerifyCurrentRoutingFunction("141956994", "REWORK"); // this may need changing.
            //Asert
            Assert.Equal("REWORK", testObj.fromFunction);
            Assert.True(testObj.functionMatches);
            Assert.Null(testObj.errorMessage);
        }


    }
}