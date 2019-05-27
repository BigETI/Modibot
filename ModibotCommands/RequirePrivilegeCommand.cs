using ModibotAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Modibot commands namespace
/// </summary>
namespace ModibotCommands
{
    /// <summary>
    /// Require privilege command
    /// </summary>
    public class RequirePrivilegeCommand : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name => "requireprivilege";

        /// <summary>
        /// Command description
        /// </summary>
        public string Description => "Require command privilege";

        /// <summary>
        /// Command full description
        /// </summary>
        public string FullDescription => "This command sets a requirement for a command privilege" + Environment.NewLine + "Usage: " + Name + " <command> <privilege> <level>";

        /// <summary>
        /// Force required privileges
        /// </summary>
        public ReadOnlyDictionary<string, uint> ForceRequiredPrivileges { get; private set; } = new ReadOnlyDictionary<string, uint>(new Dictionary<string, uint>
        {
            { "bot.administrator", 1U }
        });

        /// <summary>
        /// Command group
        /// </summary>
        public string CommandGroup => "administration";

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="commandArguments">Command arguments</param>
        /// <returns>Command result</returns>
        public ECommandResult Execute(ICommandArguments commandArguments)
        {
            ECommandResult ret = ECommandResult.Failed;
            if (commandArguments.Arguments.Count == 3)
            {
                string command = commandArguments.Arguments[0].Trim().ToLower();
                string privilege = commandArguments.Arguments[1];
                uint level;
                if (uint.TryParse(commandArguments.Arguments[2], out level))
                {
                    CommandsConfiguration commands_configuration = commandArguments.Bot.GetService<CommandsConfiguration>();
                    if (commands_configuration != null)
                    {
                        string message = null;
                        CommandConfigurationData command_configuration_data = null;
                        if (commands_configuration.Data.Commands.ContainsKey(command))
                        {
                            command_configuration_data = commands_configuration.Data.Commands[command];
                            if (command_configuration_data == null)
                            {
                                command_configuration_data = new CommandConfigurationData();
                                commands_configuration.Data.Commands[command] = command_configuration_data;
                            }
                        }
                        else
                        {
                            command_configuration_data = new CommandConfigurationData();
                            commands_configuration.Data.Commands.Add(command, command_configuration_data);
                        }
                        if (level == 0U)
                        {
                            if (command_configuration_data.Privileges.ContainsKey(privilege))
                            {
                                command_configuration_data.Privileges.Remove(privilege);
                                commands_configuration.Save();
                                message = "Privilege requirement \"" + privilege + "\" for \"" + command + "\" has been successfully removed.";
                                ret = ECommandResult.Successful;
                            }
                            else
                            {
                                message = "Privilege requirement \"" + privilege + "\" doesn't exist for \"" + command + "\".";
                            }
                        }
                        else
                        {
                            if (command_configuration_data.Privileges.ContainsKey(privilege))
                            {
                                command_configuration_data.Privileges[privilege] = level;
                                commands_configuration.Save();
                                message = "Privilege requirement \"" + privilege + "\" for \"" + command + "\" has been successfully updated to level " + level + ".";
                            }
                            else
                            {
                                command_configuration_data.Privileges.Add(privilege, level);
                                commands_configuration.Save();
                                message = "Privilege requirement \"" + privilege + "\" for \"" + command + "\" has been successfully set to level " + level + ".";
                            }
                            ret = ECommandResult.Successful;
                        }
                        if (message != null)
                        {
                            IChat[] chat_services = commandArguments.Bot.GetServices<IChat>();
                            if (chat_services != null)
                            {
                                foreach (IChat chat in chat_services)
                                {
                                    if (chat != null)
                                    {
                                        chat.SendMessage(message, commandArguments.MessageChannel);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }
    }
}
