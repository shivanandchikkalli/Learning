using System;
using System.Threading.Tasks;

namespace Continuations
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var task1 = Task.Factory.StartNew(() => "Task 1");

            task1.ContinueWith(task =>
            {
                Console.WriteLine($"Task 1 is done with output {task.Result}, Task 2 is running");
            });


            var taskOne = Task.Factory.StartNew(() => "Task 1");
            var taskTwo = Task.Factory.StartNew(() => "Task 2");

            Task.Factory.ContinueWhenAll(new[] {taskOne, taskTwo}, tasks =>
            {
                Console.WriteLine("Below tasks are done");
                foreach (var task in tasks)
                {
                    Console.WriteLine($" - {task.Result}");
                }
            });

            Task.Factory.ContinueWhenAny(new[] {taskOne, taskTwo}, task =>
            {
                Console.WriteLine($"{task.Result} is done its execution");
            });

            Console.ReadKey();
        }
    }
}
