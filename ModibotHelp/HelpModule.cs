using ModibotAPI;
using System.Threading.Tasks;

/// <summary>
/// Modibot help namespace
/// </summary>
namespace ModibotHelp
{
    /// <summary>
    /// Help module
    /// </summary>
    public class HelpModule : IModule
    {
        /// <summary>
        /// Module name
        /// </summary>
        public string Name => "Help topics";

        /// <summary>
        /// Module version
        /// </summary>
        public string Version => "1.0.0.0";

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
