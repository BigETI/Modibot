using Discord;
using ModibotAPI;
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
        /// Prepare embed builder
        /// </summary>
        /// <returns>Embed builder</returns>
        private static EmbedBuilder PrepareEmbedBuilder()
        {
            EmbedBuilder ret = new EmbedBuilder();
            ret.WithColor(Color.Blue);
            return ret;
        }

        /// <summary>
        /// Prepare embed builder
        /// </summary>
        /// <param name="title">Embed title</param>
        /// <returns>Embed builder</returns>
        private static EmbedBuilder PrepareEmbedBuilder(string title)
        {
            EmbedBuilder ret = new EmbedBuilder();
            ret.WithTitle(title);
            ret.WithColor(Color.Blue);
            return ret;
        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="commandArguments">Command arguments</param>
        /// <returns>Command result</returns>
        public ECommandResult Execute(ICommandArguments commandArguments)
        {
            ECommandResult ret = ECommandResult.Failed;
            ICommands[] commands_services = commandArguments.Bot.GetServices<ICommands>();
            IChat[] chat_services = commandArguments.Bot.GetServices<IChat>();
            HelpConfiguration help_configuration = commandArguments.Bot.GetService<HelpConfiguration>();
            if ((commands_services != null) && (chat_services != null) && (help_configuration != null))
            {
                List<Embed> embeds = new List<Embed>();
                if (commandArguments.Arguments.Count > 0)
                {
                    string command_string = commandArguments.Arguments[0];
                    List<KeyValuePair<ICommands, ICommand>> command_list = new List<KeyValuePair<ICommands, ICommand>>();
                    foreach (ICommands commands in commands_services)
                    {
                        ICommand command = commands.FindCommand(command_string);
                        if (command != null)
                        {
                            command_list.Add(new KeyValuePair<ICommands, ICommand>(commands, command));
                        }
                    }
                    if (command_list.Count > 0)
                    {
                        foreach (KeyValuePair<ICommands, ICommand> command in command_list)
                        {
                            EmbedBuilder embed_builder = PrepareEmbedBuilder("Help topic for command \"" + command.Value.Name + "\"");
                            embed_builder.AddField("Command", command.Value.Name, true);
                            embed_builder.AddField("Description", command.Value.Description, true);
                            embed_builder.AddField("Full description", command.Value.FullDescription);
                            StringBuilder required_privileges = new StringBuilder();
                            foreach (KeyValuePair<string, uint> privilege in command.Value.RequiredPrivileges)
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
                            ICommandGroup command_group = command.Key.GetCommandGroup(command.Value.CommandGroup);
                            if (command_group != null)
                            {
                                StringBuilder more_commands = new StringBuilder();
                                bool first = true;
                                foreach (ICommand command_group_command in command.Key.FromCommandGroup(command_group))
                                {
                                    if (first)
                                    {
                                        first = false;
                                    }
                                    else
                                    {
                                        more_commands.Append(", ");
                                    }
                                    more_commands.Append(command_group_command.Name);
                                }
                                if (first)
                                {
                                    more_commands.Append("No commands?");
                                }
                                embed_builder.AddField("More commands " + command_group.Icon, more_commands.ToString());
                            }
                            embeds.Add(embed_builder.Build());
                        }
                    }
                    else
                    {
                        List<KeyValuePair<ICommands, ICommandGroup>> command_group_list = new List<KeyValuePair<ICommands, ICommandGroup>>();
                        string command_group_name_key = commandArguments.Arguments[0].Trim().ToLower();
                        foreach (ICommands commands in commands_services)
                        {
                            ICommandGroup command_group = commands.GetCommandGroup(command_string);
                            if (command_group != null)
                            {
                                if (command_group.Name.Trim().ToLower() == command_group_name_key)
                                {
                                    command_group_list.Add(new KeyValuePair<ICommands, ICommandGroup>(commands, command_group));
                                }
                            }
                        }
                        if (command_group_list.Count > 0)
                        {
                            foreach (KeyValuePair<ICommands, ICommandGroup> command_group in command_group_list)
                            {
                                EmbedBuilder embed_builder = PrepareEmbedBuilder("Help topic for command group \"" + command_group.Value.Icon + " " + command_group.Value.Name + "\"");
                                StringBuilder command_group_commands = new StringBuilder();
                                bool first = true;
                                foreach (ICommand command_group_command in command_group.Key.FromCommandGroup(command_group.Value))
                                {
                                    if (first)
                                    {
                                        first = false;
                                    }
                                    else
                                    {
                                        command_group_commands.Append(", ");
                                    }
                                    command_group_commands.Append(command_group_command.Name);
                                }
                                if (first)
                                {
                                    command_group_commands.Append("No commands?");
                                }
                                embed_builder.AddField("Commands", command_group_commands.ToString());
                                command_group_commands.Clear();
                                embeds.Add(embed_builder.Build());
                            }
                        }
                        else
                        {
                            foreach (ICommands commands in commands_services)
                            {
                                EmbedBuilder embed_builder = PrepareEmbedBuilder();
                                embed_builder.WithTitle("Help topics similar to \"" + command_string + "\"");
                                StringBuilder all_commands_groups = new StringBuilder();
                                bool first = true;
                                foreach (ICommand command in commands.AvailableCommands)
                                {
                                    if (Levenshtein.GetDistance(command_string, command.Name) <= help_configuration.Data.MaximumLevenshteinDistance)
                                    {
                                        if (first)
                                        {
                                            first = false;
                                        }
                                        else
                                        {
                                            all_commands_groups.Append(", ");
                                        }
                                        all_commands_groups.Append(command.Name);
                                    }
                                }
                                if (first)
                                {
                                    all_commands_groups.Append("No similar commands found");
                                }
                                embed_builder.AddField("Commands", all_commands_groups.ToString());
                                all_commands_groups.Clear();
                                first = true;
                                foreach (ICommandGroup command_group in commands.AvailableCommandGroups)
                                {
                                    if (Levenshtein.GetDistance(command_string, command_group.Name) <= help_configuration.Data.MaximumLevenshteinDistance)
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
                                embed_builder.AddField("Command groups", all_commands_groups.ToString());
                                embeds.Add(embed_builder.Build());
                            }
                        }
                        command_group_list.Clear();
                    }
                    command_list.Clear();
                }
                else
                {
                    foreach (ICommands commands in commands_services)
                    {
                        EmbedBuilder embed_builder = PrepareEmbedBuilder("Help topics");
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
                                command_group_commands.Append(command_group_command.Name);
                            }
                            if (first)
                            {
                                command_group_commands.Append("No commands specified yet.");
                            }
                            embed_builder.AddField(command_group.Icon + " " + command_group.Name, command_group_commands.ToString());
                        }
                        embeds.Add(embed_builder.Build());
                    }
                }
                foreach (IChat chat in chat_services)
                {
                    foreach (Embed embed in embeds)
                    {
                        chat.SendEmbedAsync(embed, commandArguments.MessageChannel);
                    }
                }
                embeds.Clear();
                ret = ECommandResult.Successful;
            }
            return ret;
        }
    }
}
