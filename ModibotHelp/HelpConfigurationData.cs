using System.Runtime.Serialization;

/// <summary>
/// Modibot help namespace
/// </summary>
namespace ModibotHelp
{
    /// <summary>
    /// Help configuration data class
    /// </summary>
    [DataContract]
    public class HelpConfigurationData
    {
        /// <summary>
        /// Maximum levenshtein distance
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        private uint maximumLevenshteinDistance = 3U;

        /// <summary>
        /// Maximum levenshtein distance
        /// </summary>
        public uint MaximumLevenshteinDistance
        {
            get => maximumLevenshteinDistance;
            set => maximumLevenshteinDistance = value;
        }
    }
}
