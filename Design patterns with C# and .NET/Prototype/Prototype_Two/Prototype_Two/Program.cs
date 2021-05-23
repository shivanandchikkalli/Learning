using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_One
{
    public interface IDeepCopyable<T>
        where T : new()
    {
        void CopyTo(T target);

        public T DeepCopy()
        {
            T t = new T();
            CopyTo(t);
            return t;
        }
    }

    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this IDeepCopyable<T> target) where T : new()
        {
            return target.DeepCopy();
        }

        public static T DeepCopy<T>(this T target)
            where T : new()
        {
            return ((IDeepCopyable<T>) target).DeepCopy();
        }
    }

    public class Address : IDeepCopyable<Address>
    {
        public int StreetNumber;
        public string City;

        public Address()
        {
        }

        public override string ToString()
        {
            return $"{nameof(StreetNumber)} : {StreetNumber} {nameof(City)} : {City}";
        }

        public void CopyTo(Address target)
        {
            target.StreetNumber = StreetNumber;
            target.City = City;
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

        public override string ToString()
        {
            return $"{nameof(FirstName)} : {FirstName} , {nameof(LastName)} : {LastName} , {nameof(Address)} : {Address}";
        }

        public void CopyTo(Person target)
        {
            target.FirstName = FirstName;
            target.LastName = LastName;
            target.Address = Address.DeepCopy();  // ((IDeepCopyable<Address>) Address).DeepCopy();
        }
    }

    public class Employee : Person, IDeepCopyable<Employee>
    {
        public int Salary;

        public Employee()
        {
        }

        public override string ToString()
        {
            return $"{base.ToString()} , {nameof(Salary)} : {Salary}";
        }

        public void CopyTo(Employee target)
        {
            base.CopyTo(target);
            target.Salary = Salary;
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
                Address = new Address() { StreetNumber = 123, City = "London"},
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