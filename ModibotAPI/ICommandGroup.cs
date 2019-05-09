/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Command group interface
    /// </summary>
    public interface ICommandGroup
    {
        /// <summary>
        /// Command group icon (emoji)
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Command group name
        /// </summary>
        string Name { get; }
    }
}
