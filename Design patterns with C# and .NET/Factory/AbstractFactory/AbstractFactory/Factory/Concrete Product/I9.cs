using AbstractFactory.Factory.Abstract_Product;
using AbstractFactory.Models;

namespace AbstractFactory.Factory.Concrete_Product
{
    public class I9 : IProcessor
    {
        public Processor GetProcessor()
        {
            return Processor.I9;
        }
    }
}
