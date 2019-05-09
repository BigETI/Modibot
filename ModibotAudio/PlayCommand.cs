using ModibotAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Modibot audio namespace
/// </summary>
namespace ModibotAudio
{
    /// <summary>
    /// Play command class
    /// </summary>
    public class PlayCommand : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name => "play";

        /// <summary>
        /// Command description
        /// </summary>
        public string Description => "Plays music from specified URI";

        /// <summary>
        /// Command full description
        /// </summary>
        public string FullDescription => "This command plays music from the specified URI" + Environment.NewLine + "Usage: " + Name + " <URI>";

        /// <summary>
        /// Required privileges
        /// </summary>
        public ReadOnlyDictionary<string, uint> RequiredPrivileges => new ReadOnlyDictionary<string, uint>(new Dictionary<string, uint>());

        /// <summary>
        /// Command group
        /// </summary>
        public string CommandGroup => "audio and music";

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
