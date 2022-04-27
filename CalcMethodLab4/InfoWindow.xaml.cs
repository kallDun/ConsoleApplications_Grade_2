using System.Windows;

namespace CalcMethodLab4
{
    /// <summary>
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow(string data)
        {
            InitializeComponent();
            MainTextBox.Text = data;
        }
    }
}