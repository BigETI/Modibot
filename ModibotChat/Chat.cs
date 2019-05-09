using Discord;
using Discord.Rest;
using Discord.WebSocket;
using ModibotAPI;
using System.Threading.Tasks;

/// <summary>
/// Modibot chat namespace
/// </summary>
namespace ModibotChat
{
    /// <summary>
    /// Chat class
    /// </summary>
    [Service]
    public class Chat : IChat
    {
        /// <summary>
        /// Chat configuration
        /// </summary>
        private ChatConfiguration chatConfiguration;

        /// <summary>
        /// Chat configuration
        /// </summary>
        public ChatConfiguration ChatConfiguration
        {
            get
            {
                if (chatConfiguration == null)
                {
                    chatConfiguration = new ChatConfiguration();
                }
                return chatConfiguration;
            }
        }

        /// <summary>
        /// Update configuration service
        /// </summary>
        /// <param name="serviceProvider">Service provider</param>
        public void UpdateConfigurationService(IServiceProvider serviceProvider)
        {
            if (serviceProvider != null)
            {
                chatConfiguration = serviceProvider.GetService<ChatConfiguration>();
            }
        }

        /// <summary>
        /// Is TTS enabled for guild channel
        /// </summary>
        /// <param name="guildChannel">Guild channel</param>
        /// <returns>"true" if TTS is enbabled, otherwise "false"</returns>
        private bool IsTTSEnabledForGuildChannel(SocketGuildChannel guildChannel)
        {
            return ((ChatConfiguration == null) ? false : ((guildChannel == null) ? false : ChatConfiguration.Data.GetGuildConfiguration(guildChannel.Guild).EnableTTS));
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
            return messageChannel.SendMessageAsync(text, IsTTSEnabledForGuildChannel(messageChannel as SocketGuildChannel));
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
            return messageChannel.SendMessageAsync("", IsTTSEnabledForGuildChannel(messageChannel as SocketGuildChannel), embed);
        }
    }
}
