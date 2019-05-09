using System.Runtime.Serialization;

/// <summary>
/// Modibot chat namespace
/// </summary>
namespace ModibotChat
{
    /// <summary>
    /// Guild configuration class
    /// </summary>
    [DataContract]
    public class GuildConfigurationData
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
