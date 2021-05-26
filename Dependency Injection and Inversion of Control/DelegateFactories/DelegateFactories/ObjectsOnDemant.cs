using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace DelegateFactories
{
    public class Entity
    {
        private Random _random = new Random();
        public int RandomInt;

        public delegate Entity Factory();

        public Entity()
        {
            RandomInt = new Random().Next();
        }

        public override string ToString()
        {
            return "Number : " + RandomInt;
        }
    }

    public class Service
    {
        private readonly Entity.Factory _factory;

        public Service(Entity.Factory factory)
        {
            _factory = factory;
        }
        public Entity Something()
        {
            var factory = _factory();
            Console.WriteLine(factory);
            return factory;
        }
    }

    internal class Program2
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<Entity>().SingleInstance();
            containerBuilder.RegisterType<Service>();

            var container = containerBuilder.Build();

            var service = container.Resolve<Service>();

            var one = service.Something();
            var two = service.Something();

            if(ReferenceEquals(one, two))
                Console.WriteLine("Equal");

            Console.ReadLine();
        }
    }
}
