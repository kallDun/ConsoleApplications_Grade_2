using Lab_3__Part_1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3__Part_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = @"C:\Users\User\Desktop\dir";
            FileDirector.OutputDirectoryPATH = directory;

            List<FileEntity> AllFiles = new();
            for (int i = 10; i <= 29; i++)
            {
                AllFiles.Add(new FileEntity($"{directory}\\{i}.txt"));
            }

            FileDirector director = new(AllFiles);
            director.ProcessFiles();
            director.ReturnResultInFiles();
            Console.WriteLine(director.CalculateSum());
        }
    }
}
