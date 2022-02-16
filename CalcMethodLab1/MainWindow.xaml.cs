using CalcMethodLab1.Logic;
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
            EquationCalculator calculator = new EquationCalculator();
            Equation equation = new Equation(-5, 5, x => Math.Sin(Math.Pow(x, 3)) / 5);
            /*var (divs, roots) = calculator.DivideIntoSegments(equation);
            OutputData.ItemsSource = divs.Select(x => new { x.Min, x.Max, X = x.Max - x.Min / 2, F = x.Func((x.Max - x.Min) / 2) });*/
            var roots = calculator.GetEquationResults(equation);
            OutputData.ItemsSource = roots.Select(x => new { X = x, Fx = equation.Func(x) });
        }
    }
}