using System;

namespace MultipleInheritanceDefaultInterfaceMembers
{
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird : ICreature
    {
        void Fly()
        {
            if(Age >= 10)
                Console.WriteLine("I am flying");
        }
    }

    public interface ILizard : ICreature
    {
        void Crawl()
        {
            if(Age < 10)
                Console.WriteLine("I am crawling");
        }
    }

    public abstract class Organism
    {
    }

    public class Dragon : Organism, ILizard, IBird
    {
        public int Age { get; set; }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var dragon = new Dragon() {Age = 100};
            if(dragon is IBird dr)
                dr.Fly();
            if(dragon is ILizard liz)
                liz.Crawl();

            Console.ReadKey();
        }
    }
}
