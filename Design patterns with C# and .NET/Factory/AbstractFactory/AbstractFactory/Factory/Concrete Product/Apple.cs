using AbstractFactory.Factory.Abstract_Product;
using AbstractFactory.Models;

namespace AbstractFactory.Factory.Concrete_Product
{
    public class Apple : IBrand
    {
        public Brand GetBrand()
        {
            return Brand.Apple;
        }
    }
}
