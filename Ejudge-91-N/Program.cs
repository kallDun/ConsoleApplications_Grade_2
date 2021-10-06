using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejudge_91_N
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedSet<INT> set = new SortedSet<INT>();
            var data = File.ReadAllLines("input.txt").Select(x => x.Split());

            foreach (var command in data)
            {
                switch (command[0])
                {
                    case "ADD":
                        set.Add(new INT(int.Parse(command[1])));
                        break;
                    case "CLEAR":
                        set.Clear();
                        break;
                    case "EXTRACT":
                        if (set.Count > 0)
                        {
                            var min = set.Min;
                            set.Remove(min);
                            Console.WriteLine(min.value);
                        }
                        else
                        {
                            Console.WriteLine("CANNOT");
                        }
                        break;
                }
            }
        }
    }

    class INT : IComparable<INT>
    {
        public int value;
        public int id;
        static int Id;

        public INT(int value)
        {
            this.value = value;
            id = Id++;
        }

        public int CompareTo(INT other)
        {
            var compare = value.CompareTo(other.value);
            if (compare != 0) return compare;
            else return id.CompareTo(other.id);
        }

        public override int GetHashCode() => value * 17 + id;
    }
}
