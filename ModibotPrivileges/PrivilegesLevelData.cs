using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Modibot privileges namespace
/// </summary>
namespace ModibotPrivileges
{
    /// <summary>
    /// Privileges level data class
    /// </summary>
    [DataContract]
    public class PrivilegesLevelData
    {
        /// <summary>
        /// Privileges
        /// </summary>
        [DataMember]
        private Dictionary<string, uint> privileges;

        /// <summary>
        /// Privileges
        /// </summary>
        public Dictionary<string, uint> Privileges
        {
            get
            {
                if (privileges == null)
                {
                    privileges = new Dictionary<string, uint>();
                }
                return privileges;
            }
        }
    }
}
