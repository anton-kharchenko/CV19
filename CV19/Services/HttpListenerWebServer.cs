using System.IO;
using CV19.Services.Interfaces;
using CV19.WebServerProject;

namespace CV19.Services
{
    internal class HttpListenerWebServer : IWebServerService
    {
        private readonly WebServer _server = new(8080);

        public HttpListenerWebServer() => _server.RequestReceived += OnRequestReceived;

        private static void OnRequestReceived(object sender, RequestReceiverEventArgs e)
        {
            using var writer = new StreamWriter(e.Context.Response.OutputStream);
            writer.WriteLine("CV19 Application");
        }

        public bool Enabled
        {
            get => _server.Enabled;
            set => _server.Enabled = value;
        }

        public void Start() => _server.Start();

        public void Stop() => _server.Stop();
    }
}