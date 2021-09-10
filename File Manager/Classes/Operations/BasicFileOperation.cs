using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Operations
{
    class BasicFileOperation
    {
        private string copy_path;
        private bool is_cutted;

        public void Copy(string path)
        {
            copy_path = path;
            is_cutted = false;
        }

        public void Cut(string path)
        {
            copy_path = path;
            is_cutted = true;
        }

        public void Paste(string paste_path)
        {
            if (string.IsNullOrEmpty(copy_path))
            {
                return;
            }
            if (!IsDirectory(paste_path))
            {
                return;
            }
            var file_name = copy_path.Split('\\').Last();
            var new_loc = $@"{paste_path}\{file_name}";

            if (File.Exists(new_loc))
            {
                return;
            }
            File.Copy(copy_path, new_loc);
            if (is_cutted) File.Delete(copy_path);
        }

        public static bool IsDirectory(string path)
        {
            return path.Last().ToString() == @"\" ||
                            File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        }
    }
}
