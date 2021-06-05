using System;
using FactoryExample.Models;

namespace FactoryExample.Interfaces
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
    }
}
