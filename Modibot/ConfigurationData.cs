using ModibotAPI;
using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Configuration data class
    /// </summary>
    [DataContract]
    internal class ConfigurationData : IConfigurationData
    {
        /// <summary>
        /// Bot token
        /// </summary>
        [DataMember]
        private string botToken;

        /// <summary>
        /// Commands
        /// </summary>
        [DataMember]
        private CommandsConfigurationData commands;

        /// <summary>
        /// Logger
        /// </summary>
        [DataMember]
        private LoggerConfigurationData logger;

        /// <summary>
        /// Modules
        /// </summary>
        [DataMember]
        private ModulesConfigurationData modules;

        /// <summary>
        /// Guilds
        /// </summary>
        [DataMember]
        private Dictionary<ulong, GuildConfiguration> guilds;

        /// <summary>
        /// Bot token
        /// </summary>
        public string BotToken
        {
            get
            {
                if (botToken == null)
                {
                    botToken = "";
                }
                return botToken;
            }
        }

        /// <summary>
        /// Commands
        /// </summary>
        public ICommandsConfigurationData Commands
        {
            get
            {
                if (commands == null)
                {
                    commands = new CommandsConfigurationData();
                }
                return commands;
            }
        }

        /// <summary>
        /// Logger
        /// </summary>
        public ILoggerConfigurationData Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = new LoggerConfigurationData();
                }
                return logger;
            }
        }

        /// <summary>
        /// Modules
        /// </summary>
        public IModulesConfigurationData Modules
        {
            get
            {
                if (modules == null)
                {
                    modules = new ModulesConfigurationData();
                }
                return modules;
            }
        }

        /// <summary>
        /// Guilds
        /// </summary>
        public IDictionary<ulong, GuildConfiguration> Guilds
        {
            get
            {
                if (guilds == null)
                {
                    guilds = new Dictionary<ulong, GuildConfiguration>();
                }
                return guilds;
            }
        }
    }
}
