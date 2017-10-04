using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Unict
{
    class E15_ProducerConsumerProblem
    {
        static private int queueMaxSize = 10;
        static int itemsCount = 20;
        static private Queue<int> myQueue = new Queue<int>();
        static object lockObj = new object();
        static bool isProducing = true;


        public static void AddItem(int someItem)
        {
            myQueue.Enqueue(someItem);
        }

        public static int RemoveItem()
        {
            int item = 0;
            try
            {
                item = myQueue.Dequeue();
            }
            catch (Exception ex)
            {
                item = -1;
                Console.WriteLine(ex.Message);
            }
            
            return item;
        }


        public static void Run()
        {
            Task producer = new Task(() =>
            {
                for (int item = 0; item < itemsCount; item++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Producing Item = {0}", item);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    AddItem(item);

                    Task.Delay(100).Wait();
                }

                isProducing = false;
            });


            Task consumer = new Task(() =>
            {
                int item;

                while ((item = RemoveItem()) >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Consuming Item = {0}", item);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Task.Delay(2000).Wait();
                }
            });

            producer.Start();
            consumer.Start();

            Task.WaitAll(producer, consumer);

            Console.WriteLine("Process Completed!");

        }

    }
}
