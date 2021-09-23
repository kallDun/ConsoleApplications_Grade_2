using File_Manager.Classes.Operations.DocumentMenu;
using File_Manager.Classes.Operations.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace File_Manager.Classes.Views.Reader
{
    /// <summary>
    /// Interaction logic for ImageReaderWindow.xaml
    /// </summary>
    public partial class ImageReaderWindow : Window, IReadonlyDocument
    {
        private string path;
        public ImageReaderWindow()
        {
            InitializeComponent();
            Show();
            IMG.Stretch = Stretch.Uniform;
        }

        // DOCUMENT METHODS
        public bool CloseDocument()
        {
            path = null;
            IMG.Source = null;
            return true;
        }

        public void OpenDocument(string path)
        {
            this.path = path;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            
            IMG.Source = bitmap;
        }

        // MENU BUTTONS
        private void Menu_Open__item_Click(object sender, RoutedEventArgs e)
        {
            string filter_name = "Image Files";
            OpenFileDialog dialog = DialogHelper.GetOpenFileDialog(Format.ImageFormats, filter_name);

            if (dialog.ShowDialog() == true)
            {
                OpenDocument(dialog.FileName);
            }
        }

        private void Menu_Close__item_Click(object sender, RoutedEventArgs e) => CloseDocument();
    }
}
