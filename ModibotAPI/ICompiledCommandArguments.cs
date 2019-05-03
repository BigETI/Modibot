using System.Collections.ObjectModel;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Compiled command arguments namespace
    /// </summary>
    public interface ICompiledCommandArguments
    {
        /// <summary>
        /// Command
        /// </summary>
        ICommand Command { get; }

        /// <summary>
        /// Raw arguments
        /// </summary>
        string RawArguments { get; }

        /// <summary>
        /// Arguments
        /// </summary>
        ReadOnlyCollection<string> Arguments { get; }

        /// <summary>
        /// Bot
        /// </summary>
        IBot Bot { get; }
    }
}
