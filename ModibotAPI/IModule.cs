using System.Threading.Tasks;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Module interface
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Module name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Module version
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Module author
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Module URI
        /// </summary>
        string URI { get; }

        /// <summary>
        /// Initialize module (asynchronous)
        /// </summary>
        /// <returns>Task</returns>
        Task InitAsync();

        /// <summary>
        /// Exit module (asynchronous)
        /// </summary>
        /// <returns>Task</returns>
        Task ExitAsync();
    }
}
