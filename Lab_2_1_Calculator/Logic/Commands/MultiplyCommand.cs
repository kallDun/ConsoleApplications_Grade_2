namespace Lab_2_1_Calculator.Logic.Commands
{
    class MultiplyCommand : ICommand
    {
        ICalculator calc;
        public double Number { get; set; }

        public MultiplyCommand(ICalculator calc)
        {
            this.calc = calc;
        }

        public void Execute()
        {
            calc.Multiply(Number);
        }

        public void Undo()
        {
            calc.Divide(Number);
        }
    }
}