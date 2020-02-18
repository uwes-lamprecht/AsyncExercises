using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    public static class Pi
    {
        public static async Task<double> ComputePi()
        {
            var sum = 0.0;
            var step = 1e-9;
            
            await Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 1000000000; i++)
                {
                    var x = (i + 0.5) * step;
                    sum += 4.0 / (1.0 + x * x);
                }
            });
            Console.WriteLine($"Finished calculating Thread {Thread.CurrentThread.ManagedThreadId}");

            return sum * step;
        }
    }
}
