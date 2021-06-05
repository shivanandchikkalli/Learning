using AbstractFactory.Factory.Abstract_Product;
using AbstractFactory.Models;

namespace AbstractFactory.Factory.Concrete_Product
{
    public class I5 : IProcessor
    {
        public Processor GetProcessor()
        {
            return Processor.I5;
        }
    }
}
