using Discord.WebSocket;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// COmmands interface
    /// </summary>
    public interface ICommands
    {
        /// <summary>
        /// Available commands
        /// </summary>
        ReadOnlyCollection<ICommand> AvailableCommands { get; }

        /// <summary>
        /// Available command groups
        /// </summary>
        ReadOnlyCollection<ICommandGroup> AvailableCommandGroups { get; }

        /// <summary>
        /// Compile cokmmand
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="bot">Bot</param>
        /// <returns>Compiled command</returns>
        ICompiledCommand Compile(string command, IBot bot);

        /// <summary>
        /// Compile command (asynchronous)
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="bot">Bot</param>
        /// <returns>Compiled command task</returns>
        Task<ICompiledCommand> CompileAsync(string command, IBot bot);

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="author">Author</param>
        /// <param name="messageChannel">Message channel</param>
        /// <param name="bot">Bot</param>
        /// <returns>Command result</returns>
        ICommandResult Execute(string command, SocketUser author, ISocketMessageChannel messageChannel, IBot bot);

        /// <summary>
        /// Execute command (asynchronous)
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="author">Author</param>
        /// <param name="messageChannel">Message channel</param>
        /// <param name="bot">Bot</param>
        /// <returns>Command result task</returns>
        Task<ICommandResult> ExecuteAsync(string command, SocketUser author, ISocketMessageChannel messageChannel, IBot bot);

        /// <summary>
        /// Find command
        /// </summary>
        /// <param name="commandName">Command name</param>
        /// <returns>Command</returns>
        ICommand FindCommand(string commandName);

        /// <summary>
        /// Get command group
        /// </summary>
        /// <param name="commandGroupName">Command group name</param>
        /// <returns>Command group</returns>
        ICommandGroup GetCommandGroup(string commandGroupName);

        /// <summary>
        /// Get commands from command group
        /// </summary>
        /// <param name="commandGroup">Command group</param>
        /// <returns>Commands from command group</returns>
        ICommand[] FromCommandGroup(ICommandGroup commandGroup);
    }
}
