using File_Manager.Classes.Operations.DocumentMenu;
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
        }

        // DOCUMENT METHODS
        public void CloseDocument()
        {
            throw new NotImplementedException();
        }

        public void OpenDocument(string path)
        {
            this.path = path;
        }
    }
}
