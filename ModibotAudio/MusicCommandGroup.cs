using ModibotAPI;

/// <summary>
/// Modibot audio namespace
/// </summary>
namespace ModibotAudio
{
    /// <summary>
    /// Audio and music command group class
    /// </summary>
    public class MusicCommandGroup : ICommandGroup
    {
        /// <summary>
        /// Command group icon
        /// </summary>
        public string Icon => ":musical_note:";

        /// <summary>
        /// Command group name
        /// </summary>
        public string Name => "Audio and music";
    }
}
