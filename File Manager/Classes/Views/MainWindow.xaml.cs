using File_Manager.Classes.Operations;
using File_Manager.Classes.Operations.Actions;
using File_Manager.Classes.Operations.DocumentMenu;
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
        
        // METHODS
        private static void CreateFile()
        {
            var dialog = DialogHelper.GetSaveFileDialog("Create Document");
            if (dialog.ShowDialog() is true)
            {
                var path = dialog.FileName;
                FileOperationsFacade.Create(path);
            }
        }
        
        // MAIN 6 BUTTONS
        private void Copy_Button_Click(object sender, RoutedEventArgs e) => fileOperations.Copy(GetPath());
        private async void Paste_Button_Click(object sender, RoutedEventArgs e) 
        {
            var path = GetPath();
            var action = await fileOperations.Paste(path);
            if (action == DirectoryActions.Ok) observer.CallPathChangedEvent(path);
        }
        private void Cut_Button_Click(object sender, RoutedEventArgs e) => fileOperations.Cut(GetPath());
        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            var path = GetPath();
            var action = FileOperationsFacade.Remove(path);
            if (action == DirectoryActions.Ok) observer.CallPathChangedEvent(path.GoBackInPath());
        }
        private void Open_Button_Click(object sender, RoutedEventArgs e) => FileOperationsFacade.TryToOpen(GetPath());
        private void Create_Button_Click(object sender, RoutedEventArgs e) => CreateFile();

        // MENU BUTTONS
        private void Open_TextReader__item_Click(object sender, RoutedEventArgs e) => new TextReaderWindow();
        private void Open_ImageReader__item_Click(object sender, RoutedEventArgs e) => new ImageReaderWindow();
        private void New_Document__item_Click(object sender, RoutedEventArgs e) => CreateFile();
        private void Open_Document__item_Click(object sender, RoutedEventArgs e)
        {
            var dialog = DialogHelper.GetOpenFileDialog();
            if (dialog.ShowDialog() is true)
            {
                var path = dialog.FileName;
                FileOperationsFacade.TryToOpen(path);
            }
        }
    }
}
