using Discord;
using Discord.WebSocket;
using ModibotAPI;
using System;
using System.Reflection;
using System.Threading.Tasks;

/// <summary>
/// Modibot console logger namspace
/// </summary>
namespace ModibotConsoleLogger
{
    /// <summary>
    /// Console logger module class
    /// </summary>
    public class ConsoleLoggerModule : IModule
    {
        /// <summary>
        /// Module name
        /// </summary>
        public string Name => "Console logger";

        /// <summary>
        /// Module version
        /// </summary>
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Module author
        /// </summary>
        public string Author => "Ethem Kurt";

        /// <summary>
        /// Module URI
        /// </summary>
        public string URI => "https://github.com/BigETI/Modibot";

        /// <summary>
        /// Initialize (asynchronous)
        /// </summary>
        /// <param name="bot">Bot</param>
        /// <returns>Task</returns>
        public Task InitAsync(IBot bot)
        {
            DiscordSocketClient discord_client = bot.GetService<DiscordSocketClient>();
            ConsoleLogger logger = bot.GetService<ConsoleLogger>();
            ConsoleLoggerConfiguration console_logger_configuration = bot.GetService<ConsoleLoggerConfiguration>();
            logger.consoleLoggerConfiguration = console_logger_configuration;
            if ((discord_client != null) && (logger != null) && (console_logger_configuration != null))
            {
                discord_client.ChannelCreated += (channel) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.ChannelCreated, channel.ToString(), channel.Id));
                };
                discord_client.ChannelDestroyed += (channel) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.ChannelDestroyed, channel.ToString(), channel.Id));
                };
                discord_client.ChannelUpdated += (oldChannel, newChannel) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.ChannelUpdated, oldChannel.ToString(), oldChannel.Id, newChannel.ToString(), newChannel.Id));
                };
                discord_client.Connected += () =>
                {
                    return logger.LogEventAsync(console_logger_configuration.Data.Texts.Connected);
                };
                discord_client.CurrentUserUpdated += (oldUser, newUser) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.CurrentUserUpdated, oldUser.ToString(), oldUser.Id, newUser.ToString(), newUser.Id));
                };
                discord_client.Disconnected += (exception) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.Disconnected, exception.ToString()));
                };
                discord_client.GuildAvailable += (guild) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.GuildAvailable, guild.ToString(), guild.Id));
                };
                discord_client.GuildMembersDownloaded += (guild) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.GuildMembersDownloaded, guild.ToString(), guild.Id));
                };
                discord_client.GuildMemberUpdated += (oldGuildUser, newGuildUser) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.GuildMemberUpdated, oldGuildUser.ToString(), oldGuildUser.Id, newGuildUser.ToString(), newGuildUser.Id));
                };
                discord_client.GuildUnavailable += (guild) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.GuildUnavailable, guild.ToString(), guild.Id));
                };
                discord_client.GuildUpdated += (oldGuild, newGuild) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.GuildUpdated, oldGuild.ToString(), oldGuild.Id, newGuild.ToString(), newGuild.Id));
                };
                discord_client.JoinedGuild += (guild) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.JoinedGuild, guild.ToString(), guild.Id));
                };
                discord_client.LatencyUpdated += (oldLatency, newLatency) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.LatencyUpdated, oldLatency, newLatency));
                };
                discord_client.LeftGuild += (guild) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.LeftGuild, guild.ToString(), guild.Id));
                };
                discord_client.Log += (log) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.Log, log));
                };
                discord_client.LoggedIn += () =>
                {
                    return logger.LogEventAsync(console_logger_configuration.Data.Texts.LoggedIn);
                };
                discord_client.LoggedOut += () =>
                {
                    return logger.LogEventAsync(console_logger_configuration.Data.Texts.LoggedOut);
                };
                discord_client.MessageDeleted += async (message, messageChannel) =>
                {
                    IMessage deleted_message = await message.GetOrDownloadAsync();
                    if (deleted_message != null)
                    {
                        await logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.MessageDeleted, deleted_message.Content, deleted_message.Id, messageChannel.Name, messageChannel.Id));
                    }
                };
                discord_client.MessageReceived += async (message) =>
                {
                    await logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.MessageReceived, message.Content, message.Id));
                };
                discord_client.MessageUpdated += async (oldMessage, newMessage, messageChannel) =>
                {
                    IMessage old_message = await oldMessage.GetOrDownloadAsync();
                    if (old_message != null)
                    {
                        await logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.MessageUpdated, old_message.Content, old_message.Id, newMessage.Content, newMessage.Id, messageChannel.Name, messageChannel.Id));
                    }
                };
                discord_client.ReactionAdded += async (userMessage, messageChannel, reaction) =>
                {
                    IUserMessage user_message = await userMessage.GetOrDownloadAsync();
                    if (user_message != null)
                    {
                        await logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.ReactionAdded, user_message.Content, user_message.Id, messageChannel.Name, messageChannel.Id, reaction.User.Value.ToString(), reaction.Emote.Name));
                    }
                };
                discord_client.ReactionRemoved += async (userMessage, messageChannel, reaction) =>
                {
                    IUserMessage user_message = await userMessage.GetOrDownloadAsync();
                    if (user_message != null)
                    {
                        await logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.ReactionRemoved, user_message.Content, user_message.Id, messageChannel.Name, messageChannel.Id, reaction.User.Value.ToString(), reaction.Emote.Name));
                    }
                };
                discord_client.ReactionsCleared += async (userMessage, messageChannel) =>
                {
                    IUserMessage user_message = await userMessage.GetOrDownloadAsync();
                    if (user_message != null)
                    {
                        await logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.ReactionsCleared, user_message.Content, user_message.Id, messageChannel.Name, messageChannel.Id));
                    }
                };
                discord_client.Ready += () =>
                {
                    return logger.LogEventAsync(console_logger_configuration.Data.Texts.Ready);
                };
                discord_client.RecipientAdded += (groupUser) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.RecipientAdded, groupUser.ToString(), groupUser.Id));
                };
                discord_client.RecipientRemoved += (groupUser) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.RecipientRemoved, groupUser.ToString(), groupUser.Id));
                };
                discord_client.RoleCreated += (role) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.RoleCreated, role.ToString(), role.Id));
                };
                discord_client.RoleDeleted += (role) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.RoleDeleted, role.ToString(), role.Id));
                };
                discord_client.RoleUpdated += (oldRole, newRole) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.RoleUpdated, oldRole.ToString(), oldRole.Id, newRole.ToString(), newRole.Id));
                };
                discord_client.UserBanned += (user, guild) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.UserBanned, user.ToString(), user.Id, guild.ToString(), guild.Id));
                };
                discord_client.UserIsTyping += (user, messageChannel) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.UserIsTyping, user.ToString(), user.Id, messageChannel.Name, messageChannel.Id));
                };
                discord_client.UserJoined += (guildUser) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.UserJoined, guildUser.ToString(), guildUser.Id, guildUser.Guild.ToString(), guildUser.Guild.Id));
                };
                discord_client.UserLeft += (guildUser) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.UserLeft, guildUser.ToString(), guildUser.Id, guildUser.Guild.ToString(), guildUser.Guild.Id));
                };
                discord_client.UserUnbanned += (user, guild) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.UserUnbanned, user.ToString(), user.Id, guild.ToString(), guild.Id));
                };
                discord_client.UserUpdated += (oldUser, newUser) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.UserUpdated, oldUser.ToString(), oldUser.Id, newUser.ToString(), newUser.Id));
                };
                discord_client.UserVoiceStateUpdated += (user, oldVoiceState, newVoiceState) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.UserVoiceStateUpdated, user.ToString(), user.Id, oldVoiceState.ToString(), oldVoiceState.VoiceSessionId, oldVoiceState.IsDeafened, oldVoiceState.IsMuted, oldVoiceState.IsSelfDeafened, oldVoiceState.IsSelfMuted, oldVoiceState.IsSuppressed, newVoiceState.ToString(), newVoiceState.VoiceSessionId, newVoiceState.IsDeafened, newVoiceState.IsMuted, newVoiceState.IsSelfDeafened, newVoiceState.IsSelfMuted, newVoiceState.IsSuppressed));
                };
                discord_client.VoiceServerUpdated += (voiceServer) =>
                {
                    return logger.LogEventAsync(string.Format(console_logger_configuration.Data.Texts.VoiceServerUpdated, voiceServer.Endpoint, voiceServer.Token, voiceServer.Guild.Value.ToString(), voiceServer.Guild.Value.Id));
                };
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Exit (asynchronous)
        /// </summary>
        /// <returns>Task</returns>
        public Task ExitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
