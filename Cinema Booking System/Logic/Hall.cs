using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinema_Booking_System.Classes
{
    class Hall
    {
        public event Action OnChanged;

        public string MovieName { get; set; }
        public DateTime Time { get; set; }

        private List<Place> places;
        public List<Place> Places => places;
        public int Seats => places.Count;
        public int VacantSeats { get; private set; }
        public int ReservedSeats { get; private set; }
        public double TotalValue { get; private set; }

        public Hall(List<Place> places)
        {
            this.places = places;
            places.ForEach(place => place.OnChanged += OnChanged.Invoke);
            OnChanged += ChangeInfo;
        }

        private void ChangeInfo()
        {
            VacantSeats = Places.Where(x => x.Status == PlaceStatus.Vacant).Count();
            ReservedSeats = Places.Where(x => x.Status == PlaceStatus.Reserved).Count();
            TotalValue = Places.Select(x => x.Cost).Sum();
        }

    }
}