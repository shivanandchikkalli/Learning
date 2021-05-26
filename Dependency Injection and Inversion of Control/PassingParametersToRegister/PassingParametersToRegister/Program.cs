using Autofac;
using System;
using Autofac.Core;

namespace PassingParametersToRegister
{
    public interface ILogging
    {
        void Log(string message);
    }

    public class SMSLogger : ILogging
    {
        public string PhoneNumber;

        public SMSLogger(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        public void Log(string message)
        {
            Console.WriteLine($"SMS sent to {PhoneNumber} : Message : {message}");
        }
    }

    public class Engine
    {
        private ILogging _logger;

        public Engine(ILogging logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            _logger.Log("Engine is started...");
        }
    }

    public class Car
    {
        private ILogging _logger;
        private Engine _engine;

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
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            // 1
            //builder.RegisterType<SMSLogger>()
            //    .As<ILogging>()
            //    .WithParameter("phoneNumber", "23134679");

            // 2
            // Typed Parameter
            //builder.RegisterType<SMSLogger>()
            //    .As<ILogging>()
            //    .WithParameter(new TypedParameter(typeof(string), "1245251"));

            // 3
            // Resolved Parameter
            //builder.RegisterType<SMSLogger>()
            //    .As<ILogging>()
            //    .WithParameter(
            //        new ResolvedParameter(
            //            (paramInfo, context) => paramInfo.ParameterType == typeof(string) && paramInfo.Name == "phoneNumber",
            //            (paramInfo, context) => "3434343434"
            //        )
            //    );

            // To pass the parameters while resolving
            builder.Register((context, v) => new SMSLogger(v.Named<string>("phoneNumber")))
                    .As<ILogging>();


            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            var container = builder.Build();

            var logger = container.Resolve<ILogging>(new NamedParameter("phoneNumber", "576855634"));
            logger.Log("Parameter is passed while resolving");

            //var car = container.Resolve<Car>();

            //car.Move();

            Console.ReadLine();
        }
    }
}
