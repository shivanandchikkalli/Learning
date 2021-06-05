using AbstractFactory.Factory.Abstract_Interface;
using AbstractFactory.Factory.Abstract_Product;
using AbstractFactory.Factory.Concrete_Product;
using AbstractFactory.Interfaces;
using AbstractFactory.Models;

namespace AbstractFactory.Factory.Concrete_Factory
{
    public static class AppleFactory
    {
        public static IDeviceFactory GetDevice(IBaseEmployee employee)
        {
            return employee.RequestedDeviceType switch
            {
                DeviceType.Laptop => AppleLaptopFactory.GetDevice(employee),
                DeviceType.Desktop => AppleDesktopFactory.GetDevice(employee),
                _ => AppleDesktopFactory.GetDevice(employee)
            };
        }
    }

    public static class AppleLaptopFactory
    {
        public static IDeviceFactory GetDevice(IBaseEmployee employee)
        {
            return employee.Department switch
            {
                Department.Management => new AppleLaptopI5Factory(),
                Department.Engineering => new AppleLaptopI7Factory(),
                Department.Research => new AppleLaptopI9Factory(),
                _ => new AppleLaptopI5Factory()
            };
        }
    }

    public static class AppleDesktopFactory
    {
        public static IDeviceFactory GetDevice(IBaseEmployee employee)
        {
            return employee.Department switch
            {
                Department.Management => new AppleDesktopI5Factory(),
                Department.Engineering => new AppleDesktopI7Factory(),
                Department.Research => new AppleDesktopI9Factory(),
                _ => new AppleDesktopI5Factory()
            };
        }
    }


    #region Concrete Factory Classes

    public class AppleLaptopI7Factory : IDeviceFactory
    {
        public IBrand Brand()
        {
            return new Apple();
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

    public class AppleLaptopI5Factory : AppleLaptopI7Factory
    {
        public override IProcessor Processor()
        {
            return new I5();
        }
    }

    public class AppleLaptopI9Factory : AppleLaptopI7Factory
    {
        public override IProcessor Processor()
        {
            return new I9();
        }
    }

    public class AppleDesktopI9Factory : AppleLaptopI9Factory
    {
        public override IDeviceType DeviceType()
        {
            return new Desktop();
        }
    }

    public class AppleDesktopI7Factory : AppleLaptopI7Factory
    {
        public override IDeviceType DeviceType()
        {
            return new Desktop();
        }
    }

    public class AppleDesktopI5Factory : AppleLaptopI5Factory
    {
        public override IDeviceType DeviceType()
        {
            return new Desktop();
        }
    }

    #endregion
}
