using Autofac;
using System;

namespace PropertiesAndMethodInjection
{
    public class Parent
    {
        public string Name;

        public Parent(string name = "Dummy")
        {
            Name = name;
        }

        public override string ToString()
        {
            return "I am your father";
        }
    }

    public class Child
    {
        public string Name { set; get; }
        public Parent Parent { set; get; }

        public void SetParent(Parent p)
        {
            Parent = p;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Parent)}: {Parent}";
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Parent>();
            //builder.RegisterType<Child>(); // This will not resolve the Parent type member of Child class on resolving Child object

            // 1
            //builder.RegisterType<Child>().PropertiesAutowired();

            // 2
            //builder.RegisterType<Child>()
            //    .WithProperty("Parent", new Parent());

            // 3
            //builder.Register(
            //    c =>
            //    {
            //        var _child = new Child {Parent = c.Resolve<Parent>()};
            //        return _child;
            //    }
            //);

            // 4
            builder.RegisterType<Child>()
                .OnActivated(c =>
                {
                    var parent = c.Context.Resolve<Parent>();
                    c.Instance.SetParent(parent);
                });

            var container = builder.Build();

            var child = container.Resolve<Child>().Parent; // This will return the default value for the Parent type i.e., null
            
            Console.WriteLine(child);


            Console.ReadLine();
        }
    }
}
