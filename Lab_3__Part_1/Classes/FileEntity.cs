using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_3__Part_1.Classes.FileOperationResult;

namespace Lab_3__Part_1.Classes
{
    class FileEntity
    {
        public FileEntity(string path)
        {
            PATH = path;
        }
        public string PATH { get; }
        public int multiplyResult { get; private set; }

        public FileOperationResult GetNumbersMultiply()
        {
            if (!DoesExist()) return NoFile;

            var text = TryToReadFile();
            var lines = text.Trim().Split('\n').Select(x => x.Split()).ToArray();

            if (lines.Length == 0 || lines[0].Length < 2) return BadData;
            var (str_num1, str_num2) = (lines[0][0], lines[0][1]);

            var num1__parsed = int.TryParse(str_num1, out int num1);
            var num2__parsed = int.TryParse(str_num2, out int num2);
            if (!(num1__parsed && num2__parsed)) return BadData;

            long mult = num1 * num2;
            if (mult > int.MaxValue || mult < int.MinValue) return Overflow;

            multiplyResult = (int)mult;
            return Ok;
        }

        private bool DoesExist() => File.Exists(PATH);
        private string TryToReadFile()
        {
            try
            {
                return File.ReadAllText(PATH);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
