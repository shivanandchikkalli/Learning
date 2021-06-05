using AbstractFactory.Factory.Abstract_Interface;
using AbstractFactory.Factory.Abstract_Product;
using AbstractFactory.Factory.Concrete_Product;
using AbstractFactory.Interfaces;
using AbstractFactory.Models;

namespace AbstractFactory.Factory.Concrete_Factory
{
    public static class HewlettPackardFactory
    {
        public static IDeviceFactory GetDevice(IBaseEmployee employee)
        {
            return employee.RequestedDeviceType switch
            {
                DeviceType.Laptop => HewlettPackardLaptopFactory.GetDevice(employee),
                DeviceType.Desktop => HewlettPackardDesktopFactory.GetDevice(employee),
                _ => HewlettPackardDesktopFactory.GetDevice(employee)
            };
        }
    }

    public static class HewlettPackardLaptopFactory
    {
        public static IDeviceFactory GetDevice(IBaseEmployee employee)
        {
            return employee.Department switch
            {
                Department.Management => new HewlettPackardLaptopI5Factory(),
                Department.Engineering => new HewlettPackardLaptopI7Factory(),
                Department.Research => new HewlettPackardLaptopI9Factory(),
                _ => new HewlettPackardLaptopI5Factory()
            };
        }
    }

    public static class HewlettPackardDesktopFactory
    {
        public static IDeviceFactory GetDevice(IBaseEmployee employee)
        {
            return employee.Department switch
            {
                Department.Management => new HewlettPackardDesktopI5Factory(),
                Department.Engineering => new HewlettPackardDesktopI7Factory(),
                Department.Research => new HewlettPackardDesktopI9Factory(),
                _ => new HewlettPackardDesktopI5Factory()
            };
        }
    }

    #region Concrete Factory Classes

    public class HewlettPackardLaptopI7Factory : IDeviceFactory
    {
        public IBrand Brand()
        {
            return new HewlettPackard();
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

    public class HewlettPackardLaptopI5Factory : HewlettPackardLaptopI7Factory
    {
        public override IProcessor Processor()
        {
            return new I5();
        }
    }

    public class HewlettPackardLaptopI9Factory : HewlettPackardLaptopI7Factory
    {
        public override IProcessor Processor()
        {
            return new I9();
        }
    }

    public class HewlettPackardDesktopI9Factory : HewlettPackardLaptopI9Factory
    {
        public override IDeviceType DeviceType()
        {
            return new Desktop();
        }
    }

    public class HewlettPackardDesktopI7Factory : HewlettPackardLaptopI7Factory
    {
        public override IDeviceType DeviceType()
        {
            return new Desktop();
        }
    }

    public class HewlettPackardDesktopI5Factory : HewlettPackardLaptopI5Factory
    {
        public override IDeviceType DeviceType()
        {
            return new Desktop();
        }
    }

    #endregion
}
