using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Unict
{
    class E17_IOBoundOperation
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

        private static Task<string> DownloadPageAsync(string url)
        {
            return Task.Run(() =>
            {
                try
                {
                    HttpClient httpClient = new HttpClient();

                    string result = httpClient.GetStringAsync(url).Result;

                    return ProcessPage(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return ex.Message;
                }
            });
        }

        private static string ProcessPage(string page)
        {
            Console.WriteLine("Processing page");
            Task.Delay(2000).Wait();
            return page.ToLower();
        }

    }
}
