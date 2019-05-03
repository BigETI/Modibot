using Modibot.Services;
using ModibotAPI;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Modules configuration data class
    /// </summary>
    [DataContract]
    internal class ModulesConfigurationData : IModulesConfigurationData
    {
        /// <summary>
        /// Modules path
        /// </summary>
        [DataMember]
        private string modulesPath;

        /// <summary>
        /// Modules path
        /// </summary>
        public string ModulesPath
        {
            get
            {
                if (modulesPath == null)
                {
                    modulesPath = Modules.DefaultPath;
                }
                return modulesPath;
            }
        }
    }
}
