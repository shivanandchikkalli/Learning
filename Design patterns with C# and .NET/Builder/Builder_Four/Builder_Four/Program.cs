using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_Four
{
    class Program
    {
        public class Class
        {
            public string Name, Modifier;
            public List<Field> Fields = new List<Field>();
            private const int indentSize = 2;

            public Class()
            {

            }
            public Class(string name, string modifier)
            {
                Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
                Modifier = modifier ?? throw new ArgumentNullException(paramName: nameof(modifier));
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                var indentString = new string(' ', indentSize);
                if (!string.IsNullOrWhiteSpace(Modifier))
                    sb.Append($"{Modifier} ");
                sb.AppendLine($"class {Name}");
                sb.AppendLine("{");

                foreach (var item in Fields)
                {
                    sb.Append(indentString);
                    sb.AppendLine($"{item}");
                }

                sb.Append("}");

                return sb.ToString();
            }
        }

        public class Field
        {
            public string Type, Name, Modifier;

            public Field(string fieldName, string fieldType, string modifier)
            {
                Name = fieldName;
                Type = fieldType;
                Modifier = modifier;
            }

            public override string ToString()
            {
                var output = string.IsNullOrWhiteSpace(Modifier) ? string.Concat("private", " ") : string.Concat(Modifier.ToLower(), " ");
                return $"{output}{Type} {Name};";
            }
        }

        public class CodeBuilder
        {
            private Class cls = new Class();

            public CodeBuilder(string classModifier, string className)
            {
                cls.Modifier = classModifier;
                cls.Name = className;
            }

            public CodeBuilder AddField(string fieldModifier, string fieldType, string fieldName)
            {
                cls.Fields.Add(new Field(fieldName, fieldType, fieldModifier));
                return this;
            }

            public override string ToString()
            {
                return cls.ToString();
            }
        }

        static void Main(string[] args)
        {
            var cb = new CodeBuilder("public", "Person")
                                .AddField("public", "string", "Name")
                                .AddField("public", "int", "Age")
                                .AddField("private", "string", "Email");

            Console.WriteLine(cb);

            Console.ReadLine();
        }
    }
}
