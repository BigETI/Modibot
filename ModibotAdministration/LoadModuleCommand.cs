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
    /// Load module command class
    /// </summary>
    public class LoadModuleCommand : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name => "loadmodule";

        /// <summary>
        /// Command description
        /// </summary>
        public string Description => "Loads a module";

        /// <summary>
        /// Command full description
        /// </summary>
        public string FullDescription => "This command loads a module.";

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
                string path = commandArguments.RawArguments;
                if (File.Exists(path))
                {
                    try
                    {
                        path = Path.GetFullPath(path);
                        IModule module = commandArguments.Bot.LoadModuleAsync(path).GetAwaiter().GetResult();
                        if (module != null)
                        {
                            module.InitAsync(commandArguments.Bot);
                            ret = ECommandResult.Successful;
                            IChat[] chat_services = commandArguments.Bot.GetServices<IChat>();
                            if (chat_services != null)
                            {
                                foreach (IChat chat in chat_services)
                                {
                                    if (chat != null)
                                    {
                                        chat.SendMessage("Module \"" + module.Name + "\" has been successfully loaded.", commandArguments.MessageChannel);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e);
                    }
                }
            }
            return ret;
        }
    }
}
