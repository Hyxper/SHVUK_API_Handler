using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHVUK_API_Handler.Interfaces;

namespace SHVUK_API_Handler.DTO
{
    /// <summary>
    /// A base class for all DTO objects. Requires a Validate method.
    /// </summary>
    public abstract class BaseDTO : IDataTransferObject
    {
        /// <summary>
        /// Validates the DTOs data once instantiated.
        /// </summary>
        public abstract void Validate();
    }
}
