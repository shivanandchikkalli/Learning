using AbstractFactory.Factory.Abstract_Interface;
using AbstractFactory.Factory.Abstract_Product;
using AbstractFactory.Factory.Concrete_Product;
using AbstractFactory.Interfaces;
using AbstractFactory.Models;

namespace AbstractFactory.Factory.Concrete_Factory
{
    public static class DellFactory
    {
        public static IDeviceFactory GetDevice(IBaseEmployee employee)
        {
            return employee.RequestedDeviceType switch
            {
                DeviceType.Laptop => DellLaptopFactory.GetDevice(employee),
                DeviceType.Desktop => DellDesktopFactory.GetDevice(employee),
                _ => DellDesktopFactory.GetDevice(employee)
            };
        }
    }

    public static class DellLaptopFactory
    {
        public static IDeviceFactory GetDevice(IBaseEmployee employee)
        {
            return employee.Department switch
            {
                Department.Management => new DellLaptopI5Factory(),
                Department.Engineering => new DellLaptopI7Factory(),
                Department.Research => new DellLaptopI9Factory(),
                _ => new DellLaptopI5Factory()
            };
        }
    }

    public static class DellDesktopFactory
    {
        public static IDeviceFactory GetDevice(IBaseEmployee employee)
        {
            return employee.Department switch
            {
                Department.Management => new DellDesktopI5Factory(),
                Department.Engineering => new DellDesktopI7Factory(),
                Department.Research => new DellDesktopI9Factory(),
                _ => new DellDesktopI5Factory()
            };
        }
    }

    #region Concrete Factory Classes

    public class DellLaptopI7Factory : IDeviceFactory
    {
        public IBrand Brand()
        {
            return new Dell();
        }

        public virtual IProcessor Processor()
        {
            return new I7();
        }

        public virtual IDeviceType DeviceType()
        {
            return new Laptop();
        }
    }

    public class DellLaptopI5Factory : DellLaptopI7Factory
    {
        public override IProcessor Processor()
        {
            return new I5();
        }
    }

    public class DellLaptopI9Factory : DellLaptopI7Factory
    {
        public override IProcessor Processor()
        {
            return new I9();
        }
    }

    public class DellDesktopI9Factory : DellLaptopI9Factory
    {
        public override IDeviceType DeviceType()
        {
            return new Desktop();
        }
    }

    public class DellDesktopI7Factory : DellLaptopI7Factory
    {
        public override IDeviceType DeviceType()
        {
            return new Desktop();
        }
    }

    public class DellDesktopI5Factory : DellLaptopI5Factory
    {
        public override IDeviceType DeviceType()
        {
            return new Desktop();
        }
    }

    #endregion
}
