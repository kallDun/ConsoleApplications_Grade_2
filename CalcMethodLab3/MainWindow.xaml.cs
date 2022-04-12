using System;
using System.Windows;
using CalcMethodLab3.Logic;

namespace CalcMethodLab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InputDataF.Text = "sin(x)+sqrt(2*y^3)-4" + "\n" + "tg(x)-y^2+4";
            (InputX.Text, InputY.Text) = ("-6", "2");

            /*InputDataF.Text = "x^2 - 2x - y + 0.5" + "\n" + "x^2 + 4*y^2 - 4";
            InputDataItter.Text = "sqrt(2*x + y - 0.5)" + "\n" + "0.25*sqrt(4 - x^2)";
            (InputX.Text, InputY.Text) = ("2", "0.25");*/
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs args)
        {            
            try
            {
                var eps = double.Parse(InputEpsilon.Text);
                var systemSolverService = new NonLinearSystemSolverService(eps);
                var eqs_f = InputDataF.Text.Split(new string[] { "\n" }, StringSplitOptions.None);
                var group = new EquationGroup(eqs_f[0], eqs_f[1]);
                var x = double.Parse(InputX.Text);
                var y = double.Parse(InputY.Text);
                var result = systemSolverService.SolveSystem(group, x, y);
                OutputX.Text = string.Format("{0:0.########}", result.X);
                OutputY.Text = string.Format("{0:0.########}", result.Y);
                OutputTime.Text = result.Time.ToString();
                OutputItters.Text = result.Itterations.ToString();
            }
            catch (Exception e)
            {
                _ = MessageBox.Show($"Something gone wrong! Exception: {e.Message}");
            }
        }
    }
}