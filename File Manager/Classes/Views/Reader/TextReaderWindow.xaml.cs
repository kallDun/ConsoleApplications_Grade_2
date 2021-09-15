using System;
using System.Collections.Generic;
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

namespace File_Manager.Classes.Views.Reader
{
    /// <summary>
    /// Interaction logic for TextReaderWindow.xaml
    /// </summary>
    public partial class TextReaderWindow : Window
    {
        private string file_path;
        public TextReaderWindow(string file_path)
        {
            InitializeComponent();
            this.file_path = file_path;
            Show();
        }

        public async void FillText()
        {
            await Task.Run(() => Dispatcher.Invoke(() => MainTextBox.Text = File.ReadAllText(file_path)));
        }


    }
}
