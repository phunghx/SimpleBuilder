using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBuilder
{
    public class HtmlElement
    {
        public string Name { set; get; }
        public string Text { set; get; }
        public List<HtmlElement> Elements  = new List<HtmlElement>();
        private const int indentSize = 2;
        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }
        public HtmlElement()
        {

        }
        public string toStringIml(int  indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.Append($"{i}<{Name}>\n");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.Append(Text);
                sb.Append("\n");
            }
            foreach (var e in Elements)
                sb.Append(e.toStringIml(indent + 1));
            sb.Append($"{i}</{Name}>\n");
            return sb.ToString();
        }
        public override string ToString()
        {
            return toStringIml(0);
        }
    }

    public class HtmlBuilder
    {
        private readonly string rootName;
        private HtmlElement root = new HtmlElement();
        public HtmlBuilder(string rootname)
        {
            this.rootName = rootname;
            root.Name = rootname;
        }
        public void addChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            
        }
        public HtmlBuilder addChildFluent(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }
        public override string ToString()
        {
            return root.ToString();
        }
        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            var words = new[] { "hello", "world" };
            var li1 = new HtmlElement("li","hello");
            var li2 = new HtmlElement("li", "world");

            var ul = new HtmlElement("ul", "");
            ul.Elements.Add(li1);
            ul.Elements.Add(li2);
            Console.WriteLine(ul);


            var builder = new HtmlBuilder("ul");
            builder.addChild("li", "hello");//
            builder.addChild("li", "world");//
            Console.WriteLine(builder);

            builder.Clear();
            builder.addChildFluent("li", "hello")
                .addChildFluent("li", "world")
                .addChildFluent("li","class");
            Console.WriteLine(builder);

        }
    }
}
