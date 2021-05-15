using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_One
{
    public class HtmlElement
    {
        public string Name, Text;
        private const int indentSize = 2;
        public List<HtmlElement> Elements = new List<HtmlElement>();

        public HtmlElement()
        {
            
        }

        public HtmlElement(string name, string text)
        {
            this.Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            this.Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        private string ToStringImplementation(int indent)
        {
            var sb = new StringBuilder();
            var indentString = new string(' ', indent * indentSize);
            sb.AppendLine($"{indentString} <{Name}>");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', (indent+1) * indentSize));
                sb.AppendLine($"{Text}");
            }
            foreach (var item in Elements)
            {
                sb.Append(item.ToStringImplementation(indent + 1));
            }
            sb.AppendLine($"{indentString} </{Name}>");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImplementation(0);
        }
    }

    public class HtmlBuilder
    {
        HtmlElement root = new HtmlElement();
        private readonly string rootName;

        public HtmlBuilder(string rootName)
        {
            this.root.Name = rootName;
            this.rootName = rootName;
        }

        // This is Fluent Implementation
        public HtmlBuilder AddChild(string childName, string childText)
        {
            this.root.Elements.Add(new HtmlElement(childName, childText));
            return this;
        }

        // This is Non-Fluent Implementation
        public void AddHtmlChild(string childName, string childText)
        {
            this.root.Elements.Add(new HtmlElement(childName, childText));
        }

        public override string ToString()
        {
            return this.root.ToString();
        }

        public void Clear()
        {
            this.root = new HtmlElement() { Name = rootName };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Ordinary Builder Usage
            Console.WriteLine("Ordinary Builder Usage");
            HtmlBuilder oBuilder = new HtmlBuilder("ul");
            oBuilder.AddHtmlChild("li", "Hello");
            oBuilder.AddHtmlChild("li", "World!");

            Console.WriteLine(oBuilder.ToString());

            // Fluent Builder Usage
            Console.WriteLine("Fluent Builder Usage");
            HtmlBuilder fBuilder = new HtmlBuilder("ul");
            fBuilder.AddChild("li", "Hello").AddChild("li", "World!");

            Console.WriteLine(fBuilder.ToString());

            Console.ReadLine();
        }
    }
}
