using Discord;
using Discord.Rest;
using Discord.WebSocket;
using ModibotAPI;
using System.Threading.Tasks;

/// <summary>
/// Modibot services namespace
/// </summary>
namespace Modibot.Services
{
    /// <summary>
    /// Chat class
    /// </summary>
    [Service]
    internal class Chat : IChat
    {
        /// <summary>
        /// Configuration
        /// </summary>
        private Configuration configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        public Chat(IServiceProvider serviceProvider)
        {
            configuration = serviceProvider.RequireService<Configuration>();
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns>User message</returns>
        public RestUserMessage SendMessage(string text, ISocketMessageChannel messageChannel)
        {
            return SendMessageAsync(text, messageChannel).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Send message (asynchronous)
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns>User message task</returns>
        public Task<RestUserMessage> SendMessageAsync(string text, ISocketMessageChannel messageChannel)
        {
            SocketGuildChannel guild_channel = messageChannel as SocketGuildChannel;
            return messageChannel.SendMessageAsync(text, (guild_channel == null) ? false : configuration.GetGuildConfiguration(guild_channel.Guild).EnableTTS);
        }

        /// <summary>
        /// Send embed
        /// </summary>
        /// <param name="embed">Embed</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns></returns>
        public RestUserMessage SendEmbed(Embed embed, ISocketMessageChannel messageChannel)
        {
            return SendEmbedAsync(embed, messageChannel).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Send embed (asynchronous)
        /// </summary>
        /// <param name="embed">Embed</param>
        /// <param name="messageChannel">Message channel</param>
        /// <returns></returns>
        public Task<RestUserMessage> SendEmbedAsync(Embed embed, ISocketMessageChannel messageChannel)
        {
            SocketGuildChannel guild_channel = messageChannel as SocketGuildChannel;
            return messageChannel.SendMessageAsync("", (guild_channel == null) ? false : configuration.GetGuildConfiguration(guild_channel.Guild).EnableTTS, embed);
        }
    }
}
