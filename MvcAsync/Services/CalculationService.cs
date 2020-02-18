using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvcAsync.Services
{
    public class CalculationService
    {
        public static string GetResult()
        {
            StringBuilder resultBuilder = new StringBuilder();

            resultBuilder.Append("Starting GetResult on thread id ").Append(Thread.CurrentThread.ManagedThreadId).Append(". ");

            Thread.Sleep(2000);

            resultBuilder.Append("This is the result of a long running calculation. ");
            resultBuilder.Append("Finishing GetResult on thread id ").Append(Thread.CurrentThread.ManagedThreadId).Append(".");

            return resultBuilder.ToString();
        }

        public static async Task<string> GetResultAsync(CancellationToken ctk)
        {
            StringBuilder resultBuilder = new StringBuilder();

            resultBuilder.Append("Starting GetResult on thread id ").Append(Thread.CurrentThread.ManagedThreadId).Append(". ");

            ctk.ThrowIfCancellationRequested();
            await Task.Delay(2000);

            resultBuilder.Append("This is the result of a long running calculation. ");
            resultBuilder.Append("Finishing GetResult on thread id ").Append(Thread.CurrentThread.ManagedThreadId).Append(".");

            return resultBuilder.ToString();
        }
    }
}