using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SHVUK_API_Handler.Classes;

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

    }
}