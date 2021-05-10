using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCP
{
    enum Color
    {
        Blue, Green, Red, White
    }

    enum Size
    {
        Small, Medium, Large, XLarge
    }

    class Product
    {
        public string Name { get; set; }

        public Size Size { get; set; }

        public Color Color { get; set; }

        public Product(string Name, Size Size, Color Color)
        {
            this.Name = Name;
            this.Color = Color;
            this.Size = Size;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
