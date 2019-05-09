using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Bot configuration data class
    /// </summary>
    [DataContract]
    public class BotConfigurationData
    {
        /// <summary>
        /// Bot token
        /// </summary>
        [DataMember]
        private string botToken;

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
