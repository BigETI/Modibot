using ModibotAPI;
using System;
using System.Reflection;
using System.Threading.Tasks;

/// <summary>
/// Modibot database logger
/// </summary>
namespace ModibotDatabaseLogger
{
    /// <summary>
    /// Database logger module
    /// </summary>
    public class DatabaseLoggerModule : IModule
    {
        /// <summary>
        /// Module name
        /// </summary>
        public string Name => "Database logger";

        /// <summary>
        /// Module version
        /// </summary>
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Module author
        /// </summary>
        public string Author => "Ethem Kurt";

        /// <summary>
        /// Module URI
        /// </summary>
        public string URI => "https://github.com/BigETI/Modibot";

        /// <summary>
        /// Initialize (asynchronous)
        /// </summary>
        /// <param name="bot">Bot</param>
        /// <returns>Task</returns>
        public Task InitAsync(IBot bot)
        {
            // TODO
            return Task.CompletedTask;
        }

        /// <summary>
        /// Exit (asynchronous)
        /// </summary>
        /// <returns>Task</returns>
        public Task ExitAsync()
        {
            // TODO
            return Task.CompletedTask;
        }

        /// <summary>
        /// Module load (asynchronous)
        /// </summary>
        /// <param name="module">Module</param>
        /// <returns>Task</returns>
        public Task ModuleLoadAsync(IModule module)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Module unload (asynchronous)
        /// </summary>
        /// <param name="module">Module</param>
        /// <returns>Task</returns>
        public Task ModuleUnloadAsync(IModule module)
        {
            return Task.CompletedTask;
        }
    }
}
