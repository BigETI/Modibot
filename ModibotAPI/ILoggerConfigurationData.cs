/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Logger configuration data interface
    /// </summary>
    public interface ILoggerConfigurationData
    {
        /// <summary>
        /// Enable logger
        /// </summary>
        bool EnableLogger { get; }

        /// <summary>
        /// Output prefix
        /// </summary>
        string OutputPrefix { get; }

        /// <summary>
        /// Error prefix
        /// </summary>
        string ErrorPrefix { get; }

        /// <summary>
        /// Logger texts
        /// </summary>
        ILoggerTextsConfigurationData Texts { get; }
    }
}
