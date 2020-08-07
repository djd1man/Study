using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreThrottlingQueue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Task.Run(WriteToQueue);
            await Task.Run(ReadFromQueue);
        }

        public static void ReadFromQueue()
        {
            while (true)
            {
                if (_queue.TryDequeue(out var res))
                {
                    Console.WriteLine("Dequeue: " + res);

                    _semaphore.Release();
                }

                Thread.Sleep(1000);
            }
        }

        public static void WriteToQueue()
        {
            int i = 0;
            while (true)
            {
                _semaphore.WaitOne();

                _queue.Enqueue(i);

                Console.WriteLine($"{i} added to queue");

                i++;

                Thread.Sleep(100);
            }
        }

        private static ConcurrentQueue<int> _queue = new ConcurrentQueue<int>();

        private static Semaphore _semaphore = new Semaphore(10, 10);
    }
}
