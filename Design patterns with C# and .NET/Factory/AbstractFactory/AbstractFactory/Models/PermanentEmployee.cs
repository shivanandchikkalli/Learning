using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory.Models
{
    public class PermanentEmployee : BaseEmployee
    {
        public double MonthlySalary { get; set; }

        public DateTime JoinedDate { get; set; }

        public PermanentEmployee()
        {
            EmployeeType = EmployeeType.Permanent;
        }
    }
}
