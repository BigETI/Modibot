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
        /// Modules
        /// </summary>
        private Dictionary<string, IModule> loadedModules = new Dictionary<string, IModule>();

        /// <summary>
        /// modules path
        /// </summary>
        public static readonly string Path = "modules";

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
        public Task<IModule> LoadModuleAsync(string path)
        {
            Task<IModule> ret = new Task<IModule>(() => {
                IModule result = null;
                try
                {
                    if (path != null)
                    {
                        if (File.Exists(path))
                        {
                            string full_path = System.IO.Path.GetFullPath(path);
                            Assembly assembly = Assembly.LoadFile(full_path);
                            if (assembly != null)
                            {
                                Type[] types = assembly.GetTypes();
                                if (types != null)
                                {
                                    bool has_module_type = false;
                                    foreach (Type type in types)
                                    {
                                        if (type != null)
                                        {
                                            if (type.IsClass)
                                            {
                                                if (typeof(IModule).IsAssignableFrom(type))
                                                {
                                                    if (has_module_type)
                                                    {
                                                        has_module_type = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        has_module_type = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (has_module_type)
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
                                                        result = (IModule)instance;
                                                        if (loadedModules.ContainsKey(result.Name))
                                                        {
                                                            IModule old_module = loadedModules[result.Name];
                                                            old_module.ExitAsync().GetAwaiter().GetResult();
                                                            loadedModules[result.Name] = result;
                                                        }
                                                        else
                                                        {
                                                            loadedModules.Add(result.Name, result);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (result != null)
                                        {
                                            foreach (IModule loaded_module in loadedModules.Values)
                                            {
                                                loaded_module.ModuleUnloadAsync(result).GetAwaiter().GetResult();
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
                return result;
            });
            ret.Start();
            return ret;
        }

        /// <summary>
        /// Unload module (asynchronous)
        /// </summary>
        /// <param name="module">Module</param>
        /// <returns>Task</returns>
        public Task<bool> UnloadModuleAsync(IModule module)
        {
            Task<bool> ret = new Task<bool>(() =>
            {
                bool result = false;
                if (module != null)
                {
                    if (loadedModules.ContainsKey(module.Name))
                    {
                        if (loadedModules[module.Name] == module)
                        {
                            loadedModules.Remove(module.Name);
                            foreach (IModule loaded_module in loadedModules.Values)
                            {
                                loaded_module.ModuleUnloadAsync(module).GetAwaiter().GetResult();
                            }
                            result = true;
                        }
                    }
                }
                return result;
            });
            ret.Start();
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
            loadedModules.Clear();
        }
    }
}
