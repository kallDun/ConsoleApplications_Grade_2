using System;
using System.Collections.Generic;

namespace OOP_Lecture_ClassDll.Models
{
    public enum SectionType
    {
        Drawing, Dancing, Modeling, SoftToy
    }
    public class Supervisor : ICloneable, IComparable<Supervisor>
    {
        public Supervisor(string name, string surname, DateTime birthday)
        {
            Name = name;
            Surname = surname;
            Birthday = birthday;
        }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime Birthday { get; private set; }

        public object Clone() => new Supervisor(Name, Surname, Birthday);
        public int CompareTo(Supervisor other) => Name == other.Name && Surname == other.Surname && Birthday == other.Birthday ? 0 : -1;
        public override bool Equals(object obj)
        {
            if (obj is Supervisor other)
            {
                return CompareTo(other) == 0;
            }
            return false;
        }
        public override int GetHashCode() => (Name.GetHashCode() * 17 + Surname.GetHashCode()) * 17 + Birthday.GetHashCode();
        public override string ToString() => $"Supervisor {Name} {Surname} birthday:{string.Format("{0:dd/MM/yyyy}", Birthday)}";
    }
    public class Section : ICloneable, IComparable<Section>
    {
        public Section(string name, Supervisor supervisor, SectionType type, int payment, int lessonsPerMonth, int studentsCount)
        {
            Name = name;
            Supervisor = supervisor;
            Type = type;
            Payment = payment;
            LessonsPerMonth = lessonsPerMonth;
            StudentsCount = studentsCount;
        }
        public string Name { get; private set; }
        public Supervisor Supervisor { get; private set; }
        public SectionType Type { get; private set; }
        public int Payment { get; private set; }
        public int LessonsPerMonth { get; private set; }
        public int StudentsCount { get; private set; }

        public object Clone() => new Section(Name, Supervisor.Clone() as Supervisor, Type, Payment, LessonsPerMonth, StudentsCount);
        public int CompareTo(Section other) 
            => Name == other.Name 
            && Supervisor.Equals(other.Supervisor) 
            && Type == other.Type 
            && Payment == other.Payment 
            && LessonsPerMonth == other.LessonsPerMonth 
            && StudentsCount == other.StudentsCount ? 0 : -1;

        public override bool Equals(object obj)
        {
            if (obj is Section other)
            {
                return CompareTo(other) == 0;
            }
            return false;
        }
        public override int GetHashCode() 
            => ((((Name.GetHashCode() * 17 + Supervisor.GetHashCode()) * 17 + Type.GetHashCode()) 
            * 17 + Payment.GetHashCode()) * 17 + LessonsPerMonth.GetHashCode()) * 17 + StudentsCount.GetHashCode();
        public override string ToString() => $"Section {Name} '{Type}' with {Supervisor},\npayment {Payment}$, lessons/mon {LessonsPerMonth}, students: {StudentsCount}";
    }
    public class ChildrenCreativityHouse : ICloneable, IComparable<ChildrenCreativityHouse>
    {
        public ChildrenCreativityHouse(string address)
        {
            Address = address;
            Sections = new List<Section>();
        }
        public string Address { get; private set; }
        public List<Section> Sections { get; private set; }
        public void AddSection(Section section) => Sections.Add(section);

        public object Clone()
        {
            var house = new ChildrenCreativityHouse(Address);
            foreach (var item in Sections)
            {
                house.AddSection(item.Clone() as Section);
            }
            return house;
        }
        public int CompareTo(ChildrenCreativityHouse other) => Address == other.Address && Sections.Equals(other.Sections) ? 0 : -1;
        public override bool Equals(object obj)
        {
            if (obj is ChildrenCreativityHouse other)
            {
                return CompareTo(other) == 0;
            }
            return false;
        }
        public override int GetHashCode()
        {
            int hashcode = Address.GetHashCode() * 17;
            foreach (var item in Sections)
            {
                hashcode *= 17;
                hashcode += item.GetHashCode();
            }
            return hashcode;
        }
        public void RemoveSection(Section section) => Sections.Remove(section);
        public override string ToString() => $"Creativity house with address {Address}. Sections:\n{string.Join(";\n", Sections)}";
    }
}