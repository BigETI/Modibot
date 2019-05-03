using Discord;
using Discord.WebSocket;
using Modibot.Services;
using ModibotAPI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Bot class
    /// </summary>
    internal class Bot : IBot, IDisposable
    {
        /// <summary>
        /// Discord client
        /// </summary>
        private readonly DiscordSocketClient discordClient;

        /// <summary>
        /// Services
        /// </summary>
        public ModibotAPI.IServiceProvider Services { get; private set; } = new ServiceProvider();

        /// <summary>
        /// Commands
        /// </summary>
        private Commands commands;

        /// <summary>
        /// Configuration
        /// </summary>
        private Configuration configuration;

        /// <summary>
        /// Logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// Null logger
        /// </summary>
        private NullLogger nullLogger = new NullLogger();

        /// <summary>
        /// chat
        /// </summary>
        private Chat chat;

        /// <summary>
        /// Modules
        /// </summary>
        private Modules modules;

        /// <summary>
        /// Commands
        /// </summary>
        public Commands Commands
        {
            get
            {
                if (commands == null)
                {
                    commands = Services.RequireService<Commands>();
                }
                return commands;
            }
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public Configuration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    configuration = Services.RequireService<Configuration>();
                }
                return configuration;
            }
        }

        /// <summary>
        /// Logger
        /// </summary>
        public ILogger Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = Services.GetService<ILogger>();
                }
                return ((logger == null) ? nullLogger : logger);
            }
        }

        /// <summary>
        /// Chat
        /// </summary>
        public Chat Chat
        {
            get
            {
                if (chat == null)
                {
                    chat = Services.RequireService<Chat>();
                }
                return chat;
            }
        }

        /// <summary>
        /// Modules
        /// </summary>
        public Modules Modules
        {
            get
            {
                if (modules == null)
                {
                    modules = Services.RequireService<Modules>();
                }
                return modules;
            }
        }

        /// <summary>
        /// Loaded services
        /// </summary>
        public ReadOnlyCollection<object> LoadedServices => Services.LoadedServices;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Bot()
        {
            Type[] types = Assembly.GetAssembly(typeof(Configuration)).GetTypes();
            if (types != null)
            {
                foreach (Type type in types)
                {
                    if (type != null)
                    {
                        if (type.IsClass)
                        {
                            object instance = null;
                            if (Attribute.IsDefined(type, typeof(ServiceAttribute)))
                            {
                                instance = Services.RequireService(type);
                            }
                            if (typeof(ICommand).IsAssignableFrom(type))
                            {
                                instance = ((instance == null) ? Activator.CreateInstance(type) : instance);
                                Commands.AddCommand((ICommand)instance);
                            }
                            if (typeof(ICommandGroup).IsAssignableFrom(type))
                            {
                                instance = ((instance == null) ? Activator.CreateInstance(type) : instance);
                                Commands.AddCommandGroup((ICommandGroup)instance);
                            }
                        }
                    }
                }
            }
            discordClient = Services.RequireService<DiscordSocketClient>();

            if (Directory.Exists(Configuration.Modules.ModulesPath))
            {
                string[] paths = Directory.GetFiles(Configuration.Modules.ModulesPath, "*.dll");
                if (paths != null)
                {
                    foreach (string path in paths)
                    {
                        Modules.LoadModule(path);
                    }
                }
            }

            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                Assembly ret = null;
                string[] parts = args.Name.Split(",");
                if (parts != null)
                {
                    if (parts.Length > 0)
                    {
                        string assembly_name = parts[0];
                        if (assembly_name != null)
                        {
                            if (assembly_name.Length > 0)
                            {
                                string full_path = Path.GetFullPath(Path.Combine("dependencies", assembly_name));
                                try
                                {
                                    Assembly assembly = Assembly.LoadFile(full_path);
                                    if (assembly != null)
                                    {
                                        if (assembly.FullName == args.Name)
                                        {
                                            ret = assembly;
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    Logger.ErrorOutput.WriteLine(e);
                                }
                            }
                        }
                    }
                }
                return ret;
            };
        }

        /// <summary>
        /// Main (asynchronous)
        /// </summary>
        /// <returns></returns>
        public async Task MainAsync()
        {
            string token = Configuration.BotToken;
            if (token.Length <= 0)
            {
                try
                {
                    token = Environment.GetEnvironmentVariable("token");
                }
                catch (Exception e)
                {
                    Logger.ErrorOutput.WriteLine(e);
                }
            }
            if (token == null)
            {
                Logger.ErrorOutput.WriteLine("Please provide a bot token through the configuration file or through the environment variable \"token\".");
            }
            else
            {
                discordClient.ChannelCreated += (channel) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.ChannelCreated, channel.ToString(), channel.Id));
                };
                discordClient.ChannelDestroyed += (channel) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.ChannelDestroyed, channel.ToString(), channel.Id));
                };
                discordClient.ChannelUpdated += (oldChannel, newChannel) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.ChannelUpdated, oldChannel.ToString(), oldChannel.Id, newChannel.ToString(), newChannel.Id));
                };
                discordClient.Connected += () =>
                {
                    return Logger.LogEventAsync(Configuration.Logger.Texts.Connected);
                };
                discordClient.CurrentUserUpdated += (oldUser, newUser) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.CurrentUserUpdated, oldUser.ToString(), oldUser.Id, newUser.ToString(), newUser.Id));
                };
                discordClient.Disconnected += (exception) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.Disconnected, exception.ToString()));
                };
                discordClient.GuildAvailable += (guild) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.GuildAvailable, guild.ToString(), guild.Id));
                };
                discordClient.GuildMembersDownloaded += (guild) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.GuildMembersDownloaded, guild.ToString(), guild.Id));
                };
                discordClient.GuildMemberUpdated += (oldGuildUser, newGuildUser) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.GuildMemberUpdated, oldGuildUser.ToString(), oldGuildUser.Id, newGuildUser.ToString(), newGuildUser.Id));
                };
                discordClient.GuildUnavailable += (guild) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.GuildUnavailable, guild.ToString(), guild.Id));
                };
                discordClient.GuildUpdated += (oldGuild, newGuild) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.GuildUpdated, oldGuild.ToString(), oldGuild.Id, newGuild.ToString(), newGuild.Id));
                };
                discordClient.JoinedGuild += (guild) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.JoinedGuild, guild.ToString(), guild.Id));
                };
                discordClient.LatencyUpdated += (oldLatency, newLatency) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.LatencyUpdated, oldLatency, newLatency));
                };
                discordClient.LeftGuild += (guild) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.LeftGuild, guild.ToString(), guild.Id));
                };
                discordClient.Log += (log) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.Log, log));
                };
                discordClient.LoggedIn += () =>
                {
                    return Logger.LogEventAsync(Configuration.Logger.Texts.LoggedIn);
                };
                discordClient.LoggedOut += () =>
                {
                    return Logger.LogEventAsync(Configuration.Logger.Texts.LoggedOut);
                };
                discordClient.MessageDeleted += async (message, messageChannel) =>
                {
                    IMessage deleted_message = await message.GetOrDownloadAsync();
                    if (deleted_message != null)
                    {
                        await Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.MessageDeleted, deleted_message.Content, deleted_message.Id, messageChannel.Name, messageChannel.Id));
                    }
                };
                discordClient.MessageReceived += async (message) =>
                {
                    if (message.Author.Id != discordClient.CurrentUser.Id)
                    {
                        if (message.Content != null)
                        {
                            string possible_command = message.Content.TrimStart();
                            if (possible_command.StartsWith(Configuration.Commands.Delimiter))
                            {
                                string command = possible_command.Substring(Configuration.Commands.Delimiter.Length);
                                ICommandResult command_result = await Commands.ExecuteAsync(command, message.Author, message.Channel, this);
                                string msg = null;
                                switch (command_result.Result)
                                {
                                    case ECommandResult.Failed:
                                        msg = string.Format(Configuration.Commands.FailedText, command_result.CompiledCommand.CommandName, command_result.CompiledCommand.CompiledCommandArguments.RawArguments);
                                        break;
                                    case ECommandResult.Successful:
                                        msg = string.Format(Configuration.Commands.SuccessfulText, command_result.CompiledCommand.CommandName, command_result.CompiledCommand.CompiledCommandArguments.RawArguments);
                                        break;
                                    case ECommandResult.Denied:
                                        msg = string.Format(Configuration.Commands.DeniedText, command_result.CompiledCommand.CommandName, command_result.CompiledCommand.CompiledCommandArguments.RawArguments);
                                        break;
                                }
                                if (msg != null)
                                {
                                    if (msg.Length > 0)
                                    {
                                        await Chat.SendMessageAsync(msg, message.Channel);
                                        if (command_result.Result == ECommandResult.Failed)
                                        {
                                            await Commands.ExecuteAsync("help " + command, message.Author, message.Channel, this);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    await Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.MessageReceived, message.Content, message.Id));
                };
                discordClient.MessageUpdated += async (oldMessage, newMessage, messageChannel) =>
                {
                    IMessage old_message = await oldMessage.GetOrDownloadAsync();
                    if (old_message != null)
                    {
                        await Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.MessageUpdated, old_message.Content, old_message.Id, newMessage.Content, newMessage.Id, messageChannel.Name, messageChannel.Id));
                    }
                };
                discordClient.ReactionAdded += async (userMessage, messageChannel, reaction) =>
                {
                    IUserMessage user_message = await userMessage.GetOrDownloadAsync();
                    if (user_message != null)
                    {
                        await Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.ReactionAdded, user_message.Content, user_message.Id, messageChannel.Name, messageChannel.Id, reaction.User.Value.ToString(), reaction.Emote.Name));
                    }
                };
                discordClient.ReactionRemoved += async (userMessage, messageChannel, reaction) =>
                {
                    IUserMessage user_message = await userMessage.GetOrDownloadAsync();
                    if (user_message != null)
                    {
                        await Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.ReactionRemoved, user_message.Content, user_message.Id, messageChannel.Name, messageChannel.Id, reaction.User.Value.ToString(), reaction.Emote.Name));
                    }
                };
                discordClient.ReactionsCleared += async (userMessage, messageChannel) =>
                {
                    IUserMessage user_message = await userMessage.GetOrDownloadAsync();
                    if (user_message != null)
                    {
                        await Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.ReactionsCleared, user_message.Content, user_message.Id, messageChannel.Name, messageChannel.Id));
                    }
                };
                discordClient.Ready += () =>
                {
                    return Logger.LogEventAsync(Configuration.Logger.Texts.Ready);
                };
                discordClient.RecipientAdded += (groupUser) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.RecipientAdded, groupUser.ToString(), groupUser.Id));
                };
                discordClient.RecipientRemoved += (groupUser) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.RecipientRemoved, groupUser.ToString(), groupUser.Id));
                };
                discordClient.RoleCreated += (role) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.RoleCreated, role.ToString(), role.Id));
                };
                discordClient.RoleDeleted += (role) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.RoleDeleted, role.ToString(), role.Id));
                };
                discordClient.RoleUpdated += (oldRole, newRole) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.RoleUpdated, oldRole.ToString(), oldRole.Id, newRole.ToString(), newRole.Id));
                };
                discordClient.UserBanned += (user, guild) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.UserBanned, user.ToString(), user.Id, guild.ToString(), guild.Id));
                };
                discordClient.UserIsTyping += (user, messageChannel) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.UserIsTyping, user.ToString(), user.Id, messageChannel.Name, messageChannel.Id));
                };
                discordClient.UserJoined += (guildUser) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.UserJoined, guildUser.ToString(), guildUser.Id, guildUser.Guild.ToString(), guildUser.Guild.Id));
                };
                discordClient.UserLeft += (guildUser) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.UserLeft, guildUser.ToString(), guildUser.Id, guildUser.Guild.ToString(), guildUser.Guild.Id));
                };
                discordClient.UserUnbanned += (user, guild) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.UserUnbanned, user.ToString(), user.Id, guild.ToString(), guild.Id));
                };
                discordClient.UserUpdated += (oldUser, newUser) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.UserUpdated, oldUser.ToString(), oldUser.Id, newUser.ToString(), newUser.Id));
                };
                discordClient.UserVoiceStateUpdated += (user, oldVoiceState, newVoiceState) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.UserVoiceStateUpdated, user.ToString(), user.Id, oldVoiceState.ToString(), oldVoiceState.VoiceSessionId, oldVoiceState.IsDeafened, oldVoiceState.IsMuted, oldVoiceState.IsSelfDeafened, oldVoiceState.IsSelfMuted, oldVoiceState.IsSuppressed, newVoiceState.ToString(), newVoiceState.VoiceSessionId, newVoiceState.IsDeafened, newVoiceState.IsMuted, newVoiceState.IsSelfDeafened, newVoiceState.IsSelfMuted, newVoiceState.IsSuppressed));
                };
                discordClient.VoiceServerUpdated += (voiceServer) =>
                {
                    return Logger.LogEventAsync(string.Format(Configuration.Logger.Texts.VoiceServerUpdated, voiceServer.Endpoint, voiceServer.Token, voiceServer.Guild.Value.ToString(), voiceServer.Guild.Value.Id));
                };
                await discordClient.LoginAsync(TokenType.Bot, token);
                await discordClient.StartAsync();
                await Task.Delay(-1);
            }
        }

        /// <summary>
        /// Exit bot
        /// </summary>
        public async void Exit()
        {
            await discordClient.LogoutAsync();
            Environment.Exit(0);
        }

        /// <summary>
        /// Require service
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service if successful, otherwise "default(T)"</returns>
        public T RequireService<T>()
        {
            return Services.RequireService<T>();
        }

        /// <summary>
        /// Require service
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Service if successful, otherwise "null"</returns>
        public object RequireService(Type serviceType)
        {
            return Services.RequireService(serviceType);
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Service if successful, otherwise "default(T)"</returns>
        public T GetService<T>()
        {
            return Services.GetService<T>();
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceTypeName">Service type name</param>
        /// <returns>Service if successful, otherwise "null"</returns>
        public object GetService(string serviceTypeName)
        {
            return Services.GetService(serviceTypeName);
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Service if successful, otherwise "null"</returns>
        public object GetService(Type serviceType)
        {
            return Services.GetService(serviceType);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)Services).Dispose();
        }
    }
}
