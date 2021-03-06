using Lab_2_3.Logic.Models;
using Lab_2_3.Logic.Render;
using Lab_2_3.Logic.Services;
using Lab_2_3.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        private const int TraceLength = 50000;
        private bool IsRaceStarted, IsRaceStopped;
        private HorsesService _HorsesService;
        private RenderService _RenderService;
        private MainRender Render;

        public MainWindow()
        {
            InitializeComponent();
            Init__();
            Console.WriteLine("Main Thread: " + Thread.CurrentThread.ManagedThreadId);
        }

        private void Init__()
        {
            _HorsesService = new HorsesService();
            _RenderService = new RenderService(_HorsesService, new BackgroundGenerator().Generate(TraceLength),
                () => ((int)FieldGrid.ActualWidth, (int)FieldGrid.ActualHeight));
            GenerateHorsesList(HorsesCountComboBox);
            ChangeObserver(0);
            Render = new MainRender(RenderField, _RenderService);
            Render.RenderEvent += () =>
            {
                var horse = _RenderService.ObservableHorse;
                var percent = horse.Position * 100.0 / TraceLength;
                SelectedHorse_ProgressBar.Value = percent;
                SelectedHorse_Percent_TextBlock.Text = string.Format("{0:0.##}%", percent);                
            };
            Render.Start();
            Info_Grid.ItemsSource = _HorsesService.Horses;
        }
        private async void TurnOnAutoSorting()
        {
            while (IsRaceStarted)
            {
                Info_Grid.Items.SortDescriptions.Add(new SortDescription("Position", ListSortDirection.Descending));
                await Task.Delay(500);
            }
        }
        private void GenerateHorsesList(ComboBox comboBox)
        {
            if (_HorsesService is null || _RenderService is null) return;
            var count = int.Parse(((comboBox.SelectedItem as ComboBoxItem).Content as TextBlock).Text);
            _HorsesService?.GenerateList(count);
            if (!_HorsesService.Horses.Contains(_RenderService.ObservableHorse)) ChangeObserver(0);
            UpdateSelectedToBetHorse();
        }
        private void ChangeObserver(int index)
        {
            _RenderService.ChangeObserver(_HorsesService.Horses[index]);
            ChangeObservableButton.Background = new SolidColorBrush(_RenderService.ObservableHorse.Color);
        }
        public void TurnOnOffEnabledWhenStarted(bool Enabled)
        {
            HorsesCountComboBox.IsEnabled = Enabled;
            BettingsGrid.IsEnabled = Enabled;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsRaceStarted)
            {
                IsRaceStopped = true;
                _HorsesService.StopRace();
            }
            else
            {
                TurnOnOffEnabledWhenStarted(false);
                IsRaceStopped = false;
                IsRaceStarted = true;
                StartButton_TextBlock.Text = "Stop";
                TurnOnAutoSorting();

                await _HorsesService.StartRaceAsync(TraceLength);

                TurnOnOffEnabledWhenStarted(true);
                IsRaceStarted = false;
                StartButton_TextBlock.Text = "Start";
                Info_Grid.Items.SortDescriptions.Add(new SortDescription("Time", ListSortDirection.Ascending));
                if (!IsRaceStopped) CalculateBalanceAfterRace();
            }
        }
        private void HorsesCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => GenerateHorsesList(sender as ComboBox);
        private void ChangeObservableButton_Click(object sender, RoutedEventArgs e)
        {
            var index = _HorsesService.Horses.IndexOf(_RenderService.ObservableHorse);
            index = (index + 1) % _HorsesService.Horses.Count;
            ChangeObserver(index);
        }
    }
}