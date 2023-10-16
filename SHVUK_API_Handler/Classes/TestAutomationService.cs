using SHVUK_API_Handler.Interfaces;
using System.Collections.Generic;

namespace SHVUK_API_Handler.Classes
    {
     /// <summary>
     /// TestAutomationService service class, inherits from BaseService and implements ITestAutomationService. Acts as a wrapper for the Test Automation API.
     /// </summary>
    public class TestAutomationService : BaseService, ITestAutomationService
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
             { "TestUrl","https://testautomationapi.spellmanhv.local/TestAutomation/VerifyCurrentRoutingFunction?SerialNumber=141341406&function=TEST"}
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

