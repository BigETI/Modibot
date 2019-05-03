using Discord.WebSocket;
using ModibotAPI;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Command arguments class
    /// </summary>
    internal class CommandArguments : CompiledCommandArguments, ICommandArguments
    {
        /// <summary>
        /// Author
        /// </summary>
        public SocketUser Author { get; private set; }

        /// <summary>
        /// Message channel
        /// </summary>
        public ISocketMessageChannel MessageChannel { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="compiledCommandArguments">Compiled command arguments</param>
        /// <param name="messageChannel">Message channel</param>
        /// <param name="author">Author</param>
        internal CommandArguments(CompiledCommandArguments compiledCommandArguments, SocketUser author, ISocketMessageChannel messageChannel) : base(compiledCommandArguments)
        {
            MessageChannel = messageChannel;
            Author = author;
        }
    }
}
