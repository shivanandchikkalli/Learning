using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEvent
{
    internal static class Program
    {
        private static readonly System.Threading.AutoResetEvent AutoResetEvent =
            new System.Threading.AutoResetEvent(false);

        private static void Main(string[] args)
        {
            var taskOne = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 1 is starting...");
                Thread.Sleep(5000);
                AutoResetEvent.Set();
                Console.WriteLine("Task 1 is completed.");
            });

            var taskTwo = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 2 waiting for Task 1 to complete...");
                AutoResetEvent.WaitOne();
                Console.WriteLine("Task 2 is started...");
                Console.WriteLine("Waiting for AutoResetEvent to set...");
                AutoResetEvent.WaitOne();
            });


            taskTwo.Wait(); // taskTwo will never complete as its waiting for autoResetEvent to set, but no thread is executing and setting it.


            Console.ReadLine();
        }
    }
}
