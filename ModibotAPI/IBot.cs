using System.Threading.Tasks;
/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Bot interface
    /// </summary>
    public interface IBot : IServiceProvider, IModules
    {

        /// <summary>
        /// Load module (asynchronous)
        /// </summary>
        /// <param name="path">Module path</param>
        /// <returns>Module task</returns>
        Task<IModule> LoadModuleAsync(string path);

        /// <summary>
        /// Unload module
        /// </summary>
        /// <param name="module">Module</param>
        /// <returns>Task</returns>
        Task UnloadModuleAsync(IModule module);

        /// <summary>
        /// Exit bot
        /// </summary>
        void Exit();
    }
}
