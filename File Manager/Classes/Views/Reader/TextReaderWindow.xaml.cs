using File_Manager.Classes.Operations.DocumentMenu;
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
    public partial class TextReaderWindow : Window, IDocument, IDisposable
    {
        private string PATH;
        private string text;
        private int start_text_num = 0, text_row_size, TextLength;
        private string Text
        {
            get => text;
            set
            {
                text = value;
                TextLength = text.Split('\n').Length;
            }
        }

        public TextReaderWindow()
        {
            InitializeComponent();
            Closing += (s, e) => Dispose();
            Show();
        }

        public void Dispose()
        {
            Text = "";
            last_text = "";
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeRowSize();
            if (Text is null) return;
            ChangeVisibleText();
            CheckForEnableSlider();
        }

        private void CheckForEnableSlider()
        {
            if (TextLength <= text_row_size)
            {                
                Document_Slider.IsEnabled = false;
                Document_Slider.Value = 100;
            }
            else
            {
                Document_Slider.IsEnabled = true;
                Document_Slider_ChangeValue();
            }
        }

        int static_line_size = 16;
        private void ChangeRowSize() => text_row_size = ((int)Math.Floor(Text_Field.ActualHeight)) / static_line_size - 2;

        private void MenuItem_Font_Click(object sender, RoutedEventArgs e)
        {
            var font_size = int.Parse(((MenuItem)sender).Header.ToString().Substring(1, 2));
            static_line_size = (int)Math.Round((double)16 / 12 * font_size);
            Text_Field.FontSize = font_size;
            Numeration_Field.FontSize = font_size;
            ChangeRowSize();
            CheckForEnableSlider();
        }

        private void Text_Field_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var delta = e.Delta;
            start_text_num += delta < 0 ? 1 : -1;
            if (start_text_num < 0) start_text_num = 0;
            else if (start_text_num > TextLength - text_row_size) start_text_num = TextLength - text_row_size;
            else caret = 0;

            ChangeVisibleText();
            Document_Slider_ChangeValue();
        }

        private void Document_Slider_ChangeValue()
        {
            if (!Document_Slider.IsEnabled) return;
            var value = ((double)start_text_num) / (TextLength - text_row_size) * 100;
            Document_Slider.Value = value;
        }

        private void Document_Slider_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var value = Document_Slider.Value;
            var counter = (value / 100 * (TextLength - text_row_size));
            start_text_num = counter < 0 ? 0 : (int)Math.Round(counter);
            ChangeVisibleText();
        }

        private void Document_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => Slider_Percent_Bar.Text = $"{Math.Round(e.NewValue)} %";

        bool isChangedTextByProgram = false;
        string last_text;
        int caret;
        private void Text_Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isChangedTextByProgram) return;
            Text = Text.Replace(last_text, Text_Field.Text);
            caret = Text_Field.CaretIndex;
            CheckForEnableSlider();
            ChangeVisibleText();            
        }

        private void ChangeVisibleText()
        {
            isChangedTextByProgram = true;

            var selected = Text.Split('\n').Select((value, index) => new { value, index })
                .Where(x => x.index >= start_text_num && x.index <= start_text_num + text_row_size);

            var visible_text = string.Join("\n", selected.Select(x => x.value));
            var numeration = string.Join("\n", selected.Select(x => x.index + 1));
            last_text = visible_text;

            Text_Field.Text = visible_text;
            Numeration_Field.Text = numeration;
            Text_Field.CaretIndex = caret;
            isChangedTextByProgram = false;
        }

        // DOCUMENT WORKING METHODS
        public void OpenDocument(string path)
        {
            PATH = path;
            Text = File.ReadAllText(PATH);
            ChangeRowSize();
            ChangeVisibleText();
            CheckForEnableSlider();
        }
        public void CloseDocument()
        {
            throw new NotImplementedException();
        }
        public void SaveDocument()
        {
            throw new NotImplementedException();
        }
        public void SaveDocumentAs(string path)
        {
            throw new NotImplementedException();
        }

        // MENU BUTTONS
    }
}
