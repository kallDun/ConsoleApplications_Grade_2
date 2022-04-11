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
using System.Windows.Navigation;
using org.mariuszgromada.math.mxparser;
using System.Windows.Shapes;
using CalcMethodLab1.Logic;

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
            /*var func1 = new Function("sin(x)+sqrt(2*y^3)=4");
            var func2 = new Function("tg(x)-y^2=-4");
            Argument x = new Argument("x");
            Argument y = new Argument("y");*/
            // 2x - 2 - 1

            var func_string = "x^2 - 2x - y + 0.5";
            

            Function f = new Function($"f(x, y) = {func_string}");
            Argument x = new Argument("x = 2");

            Argument y = new Argument("y = 3");
            org.mariuszgromada.math.mxparser.Expression e = new org.mariuszgromada.math.mxparser.Expression("solve(f(x, y), x, -5, 5)", f, y);
            MessageBox.Show(e.calculate().ToString());


            /*org.mariuszgromada.math.mxparser.Expression e =
                new org.mariuszgromada.math.mxparser.Expression("der(f(x, y), x)", f, x, y);*/

        }
    }
}