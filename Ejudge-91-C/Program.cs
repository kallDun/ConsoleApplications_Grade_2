using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejudge_91_C
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Join("\n", File.ReadAllLines("input.txt")
                .Where(x => !string.IsNullOrEmpty(x))
                .Skip(1)
                .Select((value, index) => new { value, index })
                .GroupBy(x => x.index / 4)
                .Select(x => x.Select(y => y.value.Trim()).ToList())
                .Select(x => new Student(x[0], x[1], new Class(x[2]), x[3]))
                .OrderBy(x => x)));
        }
    }

    struct Student : IComparable<Student>
    {
        private string surname, name, birthday;
        private Class @class;

        public Student(string surname, string name, Class @class, string birthday)
        {
            this.surname = surname;
            this.name = name;
            this.@class = @class;
            this.birthday = birthday;
        }

        public override string ToString() => $"{@class} {surname} {name} {birthday}";

        public int CompareTo(Student other)
        {
            var class_compare = @class.CompareTo(other.@class);
            if (class_compare != 0) return class_compare;
            return surname.CompareTo(other.surname);
        }
    }

    struct Class : IComparable<Class>
    {
        private int number;
        private char letter;

        public Class(string data)
        {
            number = int.Parse(data.Substring(0, data.Length - 1));
            letter = data[data.Length - 1];
        }

        public override string ToString() => $"{number}{letter}";

        public int CompareTo(Class other)
        {
            var number_compare = number.CompareTo(other.number);
            if (number_compare != 0) return number_compare;
            return letter.CompareTo(other.letter);
        }
    }
}
