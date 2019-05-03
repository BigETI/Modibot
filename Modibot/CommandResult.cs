using ModibotAPI;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Command result class
    /// </summary>
    internal class CommandResult : ICommandResult
    {
        /// <summary>
        /// Result
        /// </summary>
        public ECommandResult Result { get; private set; }

        /// <summary>
        /// Compiled command
        /// </summary>
        public ICompiledCommand CompiledCommand { get; private set; }

        /// <summary>
        /// Missing privileges
        /// </summary>
        public ReadOnlyDictionary<string, uint> MissingPrivileges { get; private set; } = new ReadOnlyDictionary<string, uint>(new Dictionary<string, uint>());

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="result">Result</param>
        /// <param name="compiledCommand">Compiled command</param>
        public CommandResult(ECommandResult result, ICompiledCommand compiledCommand)
        {
            Result = result;
            CompiledCommand = compiledCommand;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="result">Result</param>
        /// <param name="compiledCommand">Compiled command</param>
        /// <param name="missingPrivileges">Missing privileges</param>
        public CommandResult(ECommandResult result, ICompiledCommand compiledCommand, IDictionary<string, uint> missingPrivileges)
        {
            Result = result;
            CompiledCommand = compiledCommand;
            MissingPrivileges = new ReadOnlyDictionary<string, uint>(missingPrivileges);
        }
    }
}
