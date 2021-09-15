using Lab_1._1_Task_3_9.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_1._1_Task_3_9
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<TDisk> disks = new HashSet<TDisk>();
            var disk_1 = new TDisk(5, new Point(4, 4));
            var disk_2 = new TDisk(5, new Point(4, 4));

            Console.WriteLine(disk_1 == disk_2);
            var disk_3 = disk_1 + disk_2;
            Console.WriteLine(disk_3);

            disks.Add(disk_1);
            disks.Add(disk_2);
            disks.Add(disk_3);

            Console.WriteLine(disks.Count);
        }
    }
}
