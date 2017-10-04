using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Unict
{
    class E18_AsyncAwaitIOBound
    {
        public static void Run()
        {
            Task<string> pageTask = DownloadPageAsync("http://www.google.it");

            Console.WriteLine("I'm doing other things");
            Task.Delay(2000).Wait();

            // this is a bloccking call
            string res = pageTask.Result;

            Console.WriteLine(res.Substring(0, 200));
        }

        private static async Task<string> DownloadPageAsync(string url)
        {
            HttpClient httpClient = new HttpClient();

            string result = await httpClient.GetStringAsync(url);

            return ProcessPage(result);
        }

        private static string ProcessPage(string page)
        {
            Console.WriteLine("Processing page");
            Task.Delay(2000).Wait();
            return page.ToLower();
        }

    }
}
