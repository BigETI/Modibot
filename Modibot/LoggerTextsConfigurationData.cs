using ModibotAPI;
using System.Runtime.Serialization;

namespace Modibot
{
    [DataContract]
    internal class LoggerTextsConfigurationData : ILoggerTextsConfigurationData
    {

        /// <summary>
        /// Default channel created text
        /// </summary>
        public static readonly string DefaultChannelCreated = "Channel \"{0}\" ({1}) has been created.";

        /// <summary>
        /// Default channel destroyed text
        /// </summary>
        public static readonly string DefaultChannelDestroyed = "Channel \"{0}\" ({1}) has been destroyed.";

        /// <summary>
        /// Default channel updated text
        /// </summary>
        public static readonly string DefaultChannelUpdated = "Channel \"{0}\" ({1}) has been updated to \"{2}\" ({3}).";

        /// <summary>
        /// Default connected text
        /// </summary>
        public static readonly string DefaultConnected = "Successfully connected to Discord!";

        /// <summary>
        /// Default current user updated text
        /// </summary>
        public static readonly string DefaultCurrentUserUpdated = "User {0} ({1}) has been updated to {2} ({3}).";

        /// <summary>
        /// Default disconnected text
        /// </summary>
        public static readonly string DefaultDisconnected = "Disconnected from Discord!";

        /// <summary>
        /// Default guild available text
        /// </summary>
        public static readonly string DefaultGuildAvailable = "Guild {0} ({1}) is available.";

        /// <summary>
        /// Default guild members downloaded text
        /// </summary>
        public static readonly string DefaultGuildMembersDownloaded = "Guild member information in {0} ({1}) have been downloaded.";

        /// <summary>
        /// Default guild member updated text
        /// </summary>
        public static readonly string DefaultGuildMemberUpdated = "Guild member {0} ({1}) has been updated to {2} ({3}).";

        /// <summary>
        /// Default guild unavailable text
        /// </summary>
        public static readonly string DefaultGuildUnavailable = "Guild {0} ({1}) is unavailable.";

        /// <summary>
        /// Default guild updated text
        /// </summary>
        public static readonly string DefaultGuildUpdated = "Guild {0} ({1}) has been updated to {2} ({3}).";

        /// <summary>
        /// Default joined guild text
        /// </summary>
        public static readonly string DefaultJoinedGuild = "Successfully joined guild {0} ({1}).";

        /// <summary>
        /// Default latency updated text
        /// </summary>
        public static readonly string DefaultLatencyUpdated = "Latency has been updated from {0} ms to {1} ms.";

        /// <summary>
        /// Default left guild text
        /// </summary>
        public static readonly string DefaultLeftGuild = "Left guild {0} ({1}).";

        /// <summary>
        /// Default log text
        /// </summary>
        public static readonly string DefaultLog = "{0}";

        /// <summary>
        /// Default logged in text
        /// </summary>
        public static readonly string DefaultLoggedIn = "Successfully logged in!";

        /// <summary>
        /// Default logged out text
        /// </summary>
        public static readonly string DefaultLoggedOut = "Logged out!";

        /// <summary>
        /// Default message deleted text
        /// </summary>
        public static readonly string DefaultMessageDeleted = "Message \"{0}\" ({1}) in {2} ({3}) has been deleted.";

        /// <summary>
        /// Default message received text
        /// </summary>
        public static readonly string DefaultMessageReceived = "Message: \"{0}\" ({1})";

        /// <summary>
        /// Default message updated text
        /// </summary>
        public static readonly string DefaultMessageUpdated = "Message \"{0}\" ({1}) has been updated to \"{2}\" ({3}) in {4} ({5}).";

        /// <summary>
        /// Default reaction added text
        /// </summary>
        public static readonly string DefaultReactionAdded = "Reaction \"{5}\" was added to \"{0}\" ({1}) in {2} ({3}) by {4}.";

        /// <summary>
        /// Default reaction removed text
        /// </summary>
        public static readonly string DefaultReactionRemoved = "Reaction \"{5}\" has been removed \"{0}\" ({1}) in {2} ({3}) from {4}.";

