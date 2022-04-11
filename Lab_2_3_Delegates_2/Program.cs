using System;
using System.Collections.Generic;

namespace Lab_2_3_Delegates_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Garage garage = new Garage();
            for (int i = 0; i < 10; i++)
            {
                garage.AddCar(new Car($"model #{i + 1}"));
            }
            Wash(garage);
        }

        static void Wash(Garage garage)
        {
            Washer washer = new Washer();
            foreach (var car in garage.Cars)
            {
                washer.WashEvent += car.SetWashed;
            }
            washer.Wash();
            Console.WriteLine("All cars are washed!");
        }
    }
    class Car
    {
        public Car(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
        public bool Washed { get; private set; }
        public void SetWashed()
        {
            Washed = true;
            Console.WriteLine($"Car {Name} is washed!");
        }
    }
    class Garage
    {
        public List<Car> Cars { get; private set; } = new List<Car>();
        public void AddCar(Car car) => Cars.Add(car);
    }
    class Washer
    {
        public event Action WashEvent;
        public void Wash()
        {
            WashEvent.Invoke();
        }
    }
}