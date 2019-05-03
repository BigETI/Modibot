using Discord.WebSocket;
using ModibotAPI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

/// <summary>
/// Modibot services namespace
/// </summary>
namespace Modibot.Services
{
    /// <summary>
    /// Commands class
    /// </summary>
    [Service]
    internal class Commands : ICommands
    {
        /// <summary>
        /// Commands
        /// </summary>
        private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        /// <summary>
        /// Command groups
        /// </summary>
        private Dictionary<string, ICommandGroup> commandGroups = new Dictionary<string, ICommandGroup>();

        /// <summary>
        /// Miscellaneous command group
        /// </summary>
        private ICommandGroup miscellaneousCommandGroup = new MiscellaneousCommandGroup();

        /// <summary>
        /// Available commands
        /// </summary>
        public ReadOnlyCollection<ICommand> AvailableCommands => new ReadOnlyCollection<ICommand>(new List<ICommand>(commands.Values));

        /// <summary>
        /// Available command groups
        /// </summary>
        public ReadOnlyCollection<ICommandGroup> AvailableCommandGroups => new ReadOnlyCollection<ICommandGroup>(new List<ICommandGroup>(commandGroups.Values));

        /// <summary>
        /// Add command
        /// </summary>
        /// <param name="command">Command</param>
        public void AddCommand(ICommand command)
        {
            if (command != null)
            {
                string key = command.Name.Trim().ToLower();
                if (commands.ContainsKey(key))
                {
                    commands[key] = command;
                }
                else
                {
                    commands.Add(key, command);
                }
            }
        }

        /// <summary>
        /// Add command group
        /// </summary>
        /// <param name="commandGroup">Command group</param>
        public void AddCommandGroup(ICommandGroup commandGroup)
        {
            if (commandGroup != null)
            {
                string key = commandGroup.Name.Trim().ToLower();
                if (commands.ContainsKey(key))
                {
                    commandGroups[key] = commandGroup;
                }
                else
                {
                    commandGroups.Add(key, commandGroup);
                }
            }
        }

        /// <summary>
        /// Compile cokmmand
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="bot">Bot</param>
        /// <returns>Compiled command</returns>
        public ICompiledCommand Compile(string command, IBot bot)
        {
            CompiledCommand ret = null;
            if (command != null)
            {
                if (command.Length > 0)
                {
                    List<string> arguments = new List<string>(command.Split(' '));
                    if (arguments.Count > 0)
                    {
                        string cmd = arguments[0].ToLower();
                        arguments.RemoveAt(0);
                        if (commands.ContainsKey(cmd))
                        {
                            ret = new CompiledCommand(commands[cmd], command.Substring(cmd.Length).TrimStart(), arguments.ToArray(), bot);
                        }
                        else
                        {
                            ret = new CompiledCommand(cmd, command.Substring(cmd.Length).TrimStart(), arguments.ToArray(), bot);
                        }
                    }
                    arguments.Clear();
                }
            }
            if (ret == null)
            {
                ret = new CompiledCommand("", "", new string[0], bot);
            }
            return ret;
        }

        /// <summary>
        /// Compile command (asynchronous)
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="bot">Bot</param>
        /// <returns>Compiled command task</returns>
        public Task<ICompiledCommand> CompileAsync(string command, IBot bot)
        {
            Task<ICompiledCommand> ret = new Task<ICompiledCommand>(() =>
            {
                return Compile(command, bot);
            });
            ret.Start();
            return ret;
        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="author">Author</param>
        /// <param name="messageChannel">Message channel</param>
        /// <param name="bot">Bot</param>
        /// <returns>Command result</returns>
        public ICommandResult Execute(string command, SocketUser author, ISocketMessageChannel messageChannel, IBot bot)
        {
            ICompiledCommand compiled_command = Compile(command, bot);
            return ((compiled_command == null) ? (new CommandResult(ECommandResult.Failed, compiled_command)) : compiled_command.Execute(author, messageChannel));
        }

        /// <summary>
        /// Execute command (asynchronous)
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="author">Author</param>
        /// <param name="messageChannel">Message channel</param>
        /// <param name="bot">Bot</param>
        /// <returns>Command result task</returns>
        public Task<ICommandResult> ExecuteAsync(string command, SocketUser author, ISocketMessageChannel messageChannel, IBot bot)
        {
            Task<ICommandResult> ret = new Task<ICommandResult>(() =>
            {
                return Execute(command, author, messageChannel, bot);
            });
            ret.Start();
            return ret;
        }

        /// <summary>
        /// Find command
        /// </summary>
        /// <param name="commandName">Command name</param>
        /// <returns>Command</returns>
        public ICommand FindCommand(string commandName)
        {
            ICommand ret = null;
            if (commandName != null)
            {
                string key = commandName.Trim().ToLower();
                if (commands.ContainsKey(key))
                {
                    ret = commands[key];
                }
            }
            return ret;
        }

        /// <summary>
        /// Get command group
        /// </summary>
        /// <param name="commandGroupName">Command group name</param>
        /// <returns>Command group</returns>
        public ICommandGroup GetCommandGroup(string commandGroupName)
        {
            ICommandGroup ret = miscellaneousCommandGroup;
            if (commandGroupName != null)
            {
                string key = commandGroupName.Trim().ToLower();
                if (commandGroups.ContainsKey(key))
                {
                    ret = commandGroups[key];
                }
            }
            return ret;
        }

        /// <summary>
        /// Get commands from command group
        /// </summary>
        /// <param name="commandGroup">Command group</param>
        /// <returns>Commands from command group</returns>
        public ICommand[] FromCommandGroup(ICommandGroup commandGroup)
        {
            List<ICommand> result = new List<ICommand>();
            ICommandGroup command_group = ((commandGroup == null) ? miscellaneousCommandGroup : commandGroup);
            if (commandGroup != null)
            {
                foreach (ICommand command in commands.Values)
                {
                    if (GetCommandGroup(command.CommandGroup) == command_group)
                    {
                        result.Add(command);
                    }
                }
            }
            ICommand[] ret = result.ToArray();
            result.Clear();
            return ret;
        }
    }
}
