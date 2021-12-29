using Addition_Task_1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addition_Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<SteelPlate> list = new List<SteelPlate>();
            var thick = 0.6;
            var density = 0.8;
            for (int i = 0; i < 5; i++)
            {
                list.Add(new Square(rnd.Next(1, 10), thick, density));
            }
            for (int i = 0; i < 7; i++)
            {
                list.Add(new Rectangle(rnd.Next(1, 10), rnd.Next(1, 10), thick, density));
            }
            for (int i = 0; i < 3; i++)
            {
                list.Add(new Triangle(rnd.Next(1, 10), rnd.Next(1, 10), thick, density));
            }
            foreach (var item in list)
            {
                Console.WriteLine(item.GetInfo());
            }
            Console.WriteLine(list.Sum(x => x.GetWeight()));
        }
    }
}
