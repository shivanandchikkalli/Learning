using System;
using System.Reflection.Metadata.Ecma335;

namespace Monostate
{
    public class Ceo
    {
        private static string name;
        private static int age;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }

        public override string ToString()
        {
            return $"{nameof(name)} : {name} , {nameof(age)} : {age}";
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var ceo1 = new Ceo {Name = "Harry Potter", Age = 33};

            Console.WriteLine(ceo1);

            var ceo2 = new Ceo { Age = 45 };

            Console.WriteLine(ceo2);

            Console.WriteLine(ReferenceEquals(ceo1.Name, ceo2.Name));

            Console.ReadLine();
        }
    }
}