        /// <summary>
        /// Default reactions cleared text
        /// </summary>
        public static readonly string DefaultReactionsCleared = "Reactions have been cleared at \"{0}\" ({1}) in {2} ({3}).";

        /// <summary>
        /// Default ready text
        /// </summary>
        public static readonly string DefaultReady = "Discord client is now ready!";

        /// <summary>
        /// Default recipient added text
        /// </summary>
        public static readonly string DefaultRecipientAdded = "Recipient {0} ({1}) has been added.";

        /// <summary>
        /// Default recipient removed text
        /// </summary>
        public static readonly string DefaultRecipientRemoved = "Recipient {0} ({1}) has been removed.";

        /// <summary>
        /// Default role created text
        /// </summary>
        public static readonly string DefaultRoleCreated = "Role {0} ({1}) has been created.";

        /// <summary>
        /// Default role deleted text
        /// </summary>
        public static readonly string DefaultRoleDeleted = "Role {0} ({1}) has been deleted.";

        /// <summary>
        /// Default role updated text
        /// </summary>
        public static readonly string DefaultRoleUpdated = "Role {0} ({1}) has been updated to {2} ({3}).";

        /// <summary>
        /// Default user banned text
        /// </summary>
        public static readonly string DefaultUserBanned = "User {0} ({1}) has been banned from {2} ({3}).";

        /// <summary>
        /// Default user is typing text
        /// </summary>
        public static readonly string DefaultUserIsTyping = "User {0} ({1}) is typing in {2} ({3}).";

        /// <summary>
        /// Default user joined text
        /// </summary>
        public static readonly string DefaultUserJoined = "User {0} ({1}) has joined {2} ({3}).";

        /// <summary>
        /// Default user left text
        /// </summary>
        public static readonly string DefaultUserLeft = "User {0} ({1}) has left {2} ({3}).";

        /// <summary>
        /// Default user unbanned text
        /// </summary>
        public static readonly string DefaultUserUnbanned = "User {0} ({1}) has been unbanned from {2} ({3}).";

        /// <summary>
        /// Default user updated text
        /// </summary>
        public static readonly string DefaultUserUpdated = "User {0} ({1}) has been updated to {2} ({3})";

        /// <summary>
        /// Default user voice state updated text
        /// </summary>
        public static readonly string DefaultUserVoiceStateUpdated = "User {0} ({1}) voice status \"{2}\" ({3}) (Deafened: {4}; Muted: {5}; Self deafened: {6}; Self muted: {7}; Supressed: {8}) has been updated to \"{9}\" ({10}) (Deafened: {11}; Muted: {12}; Self deafened: {13}; Self muted: {14}; Supressed: {15}).";

        /// <summary>
        /// Default voice server updated text
        /// </summary>
        public static readonly string DefaultVoiceServerUpdated = "Voice server in {2} ({3}) has been updated to {0} ({1}).";

        /// <summary>
        /// Channel created text
        /// </summary>
        [DataMember]
        private string channelCreated;

        /// <summary>
        /// Channel destroyed text
        /// </summary>
        [DataMember]
        private string channelDestroyed;

        /// <summary>
        /// Channel updated text
        /// </summary>
        [DataMember]
        private string channelUpdated;

        /// <summary>
        /// Connected text
        /// </summary>
        [DataMember]
        private string connected;

        /// <summary>
        /// Current user updated text
        /// </summary>
        [DataMember]
        private string currentUserUpdated;

        /// <summary>
        /// Disconnected text
        /// </summary>
        [DataMember]
        private string disconnected;

        /// <summary>
        /// Guild available text
        /// </summary>
        [DataMember]
        private string guildAvailable;

        /// <summary>
        /// Guild members downloaded text
        /// </summary>
        [DataMember]
        private string guildMembersDownloaded;

        /// <summary>
        /// Guild member updated text
        /// </summary>
        [DataMember]
        private string guildMemberUpdated;

        /// <summary>
        /// Guild unavailable text
        /// </summary>
        [DataMember]
        private string guildUnavailable;

        /// <summary>
        /// Guild updated text
        /// </summary>
        [DataMember]
        private string guildUpdated;

