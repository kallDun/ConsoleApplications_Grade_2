using Lab_2_3.Logic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_3.Logic.Services
{
    class HorsesService
    {
        public ObservableCollection<Horse> Horses { get; private set; }
        public void GenerateList(int count)
        {

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
