using ModibotAPI;
using System.IO;
using System.Threading.Tasks;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Null logger class
    /// </summary>
    internal class NullLogger : ILogger
    {
        /// <summary>
        /// Standard output
        /// </summary>
        public TextWriter StandardOutput => TextWriter.Null;

        /// <summary>
        /// Error output
        /// </summary>
        public TextWriter ErrorOutput => TextWriter.Null;

        /// <summary>
        /// Log event
        /// </summary>
        /// <param name="text">Text</param>
        public void LogEvent(string text)
        {
            // ...
        }

        /// <summary>
        /// Log event
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="isError">Is error</param>
        public void LogEvent(string text, bool isError)
        {
            // ...
        }

        /// <summary>
        /// Log event (asynchronous)
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Task</returns>
        public Task LogEventAsync(string text)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Log event (asynchronous)
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="isError">Is error</param>
        /// <returns>Task</returns>
        public Task LogEventAsync(string text, bool isError)
        {
            return Task.CompletedTask;
        }
    }
}
