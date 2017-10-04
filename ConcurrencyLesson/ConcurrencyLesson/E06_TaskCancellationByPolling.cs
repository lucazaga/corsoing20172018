using System;
using System.Threading;
using System.Threading.Tasks;

namespace Unict
{
    class E06_TaskCancellationByPolling
    {
        public static void Run()
        {
            // create the cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            // create the cancellation token
            CancellationToken token = tokenSource.Token;
            
            // create the task
            Task task = new Task(() =>
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Task cancel detected");
                        throw new OperationCanceledException(token);
                    }
                    else
                    {
                        Console.Write("Int value {0}\r", i);
                    }
                }
            }, token);

            // register a cancellation delegate
            token.Register(() => {
                Console.WriteLine(">>>>>> Delegate Invoked\n");
            });


            // wait for input before we start the task
            Console.WriteLine("Press enter to start task");
            Console.WriteLine("Press enter again to cancel task");
            Console.ReadLine();
            
            // start the task
            task.Start();
            
            // read a line from the console.
            Console.ReadLine();

            // cancel the task
            Console.WriteLine("Cancelling task");

            tokenSource.Cancel();


        }
    }
}
