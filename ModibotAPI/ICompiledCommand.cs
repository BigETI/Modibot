using Discord.WebSocket;
using System.Threading.Tasks;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Compiled command namespace
    /// </summary>
    public interface ICompiledCommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        string CommandName { get; }

        /// <summary>
        /// Compiled command arguments
        /// </summary>
        ICompiledCommandArguments CompiledCommandArguments { get; }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="author">Author</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns>Command result</returns>
        ICommandResult Execute(SocketUser author, ISocketMessageChannel messageChannel);

        /// <summary>
        /// Execute (asynchronous)
        /// </summary>
        /// <param name="author">Author</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns>Command result task</returns>
        Task<ICommandResult> ExecuteAsync(SocketUser author, ISocketMessageChannel messageChannel);
    }
}
