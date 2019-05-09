using ModibotAPI;
using System;
using System.Reflection;
using System.Threading.Tasks;

/// <summary>
/// Modibot administration namespace
/// </summary>
namespace ModibotAdministration
{
    /// <summary>
    /// Administration module class
    /// </summary>
    public class AdministrationModule : IModule
    {
        /// <summary>
        /// Module name
        /// </summary>
        public string Name => "Administration tools";

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
        /// Initialize module (asynchronous)
        /// </summary>
        /// <param name="bot">Bot</param>
        /// <returns>Task</returns>
        public Task InitAsync(IBot bot)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Exit module (asynchronous)
        /// </summary>
        /// <returns>Task</returns>
        public Task ExitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
