using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Three
{
    public class Person
    {
        //Address
        public string StreatAddress, City, PostalCode;

        //Employment
        public string Company, Position;

        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreatAddress)} : {StreatAddress} \n{nameof(City)} : {City} \n{nameof(PostalCode)} : {PostalCode} \n{nameof(Company)} : {Company} \n" +
                $"{nameof(Position)} : {Position} \n{nameof(AnnualIncome)} : {AnnualIncome}";
        }
    }

    public class PersonBuilder          // Facade
    {
        // reference!
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);

        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public Person Build() 
        {
            return this.person;
        }

        // For implicit conversion
        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            this.person.Company = companyName;
            return this;
        }

        public PersonJobBuilder WorksAsA(string position)
        {
            this.person.Position = position;
            return this;
        }

        public PersonJobBuilder Earns(int annualIncome)
        {
            this.person.AnnualIncome = annualIncome;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streatAddress)
        {
            this.person.StreatAddress = streatAddress;
            return this;
        }

        public PersonAddressBuilder On(string postalCode)
        {
            this.person.PostalCode = postalCode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            this.person.City = city;
            return this;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();

            // Implicit conversion
            Person person = pb
                .Works.At("Deloitte")
                    .WorksAsA("Engineer")
                    .Earns(700000)
                .Lives.At("Baker Streat")
                      .On("52461")
                      .In("London")
                      .Build();

            // Using the implemented Build-API
            //var person = pb
            //            .Works.At("Deloitte")
            //                .WorksAsA("Engineer")
            //                .Earns(700000)
            //            .Lives.At("Baker Streat")
            //                  .On("52461")
            //                  .In("London")
            //                  .Build();

            Console.WriteLine(person);

            Console.ReadLine();
        }
    }
}
