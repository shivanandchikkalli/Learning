using Autofac;
using System;

namespace Modules
{
    public interface IVehicle
    {
        void Go();
    }

    public class Truck : IVehicle
    {
        private readonly IDriver _driver;

        public Truck(IDriver driver)
        {
            this._driver = driver;
        }

        public void Go()
        {
            _driver.Drive();
        }
    }

    public interface IDriver
    {
        void Drive();
    }

    public class CrazyDriver : IDriver
    {
        public void Drive()
        {
            Console.WriteLine("Going fast, probably crash and kill people.");
        }
    }

    public class SaneDriver : IDriver
    {
        public void Drive()
        {
            Console.WriteLine("Going slowly towards the destination.");
        }
    }

    public class TransportModule : Module
    {
        public bool ObeySpeedLimit { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            if (ObeySpeedLimit)
                builder.RegisterType<SaneDriver>().As<IDriver>();
            else
                builder.RegisterType<CrazyDriver>().As<IDriver>();
            builder.RegisterType<Truck>().As<IVehicle>();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TransportModule());

            using (var container = builder.Build())
            {
                var vehicle = container.Resolve<IVehicle>();
                vehicle.Go();
            }

            Console.ReadLine();
        }
    }
}