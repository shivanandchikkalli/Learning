using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Coding_Exercise
{
    public class Person
    {
        public int Id;
        public string Name;

        public override string ToString() => $"Id : {Id} Name : {Name}";
    }

    public class PersonFactory
    {
        private static int _lastPersonId = -1;
        public Person CreatePerson(string name)
        {
            return new Person(){Id = ++_lastPersonId, Name = name};
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var personFactory = new PersonFactory();

            var person1 = personFactory.CreatePerson("John");
            Console.WriteLine(person1);

            var person2 = personFactory.CreatePerson("Harry");
            Console.WriteLine(person2);

            var person3 = personFactory.CreatePerson("Michael");
            Console.WriteLine(person3);

            var person4 = personFactory.CreatePerson("James");
            Console.WriteLine(person4);

            Console.ReadLine();
        }
    }
}
