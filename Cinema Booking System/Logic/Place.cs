using System;
using System.Windows;

namespace Cinema_Booking_System.Classes
{
    class Place
    {
        public event Action OnChanged;
        public Point Position { get; }
        public int Number { get; }
        public double Cost { get; }

        private PlaceStatus status = PlaceStatus.Vacant;
        private Person person;
        public PlaceStatus Status => status;
        public Person Person => person.Clone() as Person;

        public Place(Point position, int number, double cost)
        {
            Position = position;
            Number = number;
            Cost = cost;
        }

        public bool TryToReservePlace(Person person)
        {
            if (status != PlaceStatus.Vacant) return false;

            this.person = person;
            status = PlaceStatus.Reserved;
            OnChanged?.Invoke();
            return true;
        }

        public bool TryToDenyReserve(Person person)
        {
            if (status != PlaceStatus.Reserved) return false;
            if (this.person.CompareTo(person) != 0) return false;

            this.person = null;
            status = PlaceStatus.Vacant;
            OnChanged?.Invoke();
            return true;
        }

        public bool SetPlaceUnavailable()
        {
            if (status == PlaceStatus.Reserved) return false;
            status = PlaceStatus.Unavailable;
            return true;
        }

    }
}