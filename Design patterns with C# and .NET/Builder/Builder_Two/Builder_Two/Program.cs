using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Two
{
    public class Person
    {
        public string Name;
        public string Position;

        public Person()
        {
        }

        public class Builder : PersonJobBuilder<Builder>
        { 
        }

        public static Builder New => new Builder();

        public Person(string name, string position)
        {
            this.Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            this.Position = position ?? throw new ArgumentNullException(paramName: nameof(position));
        }

        public override string ToString()
        {
            return $"{nameof(Name)} : {this.Name}  {nameof(Position)} : {this.Position}";
        }
    }

    public class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF> : PersonBuilder where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF) this;
        }

        public override string ToString()
        {
            return person.ToString();
        }
    }

    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>> where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF) this;
        }

        public override string ToString()
        {
            return person.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var person = Person.New
                    .Called("John")
                    .WorksAsA("Engineer")
                    .Build();

            Console.WriteLine(person);

            Console.ReadLine();
        }
    }
}
