using Autofac;
using System;
using System.Globalization;

namespace GettingStarted
{
    public interface IOutput
    {
        void Write(string message);
    }

    public class ConsoleOutput : IOutput, IDisposable
    {
        public ConsoleOutput()
        {
            Console.WriteLine("ConsoleWriter is created");
        }
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void Dispose()
        {
            Console.WriteLine("ConsoleOutput is disposed.");
        }
    }

    public interface IDateWriter
    {
        void WriteDate();
    }

    public class TodayWriter : IDateWriter, IDisposable
    {
        private readonly IOutput _output;

        public TodayWriter(IOutput output)
        {
            Console.WriteLine("TodayWriter is created.");
            _output = output;
        }

        public void WriteDate()
        {
            _output.Write(DateTime.Now.ToString(new DateTimeFormatInfo()));
        }

        public void Dispose()
        {
            Console.WriteLine("TodayWriter is disposed.");
        }
    }

    internal static class Program
    {
        private static IContainer Container { get; set; }

        private static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ConsoleOutput>().As<IOutput>();
            containerBuilder.RegisterType<TodayWriter>().As<IDateWriter>();
            Container = containerBuilder.Build();

            WriteDate();

            Console.ReadLine();
        }

        private static void WriteDate()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }

            Console.WriteLine();
        }
    }
}
