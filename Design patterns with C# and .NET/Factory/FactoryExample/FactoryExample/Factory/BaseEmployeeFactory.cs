using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Models;

namespace FactoryExample.Factory
{
    public abstract class BaseEmployeeFactory
    {
        protected readonly IBaseEmployee Employee;

        protected BaseEmployeeFactory(IBaseEmployee employee)
        {
            Employee = employee;
        }

        public IBaseEmployee CommonInitializations()
        {
            var manager = this.Create();
            Employee.Bonus = manager.GetPay() * 5;
            return Employee;
        }

        protected abstract IEmployeeManager Create();
    }
}
