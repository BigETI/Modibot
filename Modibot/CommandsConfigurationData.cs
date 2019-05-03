using Modibot.Services;
using ModibotAPI;
using System.Runtime.Serialization;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Commands configuration data
    /// </summary>
    [DataContract]
    internal class CommandsConfigurationData : ICommandsConfigurationData
    {
        /// <summary>
        /// Command delimiter
        /// </summary>
        [DataMember]
        private string delimiter;

        /// <summary>
        /// Command failed text
        /// </summary>
        [DataMember]
        private string failedText;

        /// <summary>
        /// Command successful text
        /// </summary>
        [DataMember]
        private string successfulText;

        /// <summary>
        /// Command denied text
        /// </summary>
        [DataMember]
        private string deniedText;

        /// <summary>
        /// Maximum levenshtein distance
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        private uint maximumLevenshteinDistance = 3U;

        /// <summary>
        /// Command delimiter
        /// </summary>
        public string Delimiter
        {
            get
            {
                if (delimiter == null)
                {
                    delimiter = Configuration.DefaultCommandDelimiter;
                }
                else if (delimiter.Length < 1)
                {
                    delimiter = Configuration.DefaultCommandDelimiter;
                }
                return delimiter;
            }
            set
            {
                if (value == null)
                {
                    delimiter = Configuration.DefaultCommandDelimiter;
                }
                else if (value.Length < 1)
                {
                    delimiter = Configuration.DefaultCommandDelimiter;
                }
                else
                {
                    delimiter = value;
                }
            }
        }

        /// <summary>
        /// Command failed text
        /// </summary>
        public string FailedText
        {
            get
            {
                if (failedText == null)
                {
                    failedText = Configuration.DefaultCommandFailed;
                }
                return failedText;
            }
            set
            {
                if (value == null)
                {
                    failedText = Configuration.DefaultCommandFailed;
                }
                else
                {
                    failedText = value;
                }
            }
        }

        /// <summary>
        /// Command successful text
        /// </summary>
        public string SuccessfulText
        {
            get
            {
                if (successfulText == null)
                {
                    successfulText = Configuration.DefaultCommandSuccessful;
                }
                return successfulText;
            }
            set
            {
                if (value == null)
                {
                    successfulText = Configuration.DefaultCommandSuccessful;
                }
                else
                {
                    successfulText = value;
                }
            }
        }

        /// <summary>
        /// Command denied text
        /// </summary>
        public string DeniedText
        {
            get
            {
                if (deniedText == null)
                {
                    deniedText = Configuration.DefaultCommandDenied;
                }
                return deniedText;
            }
            set
            {
                if (value == null)
                {
                    deniedText = Configuration.DefaultCommandDenied;
                }
                else
                {
                    deniedText = value;
                }
            }
        }

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
