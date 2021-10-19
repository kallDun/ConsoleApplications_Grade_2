using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab_3__Part_2.Classes
{
    class FileDirector
    {
        public void HorizontalFlipImagesInFolder(string folder)
        {
            var files = GetAllFilesFromFolder(folder);
            var images = SelectImagesFromFiles(files);
            foreach (var image_file in images)
            {
                var bitmap = ReadImageFromFile(image_file);
                var reversed = ReverseBitmap(bitmap);
                Console.WriteLine($"{image_file} has been reversed");

                var new_path = GetPathForChanged(folder, image_file);
                SaveImageFile(new_path, reversed);
            }
        }
        private string GetPathForChanged(string folder, string file)
        {
            var name = file.Split('\\').Last();
            var format = name.Split('.').Last();
            return folder + '\\' + name.Replace('.' + format, "_changed.png");
        }
        private Bitmap ReverseBitmap(Bitmap bitmap)
        {
            var new_bitmap = new Bitmap(bitmap.Width, bitmap.Height);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    new_bitmap.SetPixel(i, j, bitmap.GetPixel(bitmap.Width - i - 1, j));
                }
            }
            return new_bitmap;
        }
        private void SaveImageFile(string file, Bitmap bitmap) => bitmap.Save(file);
        private Bitmap ReadImageFromFile(string file) => new(file);
        private string[] GetAllFilesFromFolder(string folder_path) => Directory.GetFiles(folder_path);
        private string[] SelectImagesFromFiles(string[] files)
        {
            Regex regexExtForImage= new("^(.*?)((bmp)|(gif)|(tiff?)|(jpe?g)|(png))$", RegexOptions.IgnoreCase);
            var images = new List<string>();
            foreach (var file in files)
            {
                if (regexExtForImage.IsMatch(file))
                    images.Add(file);
            }
            return images.ToArray();
        }
    }
}
