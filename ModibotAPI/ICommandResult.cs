using System.Collections.ObjectModel;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Command result interface
    /// </summary>
    public interface ICommandResult
    {
        /// <summary>
        /// Result
        /// </summary>
        ECommandResult Result { get; }

        /// <summary>
        /// Compiled command
        /// </summary>
        ICompiledCommand CompiledCommand { get; }

        /// <summary>
        /// Missing privileges
        /// </summary>
        ReadOnlyDictionary<string, uint> MissingPrivileges { get; }
    }
}
