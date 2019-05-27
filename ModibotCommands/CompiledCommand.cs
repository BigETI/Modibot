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
            CommandResult ret = null;
            if (CompiledCommandArguments.Command != null)
            {
                Dictionary<string, uint> missing_privileges = new Dictionary<string, uint>();
                Commands commands = CompiledCommandArguments.Bot.GetService<Commands>();
                if (commands != null)
                {
                    if (commands.CanCommandExecute(CompiledCommandArguments.Command))
                    {
                        IPrivileges[] privileges_services = CompiledCommandArguments.Bot.GetServices<IPrivileges>();
                        if (privileges_services != null)
                        {
                            foreach (IPrivileges privileges in privileges_services)
                            {
                                if (privileges != null)
                                {
                                    IDictionary<string, uint> missing_privileges_subset = new Dictionary<string, uint>();
                                    if (!(privileges.HasPrivileges(commands.GetRequiredCommandPrivileges(CompiledCommandArguments.Command), author, messageChannel, out missing_privileges_subset)))
                                    {
                                        foreach (KeyValuePair<string, uint> missing_privilege in missing_privileges_subset)
                                        {
                                            if (missing_privileges.ContainsKey(missing_privilege.Key))
                                            {
                                                if (missing_privileges[missing_privilege.Key] < missing_privilege.Value)
                                                {
                                                    missing_privileges[missing_privilege.Key] = missing_privilege.Value;
                                                }
                                            }
                                            else
                                            {
                                                missing_privileges.Add(missing_privilege.Key, missing_privilege.Value);
                                            }
                                        }
                                    }
                                    missing_privileges_subset.Clear();
                                }
                            }
                        }
                    }
                    else
                    {
                        ret = new CommandResult(ECommandResult.Disabled, this);
                    }
                }
                if (ret == null)
                {
                    if (missing_privileges.Count > 0)
                    {
                        ret = new CommandResult(ECommandResult.Denied, this, missing_privileges);
                    }
                    else
                    {
                        ret = new CommandResult(CompiledCommandArguments.Command.Execute(new CommandArguments((CompiledCommandArguments)CompiledCommandArguments, author, messageChannel)), this);
                    }
                }
                missing_privileges.Clear();
            }
            if (ret == null)
            {
                ret = new CommandResult(ECommandResult.Failed, this);
            }
            return ret;
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
