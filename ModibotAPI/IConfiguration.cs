/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Configuration interface
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Save configuration
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        bool Save();
    }
}
