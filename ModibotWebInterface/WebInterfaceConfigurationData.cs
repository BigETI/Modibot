using System.Runtime.Serialization;

/// <summary>
/// Modibot web interface namespace
/// </summary>
namespace ModibotWebInterface
{
    /// <summary>
    /// Web interface configuration data class
    /// </summary>
    [DataContract]
    public class WebInterfaceConfigurationData
    {
        /// <summary>
        /// Port
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        private ushort port = 80;

        /// <summary>
        /// Allow HTTPS
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        private bool allowHTTPS = true;

        /// <summary>
        /// Port
        /// </summary>
        public ushort Port
        {
            get => port;
            set => port = value;
        }

        /// <summary>
        /// Allow HTTPS
        /// </summary>
        public bool AllowHTTPS
        {
            get => allowHTTPS;
            set => allowHTTPS = value;
        }
    }
}
