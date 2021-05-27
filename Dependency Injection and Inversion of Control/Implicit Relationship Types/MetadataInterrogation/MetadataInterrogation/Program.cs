using Autofac;
using System;
using System.Collections.Generic;
using Autofac.Features.Metadata;

namespace MetadataInterrogation
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
        private readonly Meta<ConsoleLog,Settings> _logMeta;

        public Reporting(Meta<ConsoleLog, Settings> logMeta)
        {
            this._logMeta = logMeta;
        }

        public void Report()
        {
            // for key-value metadata - not strongly typed
            //var metaData = _logMeta.Metadata["mode"] as string;
            //_logMeta.Value.Log(metaData);

            var metaData = _logMeta.Metadata.LogMode;
            _logMeta.Value.Log(metaData);
        }
    }

    public class Settings
    {
        public string LogMode { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLog>().WithMetadata("mode", "Verbose"); // Using key-value pair - This is not strongly typed

            // For strongly typed Meta data
            builder.RegisterType<ConsoleLog>()
                .WithMetadata<Settings>(c => c.For(x => x.LogMode, "Verbose"));

            builder.RegisterType<Reporting>();

            var container = builder.Build();

            var report = container.Resolve<Reporting>();

            report.Report();


            Console.ReadLine();
        }
    }
}
