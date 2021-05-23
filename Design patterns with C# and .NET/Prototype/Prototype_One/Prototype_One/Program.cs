using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_One
{
    // This current implementation does not scale well.
    // Each class which inherits has to initialize its own members + the base class members (else needs to be initialized in the DeepCopy method),
    // As the inheritance chain increases there will be lot of duplicate code needs to be written in each class which requires DeepCopy interface.
    public interface IDeepCopyable<T>
    {
        T DeepCopy();
    }
    public class Address : IDeepCopyable<Address>
    {
        public int StreetNumber;
        public string City;

        public Address()
        {
            
        }

        public Address(int streetNumber, string city)
        {
            StreetNumber = streetNumber;
            City = city;
        }

        public Address DeepCopy()
        {
            return new Address(StreetNumber, City);
        }

        public override string ToString()
        {
            return $"{nameof(StreetNumber)} : {StreetNumber} {nameof(City)} : {City}";
        }
    }

    public class Person : IDeepCopyable<Person>
    {
        public string FirstName;
        public string LastName;
        public Address Address;

        public Person()
        {
            
        }

        public Person(string firstName, string lastName, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        public Person DeepCopy()
        {
            return new Person((string)FirstName.Clone(), (string)LastName.Clone(), Address.DeepCopy());
        }

        public override string ToString()
        {
            return $"{nameof(FirstName)} : {FirstName} , {nameof(LastName)} : {LastName} , {nameof(Address)} : {Address}";
        }
    }

    public class Employee : Person, IDeepCopyable<Employee>
    {
        public int Salary;

        public Employee()
        {
            
        }

        public Employee(string firstName, string lastName, Address address, int salary) 
            : base(firstName, lastName, address)
        {
            Salary = salary;
        }

        public new Employee DeepCopy()
        {
            return new Employee((string) FirstName.Clone(), (string) LastName.Clone(), Address.DeepCopy(), Salary);
        }

        public override string ToString()
        {
            return $"{base.ToString()} , {nameof(Salary)} : {Salary}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee()
            {
                FirstName = "Harry",
                LastName = "Potter",
                Address = new Address(123, "London"),
                Salary = 300000
            };
            
            Employee employee1 = employee.DeepCopy();
            employee1.FirstName = "Victor";
            employee1.LastName = "Trevor";
            employee1.Salary = 123000;

            Console.WriteLine(employee);
            Console.WriteLine(employee1);


            Console.ReadLine();
        }
    }
}
