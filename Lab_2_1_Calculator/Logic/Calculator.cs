namespace Lab_2_1_Calculator.Logic
{
    class Calculator : ICalculator
    {
        public double Number { get; set; }
        public void Divide(double num) => Number /= num;
        public void Multiply(double num) => Number *= num;
        public void Plus(double num) => Number += num;
        public void Subtract(double num) => Number -= num;
    }
}