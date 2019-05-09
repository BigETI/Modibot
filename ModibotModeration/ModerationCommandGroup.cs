using ModibotAPI;

/// <summary>
/// Modibot moderation namespace
/// </summary>
namespace ModibotModeration
{
    /// <summary>
    /// Moderation command group class
    /// </summary>
    public class ModerationCommandGroup : ICommandGroup
    {
        /// <summary>
        /// Command group icon
        /// </summary>
        public string Icon => ":cop:";

        /// <summary>
        /// Command group name
        /// </summary>
        public string Name => "Moderation";
    }
}
