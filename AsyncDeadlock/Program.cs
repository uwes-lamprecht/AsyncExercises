using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDeadlock
{
    class Program
    {
        static void Main(string[] args)
        {
            var thread = new Thread(() =>
            {
                Console.WriteLine("Thread.Start");
                var staContext = new DedicatedThreadSynchronisationContext();
                staContext.Send((state) =>
                {
                    Deadlock();
                }, null);
                Console.WriteLine("Thread.End");
            });
            thread.Start();
            Console.WriteLine("Thread.Join.Start");
            thread.Join();
            Console.WriteLine("Thread.Join.End");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
            Console.WriteLine("Pressed");
        }

        private static void Deadlock()
        {
            Console.WriteLine("Deadlock.Start");
            // Start the delay.
            var delayTask = DelayAsync();
            // Wait for the delay to complete.
            delayTask.Wait();
            Console.WriteLine("Deadlock.End");
        }

        private static async Task DelayAsync()
        {
            Console.WriteLine("DelayAsync.Start");
            await Task.Delay(1000);
            Console.WriteLine("DelayAsync.End");
        }
    }
}
