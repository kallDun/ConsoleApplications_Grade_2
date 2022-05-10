using CalcMethodLab5.Logic;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace CalcMethodLab5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
            LogsData.ItemsSource = LoggerSingleton.GetLogger().Logs;
        }

        private void Button_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                LoggerSingleton.GetLogger().Logs.Clear();
                double wall_size = double.Parse(WallSizeTextBox.Text);
                double step_size = double.Parse(StepTextBox.Text);
                double y_count = double.Parse(YCountTextBox.Text);
                TaskSolver taskSolver = new TaskSolver(wall_size, step_size, y_count);
                double result = taskSolver.Solve();
                ReturnValueTextBox.Text = $"Optimal Y value is {result}";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}