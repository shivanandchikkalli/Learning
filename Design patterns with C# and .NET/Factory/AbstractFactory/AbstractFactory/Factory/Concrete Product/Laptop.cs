using AbstractFactory.Factory.Abstract_Product;

namespace AbstractFactory.Factory.Concrete_Product
{
    public class Laptop : IDeviceType
    {
        public DeviceType GetDeviceType()
        {
            return DeviceType.Laptop;
        }
    }
}
