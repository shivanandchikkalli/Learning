using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Models;

namespace FactoryExample.Factory
{
    public static class EmployeeFactory
    {
        public static BaseEmployeeFactory GetFactory(IBaseEmployee employee)
        {
            return employee.EmployeeType switch
            {
                EmployeeType.Contract => new ContractEmployeeFactory(employee),
                EmployeeType.Permanent => new PermanentEmployeeFactory(employee),
                EmployeeType.Visiting => new VisitingEmployeeFactory(employee),
                _ => null
            };
        }
    }
}
