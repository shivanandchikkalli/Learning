using AbstractFactory.Factory.Abstract_Interface;
using AbstractFactory.Interfaces;
using AbstractFactory.Models;

namespace AbstractFactory.Factory.Concrete_Factory
{
    public static class EmployeeDeviceFactory
    {
        public static IDeviceFactory Create(IBaseEmployee employee)
        {
            return employee.EmployeeType switch
            {
                EmployeeType.Permanent => AppleFactory.GetDevice(employee),
                EmployeeType.Contract => DellFactory.GetDevice(employee),
                EmployeeType.Visiting => HewlettPackardFactory.GetDevice(employee),
                _ => HewlettPackardFactory.GetDevice(employee)
            };
        }
    }
}
