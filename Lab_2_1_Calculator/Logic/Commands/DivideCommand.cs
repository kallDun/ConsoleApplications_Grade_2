namespace Lab_2_1_Calculator.Logic.Commands
{
    class DivideCommand : ICommand
    {
        ICalculator calc;
        public double Number { get; set; }

        public DivideCommand(ICalculator calc)
        {
            this.calc = calc;
        }

        public void Execute()
        {
            calc.Divide(Number);
        }

        public void Undo()
        {
            calc.Multiply(Number);
        }
    }
}