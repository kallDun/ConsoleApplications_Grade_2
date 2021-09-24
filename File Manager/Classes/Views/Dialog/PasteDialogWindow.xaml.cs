using File_Manager.Classes.Operations;
using File_Manager.Classes.Operations.Actions;
using File_Manager.Classes.Operations.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace File_Manager.Classes.Views.Dialog
{
    /// <summary>
    /// Interaction logic for PasteDialogWindow.xaml
    /// </summary>
    public partial class PasteDialogWindow : Window
    {
        private bool isEnded, is_cutted, override_all;
        private string copy_path, paste_path;
        private long full_size, copied_size;
        private void AddCopiedSize(long value)
        {
            copied_size += value;
            Percent = 100.0 * copied_size / full_size;
        }

        private event Action OnPercentChange, OnCopiedFileChange;

        private double percent;
        private double Percent
        {
            get => percent;
            set
            {
                percent = value;
                OnPercentChange.Invoke();
                isEnded = Percent.Equals(100);
            }
        }

        private string file_on;
        private string File_on
        {
            get => file_on;
            set
            {
                file_on = value;
                OnCopiedFileChange.Invoke();
            }
        }

        public PasteDialogWindow(string copy_path, string paste_path, bool is_cutted)
        {
            this.copy_path = copy_path;
            this.paste_path = paste_path;
            this.is_cutted = is_cutted;
            InitializeComponent();
            Show();
            MainTextField.Text = $"Copying '{copy_path}' to '{paste_path}'... ";
            OnPercentChange += () =>
            {
                var percent = Math.Round(Percent);
                Dispatcher.Invoke(() => 
                {
                    progress_bar.Value = percent;
                    progress_bar_Text.Text = $"{percent} %";
                });
            };
            OnCopiedFileChange += () =>
            {
                Dispatcher.Invoke(() => MainTextField.Text = $"Copying '{copy_path}' to '{paste_path}'... ({file_on})");
            };
        }
        public async Task<DirectoryActions> Paste()
        {
            if (copy_path.IsDirectory())
            {
                full_size = GetDirSize(new(copy_path));
                await Task.Run(() => PasteDirectory(copy_path, paste_path));
                if (isEnded && is_cutted) Directory.Delete(copy_path, true);
            }
            else
            {
                bool isReplace = false;
                if (File.Exists(paste_path))
                {
                    isReplace = true;
                    var action = await Task.Run(() => AskReplace());
                    if (action == ReplaceActions.Skip || action == ReplaceActions.None) return DirectoryActions.Canceled;
                }

                if (isReplace) await Task.Run(() => File.Delete(paste_path));
                await Task.Run(() => File.Copy(copy_path, paste_path));
                
                Percent = 100;
                if (isEnded && is_cutted) File.Delete(copy_path);
            }
            Close();
            return isEnded ? DirectoryActions.Ok : DirectoryActions.Error;
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

        private void PasteDirectory(string copy_path, string paste_path)
        {
            Directory.CreateDirectory(paste_path);

            foreach (var item in Directory.GetFiles(copy_path))
            {
                bool isReplace = false;
                var name = item.Split('\\').Last();
                var new_file = $"{paste_path}\\{name}";
                File_on = name;

                if (File.Exists(new_file))
                {
                    isReplace = true;
                    if (!override_all)
                    {
                        var action = AskReplace();
                        if (action == ReplaceActions.Skip) continue;
                        if (action == ReplaceActions.ReplaceAll) override_all = true;
                        if (action == ReplaceActions.None) return;
                    }
                }
                if (isReplace) File.Delete(new_file);
                File.Copy(item, new_file);

                AddCopiedSize(new FileInfo(new_file).Length);
            }
            foreach (var item in Directory.GetDirectories(copy_path))
            {
                var new_folder = $"{paste_path}\\{item.Split('\\').Last()}";
                PasteDirectory(item, new_folder);
            }
        }

        // button actions
        private void Cancel_b_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool IsWait = false;
        private ReplaceActions ReplaceAction = ReplaceActions.None;

        private ReplaceActions AskReplace()
        {
            IsWait = true;
            Dispatcher.Invoke(() => 
            {
                BasicButtonsField.Visibility = Visibility.Hidden;
                ReplaceSkipField.Visibility = Visibility.Visible;
            });

            Closed += (s, e) => { IsWait = false; };
            while (IsWait) Thread.Sleep(250);
            return ReplaceAction;
        }

        private void ReplaceAll_b_Click(object sender, RoutedEventArgs e)
        {
            ReplaceAction = ReplaceActions.ReplaceAll;
            PasteContinue();
        }

        private void Replace_b_Click(object sender, RoutedEventArgs e)
        {
            ReplaceAction = ReplaceActions.Replace;
            PasteContinue();
        }

        private void Skip_b_Click(object sender, RoutedEventArgs e)
        {
            ReplaceAction = ReplaceActions.Skip;
            PasteContinue();
        }

        private void PasteContinue()
        {
            ReplaceSkipField.Visibility = Visibility.Hidden;
            BasicButtonsField.Visibility = Visibility.Visible;
            IsWait = false;
        }
    }
}
