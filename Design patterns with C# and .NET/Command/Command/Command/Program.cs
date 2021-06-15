using System;
using System.Collections.Generic;
using System.Linq;

namespace Command
{
    public interface IBankOperations
    {
        void Deposit(double amount);

        bool Withdraw(double amount);
    }
    public class BankAccount : IBankOperations
    {
        private double _balance;
        private readonly double _overdraftLimit = -500;


        public void Deposit(double amount)
        {
            _balance += amount;
            Console.WriteLine($"{amount} deposited , Current Balance = {_balance}");
        }

        public bool Withdraw(double amount)
        {
            if (!(_balance - amount >= _overdraftLimit)) return false;
            _balance -= amount;
            Console.WriteLine($"{amount} withdrawn , Current Balance = {_balance}");
            return true;
        }

        public override string ToString()
        {
            return $"{nameof(_balance)}: {_balance}";
        }
    }

    public interface ICommand
    {
        bool Success { get; set; }
        void Execute();

        void Undo();
    }

    public enum BankOperation
    {
        Deposit,
        Withdraw
    }

    public class BankAccountCommand : ICommand
    {
        private readonly BankAccount _bankAccount;
        private readonly BankOperation _operation;
        private readonly double _amount;

        public bool Success { get; set; }

        public BankAccountCommand(BankAccount bankAccount, BankOperation operation, double amount)
        {
            _bankAccount = bankAccount;
            _operation = operation;
            _amount = amount;
        }

        public void Execute()
        {
            switch (_operation)
            {
                case BankOperation.Deposit:
                    _bankAccount.Deposit(_amount);
                    Success = true;
                    break;
                case BankOperation.Withdraw:
                    Success = _bankAccount.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo()
        {
            if (!Success) return;
            switch (_operation)
            {
                case BankOperation.Deposit:
                    _bankAccount.Withdraw(_amount);
                    break;
                case BankOperation.Withdraw:
                    _bankAccount.Deposit(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class CompositeBankAccountCommand : List<BankAccountCommand>, ICommand
    {
        public CompositeBankAccountCommand()
        {
        }

        public CompositeBankAccountCommand(IEnumerable<BankAccountCommand> collection) : base(collection)
        {
        }

        public virtual bool Success
        {
            get => this.All(c => c.Success);
            set
            {
                foreach (var command in this)
                {
                    command.Success = value;
                }
            }
        }

        public virtual void Execute()
        {
            ForEach(c => c.Execute());
        }

        public virtual void Undo()
        {
            foreach (var command in ((IEnumerable<BankAccountCommand>)this).Reverse())
            {
                command.Undo();
            }
        }
    }

    public class MoneyTransferCommand : CompositeBankAccountCommand
    {
        public MoneyTransferCommand(BankAccount from, BankAccount to, double amount)
        {
            AddRange(
                new BankAccountCommand[]
                {
                    new BankAccountCommand(from, BankOperation.Withdraw, amount),
                    new BankAccountCommand(to, BankOperation.Deposit, amount)
                });
        }

        public override void Execute()
        {
            BankAccountCommand last = null;
            foreach (var command in this)
            {
                if (last == null || last.Success)
                {
                    command.Execute();
                    last = command;
                }
                else
                {
                    command.Undo();
                    break;
                }
            }
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Basic Bank Operations");
            Console.WriteLine();

            var bankAccount = new BankAccount();

            var bankAccountCommands = new List<BankAccountCommand>()
            {
                new(bankAccount, BankOperation.Deposit, 100),
                new(bankAccount, BankOperation.Withdraw, 1000)
            };

            foreach (var bankAccountCommand in bankAccountCommands)
            {
                bankAccountCommand.Execute();
            }

            bankAccountCommands.Reverse();
            foreach (var bankAccountCommand in bankAccountCommands)
            {
                bankAccountCommand.Undo();
            }

            Console.WriteLine();
            Console.WriteLine("Using CompositeBankAccountCommands");
            Console.WriteLine();

            var compositeBankAccountCommand = new CompositeBankAccountCommand(bankAccountCommands);

            compositeBankAccountCommand.Execute();
            compositeBankAccountCommand.Undo();

            Console.WriteLine();
            Console.WriteLine("Transferring Money");
            Console.WriteLine();

            var from = new BankAccount();
            from.Deposit(200);
            var to = new BankAccount();
            var moneyTransferCommand = new MoneyTransferCommand(
                from,
                to,
                200
            );

            moneyTransferCommand.Execute();

            Console.WriteLine();
            Console.WriteLine($"From Account balance : {from}");
            Console.WriteLine($"To Account balance : {to}");

            Console.WriteLine();
            Console.WriteLine("Undoing the transfer");
            Console.WriteLine();

            moneyTransferCommand.Undo();

            Console.WriteLine();
            Console.WriteLine($"From Account balance : {from}");
            Console.WriteLine($"To Account balance : {to}");

        }
    }
}
