using CalcMethodLab6.Logic;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace CalcMethodLab6
{
    public partial class MainWindow : Window
    {
        ValuePair[] results;

        public MainWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                string f = Function_data.Text;
                double[] interval = Interval_data.Text.Split(',').Select(double.Parse).ToArray();
                double start_value = double.Parse(FirstValue_data.Text);
                double step = double.Parse(Step_data.Text);
                CustomFunc func = new CustomFunc(f);
                IDiffMethod method = new EulerCauchyDiffMethod();
                results = method.CalcDiffOn(func, step, interval[0], interval[1], start_value);
                foreach (var item in results)
                {
                    Output_data.Children.Add(new UserControl_ValuePairView(item));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void CopyResultsButton_Click(object sender, RoutedEventArgs e)
        {
            var text = "[" + string.Join(", ", results.ToList()) + "]";
            Clipboard.SetText(text);
        }
    }
}