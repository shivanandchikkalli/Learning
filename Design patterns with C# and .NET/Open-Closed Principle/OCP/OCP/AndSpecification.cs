using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCP
{
    class AndSpecification : ISpecification<Product>
    {
        public ISpecification<Product> spec1 { get; set; }
        
        public ISpecification<Product> spec2 { get; set; }

        public AndSpecification(ISpecification<Product> spec1, ISpecification<Product> spec2)
        {
            this.spec1 = spec1 ?? throw new ArgumentNullException(paramName: nameof(spec1));
            this.spec2 = spec2 ?? throw new ArgumentNullException(paramName: nameof(spec2));
        }

        public bool IsSatified(Product t)
        {
            return spec1.IsSatified(t) && spec2.IsSatified(t);
        }
    }
}
