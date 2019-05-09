using ModibotJSONConfiguration;

/// <summary>
/// Modibot web interface namespace
/// </summary>
namespace ModibotWebInterface
{
    /// <summary>
    /// Web interface configuration class
    /// </summary>
    public class WebInterfaceConfiguration : AJSONConfiguration<WebInterfaceConfigurationData>
    {
        /// <summary>
        /// Configuration file name
        /// </summary>
        public override string ConfigurationFileName => "web_interface.json";
    }
}
