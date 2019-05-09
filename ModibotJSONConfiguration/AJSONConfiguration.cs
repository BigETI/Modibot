using ModibotAPI;
using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace ModibotJSONConfiguration
{
    /// <summary>
    /// JSON configuration class
    /// </summary>
    /// <typeparam name="T">Configuration data type</typeparam>
    [Service]
    public abstract class AJSONConfiguration<T> : IConfiguration
    {
        /// <summary>
        /// Configuration directory
        /// </summary>
        private static readonly string ConfigurationDirectory = "configuration";

        /// <summary>
        /// Configuration data serializer
        /// </summary>
        private static readonly DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), new DataContractJsonSerializerSettings()
        {
            UseSimpleDictionaryFormat = true
        });

        /// <summary>
        /// Configuration path
        /// </summary>
        public string ConfigurationPath => Path.Combine(ConfigurationDirectory, ConfigurationFileName);

        /// <summary>
        /// Configuration file name
        /// </summary>
        public abstract string ConfigurationFileName { get; }

        /// <summary>
        /// Configuration data
        /// </summary>
        private T data;

        /// <summary>
        /// Configuration data
        /// </summary>
        public T Data
        {
            get
            {
                if (data == null)
                {
                    if (!(Load()))
                    {
                        data = Activator.CreateInstance<T>();
                    }
                }
                return data;
            }
        }

        /// <summary>
        /// Load configuration
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        private bool Load()
        {
            bool ret = false;
            string path = ConfigurationPath;
            if (File.Exists(path))
            {
                try
                {
                    using (FileStream stream = File.OpenRead(path))
                    {
                        T data = (T)(serializer.ReadObject(stream));
                        if (data != null)
                        {
                            this.data = data;
                            ret = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
            return ret;
        }

        /// <summary>
        /// Save configuration
        /// </summary>
        /// <returns>"true" if successful, otherwise "false"</returns>
        public bool Save()
        {
            bool ret = false;
            string path = ConfigurationPath;
            if (path != null)
            {
                try
                {
                    using (FileStream stream = File.Open(path, FileMode.Create))
                    {
                        serializer.WriteObject(stream, Data);
                        ret = true;
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
            return ret;
        }
    }
}
