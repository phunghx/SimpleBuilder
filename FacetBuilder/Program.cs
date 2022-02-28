using System;

namespace FacetBuilder
{
    public class Person
    {
        public string Street { set; get; }
        public string Postcode { set; get; }
        public string City { set; get; }
        public int Income { set; get; }
        public override string ToString()
        {
            return $"{Street}, {City}, {Postcode} {Income}";
        }

    }
    public class PersonBuilder
    {
        protected Person person = new Person();
        public PersonAddress Lives => new PersonAddress(person);
        public PerasonJob Jobs => new PerasonJob(person);
        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }
    public class PersonAddress : PersonBuilder
    {
        public PersonAddress(Person person)
        {
            this.person = person;
        }
        public PersonAddress At (string street)
        {
            person.Street = street;
            return this;
        }

    }
    public class PerasonJob : PersonBuilder
    {
        public PerasonJob(Person person)
        {
            this.person = person;
        }
        public PerasonJob Salary(int  income)
        {
            person.Income = income;
            return this;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            var me = (Person) pb
                .Jobs
                     .Salary(10)
                .Lives
                     .At("1");
            Console.WriteLine(me);
        }
    }
}
