using AbstractFactory.Factory.Abstract_Product;
using AbstractFactory.Interfaces;

namespace AbstractFactory.Factory.Abstract_Interface
{
    public interface IDeviceFactory
    {
        IBrand Brand();
        IProcessor Processor();
        IDeviceType DeviceType();
    }
}
