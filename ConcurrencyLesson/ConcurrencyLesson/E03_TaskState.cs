using System;
using System.Threading;
using System.Threading.Tasks;

namespace Unict
{
    class E03_TaskState
    {
        public static void Run()
        {
            // use an Action delegate and a named method
            Task task1 = new Task(new Action<object>(printMessage),
            "First task");
            // use a anonymous delegate
            Task task2 = new Task(delegate (object obj) {
                printMessage(obj);
            }, "Second Task");
            // use a lambda expression and a named method
            // note that parameters to a lambda don’t need
            // to be quoted if there is only one parameter
            Task task3 = new Task((obj) => printMessage(obj), "Third task");
            // use a lambda expression and an anonymous method
            Task task4 = new Task((obj) => {
                printMessage(obj);
            }, "Fourth task");
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();
        }
        static void printMessage(object message)
        {
            Console.WriteLine("Message: {0}", message);
        }


    }
}
