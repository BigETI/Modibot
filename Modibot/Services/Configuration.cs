using Discord;
using ModibotAPI;
using System;
using System.IO;
using System.Runtime.Serialization.Json;

/// <summary>
/// Modibot services namespace
/// </summary>
namespace Modibot.Services
{
    /// <summary>
    /// Configuration class
    /// </summary>
    [Service]
    internal class Configuration : IConfiguration
    {
        /// <summary>
        /// Default configuration path
        /// </summary>
        public static readonly string DefaultPath = "config.json";

        /// <summary>
        /// Default command delimiter
        /// </summary>
        public static readonly string DefaultCommandDelimiter = "%";

        /// <summary>
        /// Default command failed text
        /// </summary>
        public static readonly string DefaultCommandFailed = "Failed to execute command \"{0}\".";

        /// <summary>
        /// Default command successful text
        /// </summary>
        public static readonly string DefaultCommandSuccessful = "";

        /// <summary>
        /// Default command denied text
        /// </summary>
        public static readonly string DefaultCommandDenied = "You are not allowed to execute \"{0}\".";

        /// <summary>
        /// Configuration data serializer
        /// </summary>
        private static readonly DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ConfigurationData), new DataContractJsonSerializerSettings()
        {
            UseSimpleDictionaryFormat = true
        });

        /// <summary>
        /// Configuration data
        /// </summary>
        private ConfigurationData data;

        /// <summary>
        /// Configuration data
        /// </summary>
        public ConfigurationData Data
        {
            get
            {
                if (data == null)
                {
                    data = Load(DefaultPath);
                    if (data == null)
                    {
                        data = new ConfigurationData();
                        Save(DefaultPath);
                    }
                }
                return data;
            }
        }

        /// <summary>
        /// Bot token
        /// </summary>
        public string BotToken => Data.BotToken;

        /// <summary>
        /// Commands
        /// </summary>
        public ICommandsConfigurationData Commands => Data.Commands;

        /// <summary>
        /// Logger
        /// </summary>
        public ILoggerConfigurationData Logger => Data.Logger;

        /// <summary>
        /// Modules
        /// </summary>
        public IModulesConfigurationData Modules => Data.Modules;

        /// <summary>
        /// Get guild configuration
        /// </summary>
        /// <param name="guild">Guild</param>
        /// <returns>Guild configuration if successful, otherwise "null"</returns>
        public IGuildConfiguration GetGuildConfiguration(IGuild guild)
        {
            GuildConfiguration ret = null;
            if (guild != null)
            {
                if (Data.Guilds.ContainsKey(guild.Id))
                {
                    ret = Data.Guilds[guild.Id];
                }
                else
                {
                    ret = new GuildConfiguration();
                    Data.Guilds.Add(guild.Id, ret);
                }
            }
            return ret;
        }

        /// <summary>
        /// Load configuration
        /// </summary>
        /// <param name="path">Configuration path</param>
        /// <returns>Configuration data if successful, otherwise "null"</returns>
        public static ConfigurationData Load(string path)
        {
            ConfigurationData ret = null;
            if (path != null)
            {
                if (File.Exists(path))
                {
                    try
                    {
                        using (FileStream stream = File.OpenRead(path))
                        {
                            ret = serializer.ReadObject(stream) as ConfigurationData;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Save configuration
        /// </summary>
        /// <param name="path">Configuration path</param>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public bool Save(string path)
        {
            bool ret = false;
            if (path != null)
            {
                try
                {
                    using (FileStream stream = File.Open(path, FileMode.Create))
                    {
                        serializer.WriteObject(stream, Data);
                        ret = true;
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
            return ret;
        }

        /// <summary>
        /// Save configuration
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public bool Save()
        {
            return Save(DefaultPath);
        }
    }
}
