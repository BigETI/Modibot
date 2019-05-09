using ModibotJSONConfiguration;

/// <summary>
/// Modibot console logger namespace
/// </summary>
namespace ModibotConsoleLogger
{
    /// <summary>
    /// Console logger configuration class
    /// </summary>
    public class ConsoleLoggerConfiguration : AJSONConfiguration<ConsoleLoggerConfigurationData>
    {
        /// <summary>
        /// Configuration file name
        /// </summary>
        public override string ConfigurationFileName => "console_logger.json";
    }
}
