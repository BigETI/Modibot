using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Privileges level data class
    /// </summary>
    [DataContract]
    internal class PrivilegesLevelData
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
