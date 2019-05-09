using Discord;
using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Modibot chat namespace
/// </summary>
namespace ModibotChat
{
    /// <summary>
    /// Chat configuration data class
    /// </summary>
    [DataContract]
    public class ChatConfigurationData
    {
        /// <summary>
        /// Guilds
        /// </summary>
        [DataMember]
        private Dictionary<ulong, GuildConfigurationData> guilds;

        /// <summary>
        /// Guilds
        /// </summary>
        public Dictionary<ulong, GuildConfigurationData> Guilds
        {
            get
            {
                if (guilds == null)
                {
                    guilds = new Dictionary<ulong, GuildConfigurationData>();
                }
                return guilds;
            }
        }

        /// <summary>
        /// Get guild configuration
        /// </summary>
        /// <param name="guild">Guild</param>
        /// <returns>Guild configuration</returns>
        public GuildConfigurationData GetGuildConfiguration(IGuild guild)
        {
            GuildConfigurationData ret = null;
            if (guild != null)
            {
                if (Guilds.ContainsKey(guild.Id))
                {
                    ret = Guilds[guild.Id];
                }
                else
                {
                    ret = new GuildConfigurationData();
                    Guilds.Add(guild.Id, ret);
                }
            }
            if (ret == null)
            {
                ret = new GuildConfigurationData();
            }
            return ret;
        }
    }
}
