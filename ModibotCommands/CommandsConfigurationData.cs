using System.Runtime.Serialization;

/// <summary>
/// Modibot commands configuration
/// </summary>
namespace ModibotCommands
{
    /// <summary>
    /// Commands configuration data class
    /// </summary>
    [DataContract]
    public class CommandsConfigurationData
    {
        /// <summary>
        /// Default command delimiter
        /// </summary>
        public static readonly string DefaultCommandDelimiter = "%";

        /// <summary>
        /// Default command failed text
        /// </summary>
        public static readonly string DefaultCommandFailed = "Failed to execute command \"{0}\".";

        /// <summary>
        /// Default command successful text
        /// </summary>
        public static readonly string DefaultCommandSuccessful = "";

        /// <summary>
        /// Default command denied text
        /// </summary>
        public static readonly string DefaultCommandDenied = "You are not allowed to execute \"{0}\".";

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
        /// Command delimiter
        /// </summary>
        public string Delimiter
        {
            get
            {
                if (delimiter == null)
                {
                    delimiter = DefaultCommandDelimiter;
                }
                else if (delimiter.Length < 1)
                {
                    delimiter = DefaultCommandDelimiter;
                }
                return delimiter;
            }
            set
            {
                if (value == null)
                {
                    delimiter = DefaultCommandDelimiter;
                }
                else if (value.Length < 1)
                {
                    delimiter = DefaultCommandDelimiter;
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
                    failedText = DefaultCommandFailed;
                }
                return failedText;
            }
            set
            {
                if (value == null)
                {
                    failedText = DefaultCommandFailed;
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
                    successfulText = DefaultCommandSuccessful;
                }
                return successfulText;
            }
            set
            {
                if (value == null)
                {
                    successfulText = DefaultCommandSuccessful;
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
                    deniedText = DefaultCommandDenied;
                }
                return deniedText;
            }
            set
            {
                if (value == null)
                {
                    deniedText = DefaultCommandDenied;
                }
                else
                {
                    deniedText = value;
                }
            }
        }
    }
}
