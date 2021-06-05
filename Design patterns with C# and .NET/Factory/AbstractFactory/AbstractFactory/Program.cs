using System;
using AbstractFactory.Factory;
using AbstractFactory.Factory.Client;
using AbstractFactory.Factory.Concrete_Factory;
using AbstractFactory.Factory.Concrete_Product;
using AbstractFactory.Models;

namespace AbstractFactory
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var visitingEmployee = new VisitingEmployee()
            {
                EmployeeId = 593826,
                FirstName = "John",
                MiddleName = "",
                LastName = "Skull",
                DateOfBirth = new DateTime(1984, 3, 12),
                Gender = Gender.Male,
                RatePerVisit = 35,
                VisitCount = 13,
                Department = Department.Management,
                RequestedDeviceType = DeviceType.Laptop
            };

            var permanentEmployee = new PermanentEmployee()
            {
                EmployeeId = 513873,
                FirstName = "Ted",
                MiddleName = "",
                LastName = "M",
                DateOfBirth = new DateTime(1995, 6, 5),
                Gender = Gender.Male,
                JoinedDate = new DateTime(2016, 11, 10),
                MonthlySalary = 150,
                Department = Department.Research,
                RequestedDeviceType = DeviceType.Laptop
            };

            var contractEmployee = new ContractEmployee()
            {
                EmployeeId = 598764,
                FirstName = "Mary",
                MiddleName = "",
                LastName = "M",
                DateOfBirth = new DateTime(1993, 8, 15),
                Gender = Gender.Female,
                ContractStartDate = new DateTime(1995, 6, 5),
                ContractEndDate = new DateTime(1995, 6, 5),
                HourlyRate = 60,
                WorkingHoursPerDay = 8,
                Department = Department.Engineering
            };

            var deviceFactory = EmployeeDeviceFactory.Create(permanentEmployee);
            var employeeDeviceManager = new EmployeeDeviceManager(deviceFactory);

            var employeeWorkStation = employeeDeviceManager.GetEmployeeWorkStation();

            Console.WriteLine(employeeWorkStation);

            Console.ReadLine();
        }
    }
}
