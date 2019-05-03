using ModibotAPI;
using System.Collections.ObjectModel;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Compiled command arguments
    /// </summary>
    internal class CompiledCommandArguments : ICompiledCommandArguments
    {
        /// <summary>
        /// Raw arguments
        /// </summary>
        private string rawArguments;

        /// <summary>
        /// Arguments
        /// </summary>
        private string[] arguments;

        /// <summary>
        /// Command
        /// </summary>
        public ICommand Command { get; private set; }

        /// <summary>
        /// Raw arguments
        /// </summary>
        public string RawArguments
        {
            get
            {
                if (rawArguments == null)
                {
                    rawArguments = "";
                }
                return rawArguments;
            }
        }

        /// <summary>
        /// Arguments
        /// </summary>
        public ReadOnlyCollection<string> Arguments
        {
            get
            {
                if (arguments == null)
                {
                    arguments = new string[0];
                }
                return new ReadOnlyCollection<string>(arguments);
            }
        }

        /// <summary>
        /// Bot
        /// </summary>
        public IBot Bot { get; private set; }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="compiledCommandArguments">Compiled command arguments</param>
        public CompiledCommandArguments(CompiledCommandArguments compiledCommandArguments)
        {
            if (compiledCommandArguments != null)
            {
                Command = compiledCommandArguments.Command;
                rawArguments = compiledCommandArguments.rawArguments;
                arguments = compiledCommandArguments.arguments;
                Bot = compiledCommandArguments.Bot;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="rawArguments">Raw arguments</param>
        /// <param name="arguments">Arguments</param>
        /// <param name="bot">Bot</param>
        public CompiledCommandArguments(ICommand command, string rawArguments, string[] arguments, IBot bot)
        {
            Command = command;
            this.rawArguments = rawArguments;
            this.arguments = arguments;
            Bot = bot;
        }
    }
}
