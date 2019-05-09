using ModibotAPI;
using System.Collections.Generic;

/// <summary>
/// Modibot web interface namespace
/// </summary>
namespace ModibotWebInterface
{
    /// <summary>
    /// Web pages class
    /// </summary>
    [Service]
    public class WebPages
    {
        /// <summary>
        /// Web pages
        /// </summary>
        private Dictionary<string, IWebPage> webPages = new Dictionary<string, IWebPage>();

        /// <summary>
        /// Add web page
        /// </summary>
        /// <param name="webPage"></param>
        public void AddWebPage(IWebPage webPage)
        {
            if (webPage != null)
            {
                if (!(webPages.ContainsKey(webPage.Name)))
                {
                    webPages.Add(webPage.Name, webPage);
                }
            }
        }

        /// <summary>
        /// Get web page
        /// </summary>
        /// <param name="webPage">Web page</param>
        /// <returns>Web page if successful, otherwise "null"</returns>
        public IWebPage GetWebPage(string webPage)
        {
            IWebPage web_page = null;
            if (webPage != null)
            {
                if (webPages.ContainsKey(webPage))
                {
                    web_page = webPages[webPage];
                }
            }
            return web_page;
        }
    }
}
