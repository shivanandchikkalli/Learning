using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CriticalSections
{
    internal interface IBankOperations
    {
        void Deposit(int amount);
        int Withdraw(int amount);
        int CurrentBalance();
    }
    public class BankAccount : IBankOperations
    {
        private int _balance;
        private readonly object _lock = new object();
        public void Deposit(int amount)
        {
            //Interlocked.Add(ref _balance, amount);
            //lock (_lock)
            _balance += amount;
        }

        public int Withdraw(int amount)
        {
            //Interlocked.Add(ref _balance, -amount);
            //lock (_lock)
            _balance -= amount;

            return _balance;
        }

        public int CurrentBalance() => _balance;
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            IBankOperations bankAccount = new BankAccount();
            var tasks = new List<Task>();

            var sl = new SpinLock();

            for (var i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (var k = 0; k < 1000; k++)
                    {
                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            bankAccount.Deposit(1000);
                        }
                        finally
                        {
                            if (lockTaken)
                                sl.Exit();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (var k = 0; k < 1000; k++)
                    {
                        var lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            bankAccount.Withdraw(1000);
                        }
                        finally
                        {
                            if (lockTaken)
                                sl.Exit();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Current balance = {bankAccount.CurrentBalance()}");

            Console.WriteLine("Main program Done");

            Console.ReadLine();
        }
    }
}
