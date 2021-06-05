using AbstractFactory.Factory.Abstract_Product;

namespace AbstractFactory.Factory.Concrete_Product
{
    public class Dell : IBrand
    {
        public Brand GetBrand()
        {
            return Brand.Dell;
        }
    }
}
