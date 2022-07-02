using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEventConsoleOutput
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Task.Run(Method1);
            Thread.Sleep(100);
            Task.Run(Method2);
            Thread.Sleep(100);
            await Task.Run(Method3);
        }

        public static void Method1()
        {
            while (true)
            {
                _autoResetEvent.WaitOne();

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Method 1 before sleep");
                Thread.Sleep(500);
                Console.WriteLine("Method 1 after sleep");

                Console.ResetColor();

                _autoResetEvent.Set();
            }
        }

        public static void Method2()
        {
            while (true)
            {
                _autoResetEvent.WaitOne();

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("Method 2 before sleep");
                Thread.Sleep(500);
                Console.WriteLine("Method 2 after sleep");

                Console.ResetColor();

                _autoResetEvent.Set();
            }
        }

        public static void Method3()
        {
            while (true)
            {
                _autoResetEvent.WaitOne();

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Method 3 waiting to key...");
                Console.ReadKey();
                Console.WriteLine("Method 3 after read key");

                Console.ResetColor();

                _autoResetEvent.Set();
            }

        }

        private static AutoResetEvent _autoResetEvent = new AutoResetEvent(true);
    }
}
