using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace OperationAsync
{
    public class Operation
    {
        public string RunSlowOperation()
        {
            Console.WriteLine($"Slow operation running on thread id {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            Console.WriteLine($"Slow operation about to finish on thread id {Thread.CurrentThread.ManagedThreadId}");
            return "This is very slow...";
        }

        public void RunTrivialOperation()
        {
            string[] words = new string[] { "i", "love", "dot", "net", "four", "dot", "five" };
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }

        public Task<string> RunSlowOperationTask()
        {
            return Task.Factory.StartNew<string>(RunSlowOperation);
        }

        public async Task<string> RunSlowOperationTaskAsync()
        {
            Console.WriteLine("Slow operation running on thread id {0}", Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(2000);
            Console.WriteLine("Slow operation about to finish on thread id {0}", Thread.CurrentThread.ManagedThreadId);
            return "This is very slow...";
        }
    }
}
