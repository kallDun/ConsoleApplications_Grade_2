using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B.Classes
{
    class TTriangle
    {
        protected double side_1, side_2, side_3;

        public TTriangle(double side_1, double side_2, double side_3)
        {
            if (!IsTriangleValidate(side_1, side_2, side_3)) throw new Exception("Invalid triangle!");
            this.side_1 = side_1;
            this.side_2 = side_2;
            this.side_3 = side_3;
        }

        public double Side_1 {
            get
            {
               return side_1;
            }
            protected set
            {
                if (IsTriangleValidate(value, side_2, side_3))
                {
                    side_1 = value;
                }
                else
                    throw new Exception("Triangle's side invalid!");
            }
        }
        public double Side_2 {
            get
            {
               return side_2;
            }
            protected set
            {
                if (IsTriangleValidate(side_1, value, side_3))
                {
                    side_2 = value;
                }
                else
                    throw new Exception("Triangle's side invalid!");
            } 
        }
        public double Side_3 {
            get
            {
               return side_3;
            }
            protected set
            {
                if (IsTriangleValidate(side_1, side_2, value))
                {
                    side_3 = value;
                }
                else
                    throw new Exception("Triangle's side invalid!");
            } 
        }
    

        private bool IsTriangleValidate(double side_1, double side_2, double side_3)
            => side_1 + side_2 > side_3 &&
            side_2 + side_3 > side_1 &&
            side_1 + side_3 > side_2;

        public double GetPerimeter() => side_1 + side_2 + side_3;

        public double GetSquare()
        {
            var half_p = GetPerimeter() / 2;
            return Math.Sqrt(half_p * (half_p - side_1) * (half_p - side_2) * (half_p - side_3));
        }
    }
}
