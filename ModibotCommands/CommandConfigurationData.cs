using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Modibot commands namespace
/// </summary>
namespace ModibotCommands
{
    /// <summary>
    /// Command configuration data
    /// </summary>
    [DataContract]
    public class CommandConfigurationData
    {
        /// <summary>
        /// Enabled
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        private bool enabled = true;

        /// <summary>
        /// Required privileges
        /// </summary>
        [DataMember]
        private Dictionary<string, uint> privileges;

        /// <summary>
        /// Is command enabled
        /// </summary>
        public bool Enabled
        {
            get => enabled;
            set => enabled = value;
        }

        /// <summary>
        /// Required privileges
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
