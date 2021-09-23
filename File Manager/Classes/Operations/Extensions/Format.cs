using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace File_Manager.Classes.Operations.Extensions
{
    static class Format
    {
        public static string[] MusicFormats { get => new string[] { "mp3", "wav", "flac" }; }
        public static string[] ImageFormats { get => new string[] { "png", "jpg", "JPG", "jpeg", "bmp", "tiff", "gif" }; }
        public static string[] TextFormats { get => new string[] { "txt", "html", "xaml" }; }

        public static bool IsMusicFormat(string path) => IsPathMatchFormat(path, MusicFormats);
        public static bool IsImageFormat(string path) => IsPathMatchFormat(path, ImageFormats);

        private static bool IsPathMatchFormat(string path, string[] filter)
        {
            foreach (var format in filter)
            {
                if (IsFormat(path, format)) return true;
            }
            return false;
        }
        public static bool IsFormat(string path, string format) => Regex.IsMatch(path, $"^(.*?)[.]{format}$");
    }
}
