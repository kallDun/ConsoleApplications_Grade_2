using Lab_2_1_Calculator.Logic.Commands;
using System;
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
                    "RootCommand" => "√",
                    "PowerCommand" => "^",
                    "LogCommand" => "log",
                    _ => "err"
                };
                return cmd_txt;
            }
        }
        public void Plus(double number) => ExecuteCommandAndSetNew(number, new PlusCommand(calculator));
        public void Subtract(double number) => ExecuteCommandAndSetNew(number, new SubtractCommand(calculator));
        public void Multiply(double number) => ExecuteCommandAndSetNew(number, new MultiplyCommand(calculator));
        public void Divide(double number) => ExecuteCommandAndSetNew(number, new DivideCommand(calculator));
        public void Power(double number) => ExecuteCommandAndSetNew(number, new PowerCommand(calculator));
        public void Root(double number) => ExecuteCommandAndSetNew(number, new RootCommand(calculator));
        public void Log(double number) => ExecuteCommandAndSetNew(number, new LogCommand(calculator));
        public void Equals(double number) => ExecuteCommandAndSetNew(number, null);
        public void ClearAll()
        {
            commands = new();
            last_command = null;
            calculator = new Calculator();
        }
        private void ExecuteCommandAndSetNew(double number, ICommand command)
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
            }
            else 
            {
                last_command = commands.Pop();
                last_command.Undo();
            }
        }
    }
}