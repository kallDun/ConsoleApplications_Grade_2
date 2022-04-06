using System;
using System.Linq;
using System.Windows;

namespace Lab_2_3
{
    public partial class MainWindow : Window
    {
        private const double MoneyToBetMagnifier = 5;
        private double CurrentBalance = 250;
        private double MoneyToBet = 20;
        private int CurrentHorseToBetIndex = 0;
        private void CalculateBalanceAfterRace()
        {
            var sorted = _HorsesService.Horses.OrderBy(x => x.Time).ToList();
            var winners = sorted.Take(2).ToArray();
            CurrentBalance += winners[0].Money * winners[0].Coeff;
            CurrentBalance += winners[1].Money * winners[1].Coeff / 1.5;
            for (int i = 0; i < sorted.Count; i++)
            {
                var item = sorted[i];
                item.AddCoeff((i - 2) * 0.05);
                item.SetMoney(0);
            }
            Balance_TextBlock.Text = Math.Floor(CurrentBalance).ToString();
            UpdateMoneyToBet();
        }
        private void UpdateMoneyToBet() => UpdateMoneyToBet(MoneyToBet);
        private void UpdateMoneyToBet(double value)
        {
            MoneyToBet = value;
            if (MoneyToBet < 0) MoneyToBet = 0;
            else if (MoneyToBet > CurrentBalance) MoneyToBet = (int)Math.Floor(CurrentBalance);
            BetOnMoney_TextBlock.Text = $"{MoneyToBet}$";
        }
        private void UpdateSelectedToBetHorse() => UpdateSelectedToBetHorse(CurrentHorseToBetIndex);
        private void UpdateSelectedToBetHorse(int index)
        {
            var horses = _HorsesService.Horses;
            if (index < 0) index = 0;
            else if (index >= horses.Count) index = horses.Count - 1;
            SelectedHorseName_TextBlock.Text = horses[index].Name;
            CurrentHorseToBetIndex = index;
        }
        private void BetOnMoney_ButtonLeft_Click(object sender, RoutedEventArgs e) => UpdateMoneyToBet(MoneyToBet - MoneyToBetMagnifier);
        private void BetOnMoney_ButtonRight_Click(object sender, RoutedEventArgs e) => UpdateMoneyToBet(MoneyToBet + MoneyToBetMagnifier);
        private void SelectedHorseName_ButtonLeft_Click(object sender, RoutedEventArgs e)
            => UpdateSelectedToBetHorse((_HorsesService.Horses.Count + CurrentHorseToBetIndex - 1) % _HorsesService.Horses.Count);
        private void SelectedHorseName_ButtonRight_Click(object sender, RoutedEventArgs e)
            => UpdateSelectedToBetHorse((CurrentHorseToBetIndex + 1) % _HorsesService.Horses.Count);
        private void BetButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateMoneyToBet();
            UpdateSelectedToBetHorse();
            var horse = _HorsesService.Horses[CurrentHorseToBetIndex];
            horse.SetMoney(horse.Money + MoneyToBet);
            CurrentBalance -= MoneyToBet;
            Balance_TextBlock.Text = Math.Floor(CurrentBalance).ToString();
            UpdateMoneyToBet();
        }
    }
}