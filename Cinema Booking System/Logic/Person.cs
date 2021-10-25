using System;

namespace Cinema_Booking_System.Classes
{
    public class Person : ICloneable, IComparable<Person>
    {
        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; }
        public string Surname { get; }

        public object Clone() => new Person(Name, Surname);

        public int CompareTo(Person other)
        {
            if (Name == other.Name && Surname == other.Surname) return 0;
            return -1;
        }

        public override string ToString() => $"{Name} {Surname}";
    }
}