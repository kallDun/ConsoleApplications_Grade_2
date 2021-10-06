using File_Manager.Classes.Logging;
using File_Manager.Classes.Operations.Actions;
using File_Manager.Classes.Operations.Extensions;
using File_Manager.Classes.Operations.OpenFile;
using File_Manager.Classes.Views.Dialog;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace File_Manager.Classes.Operations
{
    class FileOperationsFacade
    {
        private Logger logger;
        public FileOperationsFacade()
        {
            logger = LoggerSingleton.GetInstance();
        }

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
        public async Task<DirectoryActions> Paste(string path)
        {
            if (string.IsNullOrEmpty(copy_path))
            {
                return DirectoryActions.InvalidPath;
            }
            if (copy_path.IsDrive())
            {
                return DirectoryActions.InvalidPath;
            }
            if (string.IsNullOrEmpty(path))
            {
                return DirectoryActions.InvalidPath;
            }
            if (!path.IsDirectoryOrDrive())
            {
                return DirectoryActions.InvalidPath;
            }
            if (path.Contains(copy_path))
            {
                return DirectoryActions.InvalidPath;
            }
            
            var _name = copy_path.Split('\\').Last();
            var new_loc = $@"{path}\{_name}";
            try
            {
                logger.LogInformation($"Trying to paste {copy_path} into {new_loc}");
                PasteDialogWindow pasteOperationWindow = new(copy_path, new_loc, is_cutted);
                var action = await pasteOperationWindow.Paste();
                logger.LogInformation($"Paste ended with '{action}' result");
                return action;
            }
            catch (Exception e)
            {
                logger.LogError($"Error occured in paste operation", e);
                return DirectoryActions.Error;
            }            
            
        }
        public DirectoryActions Open(string path)
        {
            if (!path.IsFile()) return DirectoryActions.InvalidPath;
            OpeningFileFactory openingFileFactory;

            if (Format.IsImageFormat(path))
                openingFileFactory = new OpeningImage();
            else
                openingFileFactory = new OpeningTextFile();

            try
            {
                logger.LogInformation($"Trying to open file {path}");
                openingFileFactory.OpenFile(path);
                logger.LogInformation($"Opening was succesful");
                return DirectoryActions.Ok;
            }
            catch (Exception e)
            {
                logger.LogError($"Error occured when program opened file", e);
                return DirectoryActions.Error;
            }
        }

        public void RenameFile(string new_name, string old_name, string body)
        {
            if (new_name == old_name) return;
            File.Move($"{body}\\{old_name}", $"{body}\\{new_name}");
        }

        public DirectoryActions Delete(string path)
        {
            if (path.IsDrive()) return DirectoryActions.InvalidPath;

            var confirm = DialogHelper.DialogYesNo("Delete Operation", $"Are you sure that you want to delete {path} ?");
            if (!confirm) return DirectoryActions.Canceled;

            try
            {
                logger.LogInformation($"Trying to delete {path}");
                if (path.IsDirectory())
                {
                    Directory.Delete(path, true);
                }
                else
                {
                    File.Delete(path);
                }
                logger.LogInformation("File deleting was succesful");
                return DirectoryActions.Ok;
            }
            catch (Exception e)
            {
                logger.LogError($"Error occured when program tryied to delete file", e);
                return DirectoryActions.Error;
            }
        }
        public void Create(string path)
        {
            try
            {
                logger.LogInformation($"Create new file {path}");
                File.Create(path);
                logger.LogInformation("File created succesfully");
            }
            catch (Exception e)
            {
                logger.LogError("Error occured in creating file", e);
            }
        }
        public static DirectoryActions TryToOpen(string path)
        {
            FileOperationsFacade fileOperations = new();
            return fileOperations.Open(path);
        }
    }
}
