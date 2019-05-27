/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Actions interface
    /// </summary>
    public interface IActions
    {
        /// <summary>
        /// Is empty
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Get last recorded action state
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        T GetLastRecordedActionState<T>();

        /// <summary>
        /// Push recorded action state
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="obj">Object</param>
        void PushRecordedActionState<T>(T obj);

        /// <summary>
        /// Pop recorded action state
        /// </summary>
        /// <returns>Recorded action state</returns>
        T PopRecordedActionState<T>();
    }
}
