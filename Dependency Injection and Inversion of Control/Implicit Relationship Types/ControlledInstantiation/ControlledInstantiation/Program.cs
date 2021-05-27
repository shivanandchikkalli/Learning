using System;
using Autofac;
using Autofac.Features.OwnedInstances;

namespace ControlledInstantiation
{
    public interface ILog : IDisposable
    {
        void Log(string message);
    }

    public class SmsLog : ILog
    {
        public SmsLog()
        {
            Console.WriteLine("SmsLog object is created");
        }
        public void Dispose()
        {
            Console.WriteLine("SmsLog object is disposed");
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class ConsoleLog : ILog
    {
        public ConsoleLog()
        {
            Console.WriteLine("ConsoleLog object is created");
        }
        public void Dispose()
        {
            Console.WriteLine("ConsoleLog object is disposed");
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class Reporting
    {
        private readonly Owned<ILog> _log;

        public Reporting(Owned<ILog> log)
        {
            this._log = log;
            Console.WriteLine("Reporting object is created");
        }

        public void Report(string message)
        {
            _log.Value.Log(message);
            _log.Dispose();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLog>().As<ILog>();
            builder.RegisterType<ConsoleLog>().As<ILog>().PreserveExistingDefaults();

            builder.RegisterType<Reporting>();

            using (var container = builder.Build())
            {
                container.Resolve<Reporting>().Report("From Main");
                Console.WriteLine("Reporting is done");
            }

            Console.ReadLine();
        }
    }
}
