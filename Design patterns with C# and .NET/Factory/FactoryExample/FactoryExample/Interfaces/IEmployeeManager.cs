using System;
using System.Collections.Generic;
using System.Text;
using FactoryExample.Models;

namespace FactoryExample.Interfaces
{
    public interface IEmployeeManager
    {
        double GetPay();

        double GetTotalEarnings();
    }
}
