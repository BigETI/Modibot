using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System.Threading.Tasks;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Chat interface
    /// </summary>
    public interface IChat
    {
        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns>User message</returns>
        RestUserMessage SendMessage(string text, ISocketMessageChannel messageChannel);

        /// <summary>
        /// Send message (asynchronous)
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns>User message task</returns>
        Task<RestUserMessage> SendMessageAsync(string text, ISocketMessageChannel messageChannel);

        /// <summary>
        /// Send embed
        /// </summary>
        /// <param name="embed">Embed</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns></returns>
        RestUserMessage SendEmbed(Embed embed, ISocketMessageChannel messageChannel);

        /// <summary>
        /// Send embed (asynchronous)
        /// </summary>
        /// <param name="embed">Embed</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns></returns>
        Task<RestUserMessage> SendEmbedAsync(Embed embed, ISocketMessageChannel messageChannel);
    }
}
