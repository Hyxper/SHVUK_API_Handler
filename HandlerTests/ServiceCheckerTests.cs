using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SHVUK_API_Handler.Classes;
using System.Net.Http;

namespace HandlerTests
{
    public class ServiceCheckerTests
    {
        [Fact]
        public void IsServiceOnline_ReturnTrue()
        {
            Assert.True(ServiceChecker.IsOnline("https://www.google.com"));
        }

        [Fact]
        public void TestForHttpRequestException()
        {
            Assert.Throws<HttpRequestException>(() => ServiceChecker.IsOnline("https://google.com/404")); // This URL is just an example
        }

        [Fact]
        public void TestHttpRequestExceptionForSpecificStatusCode()
        {
            try
            {
                ServiceChecker.IsOnline("https://google.com/404"); // Example URL that would return a 404
            }
            catch (HttpRequestException ex)
            {
                // Assuming you throw an exception with a message that contains the status code or some indicative message
                Assert.Contains("404", ex.Message); // This checks if the message of the exception contains "404"
            }
        }

        [Fact]
        public void IsServiceOnline_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ServiceChecker.IsOnline(null));
        }

    }

}
