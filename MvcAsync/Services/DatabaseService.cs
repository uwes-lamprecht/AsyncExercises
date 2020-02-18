using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvcAsync.Services
{
    public class DatabaseService
    {
        public static string GetData()
        {
            StringBuilder dataBuilder = new StringBuilder();
            
            dataBuilder.Append("Starting GetData on thread id ").Append(Thread.CurrentThread.ManagedThreadId).Append(". ");
            
            Thread.Sleep(2000);
            
            dataBuilder.Append("Results from the database. ").Append(Environment.NewLine);
            dataBuilder.Append("Finishing GetData on thread id ").Append(Thread.CurrentThread.ManagedThreadId).Append(".");
            
            return dataBuilder.ToString();
        }

        public static async Task<string> GetDataAsync(CancellationToken ctk)
        {
            StringBuilder dataBuilder = new StringBuilder();

            dataBuilder.Append("Starting GetData on thread id ").Append(Thread.CurrentThread.ManagedThreadId).Append(". ");

            ctk.ThrowIfCancellationRequested();
            await Task.Delay(2000);

            dataBuilder.Append("Results from the database. ").Append(Environment.NewLine);
            dataBuilder.Append("Finishing GetData on thread id ").Append(Thread.CurrentThread.ManagedThreadId).Append(".");

            return dataBuilder.ToString();
        }
    }
}