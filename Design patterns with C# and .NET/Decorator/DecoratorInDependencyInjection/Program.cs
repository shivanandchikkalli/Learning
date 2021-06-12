using Autofac;
using System;

namespace DecoratorInDependencyInjection
{
    public interface IReportingService
    {
        void Report(string message);
    }

    public class ReportingService : IReportingService
    {
        public void Report(string message)
        {
            Console.WriteLine(message);
        }
    }

    // Below class is a decorator to include the logging feature on top of the Reporting ServiceClass
    public class ReportingServiceWithLogging : IReportingService
    {
        private readonly IReportingService _reportingService;

        public ReportingServiceWithLogging(IReportingService reportingService)
        {
            this._reportingService = reportingService;
        }

        public void Report(string message)
        {
            Console.WriteLine($"Reporting Starting at {DateTime.Now}");
            _reportingService.Report(message);
            Console.WriteLine($"Reporting Ended at {DateTime.Now}");
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ReportingService>().Named<IReportingService>("reporting"); // If ReportingServiceWithLogging is registered
                                                                              //   with IReportingService, there will be stack overflow Autofac will get 
                                                                              // stuck in infinite loop to inject ReportingServiceWithLogging into
                                                                              // ReportingServiceWithLogging.

            //builder.RegisterType<ReportingServiceWithLogging>();

            builder.RegisterDecorator<IReportingService>((context, service) => new ReportingServiceWithLogging(service), "reporting");


            var container = builder.Build();

            var reportingServiceWithLogging = container.Resolve<IReportingService>();

            reportingServiceWithLogging.Report("Begin...");


            Console.ReadLine();
        }
    }
}
