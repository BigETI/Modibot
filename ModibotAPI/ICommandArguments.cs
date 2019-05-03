using Discord.WebSocket;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Command arguments interface
    /// </summary>
    public interface ICommandArguments : ICompiledCommandArguments
    {
        /// <summary>
        /// Author
        /// </summary>
        SocketUser Author { get; }

        /// <summary>
        /// Message channel
        /// </summary>
        ISocketMessageChannel MessageChannel { get; }
    }
}
