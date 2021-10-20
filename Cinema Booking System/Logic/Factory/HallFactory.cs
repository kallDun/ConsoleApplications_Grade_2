using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Booking_System.Classes.Factory
{
    abstract class HallFactory
    {
        public abstract Hall CreateHall(double basic_cost, double premium_cost);
    }
}
