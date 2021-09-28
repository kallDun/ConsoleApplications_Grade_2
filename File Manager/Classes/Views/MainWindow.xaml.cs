using File_Manager.Classes.Logging;
using File_Manager.Classes.Logging.Builder;
using File_Manager.Classes.Operations;
using File_Manager.Classes.Operations.Actions;
using File_Manager.Classes.Operations.DocumentMenu;
using File_Manager.Classes.Operations.Extensions;
using File_Manager.Classes.Operations.Observers;
using File_Manager.Classes.Views.Dialog;
using File_Manager.Classes.Views.Operation;
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
using static File_Manager.Classes.Views.Operation.FindOperationWindow;

namespace File_Manager.Classes.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileOperationsFacade fileOperations = new();
        private SystemObserver observer;
        private Logger logger;
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
            new ReportDirector().FormNewReport(new JsonLogBuilder());
            observer = SystemObserverSingleton.GetInstance();
            logger = LoggerSingleton.GetInstance();
            LoadTreeViews();
            observer.OnFolderChanged += (string path) => Dispatcher.Invoke(() => UpdateTreesPath(path));
            logger.LogInformation("Application started");
        }
        
        // METHODS
        private void CreateFile()
        {
            var dialog = DialogHelper.GetSaveFileDialog("Create Document");
            if (dialog.ShowDialog() is true)
            {
                var path = dialog.FileName;
                fileOperations.Create(path);
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
            var action = fileOperations.Delete(path);
            if (action == DirectoryActions.Ok) observer.CallPathChangedEvent(path.GoBackInPath());
        }
        private void Open_Button_Click(object sender, RoutedEventArgs e) => fileOperations.Open(GetPath());
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
                fileOperations.Open(path);
            }
        }
        private void RadioButton_LogsType_Click(object sender, RoutedEventArgs e)
        {
            var name = (sender as RadioButton).Content;
            LogBuilder builder;
            switch (name)
            {
                case "xml":
                    builder = new XmlLogBuilder();
                    break;
                case "txt":
                    builder = new TxtLogBuilder();
                    break;
                case "json":
                default:
                    builder = new JsonLogBuilder();
                    break;
            }
            var director = new ReportDirector();
            director.FormNewReport(builder);
        }
        private async void FindName__item_Click(object sender, RoutedEventArgs e)
        {
            PromptDialogWindow promptDialog = new("Type file name");
            promptDialog.ShowDialog();
            if (!promptDialog.IsOk) return;

            string file_name = promptDialog.ResponseText;
            var where_find_folder = GetPath();

            if (string.IsNullOrEmpty(where_find_folder) ||
                !where_find_folder.IsDirectoryOrDrive())
            {
                MessageBox.Show("Invalid file name!");
                return;
            }

            if (string.IsNullOrEmpty(file_name) ||
                !file_name.Contains('.')) 
            {
                MessageBox.Show("Invalid path!");
                return;
            }

            FileFound fileFound = new((string file) => 
            { 
                var name = file.Substring(file.LastIndexOf("\\") + 1);
                return name == file_name; 
            });
            FindOperationWindow findOperation = new(fileFound, $"Finding file '{file_name}'");
            await findOperation.Start(where_find_folder);

            if (!findOperation.isDone)
            {
                MessageBox.Show($"Could not find the file '{file_name}' in '{where_find_folder}'");
                return;
            }
            OpenOperationResultPathInTree(findOperation.founded_file);
        }
    }
}
