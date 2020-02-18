using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientAsync
{
    class Program
    {
        private static HttpClient _httpClient;
        static async Task Main(string[] args)
        {
            Console.WriteLine("1. Calling AccessTheWebAsync...");
            Console.WriteLine();
            Task<int> getLengthTask = AccessTheWebAsync();

            Console.WriteLine("4. Task getLengthTask is started...");
            Console.WriteLine("4. About to await getLengthTask -- no caller to return to...");
            Console.WriteLine();
            int contentLength = await getLengthTask;

            Console.WriteLine("6. Task getLengthTask is finished...");
            Console.WriteLine("6. Result from AccessTheWebAsync is stored in contentLength...");
            Console.WriteLine("6. About to display contentLength and finish...");
            Console.WriteLine();
            Console.WriteLine($"Length of the downloaded string is {contentLength}");

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static async Task<int> AccessTheWebAsync()
        {
            Console.WriteLine("2. Calling httpClient.GetStringAsync...");
            Console.WriteLine();
            var httpClient = GetHttpClient();

            Task<string> getStringTask = httpClient.GetStringAsync("https://msdn.microsoft.com");

            Console.WriteLine("3. Task getStringTask is started...");
            Console.WriteLine("3. About to await getStringTask & return a Task<int> to Main...");
            Console.WriteLine();
            string urlContents = await getStringTask;

            Console.WriteLine("5. Task getStringTask is complete...");
            Console.WriteLine("5. Processing the return statement...");
            Console.WriteLine("5. Exiting from AccessTheWebAsync...");
            Console.WriteLine();
            return urlContents.Length;
        }

        private static HttpClient GetHttpClient()
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
            }

            return _httpClient;
        }
    }
}
