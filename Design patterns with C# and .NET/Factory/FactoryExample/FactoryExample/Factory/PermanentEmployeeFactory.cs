using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Managers;
using FactoryExample.Models;

namespace FactoryExample.Factory
{
    public class PermanentEmployeeFactory : BaseEmployeeFactory
    {
        public PermanentEmployeeFactory(IBaseEmployee employee) : base(employee)
        {
        }

        protected override IEmployeeManager Create()
        {
            var permanentEmployeeManager = EmployeeManagerFactory.GetEmployeeManager(Employee);
            if (!Employee.GetType().IsAssignableFrom(typeof(PermanentEmployee))) 
                return permanentEmployeeManager;
            // Employee Specific Initialization
            var emp = (PermanentEmployee) Employee;
            emp.JoinedDate = ((PermanentEmployeeManager)permanentEmployeeManager).GetJoinedDate();
            emp.MonthlySalary = permanentEmployeeManager.GetPay();

            return permanentEmployeeManager;
        }
    }
}
