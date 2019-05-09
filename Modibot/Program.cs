using System;

/// <summary>
/// Modibot namespace
/// </summary>
namespace Modibot
{
    /// <summary>
    /// Program class
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            try
            {
                using (Bot bot = new Bot())
                {
                    bot.MainAsync().GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}
