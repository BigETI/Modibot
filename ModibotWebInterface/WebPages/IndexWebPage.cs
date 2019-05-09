using System.Net;

/// <summary>
/// Modibot web interface pages namespace
/// </summary>
namespace ModibotWebInterface.Pages
{
    /// <summary>
    /// Index web page class
    /// </summary>
    public class IndexWebPage : IWebPage
    {
        /// <summary>
        /// Web page name
        /// </summary>
        public string Name => "index";

        /// <summary>
        /// Get web page content
        /// </summary>
        /// <param name="httpListenerContext">HTTP listener context</param>
        /// <returns>Web page content</returns>
        public string GetContent(HttpListenerContext httpListenerContext)
        {
            // TODO
            return "It works!";
        }
    }
}
