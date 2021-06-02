using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BlockingCollection
{
    // BlockingCollection and the Producer-Consumer Pattern Example
    internal class Program
    {
        private static BlockingCollection<int> messages = new BlockingCollection<int>(new ConcurrentBag<int>(), 10);
        
        private static CancellationTokenSource cts = new CancellationTokenSource();

        private static readonly Random random = new Random();

        public static void ProducerConsumer()
        {
            var producer = Task.Factory.StartNew(Producer, cts.Token);
            var consumer = Task.Factory.StartNew(Consumer, cts.Token);

            cts.Token.Register(() =>
            {
                Console.WriteLine("Cancellation called on token");
            });

            try
            {
                Task.WaitAll(new[] {producer, consumer});
            }
            catch (AggregateException ae)
            {
                Console.WriteLine($"Aggregate Exception {ae.Message}");
                ae.Handle(x => true);
            }
        }

        private static void Producer()
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                var i = random.Next(100);
                messages.Add(i);
                Console.WriteLine($"+{i} \t Size {messages.Count}");
                Thread.Sleep(100);
            }
        }

        private static void Consumer()
        {
            foreach (var message in messages.GetConsumingEnumerable())
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"-{message}");
                Thread.Sleep(random.Next(1000));
            }
        }

        private static void Main(string[] args)
        {
            Task.Factory.StartNew(ProducerConsumer, cts.Token);

            Console.ReadKey();

            cts.Cancel();
        }
    }
}
