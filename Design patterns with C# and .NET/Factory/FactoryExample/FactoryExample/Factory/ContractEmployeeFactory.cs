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
            var contractEmployeeManager = new ContractEmployeeManager();
            if (!Employee.GetType().IsAssignableFrom(typeof(ContractEmployee)))
                return contractEmployeeManager;
            // Employee Specific Initialization
            var emp = (ContractEmployee)Employee;
            emp.HourlyRate = contractEmployeeManager.GetPay(emp);

            return contractEmployeeManager;
        }
    }
}
