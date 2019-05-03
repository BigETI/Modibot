using System.Collections.ObjectModel;

/// <summary>
/// Modibot API
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Command interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Command description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Command full description
        /// </summary>
        string FullDescription { get; }

        /// <summary>
        /// Required privileges
        /// </summary>
        ReadOnlyDictionary<string, uint> RequiredPrivileges { get; }

        /// <summary>
        /// Command group
        /// </summary>
        string CommandGroup { get; }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="commandArguments">Command arguments</param>
        /// <returns>Command result</returns>
        ECommandResult Execute(ICommandArguments commandArguments);
    }
}
