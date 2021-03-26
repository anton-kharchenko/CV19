using System;
using System.Threading;

namespace CV19Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";

            var thread = new Thread(ThreadMethod)
            {
                Name = "Other thread",
                IsBackground = true,
                Priority = ThreadPriority.AboveNormal
            };

            thread.Start();

            var mess = "Hello world";
            var sleep = 1000;
            var count = 3;

            new Thread(() => PrintMethod(mess, 3, sleep)) { IsBackground = true }.Start();

            CurrentThread();
            Console.ReadLine();
        }

        private static void ThreadMethod() => CurrentThread();

        private static void PrintMethod(string message, int count, int timeout)
        {
            for (var i = 0; i < count; i++)
            {
                Console.WriteLine(message);
                Thread.Sleep(count);
            }
        }

        private static void CurrentThread()
        {
            var thread = Thread.CurrentThread;

            Console.WriteLine("id:{0} - Name:{1}", thread.ManagedThreadId, thread.Name);
        }
    }
}