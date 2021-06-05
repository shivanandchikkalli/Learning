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
            var permanentEmployeeManager = new PermanentEmployeeManager();
            if (!Employee.GetType().IsAssignableFrom(typeof(PermanentEmployee))) 
                return permanentEmployeeManager;
            // Employee Specific Initialization
            var emp = (PermanentEmployee) Employee;
            emp.JoinedDate = PermanentEmployeeManager.GetJoinedDate(emp);
            emp.MonthlySalary = permanentEmployeeManager.GetPay(emp);

            return permanentEmployeeManager;
        }
    }
}
