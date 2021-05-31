using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterLocks
{
    internal class Program
    {
        private static readonly ReaderWriterLockSlim PadLock = new ReaderWriterLockSlim();
        private static void Main(string[] args)
        {
            int x = 0;
            var random = new Random();
            var tasks = new List<Task>();

            for (var i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    PadLock.EnterReadLock();

                    Console.WriteLine($"Value of x : {x}");
                    Thread.Sleep(3000);
                    PadLock.ExitReadLock();
                    Console.WriteLine("Reading is over");
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    PadLock.EnterWriteLock();

                    x = random.Next();
                    Console.WriteLine($"Value of x changed to random number : {x}");
                    Thread.Sleep(3000);

                    PadLock.ExitWriteLock();
                    Console.WriteLine("Writing is over");
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    PadLock.EnterUpgradeableReadLock();

                    if (i % 2 == 0)
                    {
                        PadLock.EnterWriteLock();
                        x = random.Next();
                        PadLock.ExitWriteLock();
                    }

                    Console.WriteLine($"Value of x : {x}");
                    Thread.Sleep(3000);

                    PadLock.ExitUpgradeableReadLock();

                    Console.WriteLine("Upgradable reading is over");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(exception =>
                {
                    Console.WriteLine($"{exception.Message} thrown");
                    return true;
                });
            }
        }
    }
}
