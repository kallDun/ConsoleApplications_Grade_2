using File_Manager.Classes.Operations.Actions;
using File_Manager.Classes.Operations.Extensions;
using File_Manager.Classes.Operations.OpenFile;
using File_Manager.Classes.Views.Dialog;
using System.Linq;
using System.Threading.Tasks;

namespace File_Manager.Classes.Operations
{
    class FileOperationsFacade
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
        
        public async Task<PasteActions> Paste(string path)
        {
            if (string.IsNullOrEmpty(copy_path))
            {
                return PasteActions.InvalidPath;
            }
            if (copy_path.IsDrive())
            {
                return PasteActions.InvalidPath;
            }
            if (string.IsNullOrEmpty(path))
            {
                return PasteActions.InvalidPath;
            }
            if (!path.IsDirectoryOrDrive())
            {
                return PasteActions.InvalidPath;
            }
            if (copy_path.Contains(path))
            {
                return PasteActions.InvalidPath;
            }
            
            var _name = copy_path.Split('\\').Last();
            var new_loc = $@"{path}\{_name}";

            PasteDialogWindow pasteOperationWindow = new(copy_path, new_loc, is_cutted);
            return await pasteOperationWindow.Paste();
        }

        public static void TryToOpen(string path)
        {
            if (!path.IsFile()) return;
            OpeningFileFactory openingFileFactory;

            if (Format.IsImageFormat(path))
                openingFileFactory = new OpeningImage();
            else 
                openingFileFactory = new OpeningTextFile();

            openingFileFactory.OpenFile(path);
        }

        public static void Remove(string path)
        {

        }

    }
}
