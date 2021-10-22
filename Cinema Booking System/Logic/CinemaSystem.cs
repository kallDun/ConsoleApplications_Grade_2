using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Booking_System.Classes
{
    class CinemaSystem
    {
        private List<Hall> halls = new List<Hall>();
        public Hall SelectedHall { get; private set; }
        public List<Hall> Halls => halls;

        public void SetCurrentHall(string name) => SelectedHall = halls.FirstOrDefault(x => x.Name == name);
        public void AddHall(Hall hall) => halls.Add(hall);
        public void RemoveHall(Hall hall) => halls.Remove(hall);
    }
}
