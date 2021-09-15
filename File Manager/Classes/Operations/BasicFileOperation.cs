using File_Manager.Classes.Views.Dialog;
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
        public string copy_path { get; private set; }
        public bool is_cutted { get; private set; }

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
        
        public async Task<PasteActions> Paste(string paste_path)
        {
            if (string.IsNullOrEmpty(copy_path))
            {
                return PasteActions.InvalidPath;
            }
            if (IsDrive(copy_path))
            {
                return PasteActions.InvalidPath;
            }
            if (string.IsNullOrEmpty(paste_path))
            {
                return PasteActions.InvalidPath;
            }
            if (!IsDirectoryOrDrive(paste_path))
            {
                return PasteActions.InvalidPath;
            }
            if (copy_path.Contains(paste_path))
            {
                return PasteActions.InvalidPath;
            }
            
            var _name = copy_path.Split('\\').Last();
            var new_loc = $@"{paste_path}\{_name}";

            PasteDialogWindow pasteOperationWindow = new(copy_path, new_loc, is_cutted);
            return await pasteOperationWindow.Paste();
        }

        public static bool IsDirectoryOrDrive(string path) => IsDirectory(path) || IsDrive(path);
        public static bool IsDirectory(string path) => File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        public static bool IsDrive(string path) => path.Last().ToString() == @"\";
        
    }
}
