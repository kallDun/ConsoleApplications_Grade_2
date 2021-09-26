using Microsoft.Win32;
using System.Linq;
using System.Windows;

namespace File_Manager.Classes.Operations.Extensions
{
    static class DialogHelper
    {
        public static OpenFileDialog GetOpenFileDialog(string[] formats, string filter_name, bool IsAll = false)
        {
            var filter_ = $"{filter_name}|{string.Join(";", formats.Select(x => "*." + x))}";
            if (IsAll) filter_ += "|All files (*.*)|*.*";
            return GetOpenFileDialog(filter_);
        }
        public static OpenFileDialog GetOpenFileDialog()
        {
            var filter_ = "All files (*.*)|*.*";
            return GetOpenFileDialog(filter_);
        }
        private static OpenFileDialog GetOpenFileDialog(string filter) => new()
            {
                InitialDirectory = @"C:\",
                RestoreDirectory = true,
                Filter = filter,
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                ReadOnlyChecked = true,
                ShowReadOnly = true,
            };

        public static SaveFileDialog GetSaveFileDialog(string title)
        {
            var filter_ = "All files (*.*)|*.*";
            return GetSaveFileDialog(title, filter_);
        }
        public static SaveFileDialog GetSaveFileDialog(string title, string[] formats, string filter_name, bool IsAll = false)
        {
            var filter_ = $"{filter_name}|{string.Join(";", formats.Select(x => "*." + x))}";
            if (IsAll) filter_ += "|All files (*.*)|*.*";
            return GetSaveFileDialog(title, filter_);
        }
        private static SaveFileDialog GetSaveFileDialog(string title, string filter) => new()
            {
                Title = title,
                InitialDirectory = @"C:\",
                RestoreDirectory = true,
                Filter = filter,
                CheckPathExists = true
            };
        
        public static bool DialogYesNo(string title, string question)
        {
            MessageBoxResult dialogResult = MessageBox.Show(question, title, MessageBoxButton.YesNo);
            return dialogResult == MessageBoxResult.Yes;
        }
        public static bool? DialogYesNoDiscard(string title, string question)
        {
            MessageBoxResult dialogResult = MessageBox.Show(question, title, MessageBoxButton.YesNoCancel);
            return dialogResult == MessageBoxResult.Yes ? true : dialogResult == MessageBoxResult.No ? false : null;
        }

    }
}
