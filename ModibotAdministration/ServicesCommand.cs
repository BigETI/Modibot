using Discord;
using ModibotAPI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

/// <summary>
/// Modibot administration namespace
/// </summary>
namespace ModibotAdministration
{
    public class ServicesCommand : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name => "services";

        /// <summary>
        /// Command description
        /// </summary>
        public string Description => "Lists all services";

        /// <summary>
        /// Command full description
        /// </summary>
        public string FullDescription => "This command lists all services.";

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
            ModibotAPI.IServiceProvider service_provider = commandArguments.Bot.Services;
            IChat chat = commandArguments.Bot.GetService<IChat>();
            if ((service_provider != null) && (chat != null))
            {
                EmbedBuilder embed_builder = new EmbedBuilder();
                StringBuilder loaded_services = new StringBuilder();
                bool first = true;
                embed_builder.WithTitle(":tickets: Services");
                embed_builder.WithColor(Color.Red);
                foreach (object service in service_provider.LoadedServices)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        loaded_services.Append(", ");
                    }
                    loaded_services.Append(service.GetType().FullName);
                }
                if (first)
                {
                    loaded_services.Append("No services found.");
                }
                embed_builder.AddField("Loaded services", loaded_services.ToString());
                chat.SendEmbed(embed_builder.Build(), commandArguments.MessageChannel);
                ret = ECommandResult.Successful;
            }
            return ret;
        }
    }
}
