using System.Runtime.Serialization;

/// <summary>
/// Modibot console logger namespace
/// </summary>
namespace ModibotConsoleLogger
{
    /// <summary>
    /// Console logger configuration data class
    /// </summary>
    [DataContract]
    public class ConsoleLoggerConfigurationData
    {
        /// <summary>
        /// Default output prefix
        /// </summary>
        public static readonly string DefaultOutputPrefix = "[LOG]\t";

        /// <summary>
        /// Default error prefix
        /// </summary>
        public static readonly string DefaultErrorPrefix = "[ERROR]\t";

        /// <summary>
        /// Enable logger
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        private bool enableLogger = true;

        /// <summary>
        /// Output prefix
        /// </summary>
        [DataMember]
        private string outputPrefix;

        /// <summary>
        /// Error prefix
        /// </summary>
        [DataMember]
        private string errorPrefix;

        /// <summary>
        /// Texts
        /// </summary>
        [DataMember]
        private ConsoleLoggerTextsConfigurationData texts;

        /// <summary>
        /// Enable logger
        /// </summary>
        public bool EnableLogger
        {
            get => enableLogger;
            set => enableLogger = value;
        }

        /// <summary>
        /// Output prefix
        /// </summary>
        public string OutputPrefix
        {
            get
            {
                if (outputPrefix == null)
                {
                    outputPrefix = DefaultOutputPrefix;
                }
                return outputPrefix;
            }
            set
            {
                outputPrefix = value;
            }
        }

        /// <summary>
        /// Error prefix
        /// </summary>
        public string ErrorPrefix
        {
            get
            {
                if (errorPrefix == null)
                {
                    errorPrefix = DefaultErrorPrefix;
                }
                return errorPrefix;
            }
            set
            {
                errorPrefix = value;
            }
        }

        /// <summary>
        /// Logger texts
        /// </summary>
        public ConsoleLoggerTextsConfigurationData Texts
        {
            get
            {
                if (texts == null)
                {
                    texts = new ConsoleLoggerTextsConfigurationData();
                }
                return texts;
            }
        }
    }
}
