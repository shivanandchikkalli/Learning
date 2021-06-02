using System;
using System.Threading;
using System.Threading.Tasks;

namespace Barrier
{
    internal class Program
    {
        private static readonly System.Threading.Barrier Barrier = new System.Threading.Barrier(2, x =>
        {
            Console.WriteLine();
            Console.WriteLine($"-------------Barrier phase {x.CurrentPhaseNumber} is done-------------");
            Console.WriteLine();
        });

        private static void Participant1()
        {
            Console.WriteLine("Participant1 executing phase 0");
            Barrier.SignalAndWait();
            Console.WriteLine("Participant1 executing phase 1");
            Barrier.SignalAndWait();
            Console.WriteLine("Participant1 executing phase 2");
            Barrier.SignalAndWait();
            Console.WriteLine("Participant1 done with it's execution");
        }

        private static void Participant2()
        {
            Console.WriteLine("Participant2 executing phase 0");
            Barrier.SignalAndWait();
            Console.WriteLine("Participant2 executing phase 1");
            Barrier.SignalAndWait();
            Console.WriteLine("Participant2 executing phase 2");
            Barrier.SignalAndWait();
            Console.WriteLine("Participant2 done with it's execution");
        }


        private static void Main(string[] args)
        {
            var thread1 = Task.Factory.StartNew(Participant1);
            var thread2 = Task.Factory.StartNew(Participant2);

            var task3 = Task.Factory.ContinueWhenAll(new[] {thread1, thread2}, delegate(Task[] tasks)
            {
                Console.WriteLine("Both participants successfully completed with all the phases");
            });

            task3.Wait();


            Console.ReadLine();
        }
    }
}
