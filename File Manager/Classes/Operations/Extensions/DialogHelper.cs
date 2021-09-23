using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace File_Manager.Classes.Operations.Extensions
{
    static class DialogHelper
    {
        public static OpenFileDialog GetOpenFileDialog(string[] formats, string filter_name, bool IsAll = false)
        {
            var filter_ = $"{filter_name}|{string.Join(";", formats.Select(x => "*." + x))}";
            if (IsAll) filter_ += "|All files (*.*)|*.*";

            OpenFileDialog dialog = new()
            {
                InitialDirectory = @"C:\",
                RestoreDirectory = true,
                Filter = filter_,
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                ReadOnlyChecked = true,
                ShowReadOnly = true,
            };
            return dialog;
        }

        public static bool DialogYesNo(string title, string question)
        {
            MessageBoxResult dialogResult = MessageBox.Show(question, title, MessageBoxButton.YesNo);
            return dialogResult == MessageBoxResult.Yes;
        }

    }
}
