using Discord;
using Discord.WebSocket;
using System.Collections.Generic;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Privileges interface
    /// </summary>
    public interface IPrivileges
    {
        /// <summary>
        /// Has user required privileges
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="user">User</param>
        /// <param name="messageChannel">Message channel</param>
        /// <param name="missingPrivileges">Missing privileges (out)</param>
        /// <returns>"true" if user has required privileges, otherwise "false"</returns>
        bool HasPrivileges(ICommand command, SocketUser user, ISocketMessageChannel messageChannel, out IDictionary<string, uint> missingPrivileges);

        /// <summary>
        /// Set global user privilege
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="privilege">Privilege</param>
        /// <param name="level">Level</param>
        void SetGlobalUserPrivilege(ulong userID, string privilege, uint level);

        /// <summary>
        /// Set guild user privilege
        /// </summary>
        /// <param name="guildID">Guild ID</param>
        /// <param name="userID">User ID</param>
        /// <param name="privilege">Privilege</param>
        /// <param name="level">Level</param>
        void SetGuildUserPrivilege(ulong guildID, ulong userID, string privilege, uint level);

        /// <summary>
        /// Set guild role privilege
        /// </summary>
        /// <param name="guildID">Guild ID</param>
        /// <param name="roleID">Role ID</param>
        /// <param name="privilege">Privilege</param>
        /// <param name="level">Level</param>
        void SetGuildRolePrivilege(ulong guildID, ulong roleID, string privilege, uint level);

        /// <summary>
        /// Save privileges
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        bool Save();
    }
}
