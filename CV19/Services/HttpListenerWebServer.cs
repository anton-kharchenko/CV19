using System;
using System.IO;
using System.Threading;
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
            Thread.Sleep(1000);
            using var writer = new StreamWriter(e.Context.Response.OutputStream);
            writer.WriteLine("CV19 Application - {0}", DateTime.Now);
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