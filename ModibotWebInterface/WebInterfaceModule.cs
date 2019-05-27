using ModibotAPI;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Modibot web interface namespace
/// </summary>
namespace ModibotWebInterface
{
    /// <summary>
    /// Web interface module class
    /// </summary>
    public class WebInterfaceModule : IModule
    {
        /// <summary>
        /// Web pages
        /// </summary>
        private WebPages webPages;

        /// <summary>
        /// HTTP listener
        /// </summary>
        public HttpListener HTTPListener { get; private set; }

        /// <summary>
        /// Module name
        /// </summary>
        public string Name => "Web interface";

        /// <summary>
        /// Module version
        /// </summary>
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Module author
        /// </summary>
        public string Author => "Ethem Kurt";

        /// <summary>
        /// Module URI
        /// </summary>
        public string URI => "https://github.com/BigETI/Modibot";

        /// <summary>
        /// Context recieved callback
        /// </summary>
        /// <param name="asyncResult">Asynchronous result</param>
        private void ContextReceivedCallback(IAsyncResult asyncResult)
        {
            try
            {
                if ((webPages != null) && (HTTPListener != null))
                {
                    if (HTTPListener.IsListening)
                    {
                        HttpListenerContext context = HTTPListener.EndGetContext(asyncResult);
                        HTTPListener.BeginGetContext(new AsyncCallback(ContextReceivedCallback), null);
                        context.Response.ContentEncoding = Encoding.UTF8;
                        using (Stream stream = context.Response.OutputStream)
                        {
                            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                            {
                                IWebPage web_page = webPages.GetWebPage(context.Request.RawUrl);
                                if (web_page == null)
                                {
                                    context.Response.StatusCode = 404;
                                    writer.Write("Not available!");
                                }
                                else
                                {
                                    context.Response.StatusCode = 200;
                                    writer.Write(web_page.GetContent(context));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        /// <summary>
        /// Initialize (asynchronous)
        /// </summary>
        /// <param name="bot">Bot</param>
        /// <returns>Task</returns>
        public Task InitAsync(IBot bot)
        {
            webPages = bot.GetService<WebPages>();
            if (webPages != null)
            {
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                if (types != null)
                {
                    foreach (Type type in types)
                    {
                        if (type != null)
                        {
                            if (type.IsClass)
                            {
                                if (typeof(IWebPage).IsAssignableFrom(type))
                                {
                                    webPages.AddWebPage((IWebPage)(Activator.CreateInstance(type)));
                                }
                            }
                        }
                    }
                }
            }
            WebInterfaceConfiguration web_interface_configuration = bot.GetService<WebInterfaceConfiguration>();
            if (web_interface_configuration != null)
            {
                if (HttpListener.IsSupported)
                {
                    try
                    {
                        HTTPListener = new HttpListener();
                        Console.Out.WriteLine("Initializing web server...");
                        HTTPListener.Prefixes.Add("http://*:" + web_interface_configuration.Data.Port + "/");
                        if (web_interface_configuration.Data.AllowHTTPS)
                        {
                            HTTPListener.Prefixes.Add("https://*:" + web_interface_configuration.Data.Port + "/");
                        }
                        HTTPListener.Start();
                        HTTPListener.BeginGetContext(new AsyncCallback(ContextReceivedCallback), null);
                        Console.Out.WriteLine("Finished initializing web server!");
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e);
                        Console.Out.WriteLine("Failed to initialize web server!");
                    }
                }
                else
                {
                    Console.Out.WriteLine("Listening to HTTP requests is not supported in this platform.");
                }
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Exit (asynchronous)
        /// </summary>
        /// <returns>Task</returns>
        public Task ExitAsync()
        {
            if (HTTPListener != null)
            {
                if (HTTPListener.IsListening)
                {
                    HTTPListener.Stop();
                }
                HTTPListener = null;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Module load (asynchronous)
        /// </summary>
        /// <param name="module">Module</param>
        /// <returns>Task</returns>
        public Task ModuleLoadAsync(IModule module)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Module unload (asynchronous)
        /// </summary>
        /// <param name="module">Module</param>
        /// <returns>Task</returns>
        public Task ModuleUnloadAsync(IModule module)
        {
            return Task.CompletedTask;
        }
    }
}
