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
            var func1 = new Function("sin(x)+sqrt(2*y^3)=4");
            var func2 = new Function("tg(x)-y^2=-4");
            Argument x = new Argument("x");
            Argument y = new Argument("y");
            
        }
    }
}
