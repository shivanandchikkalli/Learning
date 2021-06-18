using System;

namespace Facade
{
    public class Customer
    {
        public readonly string Name;
        public readonly double AccountBalance;
        public readonly bool HasExistingLoans;
        public readonly bool IsEmployed;
        public readonly double Salary;

        public Customer(string name, double accountBalance, bool hasExistingLoans, bool isEmployed, double salary)
        {
            this.Name = name;
            this.AccountBalance = accountBalance;
            this.HasExistingLoans = hasExistingLoans;
            IsEmployed = isEmployed;
            Salary = salary;
        }
    }

    public class Bank
    {
        public static bool HasSufficientSavingAccountBalance(Customer c)
        {
            return c.AccountBalance > 10000 ? true : false;
        }
    }

    public class Loan
    {
        public static bool HasBadLoans(Customer c)
        {
            return c.HasExistingLoans;
        }
    }

    public class EmploymentDetails
    {
        public static bool IsValidEmployment(Customer c)
        {
            return c.IsEmployed && c.Salary > 2000 ? true : false;
        }
    }

    // Here Mortgage acts as a facade.
    public static class Mortgage
    {
        public static bool IsEligible(Customer c)
        {
            if (!Bank.HasSufficientSavingAccountBalance(c) || Loan.HasBadLoans(c) || !EmploymentDetails.IsValidEmployment(c))
                return false;
            else
                return true;
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var customer = new Customer("John", 12341, false, true, 4500);

            var isEligible = Mortgage.IsEligible(customer);

            Console.WriteLine($"{customer.Name} is {(isEligible ? "Eligible" : "Not Eligible")}");
        }
    }
}
