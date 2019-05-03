using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Privileges guild data class
    /// </summary>
    [DataContract]
    internal class PrivilegesGuildData
    {
        /// <summary>
        /// Roles privileges
        /// </summary>
        [DataMember]
        private Dictionary<ulong, PrivilegesLevelData> roles;

        /// <summary>
        /// Users privileges
        /// </summary>
        [DataMember]
        private Dictionary<ulong, PrivilegesLevelData> users;

        /// <summary>
        /// Roles privileges
        /// </summary>
        public Dictionary<ulong, PrivilegesLevelData> Roles
        {
            get
            {
                if (roles == null)
                {
                    roles = new Dictionary<ulong, PrivilegesLevelData>();
                }
                return roles;
            }
        }

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
    }
}
