using Discord;
using ModibotAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

/// <summary>
/// Modibot help namespace
/// </summary>
namespace ModibotHelp
{
    /// <summary>
    /// Help command class
    /// </summary>
    public class HelpCommand : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name => "help";

        /// <summary>
        /// Command description
        /// </summary>
        public string Description => "Help topics";

        /// <summary>
        /// Command full description
        /// </summary>
        public string FullDescription => "This command shows help topics";

        /// <summary>
        /// Required privileges
        /// </summary>
        public ReadOnlyDictionary<string, uint> RequiredPrivileges { get; } = new ReadOnlyDictionary<string, uint>(new Dictionary<string, uint>());

        /// <summary>
        /// Command group
        /// </summary>
        public string CommandGroup => "miscellaneous";

        /// <summary>
        /// Embed similar commands and groups
        /// </summary>
        /// <param name="commands">Commands</param>
        /// <param name="commandGroupName">Command name</param>
        /// <param name="delimiter">Delimiter</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="embedBuilder">Embed builder</param>
        private void EmbedSimilarCommandsAndGroups(ICommands commands, IConfiguration configuration, string commandGroupName, EmbedBuilder embedBuilder)
        {
            embedBuilder.WithTitle("Help topics similar to \"" + commandGroupName + "\"");
            StringBuilder all_commands_groups = new StringBuilder();
            bool first = true;
            foreach (ICommand command in commands.AvailableCommands)
            {
                if (Levenshtein.GetDistance(commandGroupName, command.Name) <= configuration.Commands.MaximumLevenshteinDistance)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        all_commands_groups.Append(", ");
                    }
                    all_commands_groups.Append(configuration.Commands.Delimiter);
                    all_commands_groups.Append(command.Name);
                }
            }
            if (first)
            {
                all_commands_groups.Append("No similar commands found");
            }
            embedBuilder.AddField("Commands", all_commands_groups.ToString());
            all_commands_groups.Clear();
            first = true;
            foreach (ICommandGroup command_group in commands.AvailableCommandGroups)
            {
                if (Levenshtein.GetDistance(commandGroupName, command_group.Name) <= configuration.Commands.MaximumLevenshteinDistance)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        all_commands_groups.Append(", ");
                    }
                    all_commands_groups.Append(command_group.Icon);
                    all_commands_groups.Append(" ");
                    all_commands_groups.Append(command_group.Name);
                }
            }
            if (first)
            {
                all_commands_groups.Append("No similar command groups found");
            }
            embedBuilder.AddField("Command groups", all_commands_groups.ToString());
        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="commandArguments">Command arguments</param>
        /// <returns>Command result</returns>
        public ECommandResult Execute(ICommandArguments commandArguments)
        {
            ECommandResult ret = ECommandResult.Failed;
            ICommands commands = commandArguments.Bot.GetService<ICommands>();
            IChat chat = commandArguments.Bot.GetService<IChat>();
            IConfiguration configuration = commandArguments.Bot.GetService<IConfiguration>();
            if ((commands != null) && (chat != null) && (configuration != null))
            {
                EmbedBuilder embed_builder = new EmbedBuilder();
                embed_builder.WithColor(Color.Blue);
                if (commandArguments.Arguments.Count > 0)
                {
                    string command_string = commandArguments.Arguments[0];
                    if (command_string.StartsWith(configuration.Commands.Delimiter))
                    {
                        command_string = command_string.Substring(configuration.Commands.Delimiter.Length);
                    }
                    ICommand command = commands.FindCommand(command_string);
                    if (command == null)
                    {
                        ICommandGroup command_group = commands.GetCommandGroup(command_string);
                        if (command_group != null)
                        {
                            if (command_group.Name.Trim().ToLower() == commandArguments.Arguments[0].Trim().ToLower())
                            {
                                embed_builder.WithTitle("Help topic for command group \"" + command_group.Icon + " " + command_group.Name + "\"");
                                StringBuilder command_group_commands = new StringBuilder();
                                bool first = true;
                                foreach (ICommand command_group_command in commands.FromCommandGroup(command_group))
                                {
                                    if (first)
                                    {
                                        first = false;
                                    }
                                    else
                                    {
                                        command_group_commands.Append(", ");
                                    }
                                    command_group_commands.Append(configuration.Commands.Delimiter);
                                    command_group_commands.Append(command_group_command.Name);
                                }
                                embed_builder.AddField("Commands", command_group_commands.ToString());
                            }
                            else
                            {
                                EmbedSimilarCommandsAndGroups(commands, configuration, command_string, embed_builder);
                            }
                        }
                        else
                        {
                            EmbedSimilarCommandsAndGroups(commands, configuration, command_string, embed_builder);
                        }
                    }
                    else
                    {
                        embed_builder.WithTitle("Help topic for command \"" + configuration.Commands.Delimiter + command.Name + "\"");
                        embed_builder.AddField("Command", configuration.Commands.Delimiter + command.Name, true);
                        embed_builder.AddField("Description", command.Description, true);
                        embed_builder.AddField("Full description", command.FullDescription);
                        StringBuilder required_privileges = new StringBuilder();
                        foreach (KeyValuePair<string, uint> privilege in command.RequiredPrivileges)
                        {
                            required_privileges.Append("\"");
                            required_privileges.Append(privilege.Key);
                            required_privileges.Append("\" : ");
                            required_privileges.Append(privilege.Value.ToString());
                        }
                        if (required_privileges.Length > 0)
                        {
                            embed_builder.AddField("Required privileges", required_privileges);
                        }
                        ICommandGroup command_group = commands.GetCommandGroup(command.CommandGroup);
                        if (command_group != null)
                        {
                            StringBuilder more_commands = new StringBuilder();
                            bool first = true;
                            foreach (ICommand command_group_command in commands.FromCommandGroup(command_group))
                            {
                                if (first)
                                {
                                    first = false;
                                }
                                else
                                {
                                    more_commands.Append(", ");
                                }
                                more_commands.Append(configuration.Commands.Delimiter);
                                more_commands.Append(command_group_command.Name);
                            }
                            embed_builder.AddField("More commands " + command_group.Icon, more_commands.ToString());
                        }
                    }
                }
                else
                {
                    embed_builder.WithTitle("Help topics");
                    StringBuilder command_group_commands = new StringBuilder();
                    foreach (ICommandGroup command_group in commands.AvailableCommandGroups)
                    {
                        bool first = true;
                        command_group_commands.Clear();
                        foreach (ICommand command_group_command in commands.FromCommandGroup(command_group))
                        {
                            if (first)
                            {
                                first = false;
                            }
                            else
                            {
                                command_group_commands.Append(", ");
                            }
                            command_group_commands.Append(configuration.Commands.Delimiter);
                            command_group_commands.Append(command_group_command.Name);
                        }
                        if (first)
                        {
                            command_group_commands.Append("No commands specified yet.");
                        }
                        embed_builder.AddField(command_group.Icon + " " + command_group.Name, command_group_commands.ToString());
                    }
                }
                chat.SendEmbedAsync(embed_builder.Build(), commandArguments.MessageChannel);
                ret = ECommandResult.Successful;
            }
            return ret;
        }
    }
}
