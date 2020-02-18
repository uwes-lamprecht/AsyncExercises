using System;
using System.Threading;
using System.Threading.Tasks;

namespace OperationAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            Operation operation = new Operation();

            Task<string> result = operation.RunSlowOperationTaskAsync();
            operation.RunTrivialOperation();

            Console.WriteLine("Return value of slow operation: {0}", result.Result);
            Console.WriteLine("The main thread has run complete on thread number {0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();
        }
    }
}
