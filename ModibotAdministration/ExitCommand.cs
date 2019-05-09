using ModibotAPI;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Modibot administration namespace
/// </summary>
namespace ModibotAdministration
{
    /// <summary>
    /// Exit command class
    /// </summary>
    public class ExitCommand : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name => "exit";

        /// <summary>
        /// Command description
        /// </summary>
        public string Description => "Terminates current bot process";

        /// <summary>
        /// Command full description
        /// </summary>
        public string FullDescription => "This command terminates the current bot process";

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
        /// Execute command
        /// </summary>
        /// <param name="commandArguments">Command arguments</param>
        /// <returns></returns>
        public ECommandResult Execute(ICommandArguments commandArguments)
        {
            IChat[] chat_services = commandArguments.Bot.GetServices<IChat>();
            if (chat_services != null)
            {
                foreach (IChat chat in chat_services)
                {
                    if (chat != null)
                    {
                        chat.SendMessage("Goodbye!", commandArguments.MessageChannel);
                    }
                }
            }
            commandArguments.Bot.Exit();
            return ECommandResult.Successful;
        }
    }
}
