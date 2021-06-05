using AbstractFactory.Factory.Concrete_Product;
using AbstractFactory.Models;

namespace AbstractFactory.Factory.Abstract_Product
{
    public interface IDeviceType
    {
        DeviceType GetDeviceType();
    }
}
