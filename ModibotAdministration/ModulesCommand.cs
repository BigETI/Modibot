using Discord;
using ModibotAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Modibot administration namespace
/// </summary>
namespace ModibotAdministration
{
    /// <summary>
    /// Modules command class
    /// </summary>
    public class ModulesCommand : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name => "modules";

        /// <summary>
        /// Command description
        /// </summary>
        public string Description => "Lists all modules";

        /// <summary>
        /// Command full description
        /// </summary>
        public string FullDescription => "This command lists all modules.";

        /// <summary>
        /// Required privileges
        /// </summary>
        public ReadOnlyDictionary<string, uint> RequiredPrivileges { get; } = new ReadOnlyDictionary<string, uint>(new Dictionary<string, uint>()
        {
            { "bot.administrator", 1U }
        });

        /// <summary>
        /// Command group
        /// </summary>
        public string CommandGroup => "administration";

        /// <summary>
        /// Prepare embed builder
        /// </summary>
        /// <returns>Embed builder</returns>
        private static EmbedBuilder PrepareEmbedBuilder()
        {
            EmbedBuilder ret = new EmbedBuilder();
            ret.WithTitle(":floppy_disk: Modules");
            ret.WithColor(Color.Teal);
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
            IModules[] modules_services = commandArguments.Bot.GetServices<IModules>();
            IChat[] chat_services = commandArguments.Bot.GetServices<IChat>();
            if ((modules_services != null) && (chat_services != null))
            {
                List<Embed> embeds = new List<Embed>();
                foreach (IModules modules in modules_services)
                {
                    EmbedBuilder embed_builder = PrepareEmbedBuilder();
                    foreach (IModule module in modules.LoadedModules.Values)
                    {
                        embed_builder.AddField(module.Name, "Version: " + module.Version + Environment.NewLine + "Author: " + module.Author + Environment.NewLine + "URI: " + module.URI);
                    }
                    embeds.Add(embed_builder.Build());
                }
                foreach (IChat chat in chat_services)
                {
                    if (chat != null)
                    {
                        foreach (Embed embed in embeds)
                        {
                            chat.SendEmbed(embed, commandArguments.MessageChannel);
                        }
                    }
                }
                embeds.Clear();
                ret = ECommandResult.Successful;
            }
            return ret;
        }
    }
}
