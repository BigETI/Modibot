using Discord.WebSocket;
using ModibotAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Modibot commands namespace
/// </summary>
namespace ModibotCommands
{
    /// <summary>
    /// Compiled command class
    /// </summary>
    internal class CompiledCommand : ICompiledCommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string CommandName { get; private set; }

        /// <summary>
        /// Compiled command arguments
        /// </summary>
        public ICompiledCommandArguments CompiledCommandArguments { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="rawArguments">Raw arguments</param>
        /// <param name="arguments">Arguments</param>
        /// <param name="bot">Bot</param>
        internal CompiledCommand(ICommand command, string rawArguments, string[] arguments, IBot bot)
        {
            CommandName = ((command == null) ? "" : command.Name);
            CompiledCommandArguments = new CompiledCommandArguments(command, rawArguments, arguments, bot);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="commandName">Command name</param>
        /// <param name="rawArguments">Raw arguments</param>
        /// <param name="arguments">Arguments</param>
        /// <param name="bot">Bot</param>
        internal CompiledCommand(string commandName, string rawArguments, string[] arguments, IBot bot)
        {
            CommandName = commandName;
            CompiledCommandArguments = new CompiledCommandArguments(null, rawArguments, arguments, bot);
        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="author">Author</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns>Command result</returns>
        public ICommandResult Execute(SocketUser author, ISocketMessageChannel messageChannel)
        {
            IPrivileges privileges = CompiledCommandArguments.Bot.GetService<IPrivileges>();
            IDictionary<string, uint> missing_privileges = new Dictionary<string, uint>();
            return new CommandResult((CompiledCommandArguments.Command == null) ? ECommandResult.Failed : (privileges.HasPrivileges(CompiledCommandArguments.Command, author, messageChannel, out missing_privileges) ? CompiledCommandArguments.Command.Execute(new CommandArguments((CompiledCommandArguments)CompiledCommandArguments, author, messageChannel)) : ECommandResult.Denied), this, missing_privileges);
        }

        /// <summary>
        /// Execute command (asynchronous)
        /// </summary>
        /// <param name="author">Author</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns>COmmand result task</returns>
        public Task<ICommandResult> ExecuteAsync(SocketUser author, ISocketMessageChannel messageChannel)
        {
            Task<ICommandResult> ret = new Task<ICommandResult>(() =>
            {
                return Execute(author, messageChannel);
            });
            ret.Start();
            return ret;
        }
    }
}
