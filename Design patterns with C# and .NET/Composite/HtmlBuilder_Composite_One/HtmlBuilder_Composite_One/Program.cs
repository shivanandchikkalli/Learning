using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlBuilder_Composite_One
{
    public abstract class HtmlElement
    {
        protected readonly string Name;

        protected HtmlElement(string name)
        {
            this.Name = name;
        }

        public abstract void Add(HtmlElement child);

        public abstract void Remove(HtmlElement child);

        public abstract void Display();
    }

    public class Div : HtmlElement
    {
        private readonly List<HtmlElement> _childElements = new();
        public Div(string name = "") : base(name)
        {
        }

        public override void Add(HtmlElement child)
        {
            _childElements.Add(child);
        }

        public override void Remove(HtmlElement child)
        {
            _childElements.Remove(child);
        }

        public override void Display()
        {
            Console.Write(this.ToString());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div>");
            foreach (var child in _childElements)
            {
                sb.AppendLine(child.ToString());
            }

            sb.Append("</div>");

            return sb.ToString();
        }
    }

    public class Header : HtmlElement
    {
        private readonly int _size;
        public Header(int size, string name) : base(name)
        {
            _size = size;
        }

        public override void Add(HtmlElement child)
        {
            throw new Exception("You cannot add children to header objects");
        }

        public override void Remove(HtmlElement child)
        {
            throw new Exception("header doesn't contain any objects");
        }

        public override void Display()
        {
            Console.Write(this.ToString());
        }

        public override string ToString()
        {
            return $"<h{_size}>{Name}</h{_size}>";
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var div = new Div();

            var header1 = new Header(1, "Hello World!");
            div.Add(header1);
            var header2 = new Header(2, "What's going on?");
            div.Add(header2);
            var header3 = new Header(3, "Stay hungry, not literally though...");
            div.Add(header3);

            var div2 = new Div();
            div2.Add(new Header(2, "John"));
            div2.Add(new Header(3, "Mary"));
            div2.Add(new Header(4, "Julie"));

            div.Add(div2);

            Console.WriteLine(div);


            Console.ReadLine();
        }
    }
}
