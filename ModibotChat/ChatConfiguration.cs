using ModibotJSONConfiguration;

/// <summary>
/// Modibot chat namespace
/// </summary>
namespace ModibotChat
{
    /// <summary>
    /// Chat configuration class
    /// </summary>
    public class ChatConfiguration : AJSONConfiguration<ChatConfigurationData>
    {
        /// <summary>
        /// Configuration file name
        /// </summary>
        public override string ConfigurationFileName => "chat.json";
    }
}
