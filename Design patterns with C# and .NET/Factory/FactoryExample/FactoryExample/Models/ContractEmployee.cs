using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryExample.Models
{
    public class ContractEmployee : BaseEmployee
    {
        public double HourlyRate { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }

        public int WorkingHoursPerDay { get; set; }

        public ContractEmployee()
        {
            EmployeeType = EmployeeType.Contract;
            WorkingHoursPerDay = 8;
        }
    }
}
