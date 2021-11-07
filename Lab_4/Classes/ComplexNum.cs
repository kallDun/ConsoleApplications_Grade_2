using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.Classes
{
    public class ComplexNum : INumber<ComplexNum>
    {
        private double re, im;
        public ComplexNum(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        public ComplexNum Add(ComplexNum number) => new ComplexNum(re + number.re, im + number.im);
        public ComplexNum Subtract(ComplexNum number) => Add(new ComplexNum(-number.re, -number.im));
        public ComplexNum Multiply(ComplexNum number) => new ComplexNum(
            re * number.re - im * number.im, re * number.im + im * number.re);
        public ComplexNum Divide(ComplexNum number)
        {
            var divide = Math.Pow(number.re, 2) + Math.Pow(number.im, 2);
            if (divide is 0) throw new DivideByZeroException();

            var new_re = (re * number.re + im * number.im) / divide;
            var new_im = (im * number.re - re * number.im) / divide;
            return new ComplexNum(new_re, new_im);
        }

        public override string ToString() => $"{re}+{im}i";
        
        // operators
        public static ComplexNum operator +(ComplexNum first, ComplexNum second) => first.Add(second);
        public static ComplexNum operator -(ComplexNum first, ComplexNum second) => first.Subtract(second);
        public static ComplexNum operator *(ComplexNum first, ComplexNum second) => first.Multiply(second);
        public static ComplexNum operator /(ComplexNum first, ComplexNum second) => first.Divide(second);
    }
}
