using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab__Task_1_11.Classes
{
    class Equation
    {
        public Equation(double a1, double b1, double c1, double a2, double b2, double c2)
        {
            this.a1 = a1;
            this.b1 = b1;
            this.c1 = c1;
            this.a2 = a2;
            this.b2 = b2;
            this.c2 = c2;
        }

        public double a1 { get; private set; }
        public double b1 { get; private set; }
        public double c1 { get; private set; }
        public double a2 { get; private set; }
        public double b2 { get; private set; }
        public double c2 { get; private set; }

        public (double, double) solutions
        {
            get 
            {
                var determ = (a1 * b2) / (a2 * b1);
                var determ_1 = (c1 * b2) / (c2 * b1);
                var determ_2 = (a1 * c2) / (a2 * c1);

                return ( determ_1 / determ, determ_2 / determ );
            }
        }
    
        public bool IsSolution(double check)
        {
            var (a, b) = solutions;
            return check == a || check == b;
        }
    }
}
