using CalcMethodLab1.Logic;
using org.mariuszgromada.math.mxparser;
using System;
using System.Linq;
using System.Windows;

namespace CalcMethodLab1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var func = new Function("f(x) = " + InputData.Text);
                if (!func.checkSyntax()) throw new Exception("Function syntax error.");
                EquationCalculator calculator = new EquationCalculator(double.Parse(InputEpsilon.Text));
                Equation equation = new Equation(double.Parse(InputMin.Text), double.Parse(InputMax.Text),
                    x => func.calculate(x));
                var (divs, roots) = calculator.DivideIntoSegments(equation);
                OutputData_1.ItemsSource = divs.Select(x => new { x.Min, x.Max, Fmin = Math.Round(x.Func(x.Min), 5), Fmax = Math.Round(x.Func(x.Max), 5) });
                var result = calculator.GetEquationResults(equation);
                OutputData_2.ItemsSource = result.Select(x => new { X = x, Fx = equation.Func(x) });
            }
            catch (Exception exception)
            {
                MessageBox.Show($"An error occured. Message: {exception.Message}");
            }
        }
    }
}