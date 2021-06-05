using System;
using System.Collections.Generic;
using System.Text;
using AbstractFactory.Factory.Concrete_Product;
using AbstractFactory.Models;

namespace AbstractFactory.Interfaces
{
    public interface IBaseEmployee
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
    }
}
