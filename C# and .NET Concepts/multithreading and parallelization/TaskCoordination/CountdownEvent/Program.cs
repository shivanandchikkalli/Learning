using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CountdownEvent
{
    internal static class Program
    {
        private static readonly System.Threading.CountdownEvent Countdown = new System.Threading.CountdownEvent(5);

        private static readonly Random Random = new Random();

        private static void Main(string[] args)
        {
            foreach (var i in Enumerable.Range(1,5))
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Starting Task {Task.CurrentId}...");
                    Thread.Sleep(Random.Next(10000));
                    Countdown.Signal();
                    Console.WriteLine($" - Completed Task {Task.CurrentId}.");
                });
            }

            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Waiting for other tasks to complete, TaskId = {Task.CurrentId}");
                Countdown.Wait();
                Console.WriteLine("All tasks are done with their execution");
            });

            task.Wait();

            Console.ReadLine();
        }
    }
}
