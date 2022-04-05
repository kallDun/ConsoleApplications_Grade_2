using Lab_2_3.Logic.Models;
using Lab_2_3.Logic.Utilities;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lab_2_3.Logic.Services
{
    class HorsesService
    {
        public ObservableCollection<Horse> Horses { get; private set; } = new ObservableCollection<Horse>();
        public void GenerateList(int count)
        {
            if (Horses.Count < count)
            {
                HorseUtility horseUtility = new HorseUtility();
                for (int i = Horses.Count; i < count; i++)
                {
                    var horse = horseUtility.GenerateRandomHorse(i);
                    Horses.Add(horse);
                }
            }
            else if (Horses.Count > count)
            {
                var subtract = Horses.Count - count;
                for (int i = 0; i < subtract; i++) Horses.Remove(Horses.Last());
            }
        }
        public void StartRace(int traceEnds)
        {
            foreach (var horse in Horses)
            {
                horse.Position = 0;
                horse.StartRace(traceEnds);
            }
        }
    }
}