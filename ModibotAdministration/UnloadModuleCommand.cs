using ModibotAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

/// <summary>
/// Modibot administration namespace
/// </summary>
namespace ModibotAdministration
{
    /// <summary>
    /// Unload module command class
    /// </summary>
    public class UnloadModuleCommand : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name => "unloadmodule";

        /// <summary>
        /// Command description
        /// </summary>
        public string Description => "Unloads a module";

        /// <summary>
        /// Command full description
        /// </summary>
        public string FullDescription => "This command unloads a module.";

        /// <summary>
        /// Force required privileges
        /// </summary>
        public ReadOnlyDictionary<string, uint> ForceRequiredPrivileges { get; } = new ReadOnlyDictionary<string, uint>(new Dictionary<string, uint>
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
            if (commandArguments.Arguments.Count > 0)
            {
                string name = commandArguments.RawArguments;
                if (commandArguments.Bot.LoadedModules.ContainsKey(commandArguments.RawArguments))
                {
                    commandArguments.Bot.UnloadModuleAsync(commandArguments.Bot.LoadedModules[commandArguments.RawArguments]).GetAwaiter().GetResult();
                    ret = ECommandResult.Successful;
                    IChat[] chat_services = commandArguments.Bot.GetServices<IChat>();
                    if (chat_services != null)
                    {
                        foreach (IChat chat in chat_services)
                        {
                            if (chat != null)
                            {
                                chat.SendMessage("Module \"" + commandArguments.RawArguments + "\" has been successfully unloaded.", commandArguments.MessageChannel);
                            }
                        }
                    }
                }
            }
            return ret;
        }
    }
}
