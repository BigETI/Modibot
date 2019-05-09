using ModibotAPI;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Modibot moderation namespace
/// </summary>
namespace ModibotModeration
{
    /// <summary>
    /// Kick command class
    /// </summary>
    public class KickCommand : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name => "kick";

        /// <summary>
        /// Command description
        /// </summary>
        public string Description => "Kicks a user";

        /// <summary>
        /// Command full description
        /// </summary>
        public string FullDescription => "Kicks a user from a Discord guild.";

        /// <summary>
        /// Required privileges
        /// </summary>
        public ReadOnlyDictionary<string, uint> RequiredPrivileges { get; } = new ReadOnlyDictionary<string, uint>(new Dictionary<string, uint>());

        /// <summary>
        /// Command group
        /// </summary>
        public string CommandGroup => "moderation";

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="commandArguments">Command arguments</param>
        /// <returns>Command result</returns>
        public ECommandResult Execute(ICommandArguments commandArguments)
        {
            // TODO
            IChat[] chat_services = commandArguments.Bot.GetServices<IChat>();
            if (chat_services != null)
            {
                foreach (IChat chat in chat_services)
                {
                    if (chat != null)
                    {
                        chat.SendMessage("This command has not been implemented yet.", commandArguments.MessageChannel);
                    }
                }
            }
            return ECommandResult.Successful;
        }
    }
}
