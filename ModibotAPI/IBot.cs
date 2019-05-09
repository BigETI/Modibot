/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Bot interface
    /// </summary>
    public interface IBot : IServiceProvider, IModules
    {
        /// <summary>
        /// Exit bot
        /// </summary>
        void Exit();
    }
}
