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
using System.Windows.Shapes;

namespace Lab_2_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Horse> Horses = new List<Horse>()
        {
            new Horse{Name = "Horse_1", Color = Colors.Bisque, Position = 1, Coeff = 1.2, Time = new TimeSpan(0, 0, 20)},
            new Horse{Name = "Horse_3", Color = Colors.Azure, Position = 2, Coeff = 1.67, Time = new TimeSpan(0, 0, 20)},
            new Horse{Name = "Horse_5", Color = Colors.DarkBlue, Position = 3, Coeff = 1.33, Time = new TimeSpan(0, 0, 20)},
            new Horse{Name = "Horse_2", Color = Colors.DarkViolet, Position = 4, Coeff = 2.1, Time = new TimeSpan(0, 0, 20)},
            new Horse{Name = "Horse_4", Color = Colors.Indigo, Position = 5, Coeff = 1.8, Time = new TimeSpan(0, 0, 20)},
        };

        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class Horse
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public int Position { get; set; }
        public TimeSpan Time { get; set; }
        public double Coeff { get; set; }
    }
}
