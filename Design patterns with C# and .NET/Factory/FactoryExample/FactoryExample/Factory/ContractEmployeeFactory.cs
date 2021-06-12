using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Managers;
using FactoryExample.Models;

namespace FactoryExample.Factory
{
    public class ContractEmployeeFactory : BaseEmployeeFactory
    {
        public ContractEmployeeFactory(IBaseEmployee employee) : base(employee)
        {
        }

        protected override IEmployeeManager Create()
        {
            var contractEmployeeManager = EmployeeManagerFactory.GetEmployeeManager(Employee);
            if (!Employee.GetType().IsAssignableFrom(typeof(ContractEmployee)))
                return contractEmployeeManager;
            // Employee Specific Initialization
            ((ContractEmployee)Employee).HourlyRate = contractEmployeeManager.GetPay();

            return contractEmployeeManager;
        }
    }
}
