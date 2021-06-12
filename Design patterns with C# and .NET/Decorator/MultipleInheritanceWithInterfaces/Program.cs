using System;
using System.Collections.Generic;
using Autofac;

namespace MultipleInheritanceWithInterfaces
{
    public interface IBird
    {
        void Fly();
        int Weight { get; set; }
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }

        public void Fly()
        {
            Console.WriteLine($"Flying in the sky with weight {Weight}");
        }
    }

    public interface ILizard
    {
        void Crawl();
        int Weight { get; set; }
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }
        public void Crawl()
        {
            Console.WriteLine($"Crawling on the wall with weight {Weight}");
        }
    }

    public interface IDragon : IBird, ILizard
    {
    }

    public class Dragon : IDragon
    {
        private readonly IBird _bird;
        private readonly ILizard _lizard;
        private int _weight;

        public Dragon(IBird bird, ILizard lizard) //Bird bird, Lizard lizard)
        {
            this._bird = bird;
            this._lizard = lizard;
        }

        public void Fly()
        {
            _bird.Fly();
        }

        public void Crawl()
        {
            _lizard.Crawl();
        }

        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                _bird.Weight = value;
                _lizard.Weight = value;
            }
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Bird>().As<IBird>();
            builder.RegisterType<Lizard>().As<ILizard>();
            builder.RegisterType<Dragon>().As<IDragon>().AsSelf();


            var container = builder.Build();

            var dragon = container.Resolve<Dragon>();

            dragon.Weight = 100;
            dragon.Fly();
            dragon.Crawl();


            Console.ReadLine();
        }
    }
}
