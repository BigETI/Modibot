/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Bot interface
    /// </summary>
    public interface IBot : IServiceProvider
    {
        /// <summary>
        /// Services
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// Exit bot
        /// </summary>
        void Exit();
    }
}
