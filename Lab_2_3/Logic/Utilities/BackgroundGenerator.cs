using Lab_2_3.Logic.Models;
using System.Collections.Generic;

namespace Lab_2_3.Logic.Utilities
{
    class BackgroundGenerator
    {
        public (List<BackgroundObject>, List<BackgroundObject>) Generate()
        {
            var backs = new List<BackgroundObject>();
            var fores = new List<BackgroundObject>();
            backs.Add(new BackgroundObject());
            return (backs, fores);
        }
    }
}
