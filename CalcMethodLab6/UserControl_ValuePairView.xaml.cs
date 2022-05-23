using CalcMethodLab6.Logic;
using System.Linq;
using System.Windows.Controls;

namespace CalcMethodLab6
{
    public partial class UserControl_ValuePairView : UserControl
    {
        public UserControl_ValuePairView(ValuePair value)
        {
            InitializeComponent();
            string index = new string(value.index
                .ToString()
                .ToCharArray()
                .Select(s => GetSmallNumber(int.Parse(s.ToString())))
                .ToArray());

            TValueTextBox.Text = string.Format("T{0} = {1:0.########}", index, value.T);
            YValueTextBox.Text = string.Format("Y{0} = {1:0.########}", index, value.Y);
        }
        private char GetSmallNumber(int num)
        {
            return (char)(8320 + num);
        }
    }
}