using File_Manager.Classes.Operations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace File_Manager.Classes.Views
{
    partial class MainWindow : Window
    {
        private object dummyNode = null;

        private string GetPath() => foldersItem_1_arrow.Visibility == Visibility.Visible ? 
            foldersItem_1_text.Text : foldersItem_2_text.Text;

        private void UpdateTreeAfterOperation()
        {
            var tree_to_update = foldersItem_1_arrow.Visibility == Visibility.Visible ?
                foldersItem_1 : foldersItem_2;
            TreeViewItem selected = tree_to_update.SelectedItem as TreeViewItem;
            if (!BasicFileOperation.IsDirectory(selected.Tag.ToString())) 
                selected = selected.Parent as TreeViewItem;

            folder_Expanded(selected, null);
        }

        private void LoadTreeViews()
        {
            GenerateLeftTree();
            GenerateRightTree();
        }

        private void GenerateRightTree()
        {
            foreach (string s in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                foldersItem_2.Items.Add(item);
            }
            foldersItem_2.SelectedItemChanged += ((s, args) =>
            {
                foldersItem_2_text.Text = (args.NewValue as TreeViewItem).Tag.ToString();
                foldersItem_1_arrow.Visibility = Visibility.Hidden;
                foldersItem_2_arrow.Visibility = Visibility.Visible;
            });
        }

        private void GenerateLeftTree()
        {
            foreach (string s in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                foldersItem_1.Items.Add(item);
            }
            foldersItem_1.SelectedItemChanged += ((s, args) =>
            {
                foldersItem_1_text.Text = (args.NewValue as TreeViewItem).Tag.ToString();
                foldersItem_1_arrow.Visibility = Visibility.Visible;
                foldersItem_2_arrow.Visibility = Visibility.Hidden;
            });
        }

        private void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);

                        item.Items.Add(subitem);
                    }
                    foreach (string s in Directory.GetFiles(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }
    }

    #region PathToImageConverter

    [ValueConversion(typeof(string), typeof(bool))]
    public class PathToImageConverter : IValueConverter
    {
        public static PathToImageConverter Instance =
            new PathToImageConverter();

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            //var icon_name = (value as string).Contains(@"\") ? "diskdrive" : (value as string).Contains(".") ? "file" : "folder";
            /*FileAttributes attr = File.GetAttributes(value.ToString());
            var icon_name = (attr & FileAttributes.Directory) == FileAttributes.Directory ? "folder" : "file";*/

            var path = value.ToString();
            string icon_name;
            if (path.Last().ToString() == @"\") icon_name = "diskdrive";
            else if (File.GetAttributes(path).HasFlag(FileAttributes.Directory)) icon_name = "folder";
            else if (IsFormat(path, "txt")) icon_name = "txt";
            else if (IsFormat(path, "dll")) icon_name = "dll";
            else if (IsFormat(path, "exe")) icon_name = "exe";
            else if (IsFormat(path, "html")) icon_name = "html";
            else if (IsFormat(path, "mp3") || IsFormat(path, "wav") || IsFormat(path, "flac")) 
                icon_name = "music";
            else if (IsFormat(path, "png") || IsFormat(path, "jpg") || IsFormat(path, "JPG") || IsFormat(path, "jpeg")
                || IsFormat(path, "bmp") || IsFormat(path, "tiff") || IsFormat(path, "gif")) 
                icon_name = "image";
            else icon_name = "file";

            Uri uri = new Uri($"pack://application:,,,/Images/{icon_name}.png");
            BitmapImage source = new BitmapImage(uri);
            return source;
        }

        private bool IsFormat(string path, string format) => Regex.IsMatch(path, $"^(.*?)[.]{format}$");

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }

    #endregion // PathToImageConverter

}

