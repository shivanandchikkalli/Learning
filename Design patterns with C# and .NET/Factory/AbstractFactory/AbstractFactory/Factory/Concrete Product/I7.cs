using AbstractFactory.Factory.Abstract_Product;
using AbstractFactory.Models;

namespace AbstractFactory.Factory.Concrete_Product
{
    public class I7 : IProcessor
    {
        public Processor GetProcessor()
        {
            return Processor.I7;
        }
    }
}
