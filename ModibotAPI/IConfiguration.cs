using Discord;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Configuration interface
    /// </summary>
    public interface IConfiguration : IConfigurationData
    {
        /// <summary>
        /// Get guild configuration
        /// </summary>
        /// <param name="guild">Guild</param>
        /// <returns>Guild configuration if successful, otherwise "null"</returns>
        IGuildConfiguration GetGuildConfiguration(IGuild guild);

        /// <summary>
        /// Save configuration
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        bool Save();
    }
}
