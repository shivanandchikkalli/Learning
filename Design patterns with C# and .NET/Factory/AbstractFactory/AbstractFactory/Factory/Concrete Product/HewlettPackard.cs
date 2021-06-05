using AbstractFactory.Factory.Abstract_Product;

namespace AbstractFactory.Factory.Concrete_Product
{
    public class HewlettPackard : IBrand
    {
        public Brand GetBrand()
        {
            return Brand.HewlettPackard;
        }
    }
}
