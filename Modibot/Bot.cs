using Discord;
using Discord.WebSocket;
using Modibot.Services;
using ModibotAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Bot class
    /// </summary>
    internal class Bot : IBot, IDisposable
    {
        /// <summary>
        /// Discord client
        /// </summary>
        private readonly DiscordSocketClient discordClient;

        /// <summary>
        /// Services
        /// </summary>
        public ModibotAPI.IServiceProvider Services { get; private set; } = new ServiceProvider();

        /// <summary>
        /// Commands
        /// </summary>
        private ICommands commands;

        /// <summary>
        /// Bot configuration
        /// </summary>
        private BotConfiguration botConfiguration;

        /// <summary>
        /// Chat
        /// </summary>
        private IChat chat;

        /// <summary>
        /// Modules
        /// </summary>
        private Modules modules;

        /// <summary>
        /// Commands
        /// </summary>
        public ICommands Commands
        {
            get
            {
                if (commands == null)
                {
                    commands = Services.GetService<ICommands>();
                }
                return commands;
            }
        }

        /// <summary>
        /// Bot configuration
        /// </summary>
        public BotConfiguration BotConfiguration
        {
            get
            {
                if (botConfiguration == null)
                {
                    botConfiguration = Services.GetService<BotConfiguration>();
                }
                return botConfiguration;
            }
        }

        /// <summary>
        /// Chat
        /// </summary>
        public IChat Chat
        {
            get
            {
                if (chat == null)
                {
                    chat = Services.GetService<IChat>();
                }
                return chat;
            }
        }

        /// <summary>
        /// Modules
        /// </summary>
        public Modules Modules
        {
            get
            {
                if (modules == null)
                {
                    modules = Services.RequireService<Modules>();
                }
                return modules;
            }
        }

        /// <summary>
        /// Loaded services
        /// </summary>
        public ReadOnlyCollection<object> LoadedServices => Services.LoadedServices;

        /// <summary>
        /// Loaded modules
        /// </summary>
        public IReadOnlyDictionary<string, IModule> LoadedModules => Modules.LoadedModules;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Bot()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            if (types != null)
            {
                foreach (Type type in types)
                {
                    if (type != null)
                    {
                        if (type.IsClass)
                        {
                            object instance = null;
                            if (Attribute.IsDefined(type, typeof(ServiceAttribute)))
                            {
                                instance = Services.RequireService(type);
                            }
                        }
                    }
                }
            }
            discordClient = Services.RequireService<DiscordSocketClient>();
            AppDomain.CurrentDomain.AssemblyResolve += (sender, arguments) =>
            {
                Assembly ret = null;
                string[] parts = arguments.Name.Split(",");
                if (parts != null)
                {
                    if (parts.Length > 0)
                    {
                        string assembly_name = parts[0];
                        if (assembly_name != null)
                        {
                            if (assembly_name.Length > 0)
                            {
                                string full_path = Path.GetFullPath(Path.Combine("dependencies", assembly_name));
                                try
                                {
                                    Assembly assembly = Assembly.LoadFile(full_path);
                                    if (assembly != null)
                                    {
                                        if (assembly.FullName == arguments.Name)
                                        {
                                            ret = assembly;
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.Error.WriteLine(e);
                                }
                            }
                        }
                    }
                }
                return ret;
            };
            if (Directory.Exists(Modules.Path))
            {
                string[] paths = Directory.GetFiles(Modules.Path, "*.dll");
                if (paths != null)
                {
                    foreach (string path in paths)
                    {
                        Modules.LoadModule(path);
                    }
                }
            }
        }

        /// <summary>
        /// Main (asynchronous)
        /// </summary>
        /// <returns></returns>
        public async Task MainAsync()
        {
            if (BotConfiguration != null)
            {
                string token = BotConfiguration.Data.BotToken;
                if (token.Length <= 0)
                {
                    try
                    {
                        token = Environment.GetEnvironmentVariable("token");
                    }
                    catch (Exception e)
                    {
                        await Console.Error.WriteLineAsync(e.ToString());
                    }
                }
                if (token == null)
                {
                    await Console.Error.WriteLineAsync("Please provide a bot token through the configuration file or through the environment variable \"token\".");
                }
                else
                {
                    foreach (IModule module in Modules.LoadedModules.Values)
                    {
                        if (module != null)
                        {
                            await module.InitAsync(this);
                        }
                    }
                    await discordClient.LoginAsync(TokenType.Bot, token);
                    await discordClient.StartAsync();
                    await Task.Delay(-1);
                }
            }
            else
            {
                await Console.Error.WriteLineAsync("Configuration module is required!");
            }
            Exit();
        }

        /// <summary>
        /// Load module (asynchronous)
        /// </summary>
        /// <param name="path">Module path</param>
        /// <returns>Module task</returns>
        public Task<IModule> LoadModuleAsync(string path)
        {
            Task<IModule> ret = new Task<IModule>(() =>
            {
                IModule result = Modules.LoadModuleAsync(path).GetAwaiter().GetResult();
                if (result != null)
                {
                    result.InitAsync(this);
                }
                return result;
            });
            ret.Start();
            return ret;
        }

        /// <summary>
        /// Unload module
        /// </summary>
        /// <param name="module">Module</param>
        /// <returns>Task</returns>
        public Task UnloadModuleAsync(IModule module)
        {
            Task ret = new Task(() =>
            {
                if (Modules.UnloadModuleAsync(module).GetAwaiter().GetResult())
                {
                    module.ExitAsync().GetAwaiter().GetResult();
                }
            });
            ret.Start();
            return ret;
        }

        /// <summary>
        /// Exit bot
        /// </summary>
        public async void Exit()
        {
            IConfiguration[] configuration_services = GetServices<IConfiguration>();
            if (configuration_services != null)
            {
                foreach (IConfiguration configuration in configuration_services)
                {
                    if (configuration != null)
                    {
                        configuration.Save();
                    }
                }
            }
            if (discordClient != null)
            {
                await discordClient.LogoutAsync();
            }
            Environment.Exit(0);
        }

        /// <summary>
        /// Require service
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service if successful, otherwise "default(T)"</returns>
        public T RequireService<T>()
        {
            return Services.RequireService<T>();
        }

        /// <summary>
        /// Require service
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Service if successful, otherwise "null"</returns>
        public object RequireService(Type serviceType)
        {
            return Services.RequireService(serviceType);
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Service if successful, otherwise "default(T)"</returns>
        public T GetService<T>()
        {
            return Services.GetService<T>();
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceTypeName">Service type name</param>
        /// <returns>Service if successful, otherwise "null"</returns>
        public object GetService(string serviceTypeName)
        {
            return Services.GetService(serviceTypeName);
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Service if successful, otherwise "null"</returns>
        public object GetService(Type serviceType)
        {
            return Services.GetService(serviceType);
        }

        /// <summary>
        /// Get services
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Services</returns>
        public T[] GetServices<T>()
        {
            return Services.GetServices<T>();
        }

        /// <summary>
        /// Get services
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Services</returns>
        public object[] GetServices(Type serviceType)
        {
            return Services.GetServices(serviceType);
        }

        /// <summary>
        /// Get services
        /// </summary>
        /// <param name="serviceTypeName">Service type name</param>
        /// <returns>Services</returns>
        public object[] GetServices(string serviceTypeName)
        {
            return Services.GetServices(serviceTypeName);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)Services).Dispose();
        }
    }
}
