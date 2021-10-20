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

        public void TryToReservePlace(Person person)
        {
            if (status != PlaceStatus.Vacant) return;

            this.person = person;
            status = PlaceStatus.Reserved;
            OnChanged?.Invoke();
        }

        public void TryToDenyReserve(Person person)
        {
            if (status != PlaceStatus.Reserved) return;
            if (this.person.CompareTo(person) != 0) return;

            this.person = null;
            status = PlaceStatus.Vacant;
            OnChanged?.Invoke();
        }

    }
}