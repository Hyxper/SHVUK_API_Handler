using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHVUK_API_Handler.Configurations;

namespace SHVUK_API_Handler.Interfaces
{
    /// <summary>
    /// Laying down the foundation for the API arguments.
    /// </summary>
    internal interface IApiArugments
    {
        string GetValue(ApiParamKeys key);
        Dictionary<ApiParamKeys, string> GetAllValues();
    }
}
