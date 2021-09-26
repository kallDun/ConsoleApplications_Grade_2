using File_Manager.Classes.Operations;
using File_Manager.Classes.Operations.Extensions;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace File_Manager.Classes.Views
{
    partial class MainWindow : Window
    {
        private object dummyNode = null;

        private string GetPath() => foldersItem_1_arrow.Visibility == Visibility.Visible ? foldersItem_1_text.Text : foldersItem_2_text.Text;

        private void UpdateTreesPath(string path, bool isPrevious = false)
        {
            var names = path.Split('\\');

            var active_node_1 = foldersItem_1.Items.Cast<TreeViewItem>().FirstOrDefault(x => x is not null && x.Header.Equals(names.First() + '\\'));
            var active_node_2 = foldersItem_2.Items.Cast<TreeViewItem>().FirstOrDefault(x => x is not null && x.Header.Equals(names.First() + '\\'));

            foreach (var name in names.Skip(1))
            {
                active_node_1 = active_node_1?.Items.Cast<TreeViewItem>().FirstOrDefault(x => x is not null && x.Header.Equals(name));
                active_node_2 = active_node_2?.Items.Cast<TreeViewItem>().FirstOrDefault(x => x is not null && x.Header.Equals(name));

                if (active_node_1 is null && active_node_2 is null) return;
            }

            UpdateNode(active_node_1, isPrevious);
            UpdateNode(active_node_2, isPrevious);
        }

        private void UpdateNode(TreeViewItem node, bool isPrevious)
        {
            if (node is null) return;

            if (isPrevious) 
            {
                node = node.Parent as TreeViewItem; 
                if (node is null) return;
            }

            node.Items.Clear();
            node.Items.Add(dummyNode);
            folder_Expanded(node, null);
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
                item.Expanded += (s, e) => logger.LogDebug($"Expanded folder {(s as TreeViewItem).Tag}");
                item.Collapsed += (s, e) => logger.LogDebug($"Collapsed folder {(s as TreeViewItem).Tag}");
                item.Expanded += (s, e) => observer.AddFolder((s as TreeViewItem).Tag.ToString());
                item.Collapsed += (s, e) => observer.RemoveFolder((s as TreeViewItem).Tag.ToString());
                foldersItem_2.Items.Add(item);
            }
            foldersItem_2.SelectedItemChanged += (s, args) =>
            {
                foldersItem_2_text.Text = (args.NewValue as TreeViewItem).Tag.ToString();
                foldersItem_1_arrow.Visibility = Visibility.Hidden;
                foldersItem_2_arrow.Visibility = Visibility.Visible;
            };
            foldersItem_2.PreviewMouseDown += (s, args) =>
            {
                foldersItem_1_arrow.Visibility = Visibility.Hidden;
                foldersItem_2_arrow.Visibility = Visibility.Visible;
            };
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
                item.Expanded += (s, e) => logger.LogDebug($"Expanded folder {(s as TreeViewItem).Tag}");
                item.Collapsed += (s, e) => logger.LogDebug($"Collapsed folder {(s as TreeViewItem).Tag}");
                item.Expanded += (s, e) => observer.AddFolder((s as TreeViewItem).Tag.ToString());
                item.Collapsed += (s, e) => observer.RemoveFolder((s as TreeViewItem).Tag.ToString());
                foldersItem_1.Items.Add(item);
            }
            foldersItem_1.SelectedItemChanged += (s, args) =>
            {
                foldersItem_1_text.Text = (args.NewValue as TreeViewItem).Tag.ToString();
                foldersItem_1_arrow.Visibility = Visibility.Visible;
                foldersItem_2_arrow.Visibility = Visibility.Hidden;
            };
            foldersItem_1.PreviewMouseDown += (s, args) =>
            {
                foldersItem_2_arrow.Visibility = Visibility.Hidden;
                foldersItem_1_arrow.Visibility = Visibility.Visible;
            };
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
                        TreeViewItem subitem = new();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        subitem.Expanded += (s, e) => logger.LogDebug($"Expanded folder {(s as TreeViewItem).Tag}");
                        subitem.Collapsed += (s, e) => logger.LogDebug($"Collapsed folder {(s as TreeViewItem).Tag}");
                        subitem.Expanded += (s, e) => observer.AddFolder((s as TreeViewItem).Tag.ToString());
                        subitem.Collapsed += (s, e) => observer.RemoveFolder((s as TreeViewItem).Tag.ToString());
                        item.Items.Add(subitem);
                    }
                    foreach (string s in Directory.GetFiles(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.MouseDoubleClick += (s, e) => FileOperationsFacade.TryToOpen(((TreeViewItem)s).Tag.ToString());
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
        public static PathToImageConverter Instance = new();

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var path = value.ToString();
            string icon_name;
            if (path.Last().ToString() == @"\") icon_name = "diskdrive";
            else if (File.GetAttributes(path).HasFlag(FileAttributes.Directory)) icon_name = "folder";
            else if (Format.IsFormat(path, "txt")) icon_name = "txt";
            else if (Format.IsFormat(path, "dll")) icon_name = "dll";
            else if (Format.IsFormat(path, "exe")) icon_name = "exe";
            else if (Format.IsFormat(path, "html")) icon_name = "html";
            else if (Format.IsMusicFormat(path)) icon_name = "music";
            else if (Format.IsImageFormat(path)) icon_name = "image";
            else icon_name = "file";

            Uri uri = new Uri($"pack://application:,,,/Images/{icon_name}.png");
            BitmapImage source = new BitmapImage(uri);
            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException("Cannot convert back");
    }

    #endregion // PathToImageConverter
}

