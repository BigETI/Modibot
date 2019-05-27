using ModibotAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ModibotCommands
{
    /// <summary>
    /// Disable command class
    /// </summary>
    public class DisableCommand : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name => "disable";

        /// <summary>
        /// Command description
        /// </summary>
        public string Description => "Disables a command";

        /// <summary>
        /// Command full description
        /// </summary>
        public string FullDescription => "This command disables a command." + Environment.NewLine + "Usage: " + Name + " <command>";

        /// <summary>
        /// Force required privileges
        /// </summary>
        public ReadOnlyDictionary<string, uint> ForceRequiredPrivileges => new ReadOnlyDictionary<string, uint>(new Dictionary<string, uint>
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
        /// <returns></returns>
        public ECommandResult Execute(ICommandArguments commandArguments)
        {
            ECommandResult ret = ECommandResult.Failed;
            if (commandArguments.Arguments.Count == 1)
            {
                CommandsConfiguration commands_configuration = commandArguments.Bot.GetService<CommandsConfiguration>();
                if (commands_configuration != null)
                {
                    string message = null;
                    string command = commandArguments.Arguments[0].Trim().ToLower();
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
                    if (command_configuration_data.Enabled)
                    {
                        command_configuration_data.Enabled = false;
                        commands_configuration.Save();
                        message = "Command \"" + command + "\" has been successfully disabled.";
                        ret = ECommandResult.Successful;
                    }
                    else
                    {
                        message = "Command \"" + command + "\" is already disabled.";
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
            return ret;
        }
    }
}
