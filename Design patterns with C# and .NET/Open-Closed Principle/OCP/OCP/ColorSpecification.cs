using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCP
{
    class ColorSpecification : ISpecification<Product>
    {
        public Color _color { get; set; }

        public ColorSpecification(Color Color)
        {
            this._color = Color;
        }

        public bool IsSatified(Product t)
        {
            return this._color == t.Color;
        }
    }
}
