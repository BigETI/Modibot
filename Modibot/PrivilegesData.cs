using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Privileges data class
    /// </summary>
    [DataContract]
    internal class PrivilegesData
    {
        /// <summary>
        /// Users privileges
        /// </summary>
        [DataMember]
        private Dictionary<ulong, PrivilegesLevelData> users;

        /// <summary>
        /// Guilds privileges
        /// </summary>
        [DataMember]
        private Dictionary<ulong, PrivilegesGuildData> guilds;

        /// <summary>
        /// Users privileges
        /// </summary>
        public Dictionary<ulong, PrivilegesLevelData> Users
        {
            get
            {
                if (users == null)
                {
                    users = new Dictionary<ulong, PrivilegesLevelData>();
                }
                return users;
            }
        }

        /// <summary>
        /// Guilds privileges
        /// </summary>
        public Dictionary<ulong, PrivilegesGuildData> Guilds
        {
            get
            {
                if (guilds == null)
                {
                    guilds = new Dictionary<ulong, PrivilegesGuildData>();
                }
                return guilds;
            }
        }
    }
}