        /// <summary>
        /// Joined guild text
        /// </summary>
        [DataMember]
        private string joinedGuild;

        /// <summary>
        /// Latency updated text
        /// </summary>
        [DataMember]
        private string latencyUpdated;

        /// <summary>
        /// Left guild text
        /// </summary>
        [DataMember]
        private string leftGuild;

        /// <summary>
        /// Log text
        /// </summary>
        [DataMember]
        private string log;

        /// <summary>
        /// Logged in text
        /// </summary>
        [DataMember]
        private string loggedIn;

        /// <summary>
        /// Logged out text
        /// </summary>
        [DataMember]
        private string loggedOut;

        /// <summary>
        /// Message deleted text
        /// </summary>
        [DataMember]
        private string messageDeleted;

        /// <summary>
        /// Message received text
        /// </summary>
        [DataMember]
        private string messageReceived;

        /// <summary>
        /// Message updated text
        /// </summary>
        [DataMember]
        private string messageUpdated;

        /// <summary>
        /// Reaction added text
        /// </summary>
        [DataMember]
        private string reactionAdded;

        /// <summary>
        /// Reaction removed text
        /// </summary>
        [DataMember]
        private string reactionRemoved;

        /// <summary>
        /// Reactions cleared text
        /// </summary>
        [DataMember]
        private string reactionsCleared;

        /// <summary>
        /// Ready text
        /// </summary>
        [DataMember]
        private string ready;

        /// <summary>
        /// Recipient added text
        /// </summary>
        [DataMember]
        private string recipientAdded;

        /// <summary>
        /// Recipient removed text
        /// </summary>
        [DataMember]
        private string recipientRemoved;

        /// <summary>
        /// Role created text
        /// </summary>
        [DataMember]
        private string roleCreated;

        /// <summary>
        /// Role deleted text
        /// </summary>
        [DataMember]
        private string roleDeleted;

        /// <summary>
        /// Role updated text
        /// </summary>
        [DataMember]
        private string roleUpdated;

        /// <summary>
        /// User banned text
        /// </summary>
        [DataMember]
        private string userBanned;

        /// <summary>
        /// User is typing text
        /// </summary>
        [DataMember]
        private string userIsTyping;

        /// <summary>
        /// User joined text
        /// </summary>
        [DataMember]
        private string userJoined;

        /// <summary>
        /// User left text
        /// </summary>
        [DataMember]
        private string userLeft;

        /// <summary>
        /// User unbanned text
        /// </summary>
        [DataMember]
        private string userUnbanned;

        /// <summary>
        /// User updated text
        /// </summary>
        [DataMember]
        private string userUpdated;

        /// <summary>
        /// User voice state updated text
        /// </summary>
        [DataMember]
        private string userVoiceStateUpdated;

        /// <summary>
        /// Voice server updated text
        /// </summary>
        [DataMember]
        private string voiceServerUpdated;

        /// <summary>
        /// Channel created text
        /// </summary>
        public string ChannelCreated
        {
            get
            {
                if (channelCreated == null)
                {
                    channelCreated = DefaultChannelCreated;
                }
                return channelCreated;
            }
        }

        /// <summary>
        /// Channel destroyed text
        /// </summary>
        public string ChannelDestroyed
        {
            get
            {
                if (channelDestroyed == null)
                {
                    channelDestroyed = DefaultChannelDestroyed;
                }
                return channelDestroyed;
            }
        }

        /// <summary>
        /// Channel updated text
        /// </summary>
        public string ChannelUpdated
        {
            get
            {
                if (channelUpdated == null)
                {
                    channelUpdated = DefaultChannelUpdated;
                }
                return channelUpdated;
            }
        }

        /// <summary>
        /// Connected text
        /// </summary>
        public string Connected
        {
            get
            {
                if (connected == null)
                {
                    connected = DefaultConnected;
                }
                return connected;
            }
        }

        /// <summary>
        /// Current user updated text
        /// </summary>
        public string CurrentUserUpdated
        {
            get
            {
                if (currentUserUpdated == null)
                {
                    currentUserUpdated = DefaultCurrentUserUpdated;
                }
                return currentUserUpdated;
            }
        }

