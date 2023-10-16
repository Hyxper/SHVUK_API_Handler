using SHVUK_API_Handler.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HandlerTests
{
    internal class BaseServiceTests
    {
        private class TestableBaseService : BaseService
        {
            public override Dictionary<string, string> Commands => throw new NotImplementedException();
        }

        [Fact] 
        public void CheckEndpoint_WhenCalled_ReturnsTrue()
        {
            // Arrange
            var service = new TestableBaseService();

            // Act
            var result = service.CheckEndpoint("https://www.google.com");

            // Assert
            Assert.True(result);
        }
    }
}
