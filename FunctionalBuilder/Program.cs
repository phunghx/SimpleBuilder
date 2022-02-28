using System;
using System.Collections.Generic;

namespace FunctionalBuilder
{
    public class Person
    {
        public string Name { set; get; }
        public string Position { set; get; }
        public DateTime NgaySinh { set; get; }
        public override string ToString()
        {
            return $"{Name}: {Position} - {NgaySinh}";
        }

    }
    public sealed class PersonBuilder
    {
        public readonly List<Action<Person>> Actions = new List<Action<Person>>();
        public PersonBuilder Called(string name)
        {
            Actions.Add(p => { p.Name = name; });
            return this;
        }
        public Person Build()
        {
            var p = new Person();
            Actions.ForEach(a => a(p));
            return p;
        }

    }
    public static class PersonBuilderExt
    {
        public static PersonBuilder WorkAs (this PersonBuilder builder, 
            string position)
        {
            builder.Actions.Add(p =>
            {
                p.Position = position;
            });
            return builder;
        }
    }
    public static class PersonBuilderExtDate
    {
        public static PersonBuilder Birth(this PersonBuilder builder,
            DateTime date)
        {
            builder.Actions.Add(p =>
            {
                p.NgaySinh = date;
            });
            return builder;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var bd = new PersonBuilder();
            var me = bd.WorkAs("Lecturer")
                .Called("PhungHX")
                .Birth(DateTime.Now)
                .Build();
            Console.WriteLine(me);
        }
    }
}
