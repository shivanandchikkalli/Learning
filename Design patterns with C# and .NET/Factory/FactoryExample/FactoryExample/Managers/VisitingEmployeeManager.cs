using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Models;

namespace FactoryExample.Managers
{
    public class VisitingEmployeeManager : IEmployeeManager
    {
        public double GetPay(IBaseEmployee employee)
        {
            if (!employee.GetType().IsAssignableFrom(typeof(VisitingEmployee)))
                throw new Exception("Not a Visiting Employee");
            return ((VisitingEmployee) employee).RatePerVisit;
        }

        public double GetTotalEarnings(IBaseEmployee employee)
        {
            if (!employee.GetType().IsAssignableFrom(typeof(VisitingEmployee)))
                throw new Exception("Not a Visiting Employee");
            var emp = (VisitingEmployee) employee;
            return emp.RatePerVisit * emp.VisitCount;
        }

        public static int GetNumberOfVisits(IBaseEmployee employee)
        {
            if (!employee.GetType().IsAssignableFrom(typeof(VisitingEmployee)))
                throw new Exception("Not a Visiting Employee");
            var emp = (VisitingEmployee)employee;
            return emp.VisitCount;
        }
    }
}
