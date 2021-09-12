using File_Manager.Classes.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace File_Manager.Classes.Views.Dialog
{
    /// <summary>
    /// Interaction logic for PasteDialogWindow.xaml
    /// </summary>
    public partial class PasteDialogWindow : Window
    {
        private bool isEnded, is_cutted;
        private string copy_path, paste_path;
        private long full_size, copied_size;
        private void AddCopiedSize(long value)
        {
            copied_size += value;
            Percent = copied_size / full_size * 100;
        }

        private event Action OnPercentChange;
        private double percent;
        private double Percent { get => percent; set { percent = value; OnPercentChange.Invoke(); } }

        public PasteDialogWindow(string copy_path, string paste_path, bool is_cutted)
        {
            this.copy_path = copy_path;
            this.paste_path = paste_path;
            this.is_cutted = is_cutted;
            InitializeComponent();
            Show();
        }
        public async Task<PasteActions> Paste()
        {
            
            MainTextField.Text = $"Copying '{copy_path}' to '{paste_path}'...";
            OnPercentChange += () =>
            {
                var percent = Math.Round(Percent);
                progress_bar.Value = percent;
                progress_bar_Text.Text = $"{percent} %";
            };

            if (BasicFileOperation.IsDirectory(copy_path))
            {
                full_size = GetDirSize(new(copy_path));
                PasteDirectory(copy_path, paste_path, false);
                if (isEnded && is_cutted) Directory.Delete(copy_path, true);
            }
            else
            {
                if (File.Exists(paste_path))
                {
                    //var action = AskReplace();
                    var action = ReplaceActions.Skip;
                    if (action == ReplaceActions.Skip || action == ReplaceActions.None) return PasteActions.Canceled;
                }
                File.Copy(copy_path, paste_path);
                Percent = 100;
                if (isEnded && is_cutted) File.Delete(copy_path);
            }
            Close();
            return await Task.FromResult(PasteActions.Ok);
        }

        private static long GetDirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += GetDirSize(di);
            }
            return size;
        }

        private void PasteDirectory(string copy_path, string paste_path, bool IsOverrideAll)
        {
            Directory.CreateDirectory(paste_path);

            foreach (var item in Directory.GetFiles(copy_path))
            {
                var new_file = $"{paste_path}\\{item.Split('\\').Last()}";
                if (!IsOverrideAll && File.Exists(new_file))
                {
                    //var action = AskReplace();
                    var action = ReplaceActions.Skip;
                    if (action == ReplaceActions.Skip) continue;
                    if (action == ReplaceActions.ReplaceAll) IsOverrideAll = true;
                }
                File.Copy(item, new_file);
                AddCopiedSize(new FileInfo(new_file).Length);
            }
            foreach (var item in Directory.GetDirectories(copy_path))
            {
                var new_folder = $"{paste_path}\\{item.Split('\\').Last()}";
                PasteDirectory(item, new_folder, IsOverrideAll);
            }
        }
    
        // button actions
        private void Cancel_b_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool IsWait = false;
        private ReplaceActions ReplaceAction = ReplaceActions.None;

        public async Task<ReplaceActions> AskReplace()
        {
            IsWait = true;
            BasicButtonsField.Visibility = Visibility.Hidden;
            ReplaceSkipField.Visibility = Visibility.Visible;

            Closed += (s, e) => { IsWait = false; };
            while (IsWait)
            {
                await Task.Delay(100);
            }
            return ReplaceAction;
        }

        private void ReplaceAll_b_Click(object sender, RoutedEventArgs e)
        {
            ReplaceAction = ReplaceActions.ReplaceAll;
            Continue();
        }

        private void Replace_b_Click(object sender, RoutedEventArgs e)
        {
            ReplaceAction = ReplaceActions.Replace;
            Continue();
        }

        private void Skip_b_Click(object sender, RoutedEventArgs e)
        {
            ReplaceAction = ReplaceActions.Skip;
            Continue();
        }

        private void Continue()
        {
            ReplaceSkipField.Visibility = Visibility.Hidden;
            BasicButtonsField.Visibility = Visibility.Visible;
            IsWait = false;
        }
    }
}
