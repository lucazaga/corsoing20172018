using System;
using System.Threading;
using System.Threading.Tasks;

namespace Unict
{
    class E08_CompositecacellationToken
    {
        public static void Run()
        {
            // create the cancellation token sources
            CancellationTokenSource tokenSource1 = new CancellationTokenSource();
            CancellationTokenSource tokenSource2 = new CancellationTokenSource();
            CancellationTokenSource tokenSource3 = new CancellationTokenSource();
            // create a composite token source using multiple tokens
            CancellationTokenSource compositeSource =
            CancellationTokenSource.CreateLinkedTokenSource(
            tokenSource1.Token, tokenSource2.Token, tokenSource3.Token);
            // create a cancellable task using the composite token
            Task task = new Task(() =>
            {
                // wait until the token has been cancelled
                compositeSource.Token.WaitHandle.WaitOne();
                Console.WriteLine("Operation cancelled");

                // throw a cancellation exception
                throw new OperationCanceledException(compositeSource.Token);
            }, compositeSource.Token);

            task.Start();

            Console.WriteLine("Task Status = {0}", task.Status);

            Thread.Sleep(1000);
            Console.WriteLine("Task Status = {0}", task.Status);


            Console.WriteLine("Press enter to cancel tasks");
            Console.ReadLine();

            // cancel one of the original tokens
            tokenSource2.Cancel();

            Console.WriteLine("Task Status = {0}", task.Status);

            Thread.Sleep(1000);
            Console.WriteLine("Task Status = {0}", task.Status);



        }
    }
}
