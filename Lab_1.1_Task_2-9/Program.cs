using Lab_1._1_Task_2_9.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1._1_Task_2_9
{
    class Program
    {
        static void Main(string[] args)
        {
            TDisk disk = new TDisk();
            TBall ball = new TBall();

            Console.WriteLine(disk.Square);
            Console.WriteLine(disk);

            Console.WriteLine(ball.Volume);
            Console.WriteLine(ball);

        }
    }
}
