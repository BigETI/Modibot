﻿using ModibotAPI;
using System.Runtime.Serialization;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Guild configuration class
    /// </summary>
    [DataContract]
    public class GuildConfiguration
    {
        /// <summary>
        /// Enable TTS
        /// </summary>
        [DataMember]
        private bool enableTTS = false;

        /// <summary>
        /// Enable TTS
        /// </summary>
        public bool EnableTTS => enableTTS;
    }
}
