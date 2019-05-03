using System;
using System.Collections.ObjectModel;

/// <summary>
/// Modibot API namespace
/// </summary>
namespace ModibotAPI
{
    /// <summary>
    /// Service provider interface
    /// </summary>
    public interface IServiceProvider : System.IServiceProvider
    {
        /// <summary>
        /// Loaded services
        /// </summary>
        ReadOnlyCollection<object> LoadedServices { get; }

        /// <summary>
        /// Require service
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service</returns>
        T RequireService<T>();

        /// <summary>
        /// Require service
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Service</returns>
        object RequireService(Type serviceType);

        /// <summary>
        /// Get service
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service if successful, otherwise "default(T)"</returns>
        T GetService<T>();

        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceTypeName">Service type name</param>
        /// <returns>Service if successful, otherwise "null"</returns>
        object GetService(string serviceTypeName);
    }
}
