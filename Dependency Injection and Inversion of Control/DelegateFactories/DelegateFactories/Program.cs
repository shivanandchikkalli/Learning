using System;
using Autofac;

namespace DelegateFactories
{
    public interface ILogging
    {
        void Log(string message);
    }

    public class SmsLogger : ILogging
    {
        public void Log(string message)
        {
            Console.WriteLine($"SMS sent : Message : {message}");
        }
    }

    public class Engine
    {
        private readonly ILogging _logger;
        private readonly int _id;

        public delegate Engine CreateEngine(int power);

        public Engine(ILogging logger, int power)
        {
            _logger = logger;
            _id = power;
        }

        public void Start()
        {
            _logger.Log($"Engine {_id} is started...");
        }
    }

    public class Car
    {
        private readonly ILogging _logger;
        private readonly Engine _engine;

        public Car(ILogging logger, Engine engine)
        {
            _logger = logger;
            _engine = engine;
        }

        public void Move()
        {
            _engine.Start();
            _logger.Log("Car is moving");
        }
    }

    internal class Program
    {
        private static void Main2(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SmsLogger>().As<ILogging>();

            builder.RegisterType<Engine>();

            var container = builder.Build();

            //var engine = container.Resolve<Engine>();  // This will break as the Engine takes an extra parameter which is not present with the container.

            //var engine = container.Resolve<Engine>(new PositionalParameter(1, 123)); // This works but strictly binds with the placement of the parameter


            // Rather than resolving Engine, delegate is resolved which returns delegate object which can be invoked with parameter as follows
            var engineDelegate = container.Resolve<Engine.CreateEngine>();
            var engine = engineDelegate.Invoke(34);

            engine.Start();

            Console.ReadLine();
        }
    }
}
