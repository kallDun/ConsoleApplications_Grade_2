using Cinema_Booking_System.Classes;
using Cinema_Booking_System.Classes.Factory;
using Cinema_Booking_System.Logic.Factory;
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

namespace Cinema_Booking_System
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CinemaSystem cinemaSystem;

        public MainWindow() => InitializeComponent();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cinemaSystem = new CinemaSystem();
            HallFactory basicHallFactory = new BasicHallFactory(), 
                premiumHallFactory = new PremiumHallFactory();
            var basic_hall = basicHallFactory.CreateHall(5.5, 6.75);
            basic_hall.Name = "BasicHall";
            basic_hall.Time = new DateTime(2021, 11, 01, 17, 0, 0);
            cinemaSystem.AddHall(basic_hall);

            var premium_hall = premiumHallFactory.CreateHall(8, 8);
            premium_hall.Name = "PremiumHall";
            premium_hall.Time = new DateTime(2021, 11, 02, 21, 30, 0);
            cinemaSystem.AddHall(premium_hall);

            cinemaSystem.SetCurrentHall("BasicHall");
            HallListBox.ItemsSource = cinemaSystem.Halls.Select(x => new ListBoxItem { Content = $"{x.Name} - {x.Time}", Tag = x.Name });
            HallListBox.SelectedItem = HallListBox.Items.Cast<ListBoxItem>().First(x => x.Tag.ToString() == "BasicHall");
            foreach (var item in HallListBox.Items.Cast<ListBoxItem>()) item.Selected += ListBoxItem_Selected;

            ShowHallViewer();
            init__data_field();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            cinemaSystem.SetCurrentHall((sender as ListBoxItem).Tag.ToString());
            UpdateView();
        }

        private void UpdateView()
        {
            if (HallViewerDisplay)
            {
                ShowHallViewer();
            }
            else
            {
                ShowReservationList();
            }
            init__data_field();
        }

        // hall viewer

        private double left_offset = 135, top_offset = 25, place_size = 20, margin = 5, screen_height = 35; // all in px
        private void ShowHallViewer()
        {
            ViewerField.Children.Clear();

            var hall = cinemaSystem.SelectedHall;

            Rectangle rectangle = new Rectangle
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = (place_size + margin) * hall.ScreenSizeInPlaces - margin,
                Height = screen_height,
                Fill = Brushes.DimGray,
                Margin = new Thickness((place_size + margin) * hall.ScreenPosition.X + left_offset, (place_size + margin) * hall.ScreenPosition.Y + top_offset, 0, 0)
            };
            ViewerField.Children.Add(rectangle);

            foreach (var place in hall.Places)
            {
                var button = new Button
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Content = place.Number,
                    Height = place_size,
                    Width = place_size,
                    Margin = new Thickness(
                        (place_size + margin) * place.Position.X + left_offset, 
                        (place_size + margin) * place.Position.Y + screen_height * 1.5 + top_offset, 0, 0)                    
                };
                SetButtonColorByProp(place, button, false);

                button.Click += (s, e) =>
                {
                    var place_ = place;
                    SetButtonEnabledByPlace(place_);
                    viewer_control_cost_textbox.Text = $"Cost: {place_.Cost}$";

                    if (cinemaSystem.SelectedHall.SelectedPlace != null) SetButtonColorByProp(cinemaSystem.SelectedHall.SelectedPlace, false);
                    cinemaSystem.SelectedHall.SetCurrentPlace(place_);
                    viewer_control_stackpanel.Visibility = Visibility.Visible;
                    SetButtonColorByProp(place_, s as Button, true);
                };

                ViewerField.Children.Add(button);
            }

            if (cinemaSystem.SelectedHall.SelectedPlace != null)
            {
                SetButtonEnabledByPlace(cinemaSystem.SelectedHall.SelectedPlace);
                viewer_control_cost_textbox.Text = $"Cost: {cinemaSystem.SelectedHall.SelectedPlace.Cost}$";
                SetButtonColorByProp(cinemaSystem.SelectedHall.SelectedPlace, true);
                viewer_control_stackpanel.Visibility = Visibility.Visible;
            }
            else viewer_control_stackpanel.Visibility = Visibility.Hidden;
        }
        private void SetButtonEnabledByPlace(Place place_)
        {
            viewer_control_reserve_but.IsEnabled = place_.Status == PlaceStatus.Vacant;
            viewer_control_cancel_but.IsEnabled = place_.Status == PlaceStatus.Reserved;
        }
        private void SetButtonColorByProp(Place place, Button button, bool IsSelected)
        {
            if (IsSelected)
            {
                button.Style = FindResource("MaterialDesignPaperDarkButton") as Style;
            }
            else
            {
                switch (place.Status)
                {
                    case PlaceStatus.Vacant:
                        button.Style = FindResource("MaterialDesignPaperSecondaryButton") as Style;
                        break;
                    case PlaceStatus.Reserved:
                        button.Style = FindResource("MaterialDesignRaisedSecondaryDarkButton") as Style;
                        break;
                    case PlaceStatus.Unavailable:
                        button.Style = FindResource("MaterialDesignFlatDarkBgButton") as Style;
                        button.IsEnabled = false;
                        break;
                }
            }
        }
        private void SetButtonColorByProp(Place place, bool IsSelected)
        {
            var button = ViewerField.Children.Cast<FrameworkElement>().First(x => x is Button && (x as Button).Content.ToString() == place.Number.ToString()) as Button;
            SetButtonColorByProp(place, button, IsSelected);
        }
        private Person GetPerson()
        {
            var text = viewer_control_name_textbox.Text;
            if (string.IsNullOrEmpty(text)) return null;
            var lines = text.Trim().Split();
            if (lines.Length != 2) return null;
            return new Person(lines[0], lines[1]);
        }
        private void viewer_control_reserve_but_Click(object sender, RoutedEventArgs e)
        {
            var person = GetPerson();
            if (person is null) return;
            if (cinemaSystem.SelectedHall.SelectedPlace?.TryToReservePlace(person) == true)
            {
                SetButtonEnabledByPlace(cinemaSystem.SelectedHall.SelectedPlace);
            }
        }
        private void viewer_control_cancel_but_Click(object sender, RoutedEventArgs e)
        {
            var person = GetPerson();
            if (person is null) return;
            if (cinemaSystem.SelectedHall.SelectedPlace?.TryToDenyReserve(person) == true)
            {
                SetButtonEnabledByPlace(cinemaSystem.SelectedHall.SelectedPlace);
            }
        }

        // nav buttons

        private bool HallViewerDisplay = true;
        private void nav_button_reservationList_Click(object sender, RoutedEventArgs e)
        {
            nav_button_reservationList.Style = FindResource("MaterialDesignFlatAccentBgButton") as Style;
            nav_button_hallViewer.Style = FindResource("MaterialDesignFlatAccentButton") as Style;
            HallViewer.Visibility = Visibility.Hidden;
            ReservationList.Visibility = Visibility.Visible;
            HallViewerDisplay = false;
            ShowReservationList();
        }
        private void nav_button_hallViewer_Click(object sender, RoutedEventArgs e)
        {
            nav_button_hallViewer.Style = FindResource("MaterialDesignFlatAccentBgButton") as Style;
            nav_button_reservationList.Style = FindResource("MaterialDesignFlatAccentButton") as Style;
            HallViewer.Visibility = Visibility.Visible;
            ReservationList.Visibility = Visibility.Hidden;
            HallViewerDisplay = true;
            ShowHallViewer();
        }

        // data
        private void init__data_field()
        {
            ChangeDataField();
            cinemaSystem.SelectedHall.OnChanged += ChangeDataField;
        }
        private void ChangeDataField()
        {
            data__seats_textbox.Text = cinemaSystem.SelectedHall?.Seats.ToString();
            data__vacant_seats_textbox.Text = cinemaSystem.SelectedHall?.VacantSeats.ToString();
            data__reserved_seats_textbox.Text = cinemaSystem.SelectedHall?.ReservedSeats.ToString();
            data__total_value_textbox.Text = $"{cinemaSystem.SelectedHall?.TotalValue}$";
        }

        // reservation list
        private void ShowReservationList()
        {
            ReservationDataGrid.ItemsSource = cinemaSystem.SelectedHall.Places
                .Where(x => x.Status == PlaceStatus.Reserved)
                .Select(x => new { Place = x.Number, Name = x.Person, Cost = $"{x.Cost}$" });
        }

    }
}
