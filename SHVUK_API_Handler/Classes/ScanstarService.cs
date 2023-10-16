using SHVUK_API_Handler.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// Scanstar service class, inherits from BaseService and implements IScanstarService. Acts as a wrapper for the Scanstar API.
    /// </summary>
    public class ScanstarService : BaseService, IScanstarService
    {
        /// <summary>
        /// Represents all commands stored internally to the class. Commands are API endpoints, consisting of string kvps.
        /// </summary>
        public override Dictionary<string, string> Commands => CommandSet;

        /// <summary>
        /// protected static readonly dictionary of commands for the service. Instantiated automatically to allow use of
        /// IsServiceOnline property.
        /// </summary>
        protected static readonly Dictionary<string, string> CommandSet = new Dictionary<string, string>
        {
             { "TestUrl","https://scanstar.spellmanhv.local/UK/en-GB/GMT-Standard-Time/Desktop//GloviaDataAccess/CheckBadgeStrMatMove?id=0010670"}      
        };

        /// <summary>
        /// Checks if the service is online by checking the TestUrl command.
        /// Allows us to not carry out additonal work if the service is offline.
        /// </summary>
        public static bool IsServiceOnline
        {
            get
            {
                return ServiceChecker.IsOnline(CommandSet["TestUrl"]);
            }
        }

        
    }
}
