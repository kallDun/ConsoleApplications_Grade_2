using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab_3__Part_1.Classes
{
    class FileDirector
    {
        private List<FileEntity> AllFiles;

        private List<FileEntity> Ok_List = new();
        private List<FileEntity> NoFile_List = new();
        private List<FileEntity> BadData_List = new();
        private List<FileEntity> Overflow_List = new();
        public FileDirector(List<FileEntity> allFiles) => AllFiles = allFiles;

        public void ProcessFiles()
        {
            foreach (var file in AllFiles)
            {
                switch (file.GetNumbersMultiply())
                {
                    case FileOperationResult.Ok:
                        Ok_List.Add(file);
                        break;
                    case FileOperationResult.NoFile:
                        NoFile_List.Add(file);
                        break;                        
                    case FileOperationResult.BadData:
                        BadData_List.Add(file);
                        break;
                    case FileOperationResult.Overflow:
                        Overflow_List.Add(file);
                        break;
                }
            }
        }

        public int CalculateSum() => Ok_List.Select(x => x.multiplyResult).Sum();

        public static string OutputDirectoryPATH; 
        private const string NoFilePATH = "no_file.txt";
        private const string BadDataPATH = "bad_data.txt";
        private const string OverflowPATH = "overflow.txt";
        public void ReturnResultInFiles()
        {
            var NoFileText = string.Join("\n", NoFile_List.Select(x => x.PATH));
            File.WriteAllText($"{OutputDirectoryPATH}\\{NoFilePATH}", NoFileText);

            var BadDataText = string.Join("\n", BadData_List.Select(x => x.PATH));
            File.WriteAllText($"{OutputDirectoryPATH}\\{BadDataPATH}", BadDataText);

            var OverflowText = string.Join("\n", Overflow_List.Select(x => x.PATH));
            File.WriteAllText($"{OutputDirectoryPATH}\\{OverflowPATH}", OverflowText);
        }

    }
}
