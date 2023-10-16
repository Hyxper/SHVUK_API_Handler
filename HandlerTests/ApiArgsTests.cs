using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHVUK_API_Handler.Classes;
using SHVUK_API_Handler.Configurations;
using Xunit;

namespace HandlerTests
{
    public class ApiArgsTests
    {
        [Fact]
        public void Constructor_SetValuesCorrectly()
        {
            string ccn = "26";
            string serialNumber = "123456789";
            string time = "11:49";
            var args = new ApiArgs((ApiParamKeys.CCN, ccn),(ApiParamKeys.SerialNumber,serialNumber),(ApiParamKeys.Time,time));
            Assert.Equal(ccn, args.GetValue(ApiParamKeys.CCN));
            Assert.Equal(serialNumber, args.GetValue(ApiParamKeys.SerialNumber));
            Assert.Equal(time, args.GetValue(ApiParamKeys.Time));
        }

        [Fact]
        public void GetValue_ReturnsCorrectValue()
        {
            var args = new ApiArgs((ApiParamKeys.SerialNumber, "456"));

            Assert.Equal("456", args.GetValue(ApiParamKeys.SerialNumber));
            Assert.Throws<NullReferenceException>(() => args.GetValue(ApiParamKeys.Time)); // Time was not set in this instance
            Assert.Throws<NullReferenceException>(() => args.GetValue(ApiParamKeys.CCN)); // CCN was not set in this instance
        }

        [Fact]
        public void GetValue_ThrowsNullReferenceException()
        {
            var args = new ApiArgs((ApiParamKeys.SerialNumber, "456"));

            Assert.Throws<NullReferenceException>(() => args.GetValue(ApiParamKeys.CCN));
        }

        [Fact]
        public void Constructor_DuplicateKeysThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new ApiArgs((ApiParamKeys.SerialNumber, "456"), (ApiParamKeys.SerialNumber, "456")));
            Assert.Throws<ArgumentException>(() => new ApiArgs((ApiParamKeys.SerialNumber, "456"), (ApiParamKeys.SerialNumber, "789")));
        }
    }
}
