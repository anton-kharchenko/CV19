using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace CV19.WebServer
{
    public class WebServer
    {
        public event EventHandler<RequestReceiverEventArgs> RequestReceived;

        //private TcpListener _Listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8080));
        private HttpListener _Listener;

        private readonly int _Port;
        private bool _Enabled;
        private readonly object _SyncRoot = new object();

        public int Port => _Port;

        public bool Enabled { get => _Enabled; set { if (value) Start(); else Stop(); } }

        public WebServer(int port) => _Port = port;

        public void Start()
        {
            if (_Enabled) return;
            lock (_SyncRoot)
            {
                if (_Enabled) return;

                _Listener = new HttpListener();
                //_Listener.Prefixes.Add($"http://*:{_Port}/"); // netsh http add urlacl url=http://*:8080/ user=user_name
                _Listener.Prefixes.Add($"http://+:{_Port}/");
                _Enabled = true;
                ListenAsync();
            }
        }

        public void Stop()
        {
            if (!_Enabled) return;
            lock (_SyncRoot)
            {
                if (!_Enabled) return;

                _Listener = null;
                _Enabled = false;
            }
        }

        private async void ListenAsync()
        {
            var listener = _Listener;

            listener.Start();

            HttpListenerContext context = null;
            while (_Enabled)
            {
                var getContextTask = listener.GetContextAsync();
                if (context != null)
                    ProcessRequestAsync(context);
                context = await getContextTask.ConfigureAwait(false);
            }

            listener.Stop();
        }

        private void ProcessRequestAsync(HttpListenerContext context)
        {
            RequestReceived?.Invoke(this, new RequestReceiverEventArgs(context));
        }
    }

    public class RequestReceiverEventArgs : EventArgs
    {
        public HttpListenerContext Context { get; }

        public RequestReceiverEventArgs(HttpListenerContext context) => Context = context;
    }
}