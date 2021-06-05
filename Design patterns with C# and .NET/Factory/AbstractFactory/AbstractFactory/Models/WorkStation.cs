using System;
using System.Collections.Generic;
using System.Text;
using AbstractFactory.Factory.Concrete_Product;

namespace AbstractFactory.Models
{
    public class WorkStation
    {
        public DeviceType DeviceType { get; set; }

        public Brand Brand { get; set; }

        public Processor Processor { get; set; }

        public override string ToString()
        {
            return $"{Brand} {Processor} {DeviceType}";
        }
    }
}
