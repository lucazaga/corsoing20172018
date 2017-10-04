using System;
using System.Threading;
using System.Threading.Tasks;

namespace Unict
{
    class E01_HelloWorld
    {
        public static void Run()
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Hello World");
            });

            //Thread.Sleep(1000);

        }
    }
}
