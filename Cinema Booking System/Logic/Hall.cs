using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Cinema_Booking_System.Classes
{
    class Hall
    {
        public event Action OnChanged;

        public string Name { get; set; }
        public DateTime Time { get; set; }

        private List<Place> places;
        public List<Place> Places => places;
        public int Seats => places.Count;
        public int VacantSeats { get; private set; }
        public int ReservedSeats { get; private set; }
        public double TotalValue { get; private set; }

        public Place SelectedPlace { get; private set; }
        public void SetCurrentPlace(Place place)
        {
            if (places.Contains(place)) SelectedPlace = place;
        }

        public Point ScreenPosition { get; private set; }
        public int ScreenSizeInPlaces { get; private set; }

        public Hall(List<Place> places, Point screenPosition, int screenSizeInPlaces)
        {
            this.places = places;
            ScreenPosition = screenPosition;
            ScreenSizeInPlaces = screenSizeInPlaces;
            places.ForEach(place => place.OnChanged += () => OnChanged?.Invoke());
            OnChanged += ChangeInfo;
            ChangeInfo();
        }

        private void ChangeInfo()
        {
            VacantSeats = Places.Where(x => x.Status == PlaceStatus.Vacant).Count();
            ReservedSeats = Places.Where(x => x.Status == PlaceStatus.Reserved).Count();
            TotalValue = Places.Where(x => x.Status == PlaceStatus.Reserved).Select(x => x.Cost).Sum();
        }

    }
}