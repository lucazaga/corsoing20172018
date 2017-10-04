using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Unict
{
    class E14_Barrier
    {
        class BankAccount
        {
            public int Balance
            {
                get;
                set;
            }
        }

        public static void Run()
        {
            int accountCount = 5;

            // create the array of bank accounts
            BankAccount[] accounts = new BankAccount[accountCount];
            for (int i = 0; i < accounts.Length; i++)
            {
                accounts[i] = new BankAccount();
            }

            // create the total balance counter
            int totalBalance = 0;

            // create the barrier
            Barrier barrier = new Barrier(accountCount, (myBarrier) =>
            {
                // zero the balance
                totalBalance = 0;
                // sum the account totals
                foreach (BankAccount account in accounts)
                {
                    totalBalance += account.Balance;
                }
                // write out the balance
                Console.WriteLine("Phase: {0} => Total balance: {1}", myBarrier.CurrentPhaseNumber, totalBalance);
            });

            // define the tasks array
            Task[] tasks = new Task[accountCount];
            // loop to create the tasks
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task((stateObj) =>
                {
                    // create a typed reference to the account
                    BankAccount account = (BankAccount)stateObj;

                    // start of phase
                    Random rnd = new Random();
                    for (int j = 0; j < 1000; j++)
                    {
                        account.Balance += rnd.Next(1, 100);
                    }
                    // end of phase
                    // tell the user that this task has has completed the phase
                    Console.WriteLine("Task {0}, phase {1} ended",
                    Task.CurrentId, barrier.CurrentPhaseNumber);
                    // signal the barrier
                    barrier.SignalAndWait();

                    // start of phase
                    // alter the balance of this Task's account using the total balance
                    // deduct 10% of the difference from the total balance
                    account.Balance -= (totalBalance - account.Balance) / 10;

                    // end of phase
                    // tell the user that this task has has completed the phase
                    Console.WriteLine("Task {0}, phase {1} ended",
                    Task.CurrentId, barrier.CurrentPhaseNumber);
                    // signal the barrier
                    barrier.SignalAndWait();
                },
                accounts[i]);
            }
            // start the task
            foreach (Task t in tasks)
            {
                t.Start();
            }
            // wait for all of the tasks to complete
            Task.WaitAll(tasks);
        }


    }
}
