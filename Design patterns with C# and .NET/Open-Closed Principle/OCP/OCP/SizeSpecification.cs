using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCP
{
    class SizeSpecification : ISpecification<Product>
    {
        public Size _size { get; set; }

        public SizeSpecification(Size Size)
        {
            this._size = Size;
        }

        public bool IsSatified(Product t)
        {
            return this._size == t.Size;
        }
    }
}
