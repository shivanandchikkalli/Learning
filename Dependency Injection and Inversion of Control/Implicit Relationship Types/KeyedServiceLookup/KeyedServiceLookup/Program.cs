using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Features.Indexed;

namespace KeyedServiceLookup
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
        private readonly IIndex<string, ILog> _iAllLogs;

        public Reporting(IIndex<string, ILog> iAllLogs)
        {
            _iAllLogs = iAllLogs;
        }

        public void Report()
        {
            _iAllLogs["sms"].Log("Key Service Lookup");
            _iAllLogs["email"].Log("Key Service Lookup");
            _iAllLogs["cmd"].Log("Key Service Lookup");
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLog>().Keyed<ILog>("cmd");
            builder.Register(c => new SmsLog("+123456789")).Keyed<ILog>("sms");
            builder.Register(c => new EmailLog("asdfg@outlook.com")).Keyed<ILog>("email");

            builder.RegisterType<Reporting>();

            var container = builder.Build();

            var report = container.Resolve<Reporting>();

            report.Report();


            Console.ReadLine();
        }
    }
}
