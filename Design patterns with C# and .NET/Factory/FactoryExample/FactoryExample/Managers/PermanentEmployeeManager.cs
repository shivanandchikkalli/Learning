using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Models;

namespace FactoryExample.Managers
{
    public class PermanentEmployeeManager : IEmployeeManager
    {
        private readonly IBaseEmployee _employee;

        public PermanentEmployeeManager(IBaseEmployee employee)
        {
            _employee = employee;
        }

        public double GetPay()
        {
            if (_employee.GetType().IsAssignableFrom(typeof(PermanentEmployee)))
                return ((PermanentEmployee) _employee).MonthlySalary;
            throw new Exception("Not a Permanent Employee");
        }

        public double GetTotalEarnings()
        {
            if (!_employee.GetType().IsAssignableFrom(typeof(PermanentEmployee)))
                throw new Exception("Not a Permanent Employee");
            var emp = (PermanentEmployee)_employee;
            return emp.MonthlySalary * (((DateTime.Now.Year - emp.JoinedDate.Year) * 12) + DateTime.Now.Month -
                                        emp.JoinedDate.Month);
        }

        public DateTime GetJoinedDate()
        {
            if (!_employee.GetType().IsAssignableFrom(typeof(PermanentEmployee)))
                throw new Exception("Not a Permanent Employee");
            var emp = (PermanentEmployee)_employee;
            return emp.JoinedDate;
        }
    }
}
