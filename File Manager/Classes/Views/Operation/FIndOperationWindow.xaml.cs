using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace File_Manager.Classes.Views.Operation
{
    /// <summary>
    /// Interaction logic for FIndOperationWindow.xaml
    /// </summary>
    public partial class FindOperationWindow : Window
    {
        public delegate bool FileFound(string file_name);

        Stopwatch stopwatch = new();
        int dir_count, files_count;
        string path_start;
        FileFound fileFoundPredicate;

        public FindOperationWindow()
        {
            InitializeComponent();
        }


        private void Found(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var file_name = file.Substring(file.LastIndexOf("\\") + 1);
                if (fileFoundPredicate(file_name))
                {
                    // make out of this shit
                }
                files_count++;
            }

            var directories = Directory.GetDirectories(path);
            foreach (var dir in directories)
            {
                Found(dir);
                dir_count++;
            }
        }

    }
}
