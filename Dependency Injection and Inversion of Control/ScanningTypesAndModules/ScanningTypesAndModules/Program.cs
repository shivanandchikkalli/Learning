using Autofac;
using System;
using System.Reflection;
using Module = Autofac.Module;

namespace ScanningTypesAndModules
{
    public interface ILog
    {
        void Log();
    }

    public class SmsLog : ILog
    {
        public void Log()
        {
            Console.WriteLine("SmsLogging...");
        }
    }

    public class ConsoleLog : ILog
    {
        public void Log()
        {
            Console.WriteLine("Console Logging...");
        }
    }

    public class Parent
    {
        public override string ToString()
        {
            return $"I am Parent";
        }
    }

    public class Child
    {
        public Parent Parent;
        public override string ToString()
        {
            return $"I am Child";
        }
    }

    public class ParentChildModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Parent>();
            builder.Register(c =>
            {
                var child = new Child() {Parent = c.Resolve<Parent>()};
                return child;
            });
            base.Load(builder);
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Registering the Types through Assembly
            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(assembly);

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("Log"))// || x.Name.Equals("Parent") || x.Name.Equals("Child"))
                .Except<ConsoleLog>()
                .Except<SmsLog>(c => c.As<ILog>().SingleInstance())
                .AsSelf();

            var container = builder.Build();


            // Registering the Types through Module

            builder.RegisterAssemblyModules(typeof(Program).Assembly);
            builder.RegisterAssemblyModules<ParentChildModule>(typeof(Program).Assembly);

            var parent = container.Resolve<Parent>();
            var child = container.Resolve<Child>();
            Console.WriteLine(parent);
            Console.WriteLine(child);

            Console.ReadLine();
        }
    }
}
