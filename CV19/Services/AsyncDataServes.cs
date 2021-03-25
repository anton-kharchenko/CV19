using System;
using System.Threading;
using CV19.Services.Interfaces;

namespace CV19.Services
{
    internal class AsyncDataService : IAsyncDataService
    {
        private const int SleepTime = 7000;

        public string GetResult(DateTime Time)
        {
            Thread.Sleep(SleepTime);
            return $"Result value {Time}";
        }
    }
}