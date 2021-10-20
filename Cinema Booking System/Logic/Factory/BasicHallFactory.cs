using System.Collections.Generic;
using System.Windows;

namespace Cinema_Booking_System.Classes.Factory
{
    class BasicHallFactory : HallFactory
    {
        public override Hall CreateHall(double basic_cost, double premium_cost)
        {
            var places = new List<Place>();
            int number = 1;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    places.Add(new Place(new Point(i, j), number++, basic_cost));
                }
            }
            for (int i = 9; i < 12; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    places.Add(new Place(new Point(i, j), number++, premium_cost));
                }
                for (int j = 10; j < 12; j++)
                {
                    places.Add(new Place(new Point(i, j), number++, premium_cost));
                }
            }

            return new Hall(places);
        }
    }
}
