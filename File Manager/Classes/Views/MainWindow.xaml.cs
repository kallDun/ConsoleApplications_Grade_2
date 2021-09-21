using File_Manager.Classes.Operations;
using File_Manager.Classes.Operations.Extensions;
using File_Manager.Classes.Operations.Observers;
using File_Manager.Classes.Views.Dialog;
using File_Manager.Classes.Views.Reader;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace File_Manager.Classes.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileOperationsFacade fileOperations = new();
        private SystemObserver observer;

        public MainWindow()
        {
            InitializeComponent();
            ToggleOperationsMenu.Click += (s, e) =>
            {
                TextOperationsMenu.Text = (bool)ToggleOperationsMenu.IsChecked ? "Right" : "Left";
            };
        }        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            observer = SystemObserverSingleton.GetInstance();
            LoadTreeViews();
            observer.OnFolderChanged += (string path) => Dispatcher.Invoke(() => UpdateTreesPath(path));
        }

        private void Copy_Button_Click(object sender, RoutedEventArgs e) => fileOperations.Copy(GetPath());

        private async void Paste_Button_Click(object sender, RoutedEventArgs e) 
        {
            var action = await fileOperations.Paste(GetPath());
        }

        private void Cut_Button_Click(object sender, RoutedEventArgs e) => fileOperations.Cut(GetPath());

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Open_Button_Click(object sender, RoutedEventArgs e) => FileOperationsFacade.TryToOpen(GetPath());

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
