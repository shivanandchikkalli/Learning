using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Prototype_One
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            var copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(stream, self);
                stream.Position = 0;
                return (T) formatter.Deserialize(stream);
            }
        }

        public static T DeepCopyNewtonSoft<T>(this T self)
        {
            var sObject = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(sObject);
        }
    }

    public class Address
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

    }

    public class Person
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

    }

    public class Employee : Person
    {
        public int Salary;

        public Employee()
        {
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
                Address = new Address() { StreetNumber = 123, City = "London" },
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