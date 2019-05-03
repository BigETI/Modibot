using System.Collections.Generic;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Modules interface
    /// </summary>
    public interface IModules
    {
        /// <summary>
        /// Loaded modules
        /// </summary>
        IReadOnlyDictionary<string, IModule> LoadedModules { get; }
    }
}
