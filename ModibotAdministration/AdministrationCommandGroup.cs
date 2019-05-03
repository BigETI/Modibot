using ModibotAPI;

/// <summary>
/// Modibot administration namespace
/// </summary>
namespace ModibotAdministration
{
    /// <summary>
    /// Administration command group class
    /// </summary>
    public class AdministrationCommandGroup : ICommandGroup
    {
        /// <summary>
        /// Command group icon
        /// </summary>
        public string Icon => ":cop:";

        /// <summary>
        /// Command group name
        /// </summary>
        public string Name => "Administration";
    }
}
