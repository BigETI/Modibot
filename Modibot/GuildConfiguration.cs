using ModibotAPI;
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
    internal class GuildConfiguration : IGuildConfiguration
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
