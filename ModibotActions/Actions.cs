using ModibotAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Modibot actions namespace
/// </summary>
namespace ModibotActions
{
    /// <summary>
    /// Actions class
    /// </summary>
    [Service]
    public class Actions : Stack<byte[]>, IActions
    {
        /// <summary>
        /// Formatter
        /// </summary>
        private static readonly BinaryFormatter formatter = new BinaryFormatter();

        /// <summary>
        /// Is empty
        /// </summary>
        public bool IsEmpty => (Count <= 0);

        /// <summary>
        /// Fetch last recorded action state
        /// </summary>
        /// <typeparam name="T">Action state type</typeparam>
        /// <param name="peek">Peek data if "true", otherwise pop data</param>
        /// <returns>Last recorded action state</returns>
        private T FetchLastRecordedActionState<T>(bool peek)
        {
            T ret = default(T);
            if (typeof(T).IsSerializable && (Count > 0))
            {
                try
                {
                    using (MemoryStream stream = new MemoryStream(peek ? Peek() : Pop()))
                    {
                        stream.Seek(0L, SeekOrigin.Begin);
                        ret = (T)(formatter.Deserialize(stream));
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
        /// Get last action state
        /// </summary>
        /// <typeparam name="T">Action state type</typeparam>
        /// <returns>Last action state</returns>
        public T GetLastRecordedActionState<T>()
        {
            return FetchLastRecordedActionState<T>(true);
        }

        /// <summary>
        /// Push recorded action state
        /// </summary>
        /// <typeparam name="T">Action state type</typeparam>
        /// <param name="obj">Action state</param>
        public void PushRecordedActionState<T>(T obj)
        {
            if (obj != null)
            {
                if (obj.GetType().IsSerializable)
                {
                    try
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(stream, obj);
                            stream.Seek(0L, SeekOrigin.Begin);
                            byte[] data = new byte[stream.Length];
                            stream.Read(data, 0, data.Length);
                            Push(data);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e);
                    }
                }
            }
        }

        /// <summary>
        /// Pop recorded action state
        /// </summary>
        /// <typeparam name="T">Action state type</typeparam>
        /// <returns>Last action state</returns>
        public T PopRecordedActionState<T>()
        {
            return FetchLastRecordedActionState<T>(false);
        }
    }
}
