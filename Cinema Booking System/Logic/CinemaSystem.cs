using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Booking_System.Classes
{
    class CinemaSystem
    {
        public List<Hall> halls { get; private set; } = new List<Hall>();
        public void AddHall(Hall hall) => halls.Add(hall);
        public void RemoveHall(Hall hall) => halls.Remove(hall);
    }
}
