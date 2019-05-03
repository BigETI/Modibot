using System;
using System.Collections.ObjectModel;

namespace ModibotAPI
{
    /// <summary>
    /// Command group interface
    /// </summary>
    public interface ICommandGroup
    {
        /// <summary>
        /// Command group icon (emoji)
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Command group name
        /// </summary>
        string Name { get; }
    }
}
