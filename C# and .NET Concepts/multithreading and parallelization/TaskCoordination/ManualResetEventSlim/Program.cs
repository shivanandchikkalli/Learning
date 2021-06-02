using System;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEventSlim
{
    internal static class Program
    {
        private static readonly System.Threading.ManualResetEventSlim ManualResetEventSlim =
            new System.Threading.ManualResetEventSlim();

        private static void Main(string[] args)
        {
            var taskOne = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 1 is starting...");
                Thread.Sleep(5000);
                ManualResetEventSlim.Set();
                Console.WriteLine("Task 1 is completed.");
            });

            var taskTwo = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 2 waiting for Task 1 to complete...");
                ManualResetEventSlim.Wait();
                Console.WriteLine("Task 2 is started...");

                Console.WriteLine("All tasks are completed");
            });

            
            taskTwo.Wait();


            Console.ReadLine();
        }
    }
}
