using Discord.WebSocket;
using ModibotAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Modibot administration namespace
/// </summary>
namespace ModibotAdministration
{
    /// <summary>
    /// Set privilege command class
    /// </summary>
    public class SetPrivilegeCommand : ICommand
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name => "setprivilege";

        /// <summary>
        /// Command description
        /// </summary>
        public string Description => "Sets privilege for user or role";

        /// <summary>
        /// Command full description
        /// </summary>
        public string FullDescription => "This command sets privilege for a specified user or role" + Environment.NewLine + "Usage: " + Name + " <\"global\" or \"guild\"> <\"role\" or \"user\"> <user ID or role ID> <privilege> <level>";

        /// <summary>
        /// Required privileges
        /// </summary>
        public ReadOnlyDictionary<string, uint> RequiredPrivileges => new ReadOnlyDictionary<string, uint>(new Dictionary<string, uint>()
        {
            { "bot.administrator", 1U }
        });

        /// <summary>
        /// Command group
        /// </summary>
        public string CommandGroup => "administration";

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="commandArguments">Command arguments</param>
        /// <returns>Command result</returns>
        public ECommandResult Execute(ICommandArguments commandArguments)
        {
            ECommandResult ret = ECommandResult.Failed;
            if (commandArguments.Arguments.Count == 5)
            {
                string global_guild_specifier = commandArguments.Arguments[0];
                string role_user_specifier = commandArguments.Arguments[1];
                string role_user_id_string = commandArguments.Arguments[2];
                string privilege = commandArguments.Arguments[3];
                string level_string = commandArguments.Arguments[4];
                ulong role_user_id;
                uint level;
                if (ulong.TryParse(role_user_id_string, out role_user_id) && uint.TryParse(level_string, out level))
                {
                    IPrivileges[] privileges_services = commandArguments.Bot.GetServices<IPrivileges>();
                    IChat[] chat_services = commandArguments.Bot.GetServices<IChat>();
                    if (privileges_services != null)
                    {
                        switch (global_guild_specifier.ToLower())
                        {
                            case "global":
                                if (role_user_specifier.ToLower() == "user")
                                {
                                    foreach (IPrivileges privileges in privileges_services)
                                    {
                                        privileges.SetGlobalUserPrivilege(role_user_id, privilege, level);
                                        privileges.Save();
                                        foreach (IChat chat in chat_services)
                                        {
                                            if (chat != null)
                                            {
                                                if (level == 0U)
                                                {
                                                    chat.SendMessage("Global privilege \"" + privilege + "\" has been removed from user \"" + role_user_id + "\".", commandArguments.MessageChannel);
                                                }
                                                else
                                                {
                                                    chat.SendMessage("Global privilege \"" + privilege + "\" has been set to level \"" + level + "\" for user \"" + role_user_id + "\".", commandArguments.MessageChannel);
                                                }
                                            }
                                        }
                                        ret = ECommandResult.Successful;
                                    }
                                }
                                break;
                            case "guild":
                                switch (role_user_specifier.ToLower())
                                {
                                    case "role":
                                        foreach (IPrivileges privileges in privileges_services)
                                        {
                                            privileges.SetGuildRolePrivilege(((SocketGuildChannel)(commandArguments.MessageChannel)).Guild.Id, role_user_id, privilege, level);
                                            privileges.Save();
                                            foreach (IChat chat in chat_services)
                                            {
                                                if (chat != null)
                                                {
                                                    if (level == 0U)
                                                    {
                                                        chat.SendMessage("Guild privilege \"" + privilege + "\" has been removed from role \"" + role_user_id + "\".", commandArguments.MessageChannel);
                                                    }
                                                    else
                                                    {
                                                        chat.SendMessage("Guild privilege \"" + privilege + "\" has been set to level \"" + level + "\" for role \"" + role_user_id + "\".", commandArguments.MessageChannel);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case "user":
                                        foreach (IPrivileges privileges in privileges_services)
                                        {
                                            privileges.SetGuildUserPrivilege(((SocketGuildChannel)(commandArguments.MessageChannel)).Guild.Id, role_user_id, privilege, level);
                                            privileges.Save();
                                            foreach (IChat chat in chat_services)
                                            {
                                                if (chat != null)
                                                {
                                                    if (level == 0U)
                                                    {
                                                        chat.SendMessage("Guild privilege \"" + privilege + "\" has been removed from user \"" + role_user_id + "\".", commandArguments.MessageChannel);
                                                    }
                                                    else
                                                    {
                                                        chat.SendMessage("Guild privilege \"" + privilege + "\" has been set to level \"" + level + "\" for user \"" + role_user_id + "\".", commandArguments.MessageChannel);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
            return ret;
        }
    }
}
