/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Commands configuration data interface
    /// </summary>
    public interface ICommandsConfigurationData
    {
        /// <summary>
        /// Command delimiter
        /// </summary>
        string Delimiter { get; }

        /// <summary>
        /// Command failed text
        /// </summary>
        string FailedText { get; }

        /// <summary>
        /// Command successful text
        /// </summary>
        string SuccessfulText { get; }

        /// <summary>
        /// Command denied text
        /// </summary>
        string DeniedText { get; }

        /// <summary>
        /// Maximum levenshtein distance
        /// </summary>
        uint MaximumLevenshteinDistance { get; }
    }
}
