using Discord.WebSocket;
using ModibotAPI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

/// <summary>
/// Modibot commands namespace
/// </summary>
namespace ModibotCommands
{
    /// <summary>
    /// Commands module
    /// </summary>
    public class CommandsModule : IModule
    {
        /// <summary>
        /// Module name
        /// </summary>
        public string Name => "Commands processor";

        /// <summary>
        /// Module version
        /// </summary>
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Module author
        /// </summary>
        public string Author => "Ethem Kurt";

        /// <summary>
        /// Module URI
        /// </summary>
        public string URI => "https://github.com/BigETI/Modibot";

        /// <summary>
        /// Initialize (asynchronous)
        /// </summary>
        /// <param name="bot">Bot</param>
        /// <returns>Task</returns>
        public Task InitAsync(IBot bot)
        {
            Commands commands = bot.GetService<Commands>();
            if (commands != null)
            {
                Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();
                foreach (IModule module in bot.LoadedModules.Values)
                {
                    if (module != null)
                    {
                        Assembly assembly = Assembly.GetAssembly(module.GetType());
                        string key = assembly.ToString();
                        if (!(assemblies.ContainsKey(key)))
                        {
                            assemblies.Add(key, assembly);
                        }
                    }
                }
                foreach (Assembly assembly in assemblies.Values)
                {
                    Type[] types = assembly.GetTypes();
                    if (types != null)
                    {
                        foreach (Type type in types)
                        {
                            if (type != null)
                            {
                                if (type.IsClass)
                                {
                                    object instance = null;
                                    if (typeof(ICommand).IsAssignableFrom(type))
                                    {
                                        instance = Activator.CreateInstance(type);
                                        commands.AddCommand((ICommand)instance);
                                    }
                                    if (typeof(ICommandGroup).IsAssignableFrom(type))
                                    {
                                        instance = ((instance == null) ? Activator.CreateInstance(type) : instance);
                                        commands.AddCommandGroup((ICommandGroup)instance);
                                    }
                                }
                            }
                        }
                    }
                }
                assemblies.Clear();
            }
            DiscordSocketClient discord_client = bot.GetService<DiscordSocketClient>();
            if (discord_client != null)
            {
                discord_client.MessageReceived += async (message) =>
                {
                    if (message.Author.Id != discord_client.CurrentUser.Id)
                    {
                        if (message.Content != null)
                        {
                            CommandsConfiguration command_configuration = bot.GetService<CommandsConfiguration>();
                            if (command_configuration != null)
                            {
                                string possible_command = message.Content.TrimStart();
                                if (possible_command.StartsWith(command_configuration.Data.Delimiter))
                                {
                                    string command = possible_command.Substring(command_configuration.Data.Delimiter.Length);
                                    ICommandResult command_result = await commands.ExecuteAsync(command, message.Author, message.Channel, bot);
                                    string msg = null;
                                    switch (command_result.Result)
                                    {
                                        case ECommandResult.Failed:
                                            msg = string.Format(command_configuration.Data.FailedText, command_result.CompiledCommand.CommandName, command_result.CompiledCommand.CompiledCommandArguments.RawArguments);
                                            break;
                                        case ECommandResult.Successful:
                                            msg = string.Format(command_configuration.Data.SuccessfulText, command_result.CompiledCommand.CommandName, command_result.CompiledCommand.CompiledCommandArguments.RawArguments);
                                            break;
                                        case ECommandResult.Denied:
                                            msg = string.Format(command_configuration.Data.DeniedText, command_result.CompiledCommand.CommandName, command_result.CompiledCommand.CompiledCommandArguments.RawArguments);
                                            break;
                                    }
                                    if (msg != null)
                                    {
                                        if (msg.Length > 0)
                                        {
                                            IChat[] chat_services = bot.GetServices<IChat>();
                                            if (chat_services != null)
                                            {
                                                foreach (IChat chat in chat_services)
                                                {
                                                    await chat.SendMessageAsync(msg, message.Channel);
                                                    if (command_result.Result == ECommandResult.Failed)
                                                    {
                                                        await commands.ExecuteAsync("help " + command, message.Author, message.Channel, bot);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Exit (asynchronous)
        /// </summary>
        /// <returns>Task</returns>
        public Task ExitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
