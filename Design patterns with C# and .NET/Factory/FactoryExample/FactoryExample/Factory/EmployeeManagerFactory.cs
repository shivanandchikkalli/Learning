using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FactoryExample.Interfaces;
using FactoryExample.Managers;
using FactoryExample.Models;

namespace FactoryExample.Factory
{
    public static class EmployeeManagerFactory
    {
        public static IEmployeeManager GetEmployeeManager(IBaseEmployee employee)
        {
            return employee.EmployeeType switch
            {
                EmployeeType.Permanent => new PermanentEmployeeManager(employee),
                EmployeeType.Contract => new ContractEmployeeManager(employee),
                EmployeeType.Visiting => new VisitingEmployeeManager(employee),
                _ => null
            };
        }
    }
}
