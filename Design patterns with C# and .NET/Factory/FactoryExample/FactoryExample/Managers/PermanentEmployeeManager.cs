using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Models;

namespace FactoryExample.Managers
{
    public class PermanentEmployeeManager : IEmployeeManager
    {
        public double GetPay(IBaseEmployee employee)
        {
            if (employee.GetType().IsAssignableFrom(typeof(PermanentEmployee)))
                return ((PermanentEmployee) employee).MonthlySalary;
            throw new Exception("Not a Permanent Employee");
        }

        public double GetTotalEarnings(IBaseEmployee employee)
        {
            if (!employee.GetType().IsAssignableFrom(typeof(PermanentEmployee)))
                throw new Exception("Not a Permanent Employee");
            var emp = (PermanentEmployee)employee;
            return emp.MonthlySalary * (((DateTime.Now.Year - emp.JoinedDate.Year) * 12) + DateTime.Now.Month -
                                        emp.JoinedDate.Month);
        }

        public static DateTime GetJoinedDate(IBaseEmployee employee)
        {
            if (!employee.GetType().IsAssignableFrom(typeof(PermanentEmployee)))
                throw new Exception("Not a Permanent Employee");
            var emp = (PermanentEmployee)employee;
            return emp.JoinedDate;
        }
    }
}