        /// <summary>
        /// Disconnected text
        /// </summary>
        public string Disconnected
        {
            get
            {
                if (disconnected == null)
                {
                    disconnected = DefaultDisconnected;
                }
                return disconnected;
            }
        }

        /// <summary>
        /// Guild available text
        /// </summary>
        public string GuildAvailable
        {
            get
            {
                if (guildAvailable == null)
                {
                    guildAvailable = DefaultGuildAvailable;
                }
                return guildAvailable;
            }
        }

        /// <summary>
        /// Guild members downloaded text
        /// </summary>
        public string GuildMembersDownloaded
        {
            get
            {
                if (guildMembersDownloaded == null)
                {
                    guildMembersDownloaded = DefaultGuildMembersDownloaded;
                }
                return guildMembersDownloaded;
            }
        }

        /// <summary>
        /// Guild member updated text
        /// </summary>
        public string GuildMemberUpdated
        {
            get
            {
                if (guildMemberUpdated == null)
                {
                    guildMemberUpdated = DefaultGuildMemberUpdated;
                }
                return guildMemberUpdated;
            }
        }

        /// <summary>
        /// Guild unavailable text
        /// </summary>
        public string GuildUnavailable
        {
            get
            {
                if (guildUnavailable == null)
                {
                    guildUnavailable = DefaultGuildUnavailable;
                }
                return guildUnavailable;
            }
        }

        /// <summary>
        /// Guild updated text
        /// </summary>
        public string GuildUpdated
        {
            get
            {
                if (guildUpdated == null)
                {
                    guildUpdated = DefaultGuildUpdated;
                }
                return guildUpdated;
            }
        }

        /// <summary>
        /// Joined guild text
        /// </summary>
        public string JoinedGuild
        {
            get
            {
                if (joinedGuild == null)
                {
                    joinedGuild = DefaultJoinedGuild;
                }
                return joinedGuild;
            }
        }

        /// <summary>
        /// Latency updated text
        /// </summary>
        public string LatencyUpdated
        {
            get
            {
                if (latencyUpdated == null)
                {
                    latencyUpdated = DefaultLatencyUpdated;
                }
                return latencyUpdated;
            }
        }

        /// <summary>
        /// Left guild text
        /// </summary>
        public string LeftGuild
        {
            get
            {
                if (leftGuild == null)
                {
                    leftGuild = DefaultLeftGuild;
                }
                return leftGuild;
            }
        }

        /// <summary>
        /// Log text
        /// </summary>
        public string Log
        {
            get
            {
                if (log == null)
                {
                    log = DefaultLog;
                }
                return log;
            }
        }

        /// <summary>
        /// Logged in text
        /// </summary>
        public string LoggedIn
        {
            get
            {
                if (loggedIn == null)
                {
                    loggedIn = DefaultLoggedIn;
                }
                return loggedIn;
            }
        }

        /// <summary>
        /// Logged out text
        /// </summary>
        public string LoggedOut
        {
            get
            {
                if (loggedOut == null)
                {
                    loggedOut = DefaultLoggedOut;
                }
                return loggedOut;
            }
        }

        /// <summary>
        /// Message deleted text
        /// </summary>
        public string MessageDeleted
        {
            get
            {
                if (messageDeleted == null)
                {
                    messageDeleted = DefaultMessageDeleted;
                }
                return messageDeleted;
            }
        }

        /// <summary>
        /// Message received text
        /// </summary>
        public string MessageReceived
        {
            get
            {
                if (messageReceived == null)
                {
                    messageReceived = DefaultMessageReceived;
                }
                return messageReceived;
            }
        }

        /// <summary>
        /// Message updated text
        /// </summary>
        public string MessageUpdated
        {
            get
            {
                if (messageUpdated == null)
                {
                    messageUpdated = DefaultMessageUpdated;
                }
                return messageUpdated;
            }
        }

        /// <summary>
        /// Reaction added text
        /// </summary>
        public string ReactionAdded
        {
            get
            {
                if (reactionAdded == null)
                {
                    reactionAdded = DefaultReactionAdded;
                }
                return reactionAdded;
            }
        }

