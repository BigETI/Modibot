using ModibotJSONConfiguration;

/// <summary>
/// Modibot help namespace
/// </summary>
namespace ModibotHelp
{
    /// <summary>
    /// Help configuration class
    /// </summary>
    public class HelpConfiguration : AJSONConfiguration<HelpConfigurationData>
    {
        /// <summary>
        /// Configuration file name
        /// </summary>
        public override string ConfigurationFileName => "help.json";
    }
}
