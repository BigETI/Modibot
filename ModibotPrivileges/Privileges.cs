using Discord;
using Discord.WebSocket;
using ModibotAPI;
using ModibotJSONConfiguration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

/// <summary>
/// Modibot privileges namespace
/// </summary>
namespace ModibotPrivileges
{
    /// <summary>
    /// Privileges class
    /// </summary>
    public class Privileges : AJSONConfiguration<PrivilegesData>, IPrivileges
    {
        /// <summary>
        /// Configuration file name
        /// </summary>
        public override string ConfigurationFileName => "privileges.json";

        /// <summary>
        /// Has user required privileges
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="user">User</param>
        /// <param name="messageChannel">Message channel</param>
        /// <param name="missingPrivileges">Missing privileges (out)</param>
        /// <returns>"true" if user has required privileges, otherwise "false"</returns>
        public bool HasPrivileges(ICommand command, SocketUser user, ISocketMessageChannel messageChannel, out IDictionary<string, uint> missingPrivileges)
        {
            bool ret = false;
            missingPrivileges = new Dictionary<string, uint>();
            if ((command != null) && (user != null) && (messageChannel != null))
            {
                ret = true;
                SocketGuildUser guild_user = user as SocketGuildUser;
                SocketGuildChannel guild_channel = messageChannel as SocketGuildChannel;
                PrivilegesLevelData user_privileges = (Data.Users.ContainsKey(user.Id) ? Data.Users[user.Id] : new PrivilegesLevelData());
                PrivilegesGuildData guild_privileges = (guild_channel == null) ? (new PrivilegesGuildData()) : (Data.Guilds.ContainsKey(guild_channel.Guild.Id) ? Data.Guilds[user.Id] : (new PrivilegesGuildData()));
                PrivilegesLevelData guild_user_privileges = (guild_privileges.Users.ContainsKey(user.Id) ? guild_privileges.Users[user.Id] : new PrivilegesLevelData());
                List<PrivilegesLevelData> guild_roles_privileges = new List<PrivilegesLevelData>();
                if (guild_user != null)
                {
                    foreach (SocketRole role in guild_user.Roles)
                    {
                        if (guild_privileges.Roles.ContainsKey(role.Id))
                        {
                            guild_roles_privileges.Add(guild_privileges.Roles[role.Id]);
                        }
                        else
                        {
                            guild_roles_privileges.Add(new PrivilegesLevelData());
                        }
                    }
                }
                foreach (KeyValuePair<string, uint> required_privilege in command.RequiredPrivileges)
                {
                    if (required_privilege.Value > 0)
                    {
                        ret = false;
                        if (user_privileges.Privileges.ContainsKey(required_privilege.Key))
                        {
                            if (user_privileges.Privileges[required_privilege.Key] >= required_privilege.Value)
                            {
                                ret = true;
                                continue;
                            }
                        }
                        if (guild_user_privileges.Privileges.ContainsKey(required_privilege.Key))
                        {
                            if (user_privileges.Privileges[required_privilege.Key] >= required_privilege.Value)
                            {
                                ret = true;
                                continue;
                            }
                        }
                        foreach (PrivilegesLevelData guild_role_privilege in guild_roles_privileges)
                        {
                            if (guild_role_privilege.Privileges.ContainsKey(required_privilege.Key))
                            {
                                if (user_privileges.Privileges[required_privilege.Key] >= required_privilege.Value)
                                {
                                    ret = true;
                                    break;
                                }
                            }
                        }
                        if (!ret)
                        {
                            missingPrivileges.Add(required_privilege.Key, required_privilege.Value);
                        }
                    }
                }
                guild_roles_privileges.Clear();
            }
            return ret;
        }

        /// <summary>
        /// Set global user privilege
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="privilege">Privilege</param>
        /// <param name="level">Level</param>
        public void SetGlobalUserPrivilege(ulong userID, string privilege, uint level)
        {
            if (privilege != null)
            {
                PrivilegesLevelData privileges_level = null;
                if (Data.Users.ContainsKey(userID))
                {
                    privileges_level = Data.Users[userID];
                }
                else
                {
                    privileges_level = new PrivilegesLevelData();
                    Data.Users.Add(userID, privileges_level);
                }
                if (level == 0U)
                {
                    if (privileges_level.Privileges.ContainsKey(privilege))
                    {
                        privileges_level.Privileges.Remove(privilege);
                    }
                }
                else
                {
                    if (privileges_level.Privileges.ContainsKey(privilege))
                    {
                        privileges_level.Privileges[privilege] = level;
                    }
                    else
                    {
                        privileges_level.Privileges.Add(privilege, level);
                    }
                }
            }
        }

        /// <summary>
        /// Set guild user privilege
        /// </summary>
        /// <param name="guildID">Guild ID</param>
        /// <param name="userID">User ID</param>
        /// <param name="privilege">Privilege</param>
        /// <param name="level">Level</param>
        public void SetGuildUserPrivilege(ulong guildID, ulong userID, string privilege, uint level)
        {
            if (privilege != null)
            {
                PrivilegesGuildData privileges_guild = null;
                if (Data.Guilds.ContainsKey(guildID))
                {
                    privileges_guild = Data.Guilds[guildID];
                }
                else
                {
                    privileges_guild = new PrivilegesGuildData();
                    Data.Guilds.Add(guildID, privileges_guild);
                }
                PrivilegesLevelData privileges_level = null;
                if (privileges_guild.Users.ContainsKey(userID))
                {
                    privileges_level = privileges_guild.Users[userID];
                }
                else
                {
                    privileges_level = new PrivilegesLevelData();
                    privileges_guild.Users.Add(userID, privileges_level);
                }
                if (level == 0U)
                {
                    if (privileges_level.Privileges.ContainsKey(privilege))
                    {
                        privileges_level.Privileges.Remove(privilege);
                    }
                }
                else
                {
                    if (privileges_level.Privileges.ContainsKey(privilege))
                    {
                        privileges_level.Privileges[privilege] = level;
                    }
                    else
                    {
                        privileges_level.Privileges.Add(privilege, level);
                    }
                }
            }
        }

        /// <summary>
        /// Set guild role privilege
        /// </summary>
        /// <param name="guildID">Guild ID</param>
        /// <param name="roleID">Role ID</param>
        /// <param name="privilege">Privilege</param>
        /// <param name="level">Level</param>
        public void SetGuildRolePrivilege(ulong guildID, ulong roleID, string privilege, uint level)
        {
            if (privilege != null)
            {
                PrivilegesGuildData privileges_guild = null;
                if (Data.Guilds.ContainsKey(guildID))
                {
                    privileges_guild = Data.Guilds[guildID];
                }
                else
                {
                    privileges_guild = new PrivilegesGuildData();
                    Data.Guilds.Add(guildID, privileges_guild);
                }
                PrivilegesLevelData privileges_level = null;
                if (privileges_guild.Roles.ContainsKey(roleID))
                {
                    privileges_level = privileges_guild.Roles[roleID];
                }
                else
                {
                    privileges_level = new PrivilegesLevelData();
                    privileges_guild.Roles.Add(roleID, privileges_level);
                }
                if (level == 0U)
                {
                    if (privileges_level.Privileges.ContainsKey(privilege))
                    {
                        privileges_level.Privileges.Remove(privilege);
                    }
                }
                else
                {
                    if (privileges_level.Privileges.ContainsKey(privilege))
                    {
                        privileges_level.Privileges[privilege] = level;
                    }
                    else
                    {
                        privileges_level.Privileges.Add(privilege, level);
                    }
                }
            }
        }
    }
}
