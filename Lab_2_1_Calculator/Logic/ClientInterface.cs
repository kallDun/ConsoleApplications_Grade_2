using Lab_2_1_Calculator.Logic.Commands;
using System.Collections.Generic;

namespace Lab_2_1_Calculator.Logic
{
    class ClientInterface
    {
        Stack<ICommand> commands = new();
        ICommand last_command;
        ICalculator calculator = new Calculator();

        public double OperationNumber => last_command.Number;
        public double CalculatorNumber => calculator.Number;
        public string Operation
        {
            get
            {
                if (last_command is null) return "";
                string cmd_txt = last_command.GetType().Name switch
                {
                    "PlusCommand" => "+",
                    "SubtractCommand" => "-",
                    "MultiplyCommand" => "*",
                    "DivideCommand" => "/",
                    _ => "err"
                };
                return cmd_txt;
            }
        }

        public void Plus(double number)
        {
            ExecuteCommand(number);
            SetCommand(new PlusCommand(calculator));
        }
        public void Subtract(double number)
        {
            ExecuteCommand(number);
            SetCommand(new SubtractCommand(calculator));
        }
        public void Multiply(double number)
        {
            ExecuteCommand(number);
            SetCommand(new MultiplyCommand(calculator));
        }
        public void Divide(double number)
        {
            ExecuteCommand(number);
            SetCommand(new DivideCommand(calculator));
        }
        public void Equals(double number)
        {
            ExecuteCommand(number);
            SetCommand(null);
        }
        public void ClearAll()
        {
            commands = new();
            last_command = null;
            calculator = new Calculator();
        }
        private void ExecuteCommand(double number)
        {
            if (last_command is not null)
            {
                last_command.Number = number;
                last_command.Execute();
                commands.Push(last_command);
            }
            else
            {
                calculator.Number = number;
            }
        }
        private void SetCommand(ICommand command)
        {
            last_command = command;
        }
        public void UndoLastCommand()
        {
            if (commands.Count is 0)
            {
                if (last_command is not null)
                {
                    last_command = null;
                }
                else
                {
                    calculator.Number = 0;
                }
                return;
            }
            last_command = commands.Pop();
            last_command.Undo();
        }
    }
}