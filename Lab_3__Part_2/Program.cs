using Lab_3__Part_2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3__Part_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\directory";
            FileDirector fileDirector = new();
            fileDirector.HorizontalFlipImagesInFolder(directory);
        }
    }
}
