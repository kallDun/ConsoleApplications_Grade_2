using Cinema_Booking_System.Classes;
using Cinema_Booking_System.Classes.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cinema_Booking_System.Logic.Factory
{
    class PremiumHallFactory : HallFactory
    {
        public override Hall CreateHall(double basic_cost, double premium_cost)
        {
            var places = new List<Place>();
            int number = 1;

            for (int i = 1; i < 8; i++)
            {
                for (int j = 1; j < 7; j++)
                {
                    var place = new Place(new Point(j, i), number++, basic_cost);
                    places.Add(place);
                }
            }

            return new Hall(places, new Point(0, 0), 8);
        }
    }
}
