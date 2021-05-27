using Autofac;
using System;

namespace DelayedInstantiation
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
        private readonly Lazy<ILog> _log;

        public Reporting(Lazy<ILog> log)
        {
            this._log = log;
            Console.WriteLine("Reporting object is created");
        }

        public void Report(string message)
        {
            _log.Value.Log(message);
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
                // first reporting object is created,
                // then when report is called on report obj , _log.Value accessed then console log object will be created
                // the message is logged
                // then the console log is disposed
            }

            Console.ReadLine();
        }
    }
}