        /// <summary>
        /// Reaction removed text
        /// </summary>
        public string ReactionRemoved
        {
            get
            {
                if (reactionRemoved == null)
                {
                    reactionRemoved = DefaultReactionRemoved;
                }
                return reactionRemoved;
            }
        }

        /// <summary>
        /// Reactions cleared text
        /// </summary>
        public string ReactionsCleared
        {
            get
            {
                if (reactionsCleared == null)
                {
                    reactionsCleared = DefaultReactionsCleared;
                }
                return reactionsCleared;
            }
        }

        /// <summary>
        /// Ready text
        /// </summary>
        public string Ready
        {
            get
            {
                if (ready == null)
                {
                    ready = DefaultReady;
                }
                return ready;
            }
        }

        /// <summary>
        /// Recipient added text
        /// </summary>
        public string RecipientAdded
        {
            get
            {
                if (recipientAdded == null)
                {
                    recipientAdded = DefaultRecipientAdded;
                }
                return recipientAdded;
            }
        }

        /// <summary>
        /// Recipient removed text
        /// </summary>
        public string RecipientRemoved
        {
            get
            {
                if (recipientRemoved == null)
                {
                    recipientRemoved = DefaultRecipientRemoved;
                }
                return recipientRemoved;
            }
        }

        /// <summary>
        /// Role created text
        /// </summary>
        public string RoleCreated
        {
            get
            {
                if (roleCreated == null)
                {
                    roleCreated = DefaultRoleCreated;
                }
                return roleCreated;
            }
        }

        /// <summary>
        /// Role deleted text
        /// </summary>
        public string RoleDeleted
        {
            get
            {
                if (roleDeleted == null)
                {
                    roleDeleted = DefaultRoleDeleted;
                }
                return roleDeleted;
            }
        }

        /// <summary>
        /// Role updated text
        /// </summary>
        public string RoleUpdated
        {
            get
            {
                if (roleUpdated == null)
                {
                    roleUpdated = DefaultRoleUpdated;
                }
                return roleUpdated;
            }
        }

        /// <summary>
        /// User banned text
        /// </summary>
        public string UserBanned
        {
            get
            {
                if (userBanned == null)
                {
                    userBanned = DefaultUserBanned;
                }
                return userBanned;
            }
        }

        /// <summary>
        /// User is typing text
        /// </summary>
        public string UserIsTyping
        {
            get
            {
                if (userIsTyping == null)
                {
                    userIsTyping = DefaultUserIsTyping;
                }
                return userIsTyping;
            }
        }

        /// <summary>
        /// User joined text
        /// </summary>
        public string UserJoined
        {
            get
            {
                if (userJoined == null)
                {
                    userJoined = DefaultUserJoined;
                }
                return userJoined;
            }
        }

        /// <summary>
        /// User left text
        /// </summary>
        public string UserLeft
        {
            get
            {
                if (userLeft == null)
                {
                    userLeft = DefaultUserLeft;
                }
                return userLeft;
            }
        }

        /// <summary>
        /// User unbanned text
        /// </summary>
        public string UserUnbanned
        {
            get
            {
                if (userUnbanned == null)
                {
                    userUnbanned = DefaultUserUnbanned;
                }
                return userUnbanned;
            }
        }

        /// <summary>
        /// User updated text
        /// </summary>
        public string UserUpdated
        {
            get
            {
                if (userUpdated == null)
                {
                    userUpdated = DefaultUserUpdated;
                }
                return userUpdated;
            }
        }

        /// <summary>
        /// User voice state updated text
        /// </summary>
        public string UserVoiceStateUpdated
        {
            get
            {
                if (userVoiceStateUpdated == null)
                {
                    userVoiceStateUpdated = DefaultUserVoiceStateUpdated;
                }
                return userVoiceStateUpdated;
            }
        }

        /// <summary>
        /// Voice server updated text
        /// </summary>
        public string VoiceServerUpdated
        {
            get
            {
                if (voiceServerUpdated == null)
                {
                    voiceServerUpdated = DefaultVoiceServerUpdated;
                }
                return voiceServerUpdated;
            }
        }
    }
}
