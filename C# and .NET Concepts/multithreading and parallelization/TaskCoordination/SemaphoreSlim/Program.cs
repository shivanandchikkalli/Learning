using System;
using System.Linq;
using System.Threading.Tasks;

namespace SemaphoreSlim
{
    internal static class Program
    {
        private static readonly System.Threading.SemaphoreSlim SemaphoreSlim = new System.Threading.SemaphoreSlim(2, 10);

        private static void Main(string[] args)
        {
            foreach (var i in Enumerable.Range(1, 10))
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Starting Task {Task.CurrentId}");
                    SemaphoreSlim.Wait();
                    Console.WriteLine($" - Completed Task {Task.CurrentId}");
                });
            }

            while (SemaphoreSlim.CurrentCount <= 2)
            {
                Console.WriteLine($"Current Count {SemaphoreSlim.CurrentCount}");
                Console.ReadKey();
                SemaphoreSlim.Release(2);
            }
        }
    }
}
