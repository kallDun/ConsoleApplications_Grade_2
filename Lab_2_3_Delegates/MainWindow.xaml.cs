using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab_2_3_Delegates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            MainButton.Click += (s, e) => MessageBox.Show("I'm button!");
            TransparentButton.Click += TransparentClick;
            BackgroundButton.Click += BackgroundClick;
            PrintButton.Click += PrintClick;
            TransparentCheckBox.Click += (s, e) => CheckBoxActivation(s as CheckBox, TransparentClick);
            BackgroundCheckBox.Click += (s, e) => CheckBoxActivation(s as CheckBox, BackgroundClick);
            PrintCheckBox.Click += (s, e) => CheckBoxActivation(s as CheckBox, PrintClick);
        }

        bool isTransparent, isBackground;
        private void PrintClick(object s, RoutedEventArgs args)
        {
            MessageBox.Show("Print string");
        }
        private void BackgroundClick(object s, RoutedEventArgs args)
        {
            foreach (var button in MainGrid.Children.Cast<object>().Where(x => x is Button).Select(x => x as Button))
            {
                button.Background = isBackground ? Brushes.Black : Brushes.Transparent;
            }
            isBackground = !isBackground;
        }
        private void TransparentClick(object s, RoutedEventArgs args)
        {
            Background = isTransparent ? Brushes.White : Brushes.Transparent;
            isTransparent = !isTransparent;
        }
        private void CheckBoxActivation(CheckBox checkBox, RoutedEventHandler _delegate)
        {
            if (checkBox.IsChecked is true)
            {
                MainButton.Click += _delegate;
            }
            else
            {
                MainButton.Click -= _delegate;
            }
        }
    }
}