using File_Manager.Classes.Operations.DocumentMenu;
using File_Manager.Classes.Operations.Extensions;
using File_Manager.Classes.Operations.Observers;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace File_Manager.Classes.Views.Reader
{
    /// <summary>
    /// Interaction logic for TextReaderWindow.xaml
    /// </summary>
    public partial class TextReaderWindow : Window, IDocument
    {
        private event Action OnChangingPath;
        private string text, last_text, path;
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
        private string Path
        {
            get => path;
            set
            {
                path = value;
                OnChangingPath.Invoke();
            }
        }
        public TextReaderWindow()
        {
            InitializeComponent();
            Closing += Closing_;
            OnChangingPath += () =>
            {
                var path_null = string.IsNullOrEmpty(Path);
                Title = path_null ? "TextReader" : $"TextReader ({Path.Split('\\').Last()})";
                Menu_Save__item.IsEnabled = !path_null;
                Menu_SaveAs__item.IsEnabled = !path_null;
                Menu_Close__item.IsEnabled = !path_null;
            };
            Show();
        }

        private void Closing_(object sender, CancelEventArgs e)
        {
            if (!CloseDocument()) e.Cancel = true;
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
        int caret;
        private void Text_Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isChangedTextByProgram) return;
            if (!string.IsNullOrEmpty(Text))
            {
                Text = Text.Replace(last_text, Text_Field.Text);
            }
            else
            {
                Text = Text_Field.Text;
            }

            IsSaved = false;
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

        private bool IsSaved = true;
        public void OpenDocument(string path)
        {
            Path = path;
            Text = File.ReadAllText(Path);
            Text_Field.IsReadOnly = false;
            ChangeRowSize();
            ChangeVisibleText();
            CheckForEnableSlider();
        }
        public bool CloseDocument()
        {
            if (!IsSaved)
            {
                bool? wannaClose = DialogHelper.DialogYesNoDiscard("Close document",
                    "You have unsaved changes. Do you want to save file before closing?");
                if (wannaClose is null) return false;
                if (wannaClose == true) SaveDocument();
            }
            
            Path = null;
            Text = "";
            last_text = "";
            Text_Field.IsReadOnly = true;
            ChangeVisibleText();
            return true;
        }
        public void SaveDocument() => SaveDocumentAs(Path);
        public void SaveDocumentAs(string path)
        {
            IsSaved = true;
            File.WriteAllText(path, Text);
        }

        // MENU BUTTONS
        private void Menu_Create__item_Click(object sender, RoutedEventArgs e)
        {
            var dialog = DialogHelper.GetSaveFileDialog("Create Item", Format.TextFormats, "Text File");

            if (dialog.ShowDialog() == true)
            {
                if (!CloseDocument()) return;

                var path = dialog.FileName;
                File.Create(path).Close();
                OpenDocument(path);

                var observer = SystemObserverSingleton.GetInstance();
                observer.CallPathChangedEvent(path);
            }
        }
        private void Menu_Open__item_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = DialogHelper.GetOpenFileDialog(Format.TextFormats, "Text File", true);

            if (dialog.ShowDialog() == true)
            {
                OpenDocument(dialog.FileName);
            }
        }
        private void Menu_Save__item_Click(object sender, RoutedEventArgs e) => SaveDocument();
        private void Menu_SaveAs__item_Click(object sender, RoutedEventArgs e) 
        {
            var dialog = DialogHelper.GetSaveFileDialog("Save As", Format.TextFormats, "Text File");

            if (dialog.ShowDialog() == true)
            {
                var path = dialog.FileName;
                SaveDocumentAs(path);
                var observer = SystemObserverSingleton.GetInstance();
                observer.CallPathChangedEvent(path);
            }
        }
        private void MenuItemClose_Click(object sender, RoutedEventArgs e) => CloseDocument();
    }
}
