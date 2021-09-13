using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejudge_91_D
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input.txt");
            Student.CompareType = data[0].Trim();

            Console.WriteLine(string.Join("\n", data
                .Where(x => !string.IsNullOrEmpty(x))
                .Skip(2)
                .Select((value, index) => new { value, index })
                .GroupBy(x => x.index / 4)
                .Select(x => x.Select(y => y.value.Trim()).ToList())
                .Select(x => new Student(x[0], x[1], x[2], x[3]))
                .OrderBy(x => x)));
        }
    }

    struct Student : IComparable<Student>
    {
        private string surname, name;
        private DateTime birthday;
        // class
        private int class_number;
        private char class_letter;

        public Student(string surname, string name, string @class, string birthday)
        {
            this.surname = surname;
            this.name = name;
            class_number = int.Parse(@class.Substring(0, @class.Length - 1));
            class_letter = @class[@class.Length - 1];
            var data = birthday.Split('.').Select(int.Parse).ToArray();
            var year = data[2] > 50 ? int.Parse($"19{string.Format("{0:00}", data[2])}") : int.Parse($"20{string.Format("{0:00}", data[2])}");
            this.birthday = new DateTime(year, data[1], data[0]);
        }

        // compare implementation

        public override string ToString() => $"{class_number}{class_letter}, {surname}, {name}, {string.Format("{0:00}.{1:00}.{2}", birthday.Day, birthday.Month, birthday.Year.ToString().Substring(2, 2))}";

        public static string CompareType;
        public int CompareTo(Student other)
        {
            var compare_order = CompareType.Split();
            var compare_result = 0;
            foreach (var item in compare_order)
            {
                switch (item)
                {
                    case "surname":
                        compare_result = SurnameCompare(other);
                        break;
                    case "fullname":
                        compare_result = FullnameCompare(other);
                        break;
                    case "birthyear":
                        compare_result = BirthYearCompare(other);
                        break;
                    case "birthdate":
                        compare_result = BirthdateCompare(other);
                        break;
                    case "birthday":
                        compare_result = BirthdayCompare(other);
                        break;
                    case "grade":
                        compare_result = GradeCompare(other);
                        break;
                    case "class":
                        compare_result = ClassCompare(other);
                        break;
                }
                if (compare_result != 0) break;
            }
            return compare_result;
        }
        private int SurnameCompare(Student other) => string.Compare(surname, other.surname, StringComparison.Ordinal);
        private int FullnameCompare(Student other)
        {
            var surname = SurnameCompare(other);
            if (surname != 0) return surname;
            return string.Compare(name, other.name, StringComparison.Ordinal);
        }
        private int BirthYearCompare(Student other) => birthday.Year.CompareTo(other.birthday.Year);
        private int BirthdateCompare(Student other) => birthday.CompareTo(other.birthday);
        private int BirthdayCompare(Student other) => birthday.DayOfYear.CompareTo(other.birthday.DayOfYear);
        private int GradeCompare(Student other) => class_number.CompareTo(other.class_number);
        private int ClassCompare(Student other)
        {
            var number = GradeCompare(other);
            if (number != 0) return number;
            return class_letter.CompareTo(other.class_letter);
        }
    }
}
