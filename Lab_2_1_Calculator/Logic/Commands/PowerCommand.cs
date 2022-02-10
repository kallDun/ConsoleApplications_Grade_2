namespace Lab_2_1_Calculator.Logic.Commands
{
    class PowerCommand : ICommand
    {
        ICalculator calc;
        public double Number { get; set; }
        public PowerCommand(ICalculator calc)
        {
            this.calc = calc;
        }

        public void Execute()
        {
            calc.Power(Number);
        }

        public void Undo()
        {
            calc.Root(Number);
        }
    }
}
