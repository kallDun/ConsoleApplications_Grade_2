using Lab_2_3.Logic.Models;
using Lab_2_3.Logic.Render;
using Lab_2_3.Logic.Services;
using Lab_2_3.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        private const int TraceEnds = 100000;
        HorsesService _HorsesService;
        RenderService _RenderService;
        MainRender Render;

        public MainWindow()
        {
            InitializeComponent();
            Init__();
        }

        private void Init__()
        {
            _HorsesService = new HorsesService();
            GenerateHorsesList(HorsesCountComboBox);
            var (backgrounds, foregrounds) = new BackgroundGenerator().Generate();
            _RenderService = new RenderService(_HorsesService, backgrounds, foregrounds);
            ChangeObserver(0);
            Render = new MainRender(RenderField, () => ((int)FieldGrid.ActualWidth, (int)FieldGrid.ActualHeight), _RenderService);
            Render.RenderEvent += () =>
            {
                var horse = _RenderService.ObservableHorse;
                var percent = horse.Position * 100.0 / TraceEnds;
                SelectedHorse_ProgressBar.Value = percent;
                SelectedHorse_Percent_TextBlock.Text = string.Format("{0:0.##}%", percent);                
            };
            Render.Start();
            Info_Grid.ItemsSource = _HorsesService.Horses;
            TurnOnAutoSorting();
        }
        private async void TurnOnAutoSorting()
        {
            while (true)
            {
                Info_Grid.Items.SortDescriptions.Add(new SortDescription("Position", ListSortDirection.Ascending));
                await Task.Delay(500);
            }
        }
        private void GenerateHorsesList(ComboBox comboBox)
        {
            var count = int.Parse(((comboBox.SelectedItem as ComboBoxItem).Content as TextBlock).Text);
            _HorsesService?.GenerateList(count);
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _HorsesService.StartRace(TraceEnds);
        }
        private void HorsesCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => GenerateHorsesList(sender as ComboBox);
        private void ChangeObservableButton_Click(object sender, RoutedEventArgs e)
        {
            var index = _HorsesService.Horses.IndexOf(_RenderService.ObservableHorse);
            index = (index + 1) % _HorsesService.Horses.Count;
            ChangeObserver(index);
        }
        private void ChangeObserver(int index)
        {
            _RenderService.ChangeObserver(_HorsesService.Horses[index]);
            ChangeObservableButton.Background = new SolidColorBrush(_RenderService.ObservableHorse.Color);
        }
    }
}