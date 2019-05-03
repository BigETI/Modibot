using ModibotAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

/// <summary>
/// Modibot services namespace
/// </summary>
namespace Modibot.Services
{
    /// <summary>
    /// Modules class
    /// </summary>
    [Service]
    internal class Modules : IModules, IDisposable
    {
        /// <summary>
        /// Service provider
        /// </summary>
        private ModibotAPI.IServiceProvider serviceProvider;

        /// <summary>
        /// Commands
        /// </summary>
        private Commands commands;

        /// <summary>
        /// Modules
        /// </summary>
        private Dictionary<string, IModule> loadedModules = new Dictionary<string, IModule>();

        /// <summary>
        /// Default modules path
        /// </summary>
        public static readonly string DefaultPath = "modules";

        /// <summary>
        /// Loaded modules
        /// </summary>
        public IReadOnlyDictionary<string, IModule> LoadedModules => loadedModules;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        public Modules(ModibotAPI.IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            commands = serviceProvider.RequireService<Commands>();
        }

        /// <summary>
        /// Load module
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Loaded module</returns>
        public IModule LoadModule(string path)
        {
            return LoadModuleAsync(path).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Load module (asynchronous)
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Loaded module task</returns>
        public async Task<IModule> LoadModuleAsync(string path)
        {
            IModule ret = null;
            try
            {
                if (path != null)
                {
                    if (File.Exists(path))
                    {
                        string full_path = Path.GetFullPath(path);
                        Assembly assembly = Assembly.LoadFile(full_path);
                        if (assembly != null)
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
                                            if (Attribute.IsDefined(type, typeof(ServiceAttribute)))
                                            {
                                                instance = serviceProvider.RequireService(type);
                                            }
                                            if (typeof(IModule).IsAssignableFrom(type))
                                            {
                                                instance = ((instance == null) ? Activator.CreateInstance(type, true) : instance);
                                                IModule module = (IModule)instance;
                                                if (loadedModules.ContainsKey(module.Name))
                                                {
                                                    IModule old_module = loadedModules[module.Name];
                                                    await old_module.ExitAsync();
                                                    loadedModules[module.Name] = module;
                                                }
                                                else
                                                {
                                                    loadedModules.Add(module.Name, module);
                                                }
                                            }
                                            if (typeof(ICommand).IsAssignableFrom(type))
                                            {
                                                instance = ((instance == null) ? Activator.CreateInstance(type, true) : instance);
                                                commands.AddCommand((ICommand)instance);
                                            }
                                            if (typeof(ICommandGroup).IsAssignableFrom(type))
                                            {
                                                instance = ((instance == null) ? Activator.CreateInstance(type, true) : instance);
                                                commands.AddCommandGroup((ICommandGroup)instance);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            return ret;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public async void Dispose()
        {
            foreach (IModule module in loadedModules.Values)
            {
                await module.ExitAsync();
            }
        }
    }
}
