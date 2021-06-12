using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Managers;
using FactoryExample.Models;

namespace FactoryExample.Factory
{
    public class VisitingEmployeeFactory : BaseEmployeeFactory
    {
        public VisitingEmployeeFactory(IBaseEmployee employee) : base(employee)
        {
        }

        protected override IEmployeeManager Create()
        {
            var visitingEmployeeManager = EmployeeManagerFactory.GetEmployeeManager(Employee);
            if (!Employee.GetType().IsAssignableFrom(typeof(VisitingEmployee)))
                return visitingEmployeeManager;
            // Employee Specific Initialization
            ((VisitingEmployee)Employee).VisitCount = ((VisitingEmployeeManager)visitingEmployeeManager).GetNumberOfVisits();

            return visitingEmployeeManager;
        }
    }
}
