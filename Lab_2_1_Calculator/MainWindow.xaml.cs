using Lab_2_1_Calculator.Logic;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab_2_1_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isAdditionPanelShow = false;
        ClientInterface client = new();
        public MainWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("gr-fr");
        }
        private void burger_show_panel_btn_Click(object sender, RoutedEventArgs e)
        {
            isAdditionPanelShow = !isAdditionPanelShow;
            if (isAdditionPanelShow)
            {
                AdditionColumnPanel.Width = new GridLength(1, GridUnitType.Star);
                burger_show_panel_btn_textblock.Text = "<";
                burger_show_panel_btn_textblock.FontSize = 35;
                MinWidth = 310;
            }
            else
            {
                AdditionColumnPanel.Width = new GridLength(0, GridUnitType.Star);
                burger_show_panel_btn_textblock.Text = "☰";
                burger_show_panel_btn_textblock.FontSize = 24;
                MinWidth = 250;
            }
        }
        private void Button_Number_Click(object sender, RoutedEventArgs e)
        {
            var character = ((sender as Button).Content as TextBlock).Text;
            InsertCharacter(character);
        }
        private void InsertCharacter(string character)
        {
            if (character is "." && Number_TextBlock.Text.Contains(".")) return;
            if (Number_TextBlock.Text is "0" && character is not "." and not "00" and not "0")
            {
                Number_TextBlock.Text = character;
            }
            else if ((Number_TextBlock.Text is "0" && character is ".") || Number_TextBlock.Text is not "0")
            {
                Number_TextBlock.Text += character;
            }
        }
        private void Button_Operation_Click(object sender, RoutedEventArgs e)
        {
            if (Number_TextBlock.Text[Number_TextBlock.Text.Length - 1] == '.') Number_TextBlock.Text += '0';
            CalculateOperation((sender as Button).Tag as string);
        }
        private void CalculateOperation(string tag)
        {
            var number = double.Parse(Number_TextBlock.Text);
            switch (tag)
            {
                case "Plus":
                    client.Plus(number);
                    break;
                case "Subtract":
                    client.Subtract(number);
                    break;
                case "Multiply":
                    client.Multiply(number);
                    break;
                case "Divide":
                    client.Divide(number);
                    break;
                case "Root":
                    client.Root(number);
                    break;
                case "Power":
                    client.Power(number);
                    break;
                case "Log":
                    client.Log(number);
                    break;
                case "Equals":
                    client.Equals(number);
                    break;
                default:
                    return;
            }
            ShowOperationText();
        }
        private void ShowOperationText()
        {
            if (client.Operation is not "")
            {
                Number_TextBlock.Text = client.OperationNumber.ToString();
                Operation_TextBlock.Text = string.Format("{0} {1}", client.CalculatorNumber, client.Operation);
            }
            else
            {
                Number_TextBlock.Text = client.CalculatorNumber.ToString();
                Operation_TextBlock.Text = "";
            }
        }
        private void Backspace_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Number_TextBlock.Text is not "0")
            {
                Number_TextBlock.Text = Number_TextBlock.Text.Substring(0, Number_TextBlock.Text.Length - 1);
                if (Number_TextBlock.Text is "") Number_TextBlock.Text = "0";
            }
        }
        private void CE_Button_Click(object sender, RoutedEventArgs e)
        {
            client.UndoLastCommand();
            ShowOperationText();
        }
        private void C_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Number_TextBlock.Text is "0")
            {
                client.ClearAll();
                Operation_TextBlock.Text = "";
            }
            Number_TextBlock.Text = "0";
        }
        private void Constant_Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Tag)
            {
                case "Pi":
                    Number_TextBlock.Text = string.Format("{0:0.##########}", Math.PI);
                    break;
                case "Exp":
                    Number_TextBlock.Text = string.Format("{0:0.##########}", Math.E);
                    break;
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1:
                case Key.NumPad1:
                    InsertCharacter("1");
                    break;
                case Key.D2:
                case Key.NumPad2:
                    InsertCharacter("2");
                    break;
                case Key.D3:
                case Key.NumPad3:
                    InsertCharacter("3");
                    break;
                case Key.D4:
                case Key.NumPad4:
                    InsertCharacter("4");
                    break;
                case Key.D5:
                case Key.NumPad5:
                    InsertCharacter("5");
                    break;
                case Key.D6:
                case Key.NumPad6:
                    InsertCharacter("6");
                    break;
                case Key.D7:
                case Key.NumPad7:
                    InsertCharacter("7");
                    break;
                case Key.D8:
                case Key.NumPad8:
                    InsertCharacter("8");
                    break;
                case Key.D9:
                case Key.NumPad9:
                    InsertCharacter("9");
                    break;
                case Key.D0:
                case Key.NumPad0:
                    InsertCharacter("0");
                    break;
                case Key.Separator:
                case Key.OemComma:
                case Key.OemPeriod:
                    InsertCharacter(".");
                    break;
                case Key.OemMinus:
                case Key.Subtract:
                    CalculateOperation("Subtract");
                    break;
                case Key.OemPlus:
                case Key.Add:
                    CalculateOperation("Plus");
                    break;
                case Key.Multiply:
                    CalculateOperation("Multiply");
                    break;
                case Key.Divide:
                case Key.OemQuestion:
                    CalculateOperation("Divide");
                    break;
                case Key.Enter:
                    CalculateOperation("Equals");
                    break;
                case Key.Back:
                    Backspace_Button_Click(null, null);
                    break;
                case Key.Escape:
                case Key.Space:
                    CE_Button_Click(null, null);
                    break;
                case Key.Delete:
                case Key.Home:
                    C_Button_Click(null, null);
                    break;
            }
        }
    }
}