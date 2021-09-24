using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Operations.Extensions
{
    static class PathExtensions
    {
        public static bool IsDirectoryOrDrive(this string path) => IsDirectory(path) || IsDrive(path);
        public static bool IsDirectory(this string path) => File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        public static bool IsDrive(this string path) => path.Last().ToString() == @"\";
        public static bool IsFile(this string path) => File.Exists(path);
        public static string GoBackInPath(this string path)
        {
            var arr = path.Split('\\');
            return string.Join("\\", arr.Take(arr.Length - 1));
        }
    }
}
