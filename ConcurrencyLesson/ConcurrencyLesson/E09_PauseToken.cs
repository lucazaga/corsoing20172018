using System;
using System.Threading;
using System.Threading.Tasks;

namespace Unict
{
    class E09_PauseToken
    {
        public static void Run()
        {
            // create the cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            // create the cancellation token
            CancellationToken token = tokenSource.Token;

            // create the first task, which we will let run fully
            Task task1 = new Task(() =>
            {
                for (int i = 0; i < Int32.MaxValue; i++)
                {
                    // put the task to sleep for 10 seconds
                    bool cancelled = token.WaitHandle.WaitOne(3000);
                    // print out a message
                    Console.WriteLine("Task 1 - Int value {0}. Cancelled? {1}",
                    i, cancelled);
                    // check to see if we have been cancelled
                    if (cancelled)
                    {
                        throw new OperationCanceledException(token);
                    }
                }
            }, token);
            // start task
            task1.Start();
            // wait for input before exiting
            Console.WriteLine("Press enter to cancel token.");
            Console.ReadLine();
            // cancel the token
            tokenSource.Cancel();


        }
    }
}
