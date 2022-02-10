using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_1_Calculator.Logic.Commands
{
    class LogCommand : ICommand
    {
        ICalculator calc;
        public double Number { get; set; }
        public LogCommand(ICalculator calc)
        {
            this.calc = calc;
        }

        public void Execute()
        {
            calc.Log(Number);
        }

        public void Undo()
        {
            calc.ReversePower(Number);
        }
    }
}
