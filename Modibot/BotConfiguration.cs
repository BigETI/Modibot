using ModibotJSONConfiguration;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Bot configuration class
    /// </summary>
    public class BotConfiguration : AJSONConfiguration<BotConfigurationData>
    {
        /// <summary>
        /// Configuration file name
        /// </summary>
        public override string ConfigurationFileName => "bot.json";
    }
}
