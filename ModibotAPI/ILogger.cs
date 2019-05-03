using System.IO;
using System.Threading.Tasks;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Logger interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Standard output
        /// </summary>
        TextWriter StandardOutput { get; }

        /// <summary>
        /// Error output
        /// </summary>
        TextWriter ErrorOutput { get; }

        /// <summary>
        /// Log event
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Task</returns>
        void LogEvent(string text);

        /// <summary>
        /// Log event
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="isError">Is error</param>
        /// <returns>Task</returns>
        void LogEvent(string text, bool isError);

        /// <summary>
        /// Log event (asynchronous)
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Task</returns>
        Task LogEventAsync(string text);

        /// <summary>
        /// Log event (asynchronous)
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="isError">Is error</param>
        /// <returns>Task</returns>
        Task LogEventAsync(string text, bool isError);
    }
}
