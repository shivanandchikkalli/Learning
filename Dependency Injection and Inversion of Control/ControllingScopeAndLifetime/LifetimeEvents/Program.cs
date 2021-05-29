using Autofac;
using System;

namespace LifetimeEvents
{
    public class Parent
    {
        public Parent()
        {
            Console.WriteLine("Parent created.");
        }
    }

    public class Child
    {
        public string Name { get ; set; }
        public Parent Parent {get ; set;}

        public void SetParent(Parent p)
        {
            Parent = p;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Parent>();
            builder.RegisterType<Child>()
                .OnActivating(a =>
                {
                    Console.WriteLine("Child activating");
                    //a.Instance.Parent = a.Context.Resolve<Parent>();
                })
                .OnActivated(a =>
                {
                    Console.WriteLine("Child activated");
                })
                .OnRelease(a =>
                {
                    Console.WriteLine("Child about to be removed");
                });

            using (var scope = builder.Build().BeginLifetimeScope())
            {
                var child = scope.Resolve<Child>();
                var parent = child.Parent;
                Console.WriteLine(child);
                Console.WriteLine(parent);

            }

            Console.ReadLine();
        }
    }
}
