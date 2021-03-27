using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Globalization;
using System.Threading;

namespace CV19Console
{
    internal class Program
    {
        private static bool _ThreadUpdate = true;

        private static void Main(string[] args)
        {
            WebServerTest.Start();
            return;
            // Thread.CurrentThread.Name = "Main thread";
            //
            // var clockThread = new Thread(ThreadMethod)
            // {
            //     Name = "Other thread",
            //     IsBackground = true,
            //     Priority = ThreadPriority.AboveNormal
            // };
            //
            // clockThread.Start();
            //
            // var mess = "Hello world";
            // var sleep = 1000;
            // var count = 3;
            //
            // new Thread(() => PrintMethod(mess, 3, sleep)) { IsBackground = true }.Start();
            // CurrentThread();

            //    var list = new List<int>();

            //    var threads = new Thread[10];
            //    object lockObject = new object();

            //    for (var i = 0; i < threads.Length; i++)
            //    {
            //        threads[i] = new Thread(() =>
            //        {
            //            for (var j = 0; j < 10; j++)
            //                lock (lockObject)
            //                {
            //                    list.Add(Thread.CurrentThread.ManagedThreadId);
            //                }
            //        });
            //    }

            //    foreach (var t in threads)
            //        t.Start();

            //    Monitor.Enter(lockObject);
            //    try
            //    {
            //    }
            //    finally
            //    {
            //        Monitor.Exit(lockObject);
            //    }

            //    if (!(clockThread.Join(1000)))
            //    {
            //        // clockThread.Abort();
            //        clockThread.Interrupt();
            //    }

            //    Console.WriteLine(string.Join(",", list));
            //    Console.ReadLine();
            //}

            //private static void ThreadMethod(object param)
            //{
            //    CurrentThread();
            //    while (_ThreadUpdate)
            //    {
            //        Thread.Sleep(1000);
            //        Console.Title = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            //    }
            //}

            //private static void PrintMethod(string message, int count, int timeout)
            //{
            //    for (var i = 0; i < count; i++)
            //    {
            //        Console.WriteLine(message);
            //        Thread.Sleep(count);
            //    }
            //}

            //private static void CurrentThread()
            //{
            //    var thread = Thread.CurrentThread;

            //    Console.WriteLine("id:{0} - Name:{1}", thread.ManagedThreadId, thread.Name);
            //}
        }
    }
}