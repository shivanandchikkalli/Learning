using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Models;

namespace FactoryExample.Managers
{
    public class ContractEmployeeManager : IEmployeeManager
    {
        private readonly IBaseEmployee _employee;

        public ContractEmployeeManager(IBaseEmployee employee)
        {
            _employee = employee;
        }

        public double GetPay()
        {
            if (_employee.GetType().IsAssignableFrom(typeof(ContractEmployee)))
                return ((ContractEmployee)_employee).HourlyRate;
            throw new Exception("Not a Contract Employee");
        }

        public double GetTotalEarnings()
        {
            if(!_employee.GetType().IsAssignableFrom(typeof(ContractEmployee)))
                throw new Exception("Not a Contract Employee");
            var emp = (ContractEmployee)_employee;
            return emp.HourlyRate * (emp.ContractEndDate - emp.ContractStartDate).Days * emp.WorkingHoursPerDay;
        }

        public double GetTotalWorkHours()
        {
            if (!_employee.GetType().IsAssignableFrom(typeof(ContractEmployee)))
                throw new Exception("Not a Contract Employee");
            var emp = (ContractEmployee)_employee;
            return (emp.ContractEndDate - emp.ContractStartDate).Days * emp.WorkingHoursPerDay;
        }
    }
}
