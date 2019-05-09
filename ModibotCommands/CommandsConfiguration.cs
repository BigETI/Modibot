using ModibotJSONConfiguration;

/// <summary>
/// Modibots commands namespace
/// </summary>
namespace ModibotCommands
{
    /// <summary>
    /// Commands configuration class
    /// </summary>
    public class CommandsConfiguration : AJSONConfiguration<CommandsConfigurationData>
    {
        /// <summary>
        /// Configuration file name
        /// </summary>
        public override string ConfigurationFileName => "commands.json";
    }
}
