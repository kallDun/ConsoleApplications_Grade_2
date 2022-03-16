using System;
using System.Collections.Generic;

namespace OOP_Lecture_Example1.Logic
{
    enum SectionType
    {
        Drawing, Dancing, Modeling, SoftToy
    }
    class Supervisor
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
        public override string ToString() => $"Supervisor {Name} {Surname} birthday:{Birthday.Date}"; // wtf with date to string output
    }
    class Section
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
        public override string ToString() => $"Section {Name} '{Type}' with {Supervisor}, payment {Payment}$, lessons/mon {LessonsPerMonth}, students: {StudentsCount}";
    }
    class ChildrenCreativityHouse
    {
        public ChildrenCreativityHouse(string address)
        {
            Address = address;
            Sections = new List<Section>();
        }
        public string Address { get; private set; }
        public List<Section> Sections { get; private set; }
        public void AddSection(Section section) => Sections.Add(section);
        public void RemoveSection(Section section) => Sections.Remove(section);
        public override string ToString() => $"Creativity house with address {Address}. Sections:\n{string.Join(";\n", Sections)}";
    }
}