﻿using Lab_2_3.Logic.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab_2_3.Logic.Models
{
    class BackgroundObject : IRenderable
    {
        public void Render(DrawingContext dc, int cameraPos)
        {
            //dc.DrawRectangle(Brushes.Yellow, null, new System.Windows.Rect(0, 0, 1000, 1000));
        }
    }
}
