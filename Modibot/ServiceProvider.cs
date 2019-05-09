using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Service provider class
    /// </summary>
    internal class ServiceProvider : ModibotAPI.IServiceProvider, IDisposable
    {
        /// <summary>
        /// Services
        /// </summary>
        private List<object> services = new List<object>();

        /// <summary>
        /// Services
        /// </summary>
        public ReadOnlyCollection<object> LoadedServices => new ReadOnlyCollection<object>(services);

        /// <summary>
        /// Require service
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service</returns>
        public T RequireService<T>()
        {
            return (T)(RequireService(typeof(T)));
        }

        /// <summary>
        /// Require service
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Service</returns>
        public object RequireService(Type serviceType)
        {
            object ret = null;
            if (serviceType != null)
            {
                if (serviceType.IsClass)
                {
                    ret = GetService(serviceType);
                    if (ret == null)
                    {
                        ConstructorInfo[] constructors = serviceType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                        bool has_parameter = false;
                        if (constructors != null)
                        {
                            foreach (ConstructorInfo constructor in constructors)
                            {
                                if (constructor != null)
                                {
                                    ParameterInfo[] parameters = constructor.GetParameters();
                                    if (parameters != null)
                                    {
                                        if (parameters.Length == 1)
                                        {
                                            ParameterInfo parameter = parameters[0];
                                            if (parameter != null)
                                            {
                                                if (typeof(IServiceProvider).IsAssignableFrom(parameter.ParameterType))
                                                {
                                                    has_parameter = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        ret = (has_parameter ? Activator.CreateInstance(serviceType, new object[] { this }) : Activator.CreateInstance(serviceType));
                        services.Add(ret);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service if successful, otherwise "default(T)"</returns>
        public T GetService<T>()
        {
            return (T)(GetService(typeof(T)));
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Service if successful, otherwise "null"</returns>
        public object GetService(Type serviceType)
        {
            object ret = null;
            foreach (object service in services)
            {
                if (serviceType.IsAssignableFrom(service.GetType()))
                {
                    ret = service;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceTypeName">Service type name</param>
        /// <returns>Service if successful, otherwise "null"</returns>
        public object GetService(string serviceTypeName)
        {
            return GetService(Type.GetType(serviceTypeName));
        }

        /// <summary>
        /// Get services
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Services</returns>
        public T[] GetServices<T>()
        {
            return Array.ConvertAll(GetServices(typeof(T)), (service) =>
            {
                return (T)service;
            });
        }

        /// <summary>
        /// Get services
        /// </summary>
        /// <param name="serviceType">Service type</param>
        /// <returns>Services</returns>
        public object[] GetServices(Type serviceType)
        {
            List<object> services = new List<object>();
            foreach (object service in this.services)
            {
                if (serviceType.IsAssignableFrom(service.GetType()))
                {
                    services.Add(service);
                }
            }
            object[] ret = services.ToArray();
            services.Clear();
            return ret;
        }

        /// <summary>
        /// Get services
        /// </summary>
        /// <param name="serviceTypeName">Service type name</param>
        /// <returns>Services</returns>
        public object[] GetServices(string serviceTypeName)
        {
            return GetServices(Type.GetType(serviceTypeName));
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            foreach (object service in services)
            {
                if (typeof(IDisposable).IsAssignableFrom(service.GetType()))
                {
                    ((IDisposable)service).Dispose();
                }
            }
            services.Clear();
        }
    }
}
