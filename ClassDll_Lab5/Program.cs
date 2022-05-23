using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDll_Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalRepository repository = new GlobalRepository();
            KeyboardAssist input = new KeyboardAssist(repository);
            input.ReadData();
            if (repository.House is null) return;
            Console.WriteLine(repository.House);
            input.WriteDataByModel();
        }
    }
}