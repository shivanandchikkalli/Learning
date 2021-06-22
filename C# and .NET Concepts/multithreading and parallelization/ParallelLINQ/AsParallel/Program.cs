using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsParallel
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var items = Enumerable.Range(1, 1000);

            items.AsParallel().ForAll(x =>
            {
                Console.WriteLine($"{x} - Thread {Task.CurrentId}");
            });

            Console.ReadLine();
        }
    }
}
