using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_1_Calculator.Logic.Commands
{
    class RootCommand : ICommand
    {
        ICalculator calc;
        public double Number { get; set; }
        public RootCommand(ICalculator calc)
        {
            this.calc = calc;
        }

        public void Execute()
        {
            calc.Root(Number);
        }

        public void Undo()
        {
            calc.Power(Number);
        }
    }
}
