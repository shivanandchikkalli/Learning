using Autofac;
using System;
using System.Collections.Generic;

namespace Enumeration
{
    public interface ILog
    {
        void Log(string message);
    }

    public class SmsLog : ILog
    {
        private readonly string _phoneNumber;

        public SmsLog(string phoneNumber)
        {
            this._phoneNumber = phoneNumber;
        }

        public void Log(string message)
        {
            Console.WriteLine($"Sms Sent to {_phoneNumber} : Message : {message}");
        }
    }

    public class ConsoleLog : ILog
    {
        public ConsoleLog()
        {
            
        }
        public void Log(string message)
        {
            Console.WriteLine($"{message}");
        }
    }

    public class EmailLog : ILog
    {
        private readonly string _email;

        public EmailLog(string email)
        {
            _email = email;
        }

        public void Log(string message)
        {
            Console.WriteLine($"Email sent to {_email} : Message {message}");
        }
    }

    public class Reporting
    {
        private readonly IList<ILog> _allLogs;

        public Reporting(IList<ILog> allLogs)
        {
            this._allLogs = allLogs;
        }

        public void Report()
        {
            foreach (var log in _allLogs)
            {
                log.Log($"The Logging type is : {log.GetType().Name}");
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLog>().As<ILog>();
            builder.Register(c => new SmsLog("+123456789")).As<ILog>();
            builder.Register(c => new EmailLog("asdfg@outlook.com")).As<ILog>();

            builder.RegisterType<Reporting>();

            var container = builder.Build();

            var report = container.Resolve<Reporting>();

            report.Report();


            Console.ReadLine();
        }
    }
}
