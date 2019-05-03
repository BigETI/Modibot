using ModibotAPI;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Miscellaneous command group class
    /// </summary>
    internal class MiscellaneousCommandGroup : ICommandGroup
    {
        /// <summary>
        /// Icon
        /// </summary>
        public string Icon => ":bookmark:";

        /// <summary>
        /// Name
        /// </summary>
        public string Name => "Miscellaneous";
    }
}
