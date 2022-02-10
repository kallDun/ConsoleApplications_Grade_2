using System;

namespace Lab_2_1_Calculator.Logic
{
    class Calculator : ICalculator
    {
        public double Number { get; set; }
        public void Divide(double num) => Number /= num;
        public void Multiply(double num) => Number *= num;
        public void Plus(double num) => Number += num;
        public void Power(double num) => Number = Math.Pow(Number, num);
        public void Root(double num) => Number = Math.Pow(Number, 1.0 / num);
        public void Subtract(double num) => Number -= num;
    }
}