/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Guild configuration interface
    /// </summary>
    public interface IGuildConfiguration
    {
        /// <summary>
        /// Enable TTS
        /// </summary>
        bool EnableTTS { get; }
    }
}
