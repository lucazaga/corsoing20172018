using System;
using System.Threading;
using System.Threading.Tasks;

namespace Unict
{
    class E04_CreatingSeveralTasks
    {
        public static void Run()
        {
            string[] messages = { "First task", "Second task", "Third task", "Fourth task" };

            foreach (string msg in messages)
            {
                Task myTask = new Task(obj => printMessage((string)obj), msg);
                myTask.Start();
            }
        }
        static void printMessage(object message)
        {
            Console.WriteLine("Message: {0}", message);
        }


    }
}
