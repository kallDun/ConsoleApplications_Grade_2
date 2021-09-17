using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace File_Manager.Classes.Views.Reader
{
    /// <summary>
    /// Interaction logic for TextReaderWindow.xaml
    /// </summary>
    public partial class TextReaderWindow : Window, IDisposable
    {
        private string file_path;
        private string[] text;
        private int start_text_num = 0, text_row_size;
        public TextReaderWindow(string file_path)
        {
            InitializeComponent();
            this.file_path = file_path;
            Closing += (s, e) => Dispose();
            Show();
        }

        public void Dispose()
        {
            text = null;
        }

        public void FillText()
        {
            Document_Slider.IsEnabled = true;
            text = File.ReadAllLines(file_path);
            ChangeRowSize();
            ChangeVisibleText();
            if (text.Length == text_row_size)
            {
                Document_Slider.Value = 100;
                Document_Slider.IsEnabled = false;
                return;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) 
        { 
            ChangeRowSize();
            ChangeVisibleText();
        }

        private void ChangeRowSize()
        {
            var static_line_size = 16;
            text_row_size = ((int)Math.Floor(Text_Field.ActualHeight)) / static_line_size - 2;
        }

        private void Text_Field_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var delta = e.Delta;
            start_text_num += delta < 0 ? 1 : -1;
            if (start_text_num < 0) start_text_num = 0;
            if (start_text_num > text.Length - text_row_size) start_text_num = text.Length - text_row_size;
            ChangeVisibleText();
            Document_Slider_ChangeValue();
        }

        private void Document_Slider_ChangeValue()
        {
            if (!Document_Slider.IsEnabled) return;
            var value = ((double)start_text_num) / (text.Length - text_row_size) * 100;
            Document_Slider.Value = value;
        }

        private void Document_Slider_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var value = Document_Slider.Value;
            var counter = (value / 100 * (text.Length - text_row_size));
            start_text_num = counter < 0 ? 0 : (int)Math.Round(counter);
            ChangeVisibleText();
        }

        private void Document_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => Slider_Percent_Bar.Text = $"{Math.Round(e.NewValue)} %";

        private void ChangeVisibleText()
        {
            if (text is null) return;

            var selected = text.Select((value, index) => new { value, index })
                .Where(x => x.index >= start_text_num && x.index <= start_text_num + text_row_size);

            var visible_text = string.Join("\n", selected.Select(x => x.value));
            var numeration = string.Join("\n", selected.Select(x => x.index + 1));

            Text_Field.Text = visible_text;
            Numeration_Field.Text = numeration;
        }

    }
}
