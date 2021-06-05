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
            var visitingEmployeeManager = new VisitingEmployeeManager();
            if (!Employee.GetType().IsAssignableFrom(typeof(VisitingEmployee)))
                return visitingEmployeeManager;
            // Employee Specific Initialization
            var emp = (VisitingEmployee)Employee;
            emp.VisitCount = VisitingEmployeeManager.GetNumberOfVisits(emp);

            return visitingEmployeeManager;
        }
    }
}
