using System;
using System.Text.RegularExpressions;

namespace Regex_Ejudge
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();
            while (str != null)
            {
                string newStr = Regex.Replace(str, "\\\\circle{[(]([0-9]+),([0-9]+)([)].*?})", "\\circle{($2,$1$3");
                Console.WriteLine(newStr);
                str = Console.ReadLine();
            }
        }
    }
}