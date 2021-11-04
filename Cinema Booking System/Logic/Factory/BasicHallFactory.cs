using System;
using System.Collections.Generic;
using System.Windows;

namespace Cinema_Booking_System.Classes.Factory
{
    class BasicHallFactory : HallFactory
    {
        Random random = new Random();

        public override Hall CreateHall(double basic_cost, double premium_cost)
        {
            var places = new List<Place>();
            int number = 1;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    var place = new Place(new Point(j, i), number++, basic_cost);
                    places.Add(place);
                }
            }
            for (int i = 7; i < 10; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    var place = new Place(new Point(j, i), number++, premium_cost);
                    places.Add(place);
                }
            }

            return new Hall(places, new Point(2, 0), 8);
        }
    }
}
