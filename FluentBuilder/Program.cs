using System;

namespace FluentBuilder
{
    public class Person
    {
        public string Name { set; get; }
        public string Position { set; get; }
        public DateTime NgaySinh { set; get; }
        public class Builder : PersonBirthdayBuilder<Builder>
        {
            internal Builder() { }
        }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"Name: {Name}, Position: {Position}, Birth Day: {NgaySinh}";
        }
    }
    public abstract class PersonBuilder
    {
        protected Person person = new Person();
        public Person build()
        {
            return person;
        }
    }

    // Build for Name
    public class PersonNameBuilder<T> : PersonBuilder
        where T : PersonNameBuilder<T>
    {
        public T Called(string name)
        {
            person.Name = name;
            return (T)this;
        }
    }
    public class PersonJobBuilder<T>: PersonNameBuilder<PersonJobBuilder<T>>
        where T : PersonJobBuilder<T>
    {
        public T WorkAs(string position)
        {
            person.Position = position;
            return (T)this;
        }
    }
    public class PersonBirthdayBuilder<T>: PersonJobBuilder<PersonBirthdayBuilder<T>>
        where T: PersonBirthdayBuilder<T>
    {
        public T Birth(DateTime date)
        {
            person.NgaySinh = date;
            return (T)this;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person
            {
                Name = "phung",
                Position = "lecturer",
                NgaySinh = DateTime.Now
            };


            //object có thứ tự
            var me = Person.New
                .Called("PhungHX")
                .WorkAs("Lecturer")
                .Birth(DateTime.Now)
                .build();

            /// Single Responsibility
            Console.WriteLine(me);
        }
    }
}
