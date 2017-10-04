using System;
using System.Collections.Generic;
using System.Text;

namespace Unict
{
    class Program
    {
        static void Main(string[] args)
        {
            //E01_HelloWorld.Run();
            //E02_TaskCreation.Run();
            //E03_TaskState.Run();
            //E04_CreatingSeveralTasks.Run();
            //E05_TaskResult.Run();
            //E06_TaskCancellationByPolling.Run();
            //E07_MultipleTaskCancellation.Run();
            //E08_CompositecacellationToken.Run();
            //E09_PauseToken.Run();
            //E10_HandlingTaskExceptions.Run();
            //E11_ReareWriterLock.Run();
            //E12_RaceConditionQueue.Run();
            //E13_RaceConditionQueueSolved.Run();
            //E14_Barrier.Run();
            //E15_ProducerConsumerProblem.Run();
            //E16_ProducerConsumerSolution.Run();
            //E17_IOBoundOperation.Run();
            E18_AsyncAwaitIOBound.Run();

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
