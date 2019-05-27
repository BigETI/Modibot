/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Command result enumerator
    /// </summary>
    public enum ECommandResult
    {
        /// <summary>
        /// Command has failed
        /// </summary>
        Failed,

        /// <summary>
        /// Command was successful
        /// </summary>
        Successful,

        /// <summary>
        /// Command execution has been rate limited
        /// </summary>
        RateLimited,

        /// <summary>
        /// Command execution has been denied
        /// </summary>
        Denied,

        /// <summary>
        /// Command has been disabled
        /// </summary>
        Disabled
    }
}
