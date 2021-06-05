using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Models;

namespace FactoryExample.Managers
{
    public class ContractEmployeeManager : IEmployeeManager
    {
        public double GetPay(IBaseEmployee employee)
        {
            if (employee.GetType().IsAssignableFrom(typeof(ContractEmployee)))
                return ((ContractEmployee)employee).HourlyRate;
            throw new Exception("Not a Contract Employee");
        }

        public double GetTotalEarnings(IBaseEmployee employee)
        {
            if(!employee.GetType().IsAssignableFrom(typeof(ContractEmployee)))
                throw new Exception("Not a Contract Employee");
            var emp = (ContractEmployee)employee;
            return emp.HourlyRate * (emp.ContractEndDate - emp.ContractStartDate).Days * emp.WorkingHoursPerDay;
        }

        public double GetTotalWorkHours(IBaseEmployee employee)
        {
            if (!employee.GetType().IsAssignableFrom(typeof(ContractEmployee)))
                throw new Exception("Not a Contract Employee");
            var emp = (ContractEmployee) employee;
            return (emp.ContractEndDate - emp.ContractStartDate).Days * emp.WorkingHoursPerDay;
        }
    }
}
