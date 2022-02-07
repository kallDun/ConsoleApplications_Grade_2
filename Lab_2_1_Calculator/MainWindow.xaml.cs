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

namespace Lab_2_1_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool isAdditionPanelShow = false;
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
    }
}
