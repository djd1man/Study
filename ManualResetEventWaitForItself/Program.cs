using System;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEventWaitForItself
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Run(Method3);
            //Thread.Sleep(100);    
            //await Task.Run(Method2);
        }

        public static void Method3()
        {
            _variable = 1;

            while (true)
            {
                _manualResetEventSlim.Wait();

                Console.WriteLine($"Method 3: {_variable}");
                _variable *= 2;

                Thread.Sleep(300);
            }
        }

        public static void Method1()
        {
            _manualResetEventSlim.Wait();
            _manualResetEventSlim.Reset();
            while (true)
            {


                Console.WriteLine($"Method 1: {++_variable}");
                Thread.Sleep(300);

                if (_variable > 10)
                {
                    _manualResetEventSlim.Set();
                }
            }

        }

        public static void Method2()
        {
            _manualResetEventSlim.Wait();

            while (true)
            {
                Console.WriteLine($"Method 2: {--_variable}");
                Thread.Sleep(300);
            }
        }

        private static int _variable = 0;

        private static ManualResetEventSlim _manualResetEventSlim = new ManualResetEventSlim(true);
    }
}
