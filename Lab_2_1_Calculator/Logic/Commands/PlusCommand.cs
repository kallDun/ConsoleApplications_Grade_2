namespace Lab_2_1_Calculator.Logic.Commands
{
    class PlusCommand : ICommand
    {
        ICalculator calc;
        public double Number { get; set; }
        public PlusCommand(ICalculator calc)
        {
            this.calc = calc;
        }

        public void Execute()
        {
            calc.Plus(Number);
        }

        public void Undo()
        {
            calc.Subtract(Number);
        }
    }
}