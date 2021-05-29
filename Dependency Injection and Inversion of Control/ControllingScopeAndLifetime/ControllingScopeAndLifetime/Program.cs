using System;
using Autofac;

namespace ControllingScopeAndLifetime
{
    public interface ILog : IDisposable
    {
        void Log(string message);
    }

    public class ConsoleLog : ILog
    {
        public ConsoleLog()
        {
            Console.WriteLine("ConsoleLog is Created");
        }
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Dispose()
        {
            Console.WriteLine("ConsoleLog is disposed");
        }
    }

    public class SmsLog : ILog
    {
        private readonly long _phoneNumber;

        public SmsLog(long phoneNumber)
        {
            _phoneNumber = phoneNumber;
            Console.WriteLine("SmsLog is Created");
        }
        public void Log(string message)
        {
            Console.WriteLine($"SMS sent to {_phoneNumber} , Message : {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("SmsLog is disposed");
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLog>().As<ILog>()
                .InstancePerLifetimeScope() // Example 1
                                            // a new instance of ConsoleLog is created whenever ILog is resolved per every lifetime scope.
                                            // Once the lifetime is over the instance is disposed and for the new score a new instance is created.
                .InstancePerMatchingLifetimeScope("foo") // Example 2 
                                                        // Instance can be created only in the Lifetime scope with the name "foo", if tried to resolve in any other
                                                        // lifetime scope , an exception is thrown
                .ExternallyOwned(); // By default Autofac calls Dispose for each type when the specified lifetime scope for that type over
                                    // and calls Dispose for all the types when the container scope is over/
                                    // If you want to take control of disposing any type the specify ExternallyOwned while registering.

            var container = builder.Build();

            #region Example 1
            //using (var scope = container.BeginLifetimeScope())
            //{
            //    var consoleLog = scope.Resolve<ILog>();
            //    consoleLog.Log("Done 1");
            //}

            //using (var scope = container.BeginLifetimeScope())
            //{
            //    var consoleLog = scope.Resolve<ILog>();
            //    consoleLog.Log("Done 2");
            //}
            #endregion

            #region Example 1
            //using (var scope = container.BeginLifetimeScope("foo"))
            //{
            //    var consoleLog = scope.Resolve<ILog>();
            //    consoleLog.Log("Done 1");
            //}
            //using (var scope = container.BeginLifetimeScope())
            //{
            //    var consoleLog = scope.Resolve<ILog>();
            //    consoleLog.Log("Done 2");
            //}
            #endregion
        }
    }
}
