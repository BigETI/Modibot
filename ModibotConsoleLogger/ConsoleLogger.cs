using ModibotAPI;
using System;
using System.IO;
using System.Threading.Tasks;

/// <summary>
/// Modibot console logger namespace
/// </summary>
namespace ModibotConsoleLogger
{
    /// <summary>
    /// Console logger class
    /// </summary>
    [Service]
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Standard output
        /// </summary>
        private TextWriter standardOutput;

        /// <summary>
        /// Error output
        /// </summary>
        private TextWriter errorOutput;

        /// <summary>
        /// Configuration
        /// </summary>
        private IConfiguration configuration;

        /// <summary>
        /// Standard output
        /// </summary>
        public TextWriter StandardOutput
        {
            get
            {
                if (standardOutput == null)
                {
                    standardOutput = Console.Out;
                }
                return standardOutput;
            }
            set
            {
                standardOutput = value;
            }
        }

        /// <summary>
        /// Error output
        /// </summary>
        public TextWriter ErrorOutput
        {
            get
            {
                if (errorOutput == null)
                {
                    errorOutput = Console.Out;
                }
                return errorOutput;
            }
            set
            {
                errorOutput = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        public ConsoleLogger(ModibotAPI.IServiceProvider serviceProvider)
        {
            configuration = serviceProvider.GetService<IConfiguration>();
        }

        /// <summary>
        /// Log event
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Task</returns>
        public void LogEvent(string text)
        {
            LogEvent(text, false);
        }

        /// <summary>
        /// Log event
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="isError">Is error</param>
        /// <returns>Task</returns>
        public void LogEvent(string text, bool isError)
        {
            LogEventAsync(text, isError).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Log event (asynchronous)
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Task</returns>
        public Task LogEventAsync(string text)
        {
            return LogEventAsync(text, false);
        }

        /// <summary>
        /// Log event (asynchronous)
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="isError">Is error</param>
        /// <returns>Task</returns>
        public Task LogEventAsync(string text, bool isError)
        {
            return (configuration.Logger.EnableLogger ? ((text != null) ? ((text.Length > 0) ? (isError ? ErrorOutput : StandardOutput).WriteLineAsync(text) : Task.CompletedTask) : Task.CompletedTask) : Task.CompletedTask);
        }
    }
}
