using AbstractFactory.Factory.Abstract_Interface;
using AbstractFactory.Models;

namespace AbstractFactory.Factory.Client
{
    internal class EmployeeDeviceManager
    {
        private readonly IDeviceFactory _deviceFactory;

        public EmployeeDeviceManager(IDeviceFactory deviceFactory)
        {
            _deviceFactory = deviceFactory;
        }

        public WorkStation GetEmployeeWorkStation()
        {
            return new WorkStation()
            {
                Brand = _deviceFactory.Brand().GetBrand(),
                DeviceType = _deviceFactory.DeviceType().GetDeviceType(),
                Processor = _deviceFactory.Processor().GetProcessor()
            };
        }
    }
}
