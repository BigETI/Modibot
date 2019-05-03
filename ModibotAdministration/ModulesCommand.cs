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
    /// Modules command
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
        public ReadOnlyDictionary<string, uint> RequiredPrivileges => new ReadOnlyDictionary<string, uint>(new Dictionary<string, uint>()
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
            IModules modules = commandArguments.Bot.GetService<IModules>();
            IChat chat = commandArguments.Bot.GetService<IChat>();
            if ((modules != null) && (chat != null))
            {
                EmbedBuilder embed_builder = new EmbedBuilder();
                embed_builder.WithTitle(":floppy_disk: Modules");
                embed_builder.WithColor(Color.Teal);
                foreach (IModule module in modules.LoadedModules.Values)
                {
                    embed_builder.AddField(module.Name, "Version: " + module.Version + Environment.NewLine + "Author: " + module.Author + Environment.NewLine + "URI: " + module.URI);
                }
                chat.SendEmbed(embed_builder.Build(), commandArguments.MessageChannel);
                ret = ECommandResult.Successful;
            }
            return ret;
        }
    }
}
