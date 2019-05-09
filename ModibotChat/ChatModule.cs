﻿using ModibotAPI;
using System;
using System.Reflection;
using System.Threading.Tasks;

/// <summary>
/// Modibot chat namespace
/// </summary>
namespace ModibotChat
{
    /// <summary>
    /// Chat module
    /// </summary>
    public class ChatModule : IModule
    {
        /// <summary>
        /// Module name
        /// </summary>
        public string Name => "Chat system";

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
            if (bot != null)
            {
                Chat chat = bot.GetService<Chat>();
                if (chat != null)
                {
                    chat.UpdateConfigurationService(bot);
                }
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