using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Autofac;

namespace RegistrationConcepts_1
{
    public interface ILogger
    {
        void Log(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class EmailLogger : ILogger
    {
        private const string _adminEmailId = "admin@foo.com"; 

        public void Log(string message)
        {
            Console.WriteLine($"Email sent to {_adminEmailId} : {message}");
        }
    }

    public class Engine
    {
        private ILogger _logger;
        public int Power;

        public Engine(ILogger logger)
        {
            _logger = logger;
        }

        public Engine(ILogger logger, int power = 100)
        {
            _logger = logger;
            Power = power;
        }

        public void Start()
        {
            _logger.Log("Engine Started...");
        }

        public override string ToString()
        {
            return $"{nameof(Power)} : {Power}";
        }
    }

    public class Car
    {
        private ILogger _logger;
        private Engine _engine;
        public int Id;

        public Car(ILogger logger, Engine engine)
        {
            _logger = logger;
            _engine = engine;
            Id = new Random().Next();
        }

        public Car(ILogger logger, Engine engine, int id)
        {
            _logger = logger;
            _engine = engine;
            Id = id;
        }

        public void Move(int distance)
        {
            _engine.Start();
            _logger.Log($"Car {Id} is moved {distance}m");
        }

        public override string ToString()
        {
            return $"{nameof(Id)} : {Id}";
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().As<ILogger>(); // ConsoleLogger is registered as a ILogger,
                                                                 // If any component is dependent on ILogger ConsoleLogger provided.
            // builder.RegisterType<EmailLogger>().As<ILogger>().PreserveExistingDefaults(); // Even though the ConsoleLogger is already registered registering another Type
                                                                                          // but with PreserveExistingDefaults which preserves the earlier registered Type
                                                                                          // as the default one.

            #region Registering the instance with the DI container
            // var log = new ConsoleLogger();
            // builder.RegisterInstance(log).As<ILogger>(); 
            #endregion

            // builder.RegisterGeneric(typeof(List<>)).As(typeof(IList<>)); // Format to register generic types

            
            //builder.RegisterType<Engine>().UsingConstructor(typeof(ILogger));


            builder.Register((c) => new Car(c.Resolve<ILogger>(), c.Resolve<Engine>(), 123)); // Format to use the other constructor with parameter passing

            // builder.RegisterType<Engine>();

            builder.RegisterType<Engine>();
            
            // builder.RegisterType<Engine>().UsingConstructor(typeof(ILogger), typeof(int)); // The constructor uses a constructor with the default parameter value.

            var container = builder.Build();

            var car = container.Resolve<Car>();


            car.Move(100);


            Console.ReadLine();
        }
    }
}
