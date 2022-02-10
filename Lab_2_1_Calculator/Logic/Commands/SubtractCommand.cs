namespace Lab_2_1_Calculator.Logic.Commands
{
    class SubtractCommand : ICommand
    {
        ICalculator calc;
        public double Number { get; set; }

        public SubtractCommand(ICalculator calc)
        {
            this.calc = calc;
        }

        public void Execute()
        {
            calc.Subtract(Number);
        }

        public void Undo()
        {
            calc.Plus(Number);
        }
    }
}