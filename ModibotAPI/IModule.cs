using System;
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
        Version Version { get; }

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
        /// <param name="bot">Bot</param>
        /// <returns>Task</returns>
        Task InitAsync(IBot bot);

        /// <summary>
        /// Exit module (asynchronous)
        /// </summary>
        /// <returns>Task</returns>
        Task ExitAsync();

        /// <summary>
        /// Module load (asynchronous)
        /// </summary>
        /// <param name="module">Module</param>
        Task ModuleLoadAsync(IModule module);

        /// <summary>
        /// Module unload (asynchronous)
        /// </summary>
        /// <param name="module">Module</param>
        Task ModuleUnloadAsync(IModule module);
    }
}
