using System.Net;

/// <summary>
/// Modibot web interface namespace
/// </summary>
namespace ModibotWebInterface
{
    /// <summary>
    /// Web page interface
    /// </summary>
    public interface IWebPage
    {
        /// <summary>
        /// Page name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get page content
        /// </summary>
        /// <param name="httpListenerContext">HTTP listener context</param>
        /// <returns>Page content</returns>
        string GetContent(HttpListenerContext httpListenerContext);
    }
}
