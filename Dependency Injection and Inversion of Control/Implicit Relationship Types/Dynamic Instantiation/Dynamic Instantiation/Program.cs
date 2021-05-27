using System;
using Autofac;

namespace Dynamic_Instantiation
{
    public interface ILog : IDisposable
    {
        void Log(string message);
    }

    public class SmsLog : ILog
    {
        private readonly string _phoneNumber;

        public SmsLog(string phoneNumber)
        {
            this._phoneNumber = phoneNumber;
            Console.WriteLine("SmsLog object is created");
        }
        public void Dispose()
        {
            Console.WriteLine("SmsLog object is disposed");
        }

        public void Log(string message)
        {
            Console.WriteLine($"Sms Sent to {_phoneNumber} : {message}");
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
        private readonly Func<ConsoleLog> _consoleLog;
        private readonly Func<string, SmsLog> _smsLog;

        public Reporting(Func<ConsoleLog> consoleLog, Func<string, SmsLog> smsLog)
        {
            this._consoleLog = consoleLog;
            _smsLog = smsLog;
        }

        public void Report()
        {
            // every time _consoleLog is invoked a new object is created
            _consoleLog.Invoke().Log("Reporting from Reporting Class");
            _consoleLog.Invoke().Log("and again");

            var smsLog1 = _smsLog.Invoke("+1-123456789");
            var smsLog2 = _smsLog.Invoke("+1-987654321");

            smsLog1.Log("smsLog1 logging..");
            smsLog2.Log("smsLog2 logging..");

        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLog>();
            builder.RegisterType<SmsLog>();

            builder.RegisterType<Reporting>();

            using (var container = builder.Build())
            {
                container.Resolve<Reporting>().Report();
            }

            Console.ReadLine();
        }
    }
}
