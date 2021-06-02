using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChildTasks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parentTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent Task Starting...");
                var childTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Child Task is started...");
                    Thread.Sleep(5000);
                    //throw new Exception();  // To demonstrate the failureHandler execution
                    Console.WriteLine("Child Task is completed...");
                }, TaskCreationOptions.AttachedToParent);

                var successHandler = childTask.ContinueWith(t =>
                {
                    Console.WriteLine("Child Task completed successfully...");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                var failureHandler = childTask.ContinueWith(t =>
                {
                    Console.WriteLine("Child Task failed...");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                Console.WriteLine("Parent Task completed...");
            });

            try
            {
                parentTask.Wait(); // Waiting on parent task will not wait for child tasks, i.e., child tasks are executed in detached mode,
                                   // task scheduler considers child tasks as normal independent tasks.
                Console.WriteLine("All tasks are over...");
            }
            catch (AggregateException ae)
            {
                Console.WriteLine(ae.Message);
                ae.Handle(e => true);
            }

            Console.ReadLine();
        }
    }
}
