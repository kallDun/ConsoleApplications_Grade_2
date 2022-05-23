using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OOP_Lecture_ClassDll.Models
{
    public enum SectionType
    {
        Drawing, Dancing, Modeling, SoftToy
    }
    public class Supervisor : ICloneable, IComparable<Supervisor>, IEquatable<Supervisor>
    {
        public Supervisor(string name, string surname, DateTime birthday, User user)
        {
            Name = name;
            Surname = surname;
            Birthday = birthday;
            User = user;
        }
        
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime Birthday { get; private set; }
        public User User { get; private set; }

        public object Clone() => new Supervisor(Name, Surname, Birthday, User);
        public int CompareTo(Supervisor other) => Name == other.Name && Surname == other.Surname && Birthday == other.Birthday ? 0 : -1;
        public bool Equals(Supervisor other) => CompareTo(other) == 0;

        public override int GetHashCode() => (Name.GetHashCode() * 17 + Surname.GetHashCode()) * 17 + Birthday.GetHashCode();
        public override string ToString() => $"Supervisor {Name} {Surname}, user: {User}, birthday:{string.Format("{0:dd/MM/yyyy}", Birthday)}";
    }
    public class Section : ICloneable, IComparable<Section>, IEquatable<Section>
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
        public bool Equals(Section other) => CompareTo(other) == 0;

        public override int GetHashCode() 
            => ((((Name.GetHashCode() * 17 + Supervisor.GetHashCode()) * 17 + Type.GetHashCode()) 
            * 17 + Payment.GetHashCode()) * 17 + LessonsPerMonth.GetHashCode()) * 17 + StudentsCount.GetHashCode();
        public override string ToString() => $"Section {Name} '{Type}' with {Supervisor},\npayment {Payment}$, lessons/mon {LessonsPerMonth}, students: {StudentsCount}";
    }
    public class ChildrenCreativityHouse : ICloneable, IComparable<ChildrenCreativityHouse>, IEquatable<ChildrenCreativityHouse>
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
        public bool Equals(ChildrenCreativityHouse other) => CompareTo(other) == 0;

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
    public class User : ICloneable, IComparable<User>, IEquatable<User>
    {
        public User(Guid id, string username, string email, string phone, string password, string salt)
        {
            Id = id;
            Username = username;
            Email = email;
            Phone = phone;
            Password = password;
            Salt = salt;
        }
        public Guid Id { get; private set; }        
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public object Clone() => new User(Id, Username, Email, Phone, Password, Salt);
        public int CompareTo(User other)
        {
            bool equals =
                Id == other.Id &&
                Username == other.Username &&
                Email == other.Email &&
                Phone == other.Phone &&
                Password == other.Password &&
                Salt == other.Salt;
            return equals ? 0 : -1;
        }
        public bool Equals(User other) => CompareTo(other) == 0;
        public override string ToString() => $"User {Id}-{Username} with email: {Email} and phone: {Phone}";
    }
}