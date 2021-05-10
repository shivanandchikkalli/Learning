using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCP
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>() {
                new Product("Apple", Size.Small, Color.Green),
                new Product("Tree", Size.Large, Color.Green),
                new Product("Mango", Size.Small, Color.Red)
            };

            Console.WriteLine("All Products");
            foreach (var item in products)
                Console.WriteLine("{0} \t {1} \t {2}", item.Name, item.Color, item.Size);

            Console.WriteLine("--------------------------------------");

            FilterItems<Product> filter = new FilterItems<Product>();

            Console.WriteLine("Filtering Items By Color - Green");
            foreach (var item in filter.Filter(products, new ColorSpecification(Color.Green)))
                Console.WriteLine("- {0}", item);

            Console.WriteLine("--------------------------------------");

            Console.WriteLine("Filtering Items By Size - Small");
            foreach (var item in filter.Filter(products, new SizeSpecification(Size.Small)))
                Console.WriteLine("- {0}", item);

            Console.WriteLine("--------------------------------------");

            Console.WriteLine("Filtering Items By Color and Size - Green and Large");
            foreach (var item in filter.Filter(products, new AndSpecification(
                    new ColorSpecification(Color.Green), new SizeSpecification(Size.Large)
                )))
                Console.WriteLine("- {0}", item);

            Console.ReadLine();
        }
    }
}
