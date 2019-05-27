using Discord.WebSocket;
using ModibotAPI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

/// <summary>
/// Modibot commands namespace
/// </summary>
namespace ModibotCommands
{
    /// <summary>
    /// Commands class
    /// </summary>
    [Service]
    public class Commands : ICommands
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
        /// Commands configuration
        /// </summary>
        public CommandsConfiguration CommandsConfiguration { get; internal set; }

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
        /// Clear commands
        /// </summary>
        public void Clear()
        {
            if (commands != null)
            {
                commands.Clear();
            }
            if (commandGroups != null)
            {
                commandGroups.Clear();
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

        /// <summary>
        /// Can command execute
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>"true" if command can be executed, otherwise "false"</returns>
        public bool CanCommandExecute(ICommand command)
        {
            bool ret = false;
            if ((command != null) && (CommandsConfiguration != null))
            {
                string key = command.Name.Trim().ToLower();
                CommandConfigurationData command_configuration_data = null;
                if (CommandsConfiguration.Data.Commands.ContainsKey(key))
                {
                    command_configuration_data = CommandsConfiguration.Data.Commands[key];
                    if (command_configuration_data == null)
                    {
                        command_configuration_data = new CommandConfigurationData();
                        CommandsConfiguration.Data.Commands[key] = command_configuration_data;
                    }
                }
                else
                {
                    command_configuration_data = new CommandConfigurationData();
                    CommandsConfiguration.Data.Commands.Add(key, command_configuration_data);
                }
                ret = command_configuration_data.Enabled;
            }
            return ret;
        }

        /// <summary>
        /// Get command required privileges
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>Command</returns>
        public IReadOnlyDictionary<string, uint> GetRequiredCommandPrivileges(ICommand command)
        {
            Dictionary<string, uint> ret = new Dictionary<string, uint>();
            if (command != null)
            {
                if (CommandsConfiguration != null)
                {
                    string key = command.Name.Trim().ToLower();
                    if (CommandsConfiguration.Data.Commands.ContainsKey(key))
                    {
                        CommandConfigurationData command_configuration = CommandsConfiguration.Data.Commands[key];
                        if (command_configuration != null)
                        {
                            foreach (KeyValuePair<string, uint> privilege in command_configuration.Privileges)
                            {
                                if (ret.ContainsKey(privilege.Key))
                                {
                                    if (ret[privilege.Key] < privilege.Value)
                                    {
                                        ret[privilege.Key] = privilege.Value;
                                    }
                                }
                                else
                                {
                                    ret.Add(privilege.Key, privilege.Value);
                                }
                            }
                        }
                    }
                }
                foreach (KeyValuePair<string, uint> force_required_privilege in command.ForceRequiredPrivileges)
                {
                    if (ret.ContainsKey(force_required_privilege.Key))
                    {
                        if (ret[force_required_privilege.Key] < force_required_privilege.Value)
                        {
                            ret[force_required_privilege.Key] = force_required_privilege.Value;
                        }
                    }
                    else
                    {
                        ret.Add(force_required_privilege.Key, force_required_privilege.Value);
                    }
                }
            }
            return ret;
        }
    }
}
