namespace Lab_2_1_Calculator.Logic
{
    interface ICalculator
    {
        double Number { get; set; }
        void Plus(double num);
        void Subtract(double num);
        void Multiply(double num);
        void Divide(double num);
        void Root(double num);
        void Power(double num);
        void Log(double num);
        void ReversePower(double num);
    }
}