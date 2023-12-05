﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHVUK_API_Handler.Interfaces
{
    /// <summary>
    /// Inteface for any Data Transfer Object in the package.
    /// </summary>
    public interface IDataTransferObject
    {
        void Validate();
    }
}
