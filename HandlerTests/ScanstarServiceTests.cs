using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SHVUK_API_Handler.Classes;

namespace HandlerTests
{
    public class ScanstarServiceTests
    {
        [Fact]
        public void IsServiceOnline_ReturnTrue()
        {
            Assert.True(ScanstarService.IsServiceOnline);
        }
    }

}
