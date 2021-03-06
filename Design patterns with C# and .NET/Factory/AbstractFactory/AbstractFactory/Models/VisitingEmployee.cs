using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory.Models
{
    public class VisitingEmployee : BaseEmployee
    {
        public double RatePerVisit { get; set; }

        public int VisitCount { get; set; }

        public VisitingEmployee()
        {
            EmployeeType = EmployeeType.Visiting;
        }
    }
}
