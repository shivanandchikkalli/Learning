using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Mutex
{
    public interface IBankOperations
    {
        void Deposit(int amount);
        int Withdraw(int amount);
        int CurrentBalance();

        void Transfer(IBankOperations toAccount, int amount);
    }
    public class BankAccount : IBankOperations
    {
        private int _balance;
        private readonly object _lock = new object();
        public void Deposit(int amount)
        {
            _balance += amount;
        }

        public int Withdraw(int amount)
        {
            if (_balance < amount)
                throw new Exception("Insufficient Balance");
            _balance -= amount;

            return _balance;
        }

        public int CurrentBalance() => _balance;

        public void Transfer(IBankOperations toAccount, int amount)
        {
            _balance -= amount;
            toAccount.Deposit(amount);
        }
    }
    internal class Program
    {
        private static void Main(string[] args)
        {
            IBankOperations bankAccount1 = new BankAccount();
            IBankOperations bankAccount2 = new BankAccount();
            var tasks = new List<Task>();

            var mutex1 = new System.Threading.Mutex();
            var mutex2 = new System.Threading.Mutex();

            for (var i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (var jIndex = 0; jIndex < 1000; jIndex++)
                    {
                        var gotLock = mutex1.WaitOne();
                        try
                        {
                            bankAccount1.Deposit(1000);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                        finally
                        {
                            if (gotLock)
                                mutex1.ReleaseMutex();
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (var jIndex = 0; jIndex < 1000; jIndex++)
                    {
                        var gotLock = mutex2.WaitOne();
                        try
                        {
                            bankAccount2.Deposit(1000);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                        finally
                        {
                            if (gotLock)
                                mutex2.ReleaseMutex();
                        }
                    }
                }));
            }

            for (var j = 0; j < 10; j++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (var jIndex = 0; jIndex < 1000; jIndex++)
                    {
                        var gotLock = WaitHandle.WaitAll(new WaitHandle[] {mutex1, mutex2});
                        try
                        {
                            bankAccount1.Transfer(bankAccount2, 1000);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                        finally
                        {
                            if (gotLock)
                            {
                                mutex1.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Account1 balance = {bankAccount1.CurrentBalance()}");
            Console.WriteLine($"Account2 balance = {bankAccount2.CurrentBalance()}");


            Console.ReadLine();
        }
    }
}
