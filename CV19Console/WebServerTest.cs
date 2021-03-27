using System;
using System.IO;
using CV19.WebServer;

namespace CV19Console
{
    internal static class WebServerTest
    {
        public static void Start()
        {
            var server = new WebServer(8080);
            server.RequestRecieved += OnRequestReceived;
            server.Start();

            Console.WriteLine("Server is running!");
            Console.ReadLine();
        }

        private static void OnRequestReceived(object sender, RequestReceivedEventArgs e)
        {
            var context = e.Context;
            Console.WriteLine("Connection {0}", context.Request.UserHostAddress);

            using var writer = new StreamWriter(context.Response.OutputStream);
            writer.WriteLine("Hello from Test Web Server!");
        }
    }
}