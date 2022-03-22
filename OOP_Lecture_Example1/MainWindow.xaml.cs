using OOP_Lecture_Example1.Logic;
using System.Windows;

namespace OOP_Lecture_Example1
{
    public partial class MainWindow : Window
    {
        private Client client = new Client();
        private int state = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            state = (state + 1) % 3;
            if (state is 0)
            {
                RectangleFill_1.Visibility = Visibility.Hidden;
                RectangleFill_2.Visibility = Visibility.Hidden;
                BeforeSerializationTextBox.Text = "";
                AfterSerializationTextBox.Text = "";
                MainButton_TextBlock.Text = "Show model before serialization";
            }
            else if (state is 1)
            {
                RectangleFill_1.Visibility = Visibility.Visible;
                MainButton_TextBlock.Text = "Show model after serialization";
                BeforeSerializationTextBox.Text = client.GenerateAndGetCCHouse().ToString();
            }
            else if (state is 2)
            {
                RectangleFill_2.Visibility = Visibility.Visible;
                MainButton_TextBlock.Text = "Clear all";
                AfterSerializationTextBox.Text = client.GetCCHouseFromSerializator().ToString();
            }
        }
    }
}