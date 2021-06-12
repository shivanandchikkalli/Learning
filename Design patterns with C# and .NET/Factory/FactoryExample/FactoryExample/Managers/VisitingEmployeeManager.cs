using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Models;

namespace FactoryExample.Managers
{
    public class VisitingEmployeeManager : IEmployeeManager
    {
        private readonly IBaseEmployee _employee;

        public VisitingEmployeeManager(IBaseEmployee employee)
        {
            _employee = employee;
        }

        public double GetPay()
        {
            if (!_employee.GetType().IsAssignableFrom(typeof(VisitingEmployee)))
                throw new Exception("Not a Visiting Employee");
            return ((VisitingEmployee) _employee).RatePerVisit;
        }

        public double GetTotalEarnings()
        {
            if (!_employee.GetType().IsAssignableFrom(typeof(VisitingEmployee)))
                throw new Exception("Not a Visiting Employee");
            var emp = (VisitingEmployee) _employee;
            return emp.RatePerVisit * emp.VisitCount;
        }

        public int GetNumberOfVisits()
        {
            if (!_employee.GetType().IsAssignableFrom(typeof(VisitingEmployee)))
                throw new Exception("Not a Visiting Employee");
            var emp = (VisitingEmployee)_employee;
            return emp.VisitCount;
        }
    }
}
