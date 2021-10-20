using Cinema_Booking_System.Classes;
using Cinema_Booking_System.Classes.Factory;
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
            HallFactory hallFactory = new BasicHallFactory();
            var hall = hallFactory.CreateHall(5.5, 6.75);
            hall.Name = "Basic hall";
            hall.Time = new DateTime(2021, 11, 01, 17, 0, 0);
            cinemaSystem.AddHall(hall);
            cinemaSystem.SetCurrentHall("Basic hall");
            ShowHallViewer();
        }

        // hall viewer

        private double left_offset = 135, top_offset = 25, place_size = 20, margin = 5, screen_height = 35;
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
                    viewer_control_reserve_but.IsEnabled = place_.Status == PlaceStatus.Vacant;
                    viewer_control_cancel_but.IsEnabled = place_.Status == PlaceStatus.Reserved;
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
                SetButtonColorByProp(cinemaSystem.SelectedHall.SelectedPlace, true);
                viewer_control_stackpanel.Visibility = Visibility.Visible;
            }
            else viewer_control_stackpanel.Visibility = Visibility.Hidden;
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
                ShowHallViewer();
            }
        }
        private void viewer_control_cancel_but_Click(object sender, RoutedEventArgs e)
        {
            var person = GetPerson();
            if (person is null) return;
            if (cinemaSystem.SelectedHall.SelectedPlace?.TryToDenyReserve(person) == true)
            {
                ShowHallViewer();
            }
        }

        // nav buttons
        private void nav_button_reservationList_Click(object sender, RoutedEventArgs e)
        {
            nav_button_reservationList.Style = FindResource("MaterialDesignFlatAccentBgButton") as Style;
            nav_button_hallViewer.Style = FindResource("MaterialDesignFlatAccentButton") as Style;
            HallViewer.Visibility = Visibility.Hidden;
            ReservationList.Visibility = Visibility.Visible;
        }
        private void nav_button_hallViewer_Click(object sender, RoutedEventArgs e)
        {
            nav_button_hallViewer.Style = FindResource("MaterialDesignFlatAccentBgButton") as Style;
            nav_button_reservationList.Style = FindResource("MaterialDesignFlatAccentButton") as Style;
            HallViewer.Visibility = Visibility.Visible;
            ReservationList.Visibility = Visibility.Hidden;
            ShowHallViewer();
        }


    }
}
