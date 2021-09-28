using File_Manager.Classes.Operations.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace File_Manager.Classes.Views.Operation
{
    /// <summary>
    /// Interaction logic for FIndOperationWindow.xaml
    /// </summary>
    public partial class FindOperationWindow : Window
    {
        public delegate bool FileFound(string file_name);

        DispatcherTimer timer;
        DateTime timeCollapsed;
        public string founded_file { get; private set; }
        public bool isDone { get; private set; }
        bool IsWorking, isEnded;

        int dir_count, files_count;
        FileFound fileFoundPredicate;

        private Action OnChangeDirCount, OnChangeFilesCount;
        private int DirCount 
        { 
            get => dir_count;
            set
            {
                dir_count = value;
                OnChangeDirCount.Invoke();
            }
        }
        private int FilesCount 
        { 
            get => files_count;
            set
            {
                files_count = value;
                OnChangeFilesCount.Invoke();
            }
        } 

        public FindOperationWindow(FileFound fileFoundPredicate, string title)
        {
            InitializeComponent();
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, Tick, Dispatcher);
            Closing += (s, e) => OnClosing(s, e);
            OnChangeDirCount += () => Dispatcher.Invoke(() => Folder_Count_TextBox.Text = DirCount.ToString());
            OnChangeFilesCount += () => Dispatcher.Invoke(() => File_Count_TextBox.Text = FilesCount.ToString());
            this.fileFoundPredicate = fileFoundPredicate;
            Title_TextBox.Text = title;
            Show();
        }
        private void Tick(object sender, EventArgs e)
        {
            TimeExecution_TextBox.Text = string.Format("{0:00}:{1:00}", timeCollapsed.Minute, timeCollapsed.Second);
            timeCollapsed = timeCollapsed.AddSeconds(1);
        }

        private void OnClosing(object s, CancelEventArgs e)
        {            
            if (!isEnded && !DialogHelper.DialogYesNo("Close found operation", "Do you want to stop found operation?"))
            {
                e.Cancel = true;
            }
            Stop();
        }

        public async Task Start(string path_)
        {
            IsWorking = true;
            isDone = false;
            isEnded = false;
            timer.Start();
            await Task.Run(() => Found(path_));
            isEnded = true;
            Close();
        }

        public void Stop()
        {
            IsWorking = false;
            timer.Stop();
        }

        private void Found(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                Dispatcher.Invoke(() => PathOnNow_TextBox.Text = file);
                var file_name = file.Substring(file.LastIndexOf("\\") + 1);
                if (fileFoundPredicate(file_name))
                {
                    founded_file = file;
                    IsWorking = false;
                    isDone = true;
                }
                if (!IsWorking) return;
                FilesCount++;
            }

            var directories = Directory.GetDirectories(path);
            foreach (var dir in directories)
            {
                Found(dir);
                if (!IsWorking) return;
                DirCount++;
            }
        }
    }
}
