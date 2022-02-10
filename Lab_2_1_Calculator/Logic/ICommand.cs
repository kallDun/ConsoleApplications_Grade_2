namespace Lab_2_1_Calculator.Logic
{
    interface ICommand
    {
        double Number { get; set; }
        void Execute();
        void Undo();
    }
}