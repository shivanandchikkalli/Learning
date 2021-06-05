using AbstractFactory.Factory.Abstract_Product;
using AbstractFactory.Models;

namespace AbstractFactory.Factory.Concrete_Product
{
    public class Desktop : IDeviceType
    {
        public DeviceType GetDeviceType()
        {
            return DeviceType.Desktop;
        }
    }
}
