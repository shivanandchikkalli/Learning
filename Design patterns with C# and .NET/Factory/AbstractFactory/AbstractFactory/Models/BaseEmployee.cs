using System;
using System.Collections.Generic;
using System.Text;
using AbstractFactory.Factory.Concrete_Product;
using AbstractFactory.Interfaces;

namespace AbstractFactory.Models
{
    public abstract class BaseEmployee : IBaseEmployee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public double Bonus { get; set; }
        public Department Department { get; set; }
        public WorkStation WorkStation { get; set; }
        public DeviceType? RequestedDeviceType { get; set; }

        public override string ToString()
        {
            return $"Employee Details \n{nameof(EmployeeId)}: \t{EmployeeId} " +
                   $"\n{nameof(FirstName)}: \t{FirstName} " +
                   $"\n{nameof(MiddleName)}: \t{(string.IsNullOrWhiteSpace(MiddleName) ? "-" : MiddleName)} " +
                   $"\n{nameof(LastName)}: \t{LastName} " +
                   $"\n{nameof(DateOfBirth)}: \t{DateOfBirth} " +
                   $"\n{nameof(Gender)}: \t{Gender} " +
                   $"\n{nameof(Bonus)}: \t\t{Bonus} " +
                   $"\n{nameof(EmployeeType)}: \t{EmployeeType}" +
                   $"\n{nameof(WorkStation)}: \t{WorkStation}" +
                   $"\n{nameof(RequestedDeviceType)}: \t{RequestedDeviceType}";
        }
    }
}
