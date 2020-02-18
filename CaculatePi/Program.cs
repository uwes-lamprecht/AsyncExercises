using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine($"Busy calculating... Thread {Thread.CurrentThread.ManagedThreadId}");
            var piVal = await Pi.ComputePi().ConfigureAwait(false);

            DateTime end = DateTime.Now;
            var diff = end - start;
            Console.WriteLine($"Value of Pi: {piVal}... Thread {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Took: {diff.TotalSeconds} seconds");

            Console.ReadLine();
        }
    }
}
