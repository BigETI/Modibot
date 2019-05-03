/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Logger texts configuration data interface
    /// </summary>
    public interface ILoggerTextsConfigurationData
    {
        /// <summary>
        /// Channel created text
        /// </summary>
        string ChannelCreated { get; }

        /// <summary>
        /// Channel destroyed text
        /// </summary>
        string ChannelDestroyed { get; }

        /// <summary>
        /// Channel updated text
        /// </summary>
        string ChannelUpdated { get; }

        /// <summary>
        /// Connected text
        /// </summary>
        string Connected { get; }

        /// <summary>
        /// Current user updated text
        /// </summary>
        string CurrentUserUpdated { get; }

        /// <summary>
        /// Disconnected text
        /// </summary>
        string Disconnected { get; }

        /// <summary>
        /// Guild available text
        /// </summary>
        string GuildAvailable { get; }

        /// <summary>
        /// Guild members downloaded text
        /// </summary>
        string GuildMembersDownloaded { get; }

        /// <summary>
        /// Guild member updated text
        /// </summary>
        string GuildMemberUpdated { get; }

        /// <summary>
        /// Guild unavailable text
        /// </summary>
        string GuildUnavailable { get; }

        /// <summary>
        /// Guild updated text
        /// </summary>
        string GuildUpdated { get; }

        /// <summary>
        /// Joined guild text
        /// </summary>
        string JoinedGuild { get; }

        /// <summary>
        /// Latency updated text
        /// </summary>
        string LatencyUpdated { get; }

        /// <summary>
        /// Left guild text
        /// </summary>
        string LeftGuild { get; }

        /// <summary>
        /// Log text
        /// </summary>
        string Log { get; }

        /// <summary>
        /// Logged in text
        /// </summary>
        string LoggedIn { get; }

        /// <summary>
        /// Logged out text
        /// </summary>
        string LoggedOut { get; }

        /// <summary>
        /// Message deleted text
        /// </summary>
        string MessageDeleted { get; }

        /// <summary>
        /// Message received text
        /// </summary>
        string MessageReceived { get; }

        /// <summary>
        /// Message updated text
        /// </summary>
        string MessageUpdated { get; }

        /// <summary>
        /// Reaction added text
        /// </summary>
        string ReactionAdded { get; }

        /// <summary>
        /// Reaction removed text
        /// </summary>
        string ReactionRemoved { get; }

        /// <summary>
        /// Reactions cleared text
        /// </summary>
        string ReactionsCleared { get; }

        /// <summary>
        /// Ready text
        /// </summary>
        string Ready { get; }

        /// <summary>
        /// Recipient added text
        /// </summary>
        string RecipientAdded { get; }

        /// <summary>
        /// Recipient removed text
        /// </summary>
        string RecipientRemoved { get; }

        /// <summary>
        /// Role created text
        /// </summary>
        string RoleCreated { get; }

        /// <summary>
        /// Role deleted text
        /// </summary>
        string RoleDeleted { get; }

        /// <summary>
        /// Role updated text
        /// </summary>
        string RoleUpdated { get; }

        /// <summary>
        /// User banned text
        /// </summary>
        string UserBanned { get; }

        /// <summary>
        /// User is typing text
        /// </summary>
        string UserIsTyping { get; }

        /// <summary>
        /// User joined text
        /// </summary>
        string UserJoined { get; }

        /// <summary>
        /// User left text
        /// </summary>
        string UserLeft { get; }

        /// <summary>
        /// User unbanned text
        /// </summary>
        string UserUnbanned { get; }

        /// <summary>
        /// User updated text
        /// </summary>
        string UserUpdated { get; }

        /// <summary>
        /// User voice state updated text
        /// </summary>
        string UserVoiceStateUpdated { get; }

        /// <summary>
        /// Voice server updated text
        /// </summary>
        string VoiceServerUpdated { get; }
    }
}
