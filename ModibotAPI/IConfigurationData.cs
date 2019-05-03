/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Configuration data interface
    /// </summary>
    public interface IConfigurationData
    {
        /// <summary>
        /// Bot token
        /// </summary>
        string BotToken { get; }

        /// <summary>
        /// Commands
        /// </summary>
        ICommandsConfigurationData Commands { get; }

        /// <summary>
        /// Logger
        /// </summary>
        ILoggerConfigurationData Logger { get; }

        /// <summary>
        /// Modules
        /// </summary>
        IModulesConfigurationData Modules { get; }
    }
}
