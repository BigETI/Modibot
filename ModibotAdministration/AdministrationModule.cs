using ModibotAPI;
using System.Threading.Tasks;

/// <summary>
/// Modibot administration namespace
/// </summary>
namespace ModibotAdministration
{
    /// <summary>
    /// Administration module
    /// </summary>
    public class AdministrationModule : IModule
    {
        /// <summary>
        /// Module name
        /// </summary>
        public string Name => "Administration tools";

        /// <summary>
        /// Version
        /// </summary>
        public string Version => "1.0.0.0";

        /// <summary>
        /// Author
        /// </summary>
        public string Author => "Ethem Kurt";

        /// <summary>
        /// URI
        /// </summary>
        public string URI => "https://github.com/BigETI/Modibot";

        /// <summary>
        /// Initialize (asynchronous)
        /// </summary>
        /// <returns>Task</returns>
        public Task InitAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Exit (asynchronous)
        /// </summary>
        /// <returns>Task</returns>
        public Task ExitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
