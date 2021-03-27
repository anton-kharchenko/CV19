using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace CV19.WebServer
{
    internal class WebServer
    {
        private event EventHandler<RequestReceivedEventArgs> RequestRecieved;

        //private TcpListener Listener = new TcpListener(IPAddress.Any, 8080);

        private HttpListener Listener;

        private readonly int _Port;

        private bool _Enabled;

        public object _SyncRoot = new object();

        public int Port => _Port;

        public bool Enabled { get => _Enabled; set { if (value) Start(); else Stop(); } }

        public WebServer(int port) => _Port = port;

        public void Start()
        {
            if (Enabled) return;
            lock (_SyncRoot)
            {
                if (Enabled) return;
                Listener = new HttpListener();
                Listener.Prefixes.Add($"http://*:{Port}");
                Enabled = true;
                ListenAsync();
            }
        }

        public void Stop()
        {
            if (Enabled) return;
            lock (_SyncRoot)
            {
                if (Enabled) return;
                Listener = null;
                Enabled = false;
            }
        }

        public async void ListenAsync()
        {
            var listner = Listener;
            listner.Start();

            while (_Enabled)
            {
                var context = await listner.GetContextAsync().ConfigureAwait(false);
                ProcessRequest(context);
            }

            listner.Stop();
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            RequestRecieved?.Invoke(this, new RequestReceivedEventArgs(context));
        }
    }

    internal class RequestReceivedEventArgs : EventArgs
    {
        public HttpListenerContext Context { get; set; }

        public RequestReceivedEventArgs(HttpListenerContext context) => Context = context;
    }
}