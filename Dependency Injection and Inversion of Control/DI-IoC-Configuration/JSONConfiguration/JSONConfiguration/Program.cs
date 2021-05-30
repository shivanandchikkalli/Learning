using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace JSONConfiguration
{
    public interface IOperation
    {
        float Calculate(float a, float b);
    }

    public class Addition : IOperation
    {
        public float Calculate(float a, float b)
        {
            return a + b;
        }
    }

    public class Multiplication : IOperation
    {
        public float Calculate(float a, float b)
        {
            return a * b;
        }
    }

    public class CalculationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Addition>().As<IOperation>();
            builder.RegisterType<Multiplication>().As<IOperation>();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("config.json");
                .AddXmlFile("config.xml");
            var config = configurationBuilder.Build();

            var builder = new ContainerBuilder();
            var configModule = new ConfigurationModule(config);

            builder.RegisterModule(configModule);

            var container = builder.Build();

            foreach (var op in container.Resolve<IList<IOperation>>())
            {
                float a = 100, b = 54;
                Console.WriteLine($"{op.GetType().Name} of {a} and {b} = {op.Calculate(a, b)}");
            }

            Console.ReadLine();
        }
    }
}
