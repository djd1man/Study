using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterlockedProj
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(Method1);
            Task.Run(Method2);
            Console.ReadKey();
        }

        public static void Method1()
        {
            while (true)
            {
                _locker.Enter();

                Console.WriteLine($"Method 1: {++_variable}");
                Thread.Sleep(300);

                _locker.Leave();
            }
        }
        
        public static void Method2()
        {
            while (true)
            {
                _locker.Enter();

                Console.WriteLine($"Method 2: {--_variable}");
                Thread.Sleep(300);

                _locker.Leave();
            }
        }

        public static InterlockedSlim _locker = new InterlockedSlim();

        private static int _variable = 0;
    }


    public class InterlockedSlim
    {
        public void Enter()
        {
            while (true)
            {
                if (Interlocked.CompareExchange(
                    ref _inUse, 1, 0) == 0)
                {
                    return;
                }
            }
        }

        public void Leave()
        {
            if (Interlocked.CompareExchange(
                ref _inUse, 0, 1) == 0)
            {
                throw new InvalidOperationException("The lock is not entered.");
            }
        }

        // 1 - in use, 0 - not in use
        private int _inUse;
    }
}
